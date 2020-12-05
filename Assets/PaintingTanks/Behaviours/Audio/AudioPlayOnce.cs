namespace PaintingTanks.Behaviours.Audio
{
    using UnityEngine;

    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayOnce : MonoBehaviour
    {
        private AudioSource source;

        public void Awake()
        {
            source = GetComponent<AudioSource>();
            source.clip = null;
            source.loop = false;
            source.playOnAwake = false;
        }

        public void PlayOnce(AudioClip a)
        {
            source.pitch = Random.Range(0.85f, 1f);
            source.PlayOneShot(a);
        }
    }
}