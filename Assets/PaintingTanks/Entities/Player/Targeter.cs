namespace PaintingTanks.Entities.PlayerItems
{
    using UnityEngine;
    using PaintingTanks.Actor.Control;

    public class Targeter : MonoBehaviour
    {
        private void Awake()
        {
            transform = GetComponent<Transform>();
        }

        Vector3 previousPivot = default(Vector3);

        private void Update()
        {
            if (previousPivot != Pivot.position) UpdatePivot();
            HandleCursor();
        }

        private void UpdatePivot()
        {
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
            var currentDistance = Vector3.Distance(Pivot.position, point);
            var currentDirection = (Pivot.position - point).normalized;
            if (currentDistance > MaxDistance)
            {
                SetMaximalDistance(currentDirection);
            }
            else
            if (currentDistance < MinDistance)
            {
                SetMinimalDistance(currentDirection);
            }
            else transform.position = point;
        }

        private void SetMaximalDistance(Vector3 direction)
        {
            var p = Pivot.position + direction * -MaxDistance;
            if (Physics.Raycast(p + Vector3.up/10, Vector3.down/8, out RaycastHit hit))
            {
                transform.position = hit.point;
            }
        }

        private void SetMinimalDistance(Vector3 direction)
        {
            var p = Pivot.position + direction * -MinDistance;
            if (Physics.Raycast(p + Vector3.up/10, Vector3.down/8, out RaycastHit hit))
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