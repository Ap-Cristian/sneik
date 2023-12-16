using Logic.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Attributes
{
    public class Collidable
    {
        public Collider Collider { get; set; }
        public event EventHandler CollisionDelegate;
        private const Color _wireframeDebugColor = Color.YELLOW;
        public Collidable(Cell parrentCell) { 
            this.Collider = new Collider(parrentCell.Position, parrentCell.Size);
        }

        public bool CheckCollision(Collidable collidable)
        {
            if (Collider != null)
            {
                Point FirstColLU = Collider.Position; //Left up corner
                Point FirstColRD = new Point(FirstColLU.X + Collider.Size.Width, FirstColLU.Y + Collider.Size.Height); //Rigth down corner
                
                Point SecondColLU = collidable.Collider.Position;
                Point SecondColRD = new Point(SecondColLU.X + Collider.Size.Width, SecondColLU.Y + Collider.Size.Height);

                Point minLeftUpCorner = new Point(Math.Max(FirstColLU.X, SecondColLU.X), Math.Min(FirstColLU.Y, SecondColLU.Y));
                Point maxRightDownCorner = new Point(Math.Min(FirstColRD.X, SecondColRD.X), Math.Max(FirstColRD.Y, SecondColRD.Y));

                int overlappingArea = (maxRightDownCorner.X - minLeftUpCorner.X) * (maxRightDownCorner.Y - minLeftUpCorner.Y);

                if (overlappingArea > 0)
                {
                    //raise collision event here
                    CollisionDelegate?.Invoke(this, EventArgs.Empty);
                    return true;
                }
                return false;

            }
            throw new NotImplementedException();
        }
    }
}
