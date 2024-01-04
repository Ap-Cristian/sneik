using Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    public class FoodPallet : ICollidable
    {
        public Cell Cell { get; set; }
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
        public FoodPallet(Point position, Size size, Color color = Color.AIR_SUPERIORITY_BLUE)
        {
            Cell = new Cell(position, size, color);

        }
        public FoodPallet(Cell cell)
        {
            Cell = cell;
        }
        public FoodPallet(FoodPallet foodPallet)
        {
            Cell = foodPallet.Cell;
            CollisionHandler = foodPallet.CollisionHandler;

        }

    }
  
}
