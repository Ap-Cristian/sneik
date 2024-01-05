using Logic.Interfaces;
using Logic.Models;
using Moq;

namespace sneikTest.ClassesTest
{
    [TestClass]
    public class HighscoreUseCaseTests
    {
        private Mock<IHighscoreReader> _highscoreReader;
        private HighscoresUseCase _highscoresUseCase;

        public HighscoreUseCaseTests()
        {
            _highscoreReader = new Mock<IHighscoreReader>();
            _highscoresUseCase = new HighscoresUseCase(_highscoreReader.Object);
        }

        [TestMethod]
        public void TestExecute_ReturnsTop3()
        {
            _highscoreReader.Setup(x => x.ReadScores()).Returns(new List<int> { 1, 2, 3, 4, 5 });
            _highscoresUseCase.Execute();
            Assert.AreEqual(3, _highscoresUseCase._highscores.Count);
        }

        [TestMethod]
        public void TestExecute_ReturnsSortedHighscores()
        {
            _highscoreReader.Setup(x => x.ReadScores()).Returns(new List<int> { 1, 2, 3, 4, 5 });
            _highscoresUseCase.Execute();

            Assert.AreEqual(5, _highscoresUseCase._highscores[0]);
        }

        [TestMethod]
        public void TestExecute_ReturnsCorrectHighscores()
        {
            _highscoreReader.Setup(x => x.ReadScores()).Returns(new List<int> { 4, 5 });
            _highscoresUseCase.Execute();
            Assert.AreEqual(5, _highscoresUseCase._highscores[0]);
            Assert.AreEqual(4, _highscoresUseCase._highscores[1]);
        }

        [TestMethod]
        public void TestExecute_ReturnEmpltyList_WhenNoHighscoresAreFound()
        {
            _highscoreReader.Setup(x => x.ReadScores()).Returns(new List<int>());
            _highscoresUseCase.Execute();
            Assert.AreEqual(0, _highscoresUseCase._highscores.Count);
        }
    }
}
