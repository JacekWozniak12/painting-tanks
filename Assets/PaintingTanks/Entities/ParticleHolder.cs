using System.Collections;
using UnityEngine;

namespace PaintingTanks.Entities
{
    public class ParticleHolder : MonoBehaviour
    {
        ParticleSystem particle;
        public float time = 5f;

        public void Awake()
        {
            particle = GetComponent<ParticleSystem>();
        }

        public void Activate()
        {
            StartCoroutine(WaitAndDie());
        }

        IEnumerator WaitAndDie()
        {
            yield return new WaitForSeconds(particle.main.duration);
            Destroy(gameObject);
        }

        IEnumerator WaitAndDie(float time)
        {
            yield return new WaitForSeconds(time);
            Destroy(gameObject);
        }

    }
}