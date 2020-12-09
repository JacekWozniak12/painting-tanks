namespace PaintingTanks.Entities.Camera
{
    using PaintingTanks.Library;
    using UnityEngine;

    public class CameraShake : MonoBehaviour
    {

        // refractor it pls
        public static CameraShake Instance;

        private new Transform transform;

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

        private void Update()
        {
            Shake();
        }

        private void Shake(float frequency = 0.1f)
        {
            float perlin = Mathf.PerlinNoise(seed, Time.time * frequency);
            transform.localPosition = RandomL.GetRandomVector(0.4f, 0.4f, 0.4f) * perlin * trauma;
            if (trauma > 0)
            {
                trauma -= 1f * Time.deltaTime;
            }
        }

    }
}
