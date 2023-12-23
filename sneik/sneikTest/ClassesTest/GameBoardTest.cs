using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Models;

namespace sneikTest.ClassesTest
{
    [TestClass]
    public class GameBoardTest
    {
        [TestMethod]
        public void CheckGameboardEasy()
        {
            Difficulty difficulty = Difficulty.EASY;
            GameBoard gameBoard = new GameBoard(difficulty);
            Assert.AreEqual((int)ObstacleCoef.EASY,gameBoard.Obstacles.Count);
        }
        [TestMethod]
        public void CheckGameboardMedium()
        {
            Difficulty difficulty = Difficulty.MEDIUM;
            GameBoard gameBoard = new GameBoard(difficulty);
            Assert.AreEqual((int)ObstacleCoef.MEDIUM, gameBoard.Obstacles.Count);
        }
        [TestMethod]
        public void CheckGameboardHard()
        {
            Difficulty difficulty = Difficulty.HARD;
            GameBoard gameBoard = new GameBoard(difficulty);
            Assert.AreEqual((int)ObstacleCoef.HARD, gameBoard.Obstacles.Count);
        }
        [TestMethod]
        public void CheckGameboardVeryHard()
        {
            Difficulty difficulty = Difficulty.VERY_HARD;
            GameBoard gameBoard = new GameBoard(difficulty);
            Assert.AreEqual((int)ObstacleCoef.VERY_HARD, gameBoard.Obstacles.Count);
        }
        [TestMethod]
        public void CheckGameboardNightmare()
        {
            Difficulty difficulty = Difficulty.NIGHTMARE;
            GameBoard gameBoard = new GameBoard(difficulty);
            Assert.AreEqual((int)ObstacleCoef.NIGHTMARE, gameBoard.Obstacles.Count);
        }
    }
}
