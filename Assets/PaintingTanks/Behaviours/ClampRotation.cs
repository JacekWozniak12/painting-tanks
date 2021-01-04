using UnityEngine;

namespace PaintingTanks.Behaviours
{
    class ClampRotation : MonoBehaviour
    {
        public float minX;
        public float maxX;

        private new Transform transform;

        void Awake()
        {
            transform = GetComponent<Transform>();
        }

        void LateUpdate()
        {
            var r = transform.rotation.eulerAngles;
            var x = r.x;
            x = Mathf.Clamp(x, minX, maxX);
            transform.rotation = Quaternion.Euler(x, r.y, r.z);
        }
    }
}