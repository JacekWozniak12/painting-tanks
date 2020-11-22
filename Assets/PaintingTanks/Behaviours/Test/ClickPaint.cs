namespace PaintingTanks.Behaviours.Test
{
    using UnityEngine;
    using Interfaces;
    using Entities.MapItems;

    public class ClickPaint : MonoBehaviour
    {
        [SerializeField]
        private new Camera camera;

        [SerializeField]
        private Texture2D texture;

        private void Awake()
        {
            if (camera == null) camera = GetComponent<Camera>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var getMousePosition = camera.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(camera.transform.position, getMousePosition.direction * 500f, Color.magenta, 5f);
                if (Physics.Raycast(getMousePosition, out RaycastHit hit))
                {
                    var current = hit.collider?.gameObject?.GetComponent<PaintableMesh>();
                    if (current is PaintableMesh pm)
                    {
                        pm.Paint(hit.textureCoord2.x, hit.textureCoord2.y, texture, 1);
                        print("pm");
                    }
                }
                print("a");
            }

        }
    }
}