namespace PaintingTanks.Interfaces
{
    using UnityEngine;

    public interface IAudioPlayer
    {
         void SetAudioSouce(AudioSource Source);
         void Play(AudioClip clip);
         void Stop();
         

    }
}