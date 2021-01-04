using PaintingTanks.Library;
using UnityEngine;

namespace PaintingTanks.Entities
{
    public class CameraShake : MonoBehaviour
    {

        // refractor it pls
        public static CameraShake Instance;
        private new Transform transform;
        [SerializeField] Vector3 baseVectorForRandomization = new Vector3(0.4f, 0.4f, 0.4f);

        private float seed;

        private void Awake()
        {
            transform = GetComponent<Transform>();
            Instance = this;
            seed = Random.value;
        }

        private float trauma = 0;

        public void InduceStress(float stress)
        {
            trauma = Mathf.Clamp01(trauma + stress);
        }

        private void LateUpdate()
        {
            Shake();
        }

        private void Shake(float frequency = 0.1f)
        {
            float perlin = Mathf.PerlinNoise(seed, Time.time * frequency);
            transform.localRotation = Quaternion.Euler(RandomL.GetRandomVector(baseVectorForRandomization) * perlin * trauma);
            if (trauma > 0)
            {
                trauma -= 1f * Time.deltaTime;
            }
        }

    }
}
