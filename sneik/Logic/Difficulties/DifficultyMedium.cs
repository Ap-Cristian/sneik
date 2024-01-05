using System;
using Logic.Interfaces;
using Logic.Models;

namespace Logic.Difficulties
{
    public class DifficultyMedium : IStrategy
    {
        public Tuple<Size, int> SetDifficulty()
        {
            Size size = new Size(60, 60);

            return new Tuple<Size, int>(size, 70);
        }
    }
}