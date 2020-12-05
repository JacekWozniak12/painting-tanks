namespace PaintingTanks.Behaviours.Game.Commands
{
    using UnityEngine;
    using Interfaces;

    public class Fire : IGameCommand<IWeaponMechanism>
    {
        [SerializeField]
        IWeaponMechanism weapon;

        public void Setup(IWeaponMechanism weapon)
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