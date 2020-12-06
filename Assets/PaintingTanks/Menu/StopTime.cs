namespace PaintingTanks.Menu
{
    using UnityEngine;

    class StopGame : MonoBehaviour
    {
        public AudioSource[] sources;

        public void Stop()
        {
            Time.timeScale = 0;
            foreach(var source in sources)
            {
                source.Pause();
            }
        }

        public void DisconnectPlayerControls()
        {
            
        }
    }
}