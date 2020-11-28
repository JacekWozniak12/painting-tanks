namespace PaintingTanks.Entities.PlayerItems
{
    using UnityEngine;
    using System;
    using PaintingTanks.Actor.Control;
    using PaintingTanks.Interfaces;

    public class Targeter : MonoBehaviour
    {
        private void Awake()
        {
            transform = GetComponent<Transform>();
        }

        Vector3 previousPivot = default(Vector3);

        private void Update()
        {
            if(previousPivot != Pivot.position) UpdatePivot();
            HandleCursor();
            
        }

        private void UpdatePivot()
        {
            CheckBoundsAndSetPosition(transform.position);
            previousPivot = Pivot.position;
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
            var CurrentDistance = Vector3.Distance(Pivot.position, point);
            if (CurrentDistance + 0.2f > MaxDistance)
            {
                SetMaximalDistance(point);
            }
            else
            if (CurrentDistance - 0.2f < MinDistance)
            {
                SetMinimalDistance(point);
            }
            else transform.position = point;
        }

        private void SetMaximalDistance(Vector3 direction)
        {
            direction.Normalize();
            var p = Vector3.MoveTowards(Pivot.position, direction * MaxDistance, MaxDistance);
            if (Physics.Raycast(p + Vector3.up, Vector3.down * 2, out RaycastHit hit))
            {
                transform.position = hit.point;
            }
        }

        private void SetMinimalDistance(Vector3 direction)
        {
            direction.Normalize();
            var p = Vector3.MoveTowards(Pivot.position, direction * MinDistance, MinDistance);
            if (Physics.Raycast(p, Vector3.down, out RaycastHit hit))
            {
                transform.position = hit.point;
            }
        }

        private new Transform transform;

        // camera => into camera control
        [SerializeField] new Camera camera;
        [SerializeField] LayerMask layerMask;

        public Transform Pivot;
        public float MaxDistance;
        public float MinDistance;


    }
}