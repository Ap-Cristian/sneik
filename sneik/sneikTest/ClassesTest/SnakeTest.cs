
using Logic.Interfaces;
using Logic.Models;
namespace sneikTest.ClassesTest
{
    [TestClass]
    public class SnakeTest
    {
        GameBoard _testGameBoard = new GameBoard(Difficulty.EASY);


        [TestMethod]
        public void TestSnakeMove()
        {
            Snake snake = new Snake(3, _testGameBoard);

            snake.Move();
            switch (snake.GetDirection())
            {
                case Direction.UP:
                    Assert.AreEqual(0, snake.GetHeadPosBoardSpace().X);
                    Assert.AreEqual(-1, snake.GetHeadPosBoardSpace().Y);
                    break;
                case Direction.DOWN:
                    Assert.AreEqual(0, snake.GetHeadPosBoardSpace().X);
                    Assert.AreEqual(4, snake.GetHeadPosBoardSpace().Y); //workaround:)
                    break;
                case Direction.LEFT:
                    Assert.AreEqual(-1, snake.GetHeadPosBoardSpace().X);
                    Assert.AreEqual(0, snake.GetHeadPosBoardSpace().Y);
                    break;
                case Direction.RIGHT:
                    Assert.AreEqual(1, snake.GetHeadPosBoardSpace().X);
                    Assert.AreEqual(0, snake.GetHeadPosBoardSpace().Y);
                    break;
            }
        }
        [TestMethod]
        public void TestSnakeIncreaseSize()
        {
            int snakeSize = 3;
            Snake snake = new Snake(snakeSize, _testGameBoard);
           
            snake.IncreaseSize(2);

            Assert.AreEqual(snakeSize + 2, snake.GetSize());
            Assert.AreEqual(snakeSize + 2, snake.SnakeBodyPartsScreenSpace.Count);  
        }

        [TestMethod]
        public void TestSnakeDecreaseSize()
        {
            int snakeSize = 20;
            Snake snake = new Snake(snakeSize, _testGameBoard);

            snake.DecreaseSize(2);

            Assert.AreEqual(snakeSize - 2, snake.GetSize());
            Assert.AreEqual(snakeSize - 2, snake.SnakeBodyPartsScreenSpace.Count);
        }
    }
}