using System;
using Logic.Interfaces;
using Logic.Models;

namespace Logic.Difficulties
{
    public class DifficultyNightmare : IStrategy
    {
        public Tuple<Size, int, int, int, int> SetDifficulty()
        {
            Size size = new Size(100, 100);
            return new Tuple<Size, int, int, int,int>(size, 150, 5, 50,2);
        }
    }
}
