using PaintingTanks.Interfaces;
using UnityEngine;

namespace PaintingTanks.Behaviours.Audio
{

    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayOnce : MonoBehaviour, IAudioPlayer
    {
        private AudioSource source;

        public void Awake()
        {
            SetAudioSouce(GetComponent<AudioSource>());
        }

        public void Mute(bool isTrue) => source.mute = isTrue;

        public void Play(AudioClip a)
        {
            PlayOnce(a);
        }

        private void PlayOnce(AudioClip a)
        {
            source.pitch = Random.Range(0.85f, 1f);
            source.PlayOneShot(a);
        }

        public void SetAudioSouce(AudioSource source)
        {
            this.source = source;
            this.source.clip = null;
            this.source.loop = false;
            this.source.playOnAwake = false;
        }

        public void Stop() => this.source.Stop();
    }
}