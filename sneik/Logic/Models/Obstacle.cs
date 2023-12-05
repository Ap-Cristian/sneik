using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    internal class Obstacle
    {
        private const Color DEFAULT_OBSTACLE_COLOR = Color.RED;
        public Point Position { get; set; }
        public Size Size { get; set; }
        public Color Color { get; set; } = DEFAULT_OBSTACLE_COLOR;

        public Obstacle(Point position, Size size, Color color = DEFAULT_OBSTACLE_COLOR)
        {
            this.Position = position;
            this.Color = color;
            this.Size = size;
        }

    }
}
