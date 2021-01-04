using TMPro;

namespace PaintingTanks.Entities.GUI.TextMeshPro
{
    public class UGUI_FloatDisplayer : ValueDisplayer<float, TextMeshProUGUI>
    {
        public override void OnChange(float value) => Displayer.text = value.ToString("F");
    }
}