using UnityEngine;

namespace PaintingTanks.Interfaces
{
    public interface IAudioPlayer
    {
        void SetAudioSouce(AudioSource Source);
        void Play(AudioClip clip);
        void Stop();
        void Mute(bool isTrue);


    }
}