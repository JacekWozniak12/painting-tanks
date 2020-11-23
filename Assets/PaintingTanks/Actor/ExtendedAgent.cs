namespace PaintingTanks.Actor
{
    using UnityEngine;
    using System.Collections.Generic;
    using Interfaces;
    using Library;

    public class ExtendedAgent : Agent, IScrollable, IControllable
    {
        [SerializeField]
        private List<IGameCommand> gameCommands;

        [SerializeField]
        private IGameCommand currentCommand;

        int index;

        protected override void Awake()
        {
            base.Awake();
            gameCommands = new List<IGameCommand>();
        }

        public void ScrollDown()
        {
            index = MathL.Loop(--index, 0, gameCommands.Count);
            SetCurrentCommand();
        }

        public void ScrollUp()
        {
            index = MathL.Loop(++index, 0, gameCommands.Count);
            SetCurrentCommand();
        }

        public void Handle()
        {
            if (currentCommand != null) currentCommand.Execute();
        }

        private void SetCurrentCommand() => currentCommand = gameCommands[index];

        public void Scrolling(float value)
        {
            if (value > 0) ScrollUp();
            else if (value < 0) ScrollDown();
        }

        public void AddCommand(IGameCommand command)
        {
            gameCommands.Add(command);
            SetCurrentCommand();
        }

        public void DeleteCommand(IGameCommand command)
        {
            gameCommands.Remove(command);
            SetCurrentCommand();
        }

    }
}