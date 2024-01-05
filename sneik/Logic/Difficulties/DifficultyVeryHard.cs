using System;
using Logic.Interfaces;
using Logic.Models;

namespace Logic.Difficulties
{
    public class DifficultyVeryHard : IStrategy
    {
        public Tuple<Size, int> SetDifficulty()
        {
            Size size = new Size(100, 100);

            return new Tuple<Size, int>(size, 90);
        }
    }
}