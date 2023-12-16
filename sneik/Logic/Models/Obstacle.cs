using Logic.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    public class Obstacle : Collidable
    {
        public Cell Cell { get; set; }
        public Obstacle(Point position, Size size, Color color = Color.RED) 
            : base(new Cell(position, size, Color.TRANSPARENT))
        {
            this.Cell = new Cell(position, size, color);
        }

    }
}
