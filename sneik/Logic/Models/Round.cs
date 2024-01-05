﻿using Logic.Systems;
using System.Diagnostics;

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

        private void onSnakeCollision(string collidedObstacleType)
        {
            Debug.WriteLine("Snake collided with object type: " +  "["+ collidedObstacleType +"]");
            //check if collided with obstacle, if true end round
            switch (collidedObstacleType)
            {
                case "Obstacle":
                    EndRound();
                    Debug.WriteLine("Ending round...");
                    break;
                default:
                    break;
            }
        }
        private Round()
        {
            _collisionSystem = CollisionSystem.Instance;
            Board = new GameBoard(_difficulty);
            _gameBoardCenterPoint = new Point(Board.Size.Width / 2, Board.Size.Height / 2);
            Snake = new Snake(_snakeInitialSize, Board);

            //this should not happen here
            Snake.HeadCollidable.CollisionHandler += onSnakeCollision;
        }

        public void Update()
        {
            Snake.Move();
            _collisionSystem.Update();
        }
        private void EndRound()
        {
            instance = null;
            //Environment.Exit(0);
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
