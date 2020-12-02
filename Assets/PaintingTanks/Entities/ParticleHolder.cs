namespace PaintingTanks.Entities
{
    using System.Collections;
    using UnityEngine;

    public class ParticleHolder : MonoBehaviour
    {
        ParticleSystem particle;
        public float time = 5f;

        public void Activate()
        {
            StartCoroutine(WaitAndDie());
        }

        IEnumerator WaitAndDie()
        {
            yield return new WaitForSeconds(time);
            Destroy(gameObject);
        }
        
    }
}