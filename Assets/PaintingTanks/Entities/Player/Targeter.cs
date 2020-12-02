namespace PaintingTanks.Entities.PlayerItems
{
    using UnityEngine;
    using PaintingTanks.Actor.Control;
    using System;
    using PaintingTanks.Library;

    public class Targeter : MonoBehaviour
    {
        public event Action PositionChanged;
        public event Action<float> ConstrainedAngle;

        // desired location and current location todo

        private void Awake()
        {
            transform = GetComponent<Transform>();
        }

        private void Update()
        {
            if (UseConstraint) CheckCurrentPositionForConstraint();
            if (previousPosition != transform.position) UpdatePosition();
            if (UseMouse) HandleCursor();
        }

        private void CheckCurrentPositionForConstraint()
        {
            var direction = (Pivot.position - transform.position).normalized;
            var distance = Vector3.Distance(Pivot.position, transform.position);
            var angle  = Vector3.SignedAngle(direction, ConstrainedTo.forward, Vector3.up);
            if (CheckAngleConstraint(angle))
            {
                HandleAngleConstraints(ref angle, out float angleExceedsBy);
                CalculateConstraintedValue(distance, angle, angleExceedsBy);
            }
        }

        public void SetPosition(Vector3 position)
        {
            CheckBoundsAndSetPosition(position);
            if (previousPosition != transform.position) UpdatePosition();
        }

        public Vector3 GetVelocity(Vector3 start, float speedPerSecond = 25, Vector3 modifier = default(Vector3))
        {
            return KinematicsL.GetStraightVelocityFromPointsAndTime(transform.position, start, speedPerSecond, modifier);
        }

        private void UpdatePosition()
        {
            previousPosition = transform.position;
            PositionChanged?.Invoke();
        }

        private void HandleCursor()
        {
            var r = camera.ScreenPointToRay(Controller.Controls.Player.FindTarget.ReadValue<Vector2>());
            if (Physics.Raycast(r, out RaycastHit info, Mathf.Infinity, layerMask))
            {
                CheckBoundsAndSetPosition(info.point);
            }
            else CheckBoundsAndSetPosition(transform.position);
        }

        private void CheckBoundsAndSetPosition(Vector3 point)
        {
            var currentDistance = Vector3.Distance(Pivot.position, point);
            var currentDirection = (Pivot.position - point).normalized;

            if (currentDistance >= MaxDistance)
                SetConstrainedDistance(currentDirection, MaxDistance);
            else
            if (currentDistance <= MinDistance)
                SetConstrainedDistance(currentDirection, MinDistance);
            else SetConstrainedDistance(currentDirection, currentDistance);
        }

        private void SetConstrainedDistance(Vector3 direction, float distance)
        {
            var p = Pivot.position + direction * -distance;
            if (Physics.Raycast(p + Vector3.up / 10, Vector3.down / 8, out RaycastHit hit))
            {
                float angle = Vector3.SignedAngle(direction, ConstrainedTo.forward, Vector3.up);
                if (CheckAngleConstraint(angle))
                    transform.position = hit.point;
                else
                {
                    HandleAngleConstraints(ref angle, out float angleExceedsBy);
                    CalculateConstraintedValue(distance, angle, angleExceedsBy);
                }
            }
        }

        private void CalculateConstraintedValue(float distance, float angle, float angleExceedsBy)
        {
            var direction = Pivot.forward + Pivot.TransformDirection(new Vector3(0, Mathf.Sin(angle), 0).normalized);
            var p = Pivot.position + direction * distance;
            if (Physics.Raycast(p + Vector3.up / 10, Vector3.down / 8, out RaycastHit hit))
            {
                transform.position = hit.point;
            }
            ConstrainedAngle?.Invoke(angleExceedsBy);
        }

        private bool CheckAngleConstraint(float angle)
            => !UseConstraint || angle <= -180 + ConstraintValue || angle >= 180 - ConstraintValue;

        private bool HandleAngleConstraints(ref float angle, out float angleExceedsBy)
        {
            bool a = angle <= -180 + ConstraintValue;
            bool b = angle >= 180 - ConstraintValue;
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

        // camera => into camera control
        [SerializeField] private new Camera camera;
        [SerializeField] private LayerMask layerMask;

        public Transform Pivot;
        public float MaxDistance;
        public float MinDistance;

        public bool UseMouse = true;
        public bool UseConstraint = false;
        public Transform ConstrainedTo;
        public float ConstraintValue = 30f;

        private new Transform transform;
        private Vector3 previousPosition = default(Vector3);
    }
}