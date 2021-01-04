using System.Collections.Generic;
using PaintingTanks.Interfaces;
using UnityEngine;

namespace PaintingTanks.Actor
{
    public class AudioAgent : MonoBehaviour
    {
        [SerializeField] protected List<IAudioPlayer> AudioPlayers = new List<IAudioPlayer>();

        private void Awake()
        {
            AudioPlayers.AddRange(GetComponentsInChildren<IAudioPlayer>());
        }

        public void MuteAll(bool isTrue)
        {
            AudioPlayers.ForEach(x => x.Mute(isTrue));
        }


    }
}