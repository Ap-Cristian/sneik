using Logic.Attributes;
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
        private Size _cellSize = new Size(10, 10);
        private int _cellPadding = 2;
        private Size _size { get; set; } // cells count 
        private int _obstacleCount = 0;
        public Cell[,] BoardCells { get; set; }
        public List<Obstacle> Obstacles { get; set; }

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
            Obstacles = new List<Obstacle>();
            for (int i = 0; i < _obstacleCount; i++)
            {
                Point currentPos = new Point(rand.Next(0, _size.Width), rand.Next(0, _size.Height));
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
                    currentPos = new Point(rand.Next(0, _size.Width), rand.Next(0, _size.Height));
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
            }
            List<Collidable> tempColliders = new List<Collidable>();
            foreach (Collidable collidable in Obstacles)
            {
                tempColliders.Add(collidable);
            }
            _collisionSystem.AddCollidables(tempColliders);
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
                    currentPos.Y += _cellSize.Height + _cellPadding;
                }
                currentPos.X += _cellSize.Width + _cellPadding;
            }
            SpawnObstacles();
        }
    }
}
