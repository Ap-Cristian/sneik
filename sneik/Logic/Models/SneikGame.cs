﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logic.Models
{
    public class SneikGame
    {
        private static SneikGame instance;
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
        private SneikGame() {
            round = Round.Instance;
            _gameThread = new Thread(this.StartGameLoop);
            _gameThread.Start();
        }

        public void StopGameLoop()
        {
            round = null;
            _gameThread.Join();
        }


        public static SneikGame Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SneikGame();
                }
                return instance;
            }
        }


    }
}
