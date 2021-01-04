using UnityEngine;
using System;

namespace PaintingTanks.Behaviours.Game
{
    [Serializable]
    public class Acceleration
    {
        [SerializeField] float TimeToFull = 2f;
        [SerializeField] float StoppingModifier = 1f;
        [SerializeField] AnimationCurve Curve = default(AnimationCurve);
        float currentTime = 0f;

        public float GetSpeed(float deltaTime, float speed, bool active = false)
        {
            if (!active)
            {
                if (currentTime <= 0) return 0f;
                var temp = Math.Abs(StoppingModifier);
                if (temp == 0) temp = 1f;
                currentTime -= deltaTime * temp;
            }
            else
            {
                if (currentTime >= TimeToFull) return speed;
                currentTime += deltaTime;
            }
            return Curve.Evaluate(currentTime / TimeToFull) * speed;
        }
    }
}