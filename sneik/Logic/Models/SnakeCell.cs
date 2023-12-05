using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    internal class SnakeCell
    {
        public Point Position { get; set; }
        public Color Color { get; set; }
        public SnakeCell(Point position, Color color)
        {
            Position = position;
            Color = color;
        }
    }
}
