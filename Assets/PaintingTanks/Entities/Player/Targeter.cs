using System.Numerics;
using System.Data;
namespace PaintingTanks.Entities.PlayerItems
{
    using System;
    using UnityEngine;
    using PaintingTanks.Actor.Control;
    using PaintingTanks.Library;
    using PaintingTanks.Definitions;

    public class Targeter : MonoBehaviour
    {
        public event Action PositionChanged;
        public event Action<float> ConstrainedAngle;

        public void SetAngleConstraint(float angle, bool active)
        {
            ConstraintValue = angle;
            UseConstraint = active;
        }

        public void SetPosition(Vector3 position) => CheckBoundsAndSetPosition(position);

        public Vector3 GetVelocity(Vector3 start, float speedPerSecond = 25, Vector3 modifier = default(Vector3))
        {
            var point = transform.position;
            modifier = transform.TransformDirection(modifier);
            if (UsePreciseCalculation) point = GetPreciseVelocityCalculations(point);
            return KinematicsL.GetStraightVelocityFromPointsAndTime(point, start, speedPerSecond, modifier);
        }

        private Vector3 GetPreciseVelocityCalculations(Vector3 point)
        {
            var distance = Vector3.Distance(Pivot.position, point);
            var p = Pivot.position + PreciseCalculationsTowards.forward * distance;
            PerformSnapping(point);
            return point;
        }

        private void UpdatePosition()
        {
            previousPosition = transform.position;
            PositionChanged?.Invoke();
        }

        private void HandleCursor()
        {
            Vector2 cursorPosition = Controller.Controls.Player.FindTarget.ReadValue<Vector2>();
            if (CursorMoved(cursorPosition))
            {
                var r = camera.ScreenPointToRay(cursorPosition);
                if (Physics.Raycast(r, out RaycastHit info, Mathf.Infinity, layerMask)) CheckBoundsAndSetPosition(info.point);
                else CheckBoundsAndSetPosition(transform.position);
            }
        }

        private bool CursorMoved(Vector2 vec2) => vec2.x != 0 || vec2.y != 0;

        private void CheckBoundsAndSetPosition(Vector3 point)
        {
            var currentDistance = Vector3.Distance(Pivot.position, point);
            var currentDirection = (Pivot.position - point).normalized;
            if (currentDistance >= MaxDistance) SetPositionFrom(currentDirection, MaxDistance);
            else if (currentDistance < MinDistance) SetPositionFrom(currentDirection, MinDistance);
            else SetPositionFrom(currentDirection, currentDistance);
        }

        private void SetPositionFrom(Vector3 direction, float distance)
        {
            var p = Pivot.position + direction * -distance;
            PerformSnapping(p);
        }

        private void PerformSnapping(Vector3 p, bool SetPositionIfFailed = true)
        {
            if (Physics.Raycast(p + Vector3.up / 10, Vector3.down / 9, out RaycastHit hit))
            {
                transform.position = hit.point;
            }
            else
            if (SetPositionIfFailed) transform.position = p;
        }

        private void ApplyAngleConstraint()
        {
            var point = transform.position;
            var direction = -(Pivot.position - point).normalized;
            var distance = Vector3.Distance(Pivot.position, point);
            float angle = Vector3.SignedAngle(direction, ConstrainedTo.forward, Vector3.up);
            if (NotWithinAngleConstraints(ref angle, out float exceeds))
            {
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
                Vector3 ConstrainedPoint = Pivot.position + rotation * ConstrainedTo.forward * -distance;
                CheckBoundsAndSetPosition(ConstrainedPoint);
            }
        }

        private bool NotWithinAngleConstraints(ref float angle, out float angleExceedsBy)
        {
            bool a = angle < -ConstraintValue;
            bool b = angle > ConstraintValue;
            angleExceedsBy = 0;

            if (a)
            {
                angleExceedsBy = angle - (-180 + ConstraintValue);
                angle = -180 + ConstraintValue;
            }
            if (b)
            {
                angleExceedsBy = angle - (180 - ConstraintValue);
                angle = 180 - ConstraintValue;
            }

            return a || b;
        }

        private void Awake()
        {
            transform = GetComponent<Transform>();
            MinDistance.Changed += ctx => CheckBoundsAndSetPosition(transform.position);
            MaxDistance.Changed += ctx => CheckBoundsAndSetPosition(transform.position);
        }

        private void Update()
        {
            if (Lock) return;
            if (UseMouse) HandleCursor();
            if (UseConstraint) ApplyAngleConstraint();
            if (previousPosition != transform.position) UpdatePosition();
        }

        // camera => into camera control
        [SerializeField] private new Camera camera;
        [SerializeField] private LayerMask layerMask;

        public Transform Pivot;
        public ObservableValue<float> MaxDistance;
        public ObservableValue<float> MinDistance;

        public bool UsePreciseCalculation = true;
        public Transform PreciseCalculationsTowards;

        public bool UseMouse = true;
        public bool UseConstraint = false;
        public bool Lock = false;
        public Transform ConstrainedTo;
        public float ConstraintValue = 30f;

        private new Transform transform;
        private Vector3 previousPosition = default(Vector3);
    }
}