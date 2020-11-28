namespace PaintingTanks.Entities.Player
{
    using PaintingTanks.Entities.MapItems;
    using UnityEngine;

    public class Tracks : MonoBehaviour
    {
        [SerializeField]
        PaintBrushHandler trackBrush;

        [SerializeField]
        Transform[] tracks;

        private void FixedUpdate()
        {
            foreach (var item in tracks)
            {
                if (Physics.Raycast(item.position, Vector3.down, out RaycastHit hit, 1f))
                {
                    PaintableMesh.HandlePainting(hit, trackBrush.Texture, trackBrush.Affects);
                }
            }
        }

        private void Awake()
        {

        }
    }
}