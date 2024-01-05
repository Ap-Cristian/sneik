using System;
using Logic.Helpers;
using Logic.Interfaces;
using Logic.Models;

namespace Logic.Factories
{
    public class UseCaseFactory : IUseCaseFactory
    {
        public IUseCase Create<T>() where T : IUseCase
        {
            switch (typeof(T).Name)
            {
                case "SneikGameUseCase":
                    return SneikGameUseCase.Instance;
                case "HighscoresUseCase":
                    return new HighscoresUseCase(new HighscoreReader());
                default:
                    throw new ArgumentException("Invalid type");

            }
            
        }
    }
}
