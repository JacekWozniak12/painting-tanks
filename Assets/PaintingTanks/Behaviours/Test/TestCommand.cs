namespace PaintingTanks.Behaviours.Test
{
    using Interfaces;
    using UnityEngine;
    using System;

    public class TestCommand : IGameCommand
    {
        public void Execute()
        {
            Debug.Log("TEST");
        }

        public void Undo()
        {
            throw new Exception("This command can't be undone");
        }
    }
}