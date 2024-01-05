using Logic.Models;

namespace Logic.Interfaces
{
    public interface ICollidableFactory
    {
        ICollidable Create<T>(Point point, Size size, Color color) where T : ICollidable;
    }
}
