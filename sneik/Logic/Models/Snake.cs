using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    public class Snake
    {
        const int SNAKE_SPEED_DEFAULT = 2;

        private int _size;
        private int _movementSpeed;
        private ObservableCollection<Point> _snakeBodyParts;
        private Point _headPos;
        private Direction _headingDirection = Direction.UP;

        public Snake(int size, Point pos, int movementSpeed = SNAKE_SPEED_DEFAULT)
        {
            this._size = size;
            this._movementSpeed = movementSpeed;
            int bodyInitialSize = 3;
            this._snakeBodyParts = new ObservableCollection<Point>();
            for (int i = 0; i < bodyInitialSize; i++)
            {
                this._snakeBodyParts.Add(new Point(pos.X, pos.Y + i));
            }
            this._headPos = this._snakeBodyParts[0];

        }
        public Snake()
        {
            this._movementSpeed = SNAKE_SPEED_DEFAULT;
        }

        public int GetSize()
        {
            return this._size;
        }
        public Direction GetDirection()
        {
            return this._headingDirection;
        }
        public Point GetHeadPos()
        {
            return this._headPos;
        }

        public void SetDirection(Direction direction)
        {
            this._headingDirection = direction;
        }

        public void SetSize(int size)
        {
            this._size = size;
        }

        public void IncreaseSize(int size)
        {
            this._size += size;
        }

        public void Move()
        {
            Point pos = new Point(this._headPos.X, this._headPos.Y);
            this._snakeBodyParts.Insert(1, pos);
            this._snakeBodyParts.RemoveAt(this._snakeBodyParts.Count - 1);

            switch (this._headingDirection)
            {
                case Direction.UP:
                    this._headPos.Y -= 1;
                  
                    break;
                case Direction.DOWN:
                    this._headPos.Y += 1;
                   
                    break;
                case Direction.LEFT:
                    this._headPos.X -= 1;
                    break;
                case Direction.RIGHT:
                    this._headPos.X += 1;
                    break;
            }
        }

    }
}
