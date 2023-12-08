
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
    }
}