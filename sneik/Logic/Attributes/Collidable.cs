using Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Attributes
{
    internal class Collidable
    {
        public Collider Collider { get; set; }
        private const Color _wireframeDebugColor = Color.YELLOW;
        public Collidable(Cell parrentCell) { 
            Collider.Size = parrentCell.Size;
            Collider.Position = parrentCell.Position;
        }

        public bool CheckCollision(Collidable collidable)
        {
            if (Collider != null)
            {
                Point FirstColLU = Collider.Position; //Left up corner
                Point FirstColRD = FirstColLU.Add(Collider.Size); //Rigth down corner
                
                Point SecondColLU = collidable.Collider.Position;
                Point SecondColRD = SecondColLU.Add(collidable.Collider.Size);

                Point minLeftUpCorner = new Point(Math.Max(FirstColLU.X, SecondColLU.X), Math.Min(FirstColLU.Y, SecondColLU.Y));
                Point maxRightDownCorner = new Point(Math.Min(FirstColRD.X, SecondColRD.X), Math.Max(FirstColRD.Y, SecondColRD.Y));

                int overlappingArea = Math.Abs(maxRightDownCorner.X - minLeftUpCorner.X) * (maxRightDownCorner.Y - minLeftUpCorner.Y); // untested ;_;
                if(overlappingArea > 0)
                {
                    //raise collision event here
                    return true;
                }
                return false;

            }
            throw new NotImplementedException();
        }
    }
}
