namespace PaintingTanks.Actor.Control
{
    using UnityEngine;
    using Behaviours.Test;

    [RequireComponent(typeof(Controller))]
    public class PlayerTankControl : TankControl
    {
        protected override void Start()
        {
            base.Start();
            var c = Controller.Controls.Player;
            c.Fire.performed += ctx => Gun.Handle();
            Gun.AddCommand(new TestCommand());
        }
    }
}