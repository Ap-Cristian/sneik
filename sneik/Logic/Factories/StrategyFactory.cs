using System;
using Logic.Difficulties;
using Logic.Interfaces;

namespace Logic.Factories
{
    public class StrategyFactory : IStrategyFactory
    {
        public IStrategy Create(string difficulty)
        {
            switch (difficulty)
            {
                case "Easy":
                    return new DifficultyEasy();
                case "Medium":
                    return new DifficultyMedium();
                case "Hard":
                    return new DifficultyHard();
                case "VeryHard":
                    return new DifficultyVeryHard();
                case "Nightmare":
                    return new DifficultyNightmare();
                default:
                    throw new ArgumentException("Invalid difficulty");

            }
        }
    }
}
