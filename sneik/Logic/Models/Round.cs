using System;
using Logic.Enums;
using Logic.Factories;
using Logic.Interfaces;
using Logic.Systems;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Logic.Models
{
    sealed public class Round
    {
        //this will have to be set from snakeGame once the user selects a difficulty
        private StrategyFactory _strategyFactory = new StrategyFactory();
        private int _snakeSpeed;
        public bool stop;
        private static Round instance;
        private const int _snakeInitialSize = 3;
        private Point _gameBoardCenterPoint;

        private ICollidableFactory _collidableFactory;
        public GameBoard Board { get; set; }
        public Snake Snake { get; set; }

        private CollisionSystem _collisionSystem;

        private void onSnakeCollision(string collidedObstacleType)
        {
            Debug.WriteLine("Snake collided with object type: " + "[" + collidedObstacleType + "]");
            //check if collided with obstacle, if true end round
            switch (collidedObstacleType)
            {
                case "Obstacle":
                    Debug.WriteLine("Ending round...");
                    EndRound();

                    break;
                case "FoodPallet":
                    this.Snake.IncreaseSize(1);
                    break;
                case "SnakePart":
                    Debug.WriteLine("Ending round...");
                    EndRound();
                    break;
                default:
                    break;
            }
        }

        private Round(ICollidableFactory collidableFactory)
        {
            stop = false;
            _collisionSystem = CollisionSystem.Instance;
            _collidableFactory = collidableFactory;
            var strategyPath = File.ReadAllText("../../../../Logic/Helpers/Difficulty.txt");
            var strategy = _strategyFactory.Create(strategyPath);
            var settings = strategy.SetDifficulty();
            Board = new GameBoard(new Tuple<Size, int, int, int>(settings.Item1, settings.Item2, settings.Item3, settings.Item5), _collidableFactory);
            _gameBoardCenterPoint = new Point(Board.Size.Width / 2, Board.Size.Height / 2);
            Snake = new Snake(_snakeInitialSize, Board, _collidableFactory);

            Snake.HeadCollidable.CollisionHandler += onSnakeCollision;


            _snakeSpeed = settings.Item4;

        }

        public void Update()
        {
            Snake.Move();
            _collisionSystem.Update();
            Thread.Sleep(_snakeSpeed);

        }
        private void EndRound()
        {
            var score = GetScore();
            using (StreamWriter writer = File.AppendText("../../../../Logic/Helpers/Scores.txt"))
            {
                writer.WriteLine(score);
            }
            stop = true;
            instance = null;
            //Environment.Exit(0);
        }
        public static Round Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Round(new CollidableFactory());
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
