using Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sneik.Models
{
    public class Snake
    {
        const int SNAKE_SPEED_DEFAULT = 2;

        private int _size;
        private int _movementSpeed;
        private Point _headPos;
        private Direction _headingDirection = Direction.UP;

        public Snake(int size, Point pos, int movementSpeed = SNAKE_SPEED_DEFAULT)
        {
            this._size = size;
            this._headPos = pos;
            this._movementSpeed = movementSpeed;
        }
        public Snake()
        {
            this._movementSpeed = SNAKE_SPEED_DEFAULT;
        }

        public int GetSize()
        {
            return this._size;
        }

        public void SetSize(int size)
        {
            this._size = size;
        }

        public void IncreaseSize(int size)
        {
            this._size += size;
        }
    }
}
