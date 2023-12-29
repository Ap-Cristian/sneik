using Logic.Models;
using System;

namespace Logic.Attributes
{
    public class Collidable
    {
        public Collider Collider { get; set; }
        public event EventHandler CollisionHandler;
        private const Color _wireframeDebugColor = Color.YELLOW;
        public Collidable(Cell parrentCell)
        {
            this.Collider = new Collider(parrentCell.Position, parrentCell.Size);
        }

        public bool CheckCollision(Collidable collidable)
        {
            if (Collider != null && this != null)
            {
                Point FirstColLU = Collider.Position; //Left up corner
                Point FirstColRD = new Point(FirstColLU.X + Collider.Size.Width, FirstColLU.Y + Collider.Size.Height); //Rigth down corner

                Point SecondColLU = collidable.Collider.Position;
                Point SecondColRD = new Point(SecondColLU.X + collidable.Collider.Size.Width, SecondColLU.Y + collidable.Collider.Size.Height);

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
            throw new Exception("[ERR!] Collider not instanciated!");
        }
    }
}
