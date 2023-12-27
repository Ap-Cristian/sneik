using Logic.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logic.Models
{
    sealed public class Round
    {
        private static Round instance;
        private const int _snakeInitialSize = 3;
        private Point _gameBoardCenterPoint;
        public GameBoard Board { get; set; }
        public Snake Snake { get; set; }

        private CollisionSystem _collisionSystem;
        private Difficulty _difficulty = Difficulty.NIGHTMARE;
        private Round()
        {
            _collisionSystem = CollisionSystem.Instance;
            Board = new GameBoard(_difficulty);
            _gameBoardCenterPoint = new Point(Board.Size.Width / 2, Board.Size.Height / 2);
            Snake = new Snake(_snakeInitialSize, _gameBoardCenterPoint);
        }

        public void Update()
        {
            Snake.Move();
            _collisionSystem.Update();
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
