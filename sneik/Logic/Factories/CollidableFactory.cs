using Logic.Interfaces;
using Logic.Models;
using System;

namespace Logic.Factories
{
    public class CollidableFactory : ICollidableFactory
    {
        public ICollidable Create<T>(Point point, Size size, Color color) where T : ICollidable
        {
            switch (typeof(T).Name)
            {
                case "Obstacle":
                    return new Obstacle(point, size, color);
                case "FoodPallet":
                    return new FoodPallet(point, size, color);
                case "SnakePart":
                    return new SnakePart(point, size, color);
                default:
                    throw new ArgumentException("Invalid type");

            }
        }
    }
}
--