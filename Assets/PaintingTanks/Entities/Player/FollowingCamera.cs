
namespace PaintingTanks.Entities.Player
{
    using PaintingTanks.Interfaces;
    using UnityEngine;

    public class FollowingCamera : MonoBehaviour, IScrollable
    {
        public GameObject ObjectToFollow;

        [SerializeField]
        private new Camera camera;
        
        

        public void Awake()
        {
            if(camera = null) camera = GetComponent<Camera>() ?? FindObjectsOfType<Camera>()[0];
        }



        public void ScrollDown()
        {
            throw new System.NotImplementedException();
        }

        public void Scrolling(float value)
        {
            throw new System.NotImplementedException();
        }

        public void ScrollUp()
        {
            throw new System.NotImplementedException();
        }
    }
}