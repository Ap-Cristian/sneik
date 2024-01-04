using Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    public class SnakePart : ICollidable
    {


        public Cell Cell { get; set; }
        public event EventHandler CollisionHandler;

        public SnakePart(Point position, Size size, Color color = Color.RED)
        {
            Cell = new Cell(position, size, color);
            
        }
        public SnakePart(Cell cell)
        {
            Cell = cell;
        }
        public SnakePart(SnakePart snakePart)
        {
            Cell = snakePart.Cell;
            CollisionHandler = snakePart.CollisionHandler;

        }
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
