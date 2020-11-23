namespace PaintingTanks.Actor.Control
{
    using UnityEngine;

    public class Controller : MonoBehaviour
    {
        private void Awake() => SetupControls();
        private void OnEnable() => Controls.Enable();
        private void OnDisable() => Controls.Disable();
        public static GameControls Controls { get; private set; }

        private void SetupControls()
        {
            if (Controls == null) Controls = new GameControls();
        }
    }

}