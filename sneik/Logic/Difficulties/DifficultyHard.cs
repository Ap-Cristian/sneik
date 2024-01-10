using System;
using Logic.Interfaces;
using Logic.Models;

namespace Logic.Difficulties
{
    public class DifficultyHard : IStrategy
    {
        public Tuple<Size, int, int, int, int> SetDifficulty()
        {
            Size size = new Size(50, 50);

            return new Tuple<Size, int, int, int,int>(size, 60, 10, 400,3);
        }
    }

}