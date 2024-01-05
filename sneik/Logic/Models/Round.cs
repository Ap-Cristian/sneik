using Logic.Factories;
using Logic.Interfaces;
using Logic.Systems;
using System.Diagnostics;

namespace Logic.Models
{
    sealed public class Round
    {
        //this will have to be set from snakeGame once the user selects a difficulty
        private Difficulty _difficulty = Difficulty.NIGHTMARE;


        private static Round instance;
        private const int _snakeInitialSize = 3;
        private Point _gameBoardCenterPoint;

        private ICollidableFactory _collidableFactory;
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
            _collisionSystem = CollisionSystem.Instance;
            _collidableFactory = collidableFactory;
            Board = new GameBoard(_difficulty, _collidableFactory);
            _gameBoardCenterPoint = new Point(Board.Size.Width / 2, Board.Size.Height / 2);
            Snake = new Snake(_snakeInitialSize, Board, _collidableFactory);

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
