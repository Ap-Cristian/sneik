using System;
using Logic.Interfaces;
using Logic.Models;

namespace Logic.Difficulties
{
    public class DifficultyMedium : IStrategy
    {
        public Tuple<Size, int, int, int, int> SetDifficulty()
        {
            Size size = new Size(30, 30);

            return new Tuple<Size, int, int, int,int>(size, 40, 10, 600,5);
        }
    }
}