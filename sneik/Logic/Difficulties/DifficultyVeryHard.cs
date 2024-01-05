using System;
using Logic.Interfaces;
using Logic.Models;

namespace Logic.Difficulties
{
    public class DifficultyVeryHard : IStrategy
    {
        public Tuple<Size, int, int, int> SetDifficulty()
        {
            Size size = new Size(70, 70);

            return new Tuple<Size, int, int, int>(size, 80, 5, 200);
        }
    }
}