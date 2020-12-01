
namespace PaintingTanks.Entities.Player
{
    using PaintingTanks.Interfaces;
    using PaintingTanks.Library;
    using UnityEngine;
    using static Cinemachine.AxisState;

    public class ZoomCamera : MonoBehaviour, IScrollable, IInputAxisProvider
    {

        public float MaximalZoomValue;
        public float MinimalZoomValue;
        public float Step;

        public void ScrollDown()
        {
            axisValue -= Step;
            axisValue = MathL.Clamp(axisValue, MinimalZoomValue, MaximalZoomValue);
        }

        public void Scrolling(float value)
        {
            if (value < 0) ScrollDown();
            else if (value > 0) ScrollUp();
        }

        public float axisValue = 0;

        public void ScrollUp()
        {
            axisValue += Step;
            axisValue = MathL.Clamp(axisValue, MinimalZoomValue, MaximalZoomValue);
        }

        public float GetAxisValue(int axis)
        {
            return axisValue;
        }
    }
}