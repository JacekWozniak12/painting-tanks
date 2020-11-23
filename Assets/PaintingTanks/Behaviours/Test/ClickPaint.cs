namespace PaintingTanks.Behaviours.Test
{
    using UnityEngine;
    using Interfaces;
    using Entities.MapItems;
    using PaintingTanks.Actor.Control;

    public class ClickPaint : MonoBehaviour
    {
        [SerializeField]
        private new Camera camera;

        [SerializeField]
        private Texture2D texture;

        public GameControls controls;

        private void Awake()
        {
            if (camera == null) camera = GetComponent<Camera>();
            if (texture == null) texture = CreateDummyTexture();
            if (controls == null) controls = Controller.Controls;

            controls.Player.Fire.performed += ctx => PaintOn();
        }

        private Texture2D CreateDummyTexture()
        {
            var t = new Texture2D(8, 8);
            var c = new Color[64];
            for (int i = 0; i < 64; i++)
            {
                c[i] = Color.red;
            }
            t.filterMode = FilterMode.Point;
            t.anisoLevel = 0;
            t.SetPixels(c);
            t.Apply();
            return t;
        }

        private void PaintOn()
        {
            var getMousePosition = camera.ScreenPointToRay(controls.Player.FindTarget.ReadValue<Vector2>());
            Debug.DrawRay(camera.transform.position, getMousePosition.direction * 500f, Color.magenta, 5f);
            if (Physics.Raycast(getMousePosition, out RaycastHit hit))
            {
                var current = hit.collider?.gameObject?.GetComponent<PaintableMesh>();
                if (current is PaintableMesh pm)
                {
                    pm.Paint(hit.textureCoord2.x, hit.textureCoord2.y, texture, 1);
                    pm.StopPainting();
                }
            }
        }

    }
}