using sneik.Classes;
using static sneik.Classes.Snake;
namespace sneikTest.ClassesTest
{
    [TestClass]
    public class SnakeTest
    {
        [TestMethod]
        public void TestSnakeIncreaseSize()
        {
            Snake snake = new Snake(0);

            snake.IncreaseSize(20);

            Assert.AreEqual(20, snake.GetSize());

        }
    }
}