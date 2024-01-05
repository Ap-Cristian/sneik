using Logic.Systems;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logic.Models
{
    sealed public class Round
    {
        //this will have to be set from snakeGame once the user selects a difficulty
        private Difficulty _difficulty = Difficulty.EASY;

        private static Round instance;
        private const int _snakeInitialSize = 3;
        private Point _gameBoardCenterPoint;
        public GameBoard Board { get; set; }
        public Snake Snake { get; set; }

        private CollisionSystem _collisionSystem;

        private void onSnakeCollision(Object sender, EventArgs args)
        {
            Debug.WriteLine("Snake collided!");
            //check if collided with obstacle, if true end round

        }

        private Round()
        {
            _collisionSystem = CollisionSystem.Instance;
            Board = new GameBoard(_difficulty);
            _gameBoardCenterPoint = new Point(Board.Size.Width / 2, Board.Size.Height / 2);
            //Snake = new Snake(_snakeInitialSize, Board);
            Snake = Board.Snake;
            //this should not happen here
            Snake.HeadCollidable.CollisionHandler += onSnakeCollision;
        }

        public void Update()
        {
            Snake.Move();
            Board.UpdateSnake();
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
        public int GetScore()
        {
            return (Snake.GetSize() - _snakeInitialSize) * 100;
        }

        public void Start()
        {

        }
    }
}
