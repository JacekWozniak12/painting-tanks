namespace PaintingTanks.Behaviours.Audio
{
    using PaintingTanks.Interfaces;
    using UnityEngine;

    [RequireComponent(typeof(AudioSource))]
    public class AudioSwitcher : MonoBehaviour, IAudioPlayer
    {
        private AudioSource source;

        public void Awake()
        {
            SetAudioSouce(GetComponent<AudioSource>());
        }

        public void Play(AudioClip clip)
        {
            Switch(clip);
        }

        public void SetAudioSouce(AudioSource source)
        {
            this.source = source;
        }

        public void Stop() => this.source.Stop();

        private void Switch(AudioClip a)
        {
            if (a != null)
            {
                source.clip = a;
                source.Play();
            }
            else source.Stop();
        }
    }
}