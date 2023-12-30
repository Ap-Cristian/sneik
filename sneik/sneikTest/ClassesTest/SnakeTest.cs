
using Logic.Models;
namespace sneikTest.ClassesTest
{
    [TestClass]
    public class SnakeTest
    {
        GameBoard _testGameBoard = new GameBoard(Difficulty.EASY);

        [TestMethod]
        public void TestSnakeIncreaseSize()
        {
            Snake snake = new Snake();

            snake.IncreaseSize(20);

            Assert.AreEqual(20, snake.GetSize());

        }
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
    }
}