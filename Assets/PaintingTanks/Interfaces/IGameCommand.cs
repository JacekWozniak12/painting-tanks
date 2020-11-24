namespace PaintingTanks.Interfaces
{
    public interface IGameCommand
    {
        void Execute();
        void Undo();
    }
}