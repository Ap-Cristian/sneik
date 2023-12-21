using Logic.Attributes;
using Logic.Systems;
using System;
using System.Collections.Generic;

namespace Logic.Models
{
    enum ObstacleCoef
    {
        EASY = 60,
        MEDIUM = 70,
        HARD = 80,
        VERY_HARD = 90,
        NIGHTMARE = 100,
    };
    internal class GameBoard
    {
        private Size _cellSize = new Size(10, 10);
        private int _cellPadding = 2;
        private Size _size { get; set; } // cells count 
        private int _obstacleCount = 0;
        public Cell[,] BoardCells { get; set; }
        public Obstacle[] Obstacles { get; set; }

        private Size[] _gameboardSizes = {
            new Size(40, 40),   // easy
            new Size(60, 60),   // medium
            new Size(80, 80),   // hard
            new Size(100, 100), // very hard
            new Size(120, 120)  // nightmare
        };
        public Difficulty Difficulty { get; set; }
        private CollisionSystem _collisionSystem = CollisionSystem.Instance;
        private void SpawnObstacles()
        {
            var rand = new Random();
            Obstacles = new Obstacle[_obstacleCount];
            for (int i = 0; i < _obstacleCount; i++)
            {
                Point currentPos = new Point(rand.Next(0, _size.Width), rand.Next(0, _size.Height));
                Obstacle currentObstacle = new Obstacle(currentPos, _cellSize);
                bool collidingWithExisting = false;

                for (int j = 0; j < Obstacles.Length; j++)
                {
                    if (currentObstacle.CheckCollision(Obstacles[j]))
                    {
                        collidingWithExisting = true;
                        j = Obstacles.Length;
                    }
                }
                while (collidingWithExisting)
                {
                    currentPos = new Point(rand.Next(0, _size.Width), rand.Next(0, _size.Height));
                    currentObstacle.Cell.Position = currentPos;
                    collidingWithExisting = false;
                    for (int j = 0; j < Obstacles.Length; j++)
                    {
                        if (currentObstacle.CheckCollision(Obstacles[j]))
                        {
                            collidingWithExisting = true;
                            j = Obstacles.Length;
                        }
                    }
                }
                Obstacles[i] = currentObstacle;
            }
            List<Collidable> tempColl = new List<Collidable>();
            foreach (var collidable in Obstacles)
            {
                tempColl.Add((Collidable)collidable);
            }
            _collisionSystem.AddCollidables(tempColl);
        }
        public GameBoard(Difficulty difficulty)
        {
            Difficulty = difficulty;

            // TODO: generate obstacles randomly
            switch (this.Difficulty)
            {
                case Difficulty.EASY:
                    _size = _gameboardSizes[0];
                    _obstacleCount = (int)ObstacleCoef.EASY;
                    break;
                case Difficulty.MEDIUM:
                    _size = _gameboardSizes[1];
                    _obstacleCount = (int)ObstacleCoef.MEDIUM;
                    break;
                case Difficulty.HARD:
                    _size = _gameboardSizes[2];
                    _obstacleCount = (int)ObstacleCoef.HARD;
                    break;
                case Difficulty.VERY_HARD:
                    _size = _gameboardSizes[3];
                    _obstacleCount = (int)ObstacleCoef.VERY_HARD;
                    break;
                case Difficulty.NIGHTMARE:
                    _size = _gameboardSizes[4];
                    _obstacleCount = (int)ObstacleCoef.NIGHTMARE;
                    break;
            }

            this.BoardCells = new Cell[_size.Width, _size.Height];
            var currentPos = new Point(0, 0);
            for (int x = 0; x < _size.Width; x++)
            {
                for (int y = 0; y < _size.Height; y++)
                {
                    this.BoardCells[x, y] = new Cell(currentPos, _cellSize, Color.RICH_BLACK);
                    currentPos.X += _cellSize.Width + _cellPadding;
                }
                currentPos.Y += _cellSize.Height + _cellPadding;
            }
            SpawnObstacles();
        }
    }
}
