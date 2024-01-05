using Logic.Factories;
using Logic.Interfaces;
using Logic.Systems;
using System;
using System.Collections.Generic;
using System.Diagnostics;

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
        private ICollidableFactory _collidableFactory;
        public Cell[,] BoardCells { get; set; }
        public ICollidable[,] BoardObstacles { get; set; }
        public ICollidable[,] BoardFood { get; set; }
        public Size Size { get; private set; } // cells count 

        //config fields
        private Size _cellSize = new Size(10, 10);
        private int _cellPadding = 2;
        //
        private int _obstacleCount = 0;
        private int _foodCount = 0;
        public List<ICollidable> Obstacles { get; set; }

        public List<ICollidable> Food { get; set; }

        private Size[] _gameboardSizes = {
            new Size(40, 40),   // easy
            new Size(60, 60),   // medium
            new Size(80, 80),   // hard
            new Size(100, 100), // very hard
            new Size(120, 120)  // nightmare
        };
        public Difficulty Difficulty { get; set; }
        private CollisionSystem _collisionSystem;
        private void SpawnFoodPallet()
        {
            var rand = new Random();
            BoardFood = new ICollidable[Size.Width, Size.Height];
            
            Food = new List<ICollidable>();
            for (int i = 0; i < _foodCount; i++)
            {
                int randX = rand.Next(Size.Width);
                int randY = rand.Next(Size.Height);

                Point currentPos = BoardCells[randX, randY].Position;
                ICollidable currentFood = _collidableFactory.Create<FoodPallet>(currentPos, _cellSize, Color.YELLOW);

                bool collidingWithExisting = false;

                for (int j = 0; j < Food.Count; j++)
                {
                    if (currentFood.CheckCollision(Food[j]))
                    {
                        collidingWithExisting = true;
                        j = Food.Count;
                    }
                    if (currentFood.CheckCollision(Obstacles[j]))
                    {
                        collidingWithExisting = true;
                        j = Food.Count;
                    }
                }
                while (collidingWithExisting)
                {
                    randX = rand.Next(Size.Width);
                    randY = rand.Next(Size.Height);

                    currentPos = BoardCells[randX, randY].Position;
                    currentFood = _collidableFactory.Create<FoodPallet>(currentPos, _cellSize, Color.YELLOW);
                    collidingWithExisting = false;

                    for (int j = 0; j < Food.Count; j++)
                    {
                        if (currentFood.CheckCollision(Food[j]))
                        {
                            collidingWithExisting = true;
                            j = Food.Count;
                        }
                        if (currentFood.CheckCollision(Obstacles[j]))
                        {
                            collidingWithExisting = true;
                            j = Food.Count;
                        }
                    }
                }
                Food.Add(currentFood);
                BoardFood[randX, randY] = currentFood;
            }
            _collisionSystem.AddCollidable(BoardFood);
        }
        private void SpawnObstacles()
        {
            var rand = new Random();
            BoardObstacles = new ICollidable[Size.Width,Size.Height];
            
            Obstacles = new List<ICollidable>();
            for (int i = 0; i < _obstacleCount; i++)
            {
                int randX = rand.Next(Size.Width);
                int randY = rand.Next(Size.Height);

                Point currentPos = BoardCells[randX,randY].Position;
                ICollidable currentObstacle = _collidableFactory.Create<Obstacle>(currentPos, _cellSize, Color.TEA_GREEN);
                
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
                    currentObstacle = _collidableFactory.Create<Obstacle>(currentPos, _cellSize, Color.TEA_GREEN);
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
            _collisionSystem.AddCollidable(BoardObstacles);
        }
        public GameBoard(Difficulty difficulty, ICollidableFactory collidableFactory)
        {
            Difficulty = difficulty;
            _collisionSystem = CollisionSystem.Instance;
            // TODO: generate obstacles randomly
            switch (Difficulty)
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
            _foodCount = 1;
            _collidableFactory = collidableFactory;


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
            SpawnFoodPallet();
            
        }
    }
}