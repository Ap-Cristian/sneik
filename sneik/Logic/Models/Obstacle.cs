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
                CollisionHandler?.Invoke(this, EventArgs.Empty);
                return true;
            }
            return false;
        }
    }
}
