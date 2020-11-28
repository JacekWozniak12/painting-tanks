namespace PaintingTanks.Behaviours.Test
{
    using UnityEngine;
    using Interfaces;
    using Entities.MapItems;
    using PaintingTanks.Actor.Control;
    using Library;
    using Definitions;

    public class ClickPaint : MonoBehaviour, IGameCommand
    {
        [SerializeField] private new Camera camera;
        [SerializeField] private Texture2D texture;
        [SerializeField] Vector2Int size = new Vector2Int(8, 8);
        [SerializeField] private ObservableColor32Value color = new ObservableColor32Value();
        public GameControls controls;

        private void Awake()
        {
            if (camera == null) camera = GetComponent<Camera>();
            if (texture == null) texture = CreateDummyTexture();
            if (controls == null) controls = Controller.Controls;
            color.Changed += ctx => UpdateTexture();
            controls.Player.Fire.performed += ctx => Execute();
        }

        private Texture2D CreateDummyTexture()
        {
            return GraphicsL.CreateMonoColorTexture(size, color.Value);
        }


        [ContextMenu("Update Texture")]
        private void UpdateTexture() { texture = CreateDummyTexture(); }

        public void Execute()
        {
            var getMousePosition = camera.ScreenPointToRay(controls.Player.FindTarget.ReadValue<Vector2>());
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

        public void Stop() 
        {
            return;
        }

        public void Undo()
        {
            return;
        }
    }
}