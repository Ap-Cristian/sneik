using System;
using Logic.Interfaces;
using Logic.Models;

namespace Logic.Difficulties
{
    public class DifficultyNightmare : IStrategy
    {
        public Tuple<Size, int> SetDifficulty()
        {
            Size size = new Size(120, 120);
            return new Tuple<Size, int>(size, 100);
        }
    }
}
