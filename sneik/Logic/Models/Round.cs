using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logic.Models
{
    sealed class Round
    {
        private static Round instance;
        private GameBoard _board;
        private Difficulty _difficulty = Difficulty.NIGHTMARE;
        private Round() {
            _board = new GameBoard(_difficulty);
        }

        public static Round Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Round();
                }
                return instance;
            }
        }   
        public void Start()
        {

        }
    }
}
