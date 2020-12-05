namespace PaintingTanks.Behaviours.Audio
{
    using UnityEngine;

    [RequireComponent(typeof(AudioSource))]
    public class AudioSwitcher : MonoBehaviour
    {
        private AudioSource source;

        public void Awake()
        {
            source = GetComponent<AudioSource>();
        }

        public void Switch(AudioClip a)
        {
            source.clip = a;
            source.Play();
        }
    }
}