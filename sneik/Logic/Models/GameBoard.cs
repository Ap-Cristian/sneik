using Logic.Factories;
using Logic.Interfaces;
using Logic.Systems;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Logic.Models
{
    public enum ObstacleCoef
    {
        EASY = 30,
        MEDIUM = 40,
        HARD = 60,
        VERY_HARD = 80,
        NIGHTMARE = 150,
    };
    public enum  CellSizeCoef
    {
        EASY = 20,
        MEDIUM = 10,
        HARD = 10,
        VERY_HARD = 5,
        NIGHTMARE = 5,
    }
    public class GameBoard
    {
        private ICollidableFactory _collidableFactory;
        public Cell[,] BoardCells { get; set; }
        public ICollidable[,] BoardObstacles { get; set; }
        public ICollidable[,] BoardFood { get; set; }
        public Size Size { get; private set; } // cells count 

        //config fields
        public Size CellSize { get; }
        private int _cellPadding = 2;
        //
        private int _obstacleCount = 0;
        private int _foodCount = 0;
        public List<ICollidable> Obstacles { get; set; }

        public List<ICollidable> Food { get; set; }

        private Size[] _gameboardSizes = {
            new Size(20, 20),   // easy
            new Size(30, 30),   // medium
            new Size(50, 50),   // hard
            new Size(70, 70), // very hard
            new Size(100, 100)  // nightmare
        };
        public Difficulty Difficulty { get; set; }
        private CollisionSystem _collisionSystem;

        private void onFoodPalletCollision(string collidedWithType)
        {
            Food.First().CollisionHandler -= onFoodPalletCollision;
            Food.Remove(Food.First());
            SpawnFoodPallet();
        }

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
                ICollidable currentFood = _collidableFactory.Create<FoodPallet>(currentPos, CellSize, Color.YELLOW);

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
                    currentFood = _collidableFactory.Create<FoodPallet>(currentPos, CellSize, Color.YELLOW);
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
                currentFood.CollisionHandler += onFoodPalletCollision;
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
                ICollidable currentObstacle = _collidableFactory.Create<Obstacle>(currentPos, CellSize, Color.TEA_GREEN);
                
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
                    currentObstacle = _collidableFactory.Create<Obstacle>(currentPos, CellSize, Color.TEA_GREEN);
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
                    CellSize = new Size((int)CellSizeCoef.EASY, (int)CellSizeCoef.EASY);
                    _cellPadding = 5;
                    break;
                case Difficulty.MEDIUM:
                    Size = _gameboardSizes[1];
                    _obstacleCount = (int)ObstacleCoef.MEDIUM;
                    CellSize = new Size((int)CellSizeCoef.MEDIUM, (int)CellSizeCoef.MEDIUM);
                    _cellPadding = 5;
                    break;
                case Difficulty.HARD:
                    Size = _gameboardSizes[2];
                    _obstacleCount = (int)ObstacleCoef.HARD;
                    CellSize = new Size((int)CellSizeCoef.HARD, (int)CellSizeCoef.HARD);
                    _cellPadding = 3;

                    break;
                case Difficulty.VERY_HARD:
                    Size = _gameboardSizes[3];
                    _obstacleCount = (int)ObstacleCoef.VERY_HARD;
                    CellSize = new Size((int)CellSizeCoef.VERY_HARD, (int)CellSizeCoef.VERY_HARD);
                    _cellPadding = 2;
                    break;
                case Difficulty.NIGHTMARE:
                    Size = _gameboardSizes[4];
                    _obstacleCount = (int)ObstacleCoef.NIGHTMARE;
                    CellSize = new Size((int)CellSizeCoef.NIGHTMARE, (int)CellSizeCoef.NIGHTMARE);
                    _cellPadding = 2;
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
                    BoardCells[x, y] = new Cell(new Point(currentPos), CellSize, Color.PAYNES_GRAY);
                    currentPos.Y += CellSize.Height + _cellPadding;
                }
                currentPos.X += CellSize.Width + _cellPadding;
                currentPos.Y = 0;

            }
            SpawnObstacles();
            SpawnFoodPallet();
            
        }
    }
}