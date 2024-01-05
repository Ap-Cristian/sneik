using Logic.Interfaces;

namespace Logic.Factories
{
    public interface IUseCaseFactory
    {
        IUseCase Create<T>() where T : IUseCase;
    }
}
