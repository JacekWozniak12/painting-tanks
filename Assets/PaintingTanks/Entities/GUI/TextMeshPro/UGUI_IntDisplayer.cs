namespace PaintingTanks.Entities.GUI.TextMeshPro
{
    using TMPro;

    public class UGUI_IntDisplayer : ValueDisplayer<int, TextMeshProUGUI>
    {
        public override void OnChange(int value) => Displayer.text = value.ToString();
    }
}