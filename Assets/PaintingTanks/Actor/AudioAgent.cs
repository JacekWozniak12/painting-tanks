using System.Collections.Generic;
namespace PaintingTanks.Actor
{
    using PaintingTanks.Interfaces;
    using UnityEngine;

    public class AudioAgent : MonoBehaviour
    {
        [SerializeField] protected List<IAudioPlayer> AudioPlayers = new List<IAudioPlayer>();

        private void Awake()
        {
            AudioPlayers.AddRange(GetComponentsInChildren<IAudioPlayer>());
        }

        public void MuteAll()
        {

        }


    }
}