namespace PaintingTanks.Entities.GUI.TextMeshPro
{
    using TMPro;
    
    public class UGUI_StringDisplayer : ValueDisplayer<string, TextMeshProUGUI>
    {
        public override void OnChange(string value) => Displayer.text = value;
    }
}