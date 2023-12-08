
using Logic.Models;
namespace sneikTest.ClassesTest
{
    [TestClass]
    public class SnakeTest
    {
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
            Snake snake = new Snake(3, new Point(0, 0));

            snake.Move();
            switch (snake.GetDirection())
            {
                case Direction.UP:
                    Assert.AreEqual(0, snake.GetHeadPos().X);
                    Assert.AreEqual(-1, snake.GetHeadPos().Y);
                    break;
                case Direction.DOWN:
                    Assert.AreEqual(0, snake.GetHeadPos().X);
                    Assert.AreEqual(1, snake.GetHeadPos().Y);
                    break;
                case Direction.LEFT:
                    Assert.AreEqual(-1, snake.GetHeadPos().X);
                    Assert.AreEqual(0, snake.GetHeadPos().Y);
                    break;
                case Direction.RIGHT:
                    Assert.AreEqual(1, snake.GetHeadPos().X);
                    Assert.AreEqual(0, snake.GetHeadPos().Y);
                    break;
            }
        }
    }
}