using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Logic.Delegates;
using Logic.Enums;
using Logic.Interfaces;

namespace Logic.Models
{
    public class SneikGameUseCase : IUseCase
    {
        public Round round { get; set; }
        public event EventHandler DrawUpdateDelegate;
        public event InputEventHandler InputEvent;
        
        private Thread _gameThread;
        private static SneikGameUseCase instance;

        private void StartGameLoop()
        {
            InputEvent += onKeyPress;
            while (round != null)
            {
                this.round.Update();
                this.DrawUpdateDelegate?.Invoke(this, EventArgs.Empty);
               
            }

        }

        public void KeyPressed(Keys[] pressedKeys)
        {
            InputEvent?.Invoke(pressedKeys);
        }

        private void onKeyPress(Keys[] pressedKeys)
        {
            Keys firstKey = pressedKeys[0];
            switch (firstKey)
            {
                case Keys.Left:
                    round.Snake.SetDirection(Direction.LEFT);
                    break;
                case Keys.Right:
                    round.Snake.SetDirection(Direction.RIGHT);
                    break;
                case Keys.Up:
                    round.Snake.SetDirection(Direction.UP);
                    break;
                case Keys.Down:
                    round.Snake.SetDirection(Direction.DOWN);
                    break;
                default:
                    break;
            }
        }

        private SneikGameUseCase()
        {
            round = Round.Instance ?? throw new Exception("Round instance is null");
        }

        public void StopGameLoop()
        {
            round = null;
            _gameThread.Join();
            instance = null;
        }


        public static SneikGameUseCase Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SneikGameUseCase();
                }
                return instance;
            }
        }


        public void Execute()
        {
            round = Round.Instance ?? throw new Exception("Round instance is null");
            _gameThread = new Thread(this.StartGameLoop);
            _gameThread.Start();
        }
    }
}
