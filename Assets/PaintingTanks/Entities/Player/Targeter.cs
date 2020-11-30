namespace PaintingTanks.Entities.PlayerItems
{
    using UnityEngine;
    using PaintingTanks.Actor.Control;
    using System;

    public class Targeter : MonoBehaviour
    {
        public event Action PositionChanged;
        public event Action ConstrainedAngle;

        private void Awake()
        {
            transform = GetComponent<Transform>();
        }

        private void Update()
        {
            if (previousPosition != transform.position) UpdatePosition();
            if (UseMouse) HandleCursor();
        }

        public void SetPosition(Vector3 position)
        {
            CheckBoundsAndSetPosition(position);
            if (previousPosition != transform.position) UpdatePosition();
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
            print(currentDirection);

            if (currentDistance >= MaxDistance) SetConstrainedDistance(currentDirection, MaxDistance);
            else
            if (currentDistance <= MinDistance) SetConstrainedDistance(currentDirection, MinDistance);
            else SetConstrainedDistance(currentDirection, currentDistance);
        }

        private void SetConstrainedDistance(Vector3 direction, float distance)
        {
            var p = Pivot.position + direction * -distance;
            if (Physics.Raycast(p + Vector3.up / 10, Vector3.down / 8, out RaycastHit hit))
            {
                transform.position = hit.point;
            }
        }

        // camera => into camera control
        [SerializeField] private new Camera camera;
        [SerializeField] private LayerMask layerMask;

        public Transform Pivot;
        public float MaxDistance;
        public float MinDistance;

        public bool UseMouse = true;
        public bool UseConstraintX = false;
        public bool UseConstraintY = false;

        // x -> top / bottom ; y -> left / right
        private Vector2 Constraints;
        private new Transform transform;
        private Vector3 previousPosition = default(Vector3);
    }
}