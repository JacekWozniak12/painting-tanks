namespace PaintingTanks.Actor.Control
{
    using UnityEngine;

    public class Controller : MonoBehaviour
    {
        private void OnEnable() => Controls.Enable();
        private void OnDisable() => Controls.Disable();
        private static GameControls controls;
        
        public static GameControls Controls
        {
            get
            {
                if (controls == null) controls = new GameControls();
                return controls;
            }
            private set
            {
                controls = value;
            }
        }
    }

}