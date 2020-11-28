namespace PaintingTanks.Behaviours.Game.Commands
{
    using UnityEngine;
    using Interfaces;
    using Entities.MapItems;
    using PaintingTanks.Actor.Control;
    using Library;
    using Definitions;

    public class Fire : IGameCommand<IWeapon>
    {
        [SerializeField]
        IWeapon weapon;

        public void Setup(IWeapon weapon)
        {
            this.weapon = weapon;
        }

        public void Execute()
        {
            weapon.Trigger(true);
        }

        public void Stop()
        {
            weapon.Trigger(false);
        }

        public void Undo()
        {
            //
        }
    }
}