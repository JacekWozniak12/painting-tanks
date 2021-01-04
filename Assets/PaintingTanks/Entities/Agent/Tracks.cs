using PaintingTanks.Entities.MapItems;
using UnityEngine;

namespace PaintingTanks.Entities.Player
{
    public class Tracks : MonoBehaviour
    {
        [SerializeField]
        PaintBrushHandler trackBrush = default;

        [SerializeField]
        Transform[] tracks = default;

        private void FixedUpdate()
        {
            foreach (var item in tracks)
            {
                if (Physics.Raycast(item.position + Vector3.up / 4, Vector3.down / 3, out RaycastHit hit, 1f))
                {
                    PaintableMesh.HandlePainting(hit, trackBrush.Texture, trackBrush.Affects);
                }
            }
        }
    }
}