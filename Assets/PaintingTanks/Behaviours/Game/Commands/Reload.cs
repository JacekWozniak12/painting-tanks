namespace PaintingTanks.Behaviours.Game.Commands
{
    using PaintingTanks.Interfaces;
    using UnityEngine;

    public class Reload : IGameCommand<IReoladable>
    {
        [SerializeField]
        IReoladable weapon;

        public void Execute()
        {
            throw new System.NotImplementedException();
        }

        public void Setup(IReoladable component)
        {
            throw new System.NotImplementedException();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }

        public void Undo()
        {
            throw new System.NotImplementedException();
        }
    }
}