using UnityEngine;

namespace PaintingTanks.Interfaces
{
    public interface IGameCommand
    {
        void Execute();
        void Stop();
        void Undo();
    }

    public interface IGameCommand<T>
    {
        void Setup(T component);
        void Execute();
        void Stop();
        void Undo();
    }

    public interface IGameCommand<T0, T1>
    {
        void Setup(T0 component0, T1 component1);
        void Execute();
        void Stop();
        void Undo();
    }
}