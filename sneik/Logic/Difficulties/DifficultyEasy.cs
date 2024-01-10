using System;
using Logic.Interfaces;
using Logic.Models;

namespace Logic.Difficulties
{
    public class DifficultyEasy : IStrategy
    {
        public Tuple<Size, int, int, int, int> SetDifficulty()
        {
            var size = new Size(20, 20);

            return new Tuple<Size, int, int, int, int>(size, 30, 20, 800,5);
        }
    }
}