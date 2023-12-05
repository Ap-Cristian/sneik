using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    internal class GameBoad
    {
        private Size _size { get; set; }
        private Obstacle[] _obstacles { get; set; }
        public Difficulty Difficulty { get; set; }
        public GameBoad(Size size, Difficulty difficulty)
        {
            Difficulty = difficulty;
            this._size = size;

            // TODO: generate obstacles randomly
            switch (this.Difficulty)
            {
                case Difficulty.EASY:
                    break;
                case Difficulty.MEDIUM:
                    break;
                case Difficulty.HARD:
                    break;
                case Difficulty.VERY_HARD:
                    break;
                case Difficulty.NIGHTMARE:
                    break;
            }

        }
    }
}
