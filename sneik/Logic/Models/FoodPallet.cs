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
        public event CollisionEventHandler CollisionHandler;

        public bool CheckCollision(ICollidable collidable)
        {
            Point FirstColLU = Cell.Position; //Left up corner
            Point FirstColRD = new Point(FirstColLU.X + Cell.Size.Width, FirstColLU.Y + Cell.Size.Height); //Rigth down corner

            Point SecondColLU = collidable.Cell.Position;
            Point SecondColRD = new Point(SecondColLU.X + collidable.Cell.Size.Width, SecondColLU.Y + collidable.Cell.Size.Height);

            if ((SecondColLU.X > FirstColRD.X || SecondColRD.X < FirstColLU.X) || (SecondColLU.Y > FirstColRD.Y || SecondColRD.Y < FirstColLU.Y))
            {
                return false;
            }

            Point minLeftUpCorner = new Point(Math.Max(FirstColLU.X, SecondColLU.X), Math.Min(FirstColLU.Y, SecondColLU.Y));
            Point maxRightDownCorner = new Point(Math.Min(FirstColRD.X, SecondColRD.X), Math.Max(FirstColRD.Y, SecondColRD.Y));

            int overlappingArea = (maxRightDownCorner.X - minLeftUpCorner.X) * (maxRightDownCorner.Y - minLeftUpCorner.Y);

            if (overlappingArea != 0)
            {
                //raise collision event here
                //pass collidable object type as event args
                //create CollidedEventArgs 
                switch (collidable)
                {
                    case Obstacle _:
                        CollisionHandler?.Invoke("Obstacle");
                        break;
                    case FoodPallet _:
                        CollisionHandler?.Invoke("FoodPallet");
                        break;
                    case SnakePart _:
                        CollisionHandler?.Invoke("SnakePart");
                        break;
                }
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
