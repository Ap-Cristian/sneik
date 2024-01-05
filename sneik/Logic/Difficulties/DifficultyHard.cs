using System;
using Logic.Interfaces;
using Logic.Models;

namespace Logic.Difficulties
{
    public class DifficultyHard : IStrategy
    {
        public Tuple<Size, int> SetDifficulty()
        {
            Size size = new Size(80, 80);

            return new Tuple<Size, int>(size, 80);
        }
    }

}