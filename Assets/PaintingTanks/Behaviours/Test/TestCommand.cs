using PaintingTanks.Interfaces;
using UnityEngine;
using System;

namespace PaintingTanks.Behaviours.Test
{
    public class TestCommand : IGameCommand
    {
        public void Execute()
        {
            Debug.Log("TEST");
        }

        public void Stop()
        {
            throw new Exception("This command can't be stopped");
        }

        public void Undo()
        {
            throw new Exception("This command can't be undone");
        }
    }
}