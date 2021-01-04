using System.Linq;
using PaintingTanks.Actor.Control;
using PaintingTanks.Interfaces;
using UnityEngine;

namespace PaintingTanks.Menu
{
    public class PauseGame : MonoBehaviour
    {
        public AudioSource[] sources;
        public IPausable[] pausables;

        private void Awake()
        {
            sources = FindObjectsOfType<AudioSource>();
            pausables = FindObjectsOfType<MonoBehaviour>().OfType<IPausable>().ToArray();
            Controller.Controls.Player.Pause.performed += ctx => Switch();
        }

        private bool paused;
        public void Switch()
        {
            paused = !paused;
            if (paused) Pause();
            else Play();
        }

        public void Pause()
        {
            Time.timeScale = 0;
            foreach (var source in sources)
            {
                source.Pause();
            }
            foreach (var source in pausables)
            {
                source.Pause();
            }
        }

        public void Play()
        {
            Time.timeScale = 1;
            foreach (var source in sources)
            {
                source.UnPause();
            }
            foreach (var source in pausables)
            {
                source.UnPause();
            }
        }
    }
}