using System;
using Logic.Interfaces;
using Logic.Models;

namespace Logic.Difficulties
{
    public class DifficultyEasy : IStrategy
    {
        public Tuple<Size, int> SetDifficulty()
        {
            var size = new Size(40, 40);

            return new Tuple<Size, int>(size, 60);
        }
    }
}