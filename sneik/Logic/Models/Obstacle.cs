using Logic.Interfaces;
using System;

namespace Logic.Models
{
    public class Obstacle : ICollidable
    {
        public Cell Cell { get; set; }
        public Obstacle(Point position, Size size, Color color = Color.TEA_GREEN) 
        {
            Cell = new Cell(position, size, color);
        }
        public Obstacle(Cell Cell)
        {
            this.Cell = Cell;
        }

        public Obstacle(Obstacle obstacle)
        {
            this.Cell = obstacle.Cell;
            CollisionHandler = obstacle.CollisionHandler;
        }

        public event EventHandler CollisionHandler;

        public bool CheckCollision(ICollidable collidable)
        {

            if (collidable.Cell.Position.X == this.Cell.Position.X && collidable.Cell.Position.Y == this.Cell.Position.Y)
            {
                CollisionHandler?.Invoke(this, EventArgs.Empty);
                return true;
            }
            return false;
        }
    }
}
