using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    public class Cell
    {
        private Guid _id;
        //up-left corner of cell
        public Point Position { get; set; }

        public Size Size { get; set; }
        public Color Color { get; set; }
        public Cell(Point position, Size size, Color color)
        {
            Position = position;
            Size = size;
            Color = color;
            _id = Guid.NewGuid();
        }
    }
}
