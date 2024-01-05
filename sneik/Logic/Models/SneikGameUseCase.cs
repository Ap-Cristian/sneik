using System;
using System.Threading;
using Logic.Interfaces;

namespace Logic.Models
{
    public class SneikGameUseCase : IUseCase
    {
        private static SneikGameUseCase instance;
        public Round round { get; set; }

        public event EventHandler DrawUpdateDelegate;
        private Thread _gameThread;
        private void StartGameLoop()
        {
            while (round != null)
            {
                this.round.Update();
                this.DrawUpdateDelegate?.Invoke(this, EventArgs.Empty);
                Thread.Sleep(100);
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
