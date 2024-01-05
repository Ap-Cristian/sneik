using Logic.Interfaces;
using Logic.Systems;
using System;
using System.Collections.Generic;

namespace Logic.Models
{
    public enum ObstacleCoef
    {
        EASY = 60,
        MEDIUM = 70,
        HARD = 80,
        VERY_HARD = 90,
        NIGHTMARE = 100,
    };
    public class GameBoard
    {
        public Cell[,] BoardCells { get; set; }
        public Obstacle[,] BoardObstacles { get; set; }
        public Size Size { get; private set; } // cells count 

        //config fields
        private Size _cellSize = new Size(10, 10);
        private int _cellPadding = 2;
        //
        private int _obstacleCount = 0;
        public List<ICollidable> Obstacles { get; set; }

        private Size[] _gameboardSizes = {
            new Size(40, 40),   // easy
            new Size(60, 60),   // medium
            new Size(80, 80),   // hard
            new Size(100, 100), // very hard
            new Size(120, 120)  // nightmare
        };
        public Difficulty Difficulty { get; set; }
        private CollisionSystem _collisionSystem;
        private void SpawnObstacles()
        {
            var rand = new Random();
            BoardObstacles = new Obstacle[Size.Width,Size.Height];
            
            Obstacles = new List<ICollidable>();
            for (int i = 0; i < _obstacleCount; i++)
            {
                int randX = rand.Next(Size.Width);
                int randY = rand.Next(Size.Height);

                Point currentPos = BoardCells[randX,randY].Position;
                Obstacle currentObstacle = new Obstacle(currentPos, _cellSize);
                bool collidingWithExisting = false;

                for (int j = 0; j < Obstacles.Count; j++)
                {
                    if (currentObstacle.CheckCollision(Obstacles[j]))
                    {
                        collidingWithExisting = true;
                        j = Obstacles.Count;
                    }
                }
                while (collidingWithExisting)
                {
                    randX = rand.Next(Size.Width);
                    randY = rand.Next(Size.Height);

                    currentPos = BoardCells[randX, randY].Position;
                    currentObstacle = new Obstacle(currentPos, _cellSize);
                    collidingWithExisting = false;

                    for (int j = 0; j < Obstacles.Count; j++)
                    {
                        if (currentObstacle.CheckCollision(Obstacles[j]))
                        {
                            collidingWithExisting = true;
                            j = Obstacles.Count;
                        }
                    }
                }
                Obstacles.Add(currentObstacle);
                BoardObstacles[randX,randY] = currentObstacle;
            }
            _collisionSystem.AddObstacles(BoardObstacles);
        }
        public GameBoard(Difficulty difficulty)
        {
            Difficulty = difficulty;
            _collisionSystem = CollisionSystem.Instance;

            switch (this.Difficulty)
            {
                case Difficulty.EASY:
                    Size = _gameboardSizes[0];
                    _obstacleCount = (int)ObstacleCoef.EASY;
                    break;
                case Difficulty.MEDIUM:
                    Size = _gameboardSizes[1];
                    _obstacleCount = (int)ObstacleCoef.MEDIUM;
                    break;
                case Difficulty.HARD:
                    Size = _gameboardSizes[2];
                    _obstacleCount = (int)ObstacleCoef.HARD;
                    break;
                case Difficulty.VERY_HARD:
                    Size = _gameboardSizes[3];
                    _obstacleCount = (int)ObstacleCoef.VERY_HARD;
                    break;
                case Difficulty.NIGHTMARE:
                    Size = _gameboardSizes[4];
                    _obstacleCount = (int)ObstacleCoef.NIGHTMARE;
                    break;
            }

            BoardCells = new Cell[Size.Width, Size.Height];
            var currentPos = new Point(0, 0);
            for (int x = 0; x < Size.Width; x++)
            {
                for (int y = 0; y < Size.Height; y++)
                {
                    BoardCells[x, y] = new Cell(new Point(currentPos), _cellSize, Color.PAYNES_GRAY);
                    currentPos.Y += _cellSize.Height + _cellPadding;
                }
                currentPos.X += _cellSize.Width + _cellPadding;
                currentPos.Y = 0;

            }
            SpawnObstacles();
        }
    }
}