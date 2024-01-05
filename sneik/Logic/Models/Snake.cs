
using Logic.Interfaces;
using Logic.Systems;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic.Models
{
    public class Snake
    {
        const int SNAKE_SPEED_DEFAULT = 2;

        private int _currentSize;
        private int _movementSpeed;

        private ICollidableFactory _collidableFactory;

        private GameBoard _board;
        private List<Point> _snakeBodyPartsBoardIdx;
        public List<ICollidable> SnakeBodyPartsScreenSpace { get; set; }

        private Point _headPosBoardIdx;
        private readonly Point _headPosScreenSpace;

        private Size _snakeCellSize = new Size(2, 2);
        private Direction _headingDirection = Direction.DOWN;
        private CollisionSystem _collisionSystem;

        public ICollidable HeadCollidable { get; set; }

        public Snake(int size, GameBoard gameBoard, ICollidableFactory collidableFactory, int movementSpeed = SNAKE_SPEED_DEFAULT)
        {
            _currentSize = size;

            _collidableFactory = collidableFactory;

            _movementSpeed = movementSpeed;
            _board = gameBoard;

            _snakeBodyPartsBoardIdx = new List<Point>();
            SnakeBodyPartsScreenSpace = new List<ICollidable>();

            for (int i = 0; i < _currentSize; i++)
            {
                Point cellCoordsBoardIdx = new Point(0, size - i);
                Point cellCoordsScreenSpace = new Point(_board.BoardCells[cellCoordsBoardIdx.X, cellCoordsBoardIdx.Y].Position);

                _snakeBodyPartsBoardIdx.Add(cellCoordsBoardIdx);
                SnakeBodyPartsScreenSpace.Add(new SnakePart(cellCoordsScreenSpace, _snakeCellSize, Color.RED));
            }
            _headPosBoardIdx = _snakeBodyPartsBoardIdx.First();
            _headPosScreenSpace =SnakeBodyPartsScreenSpace.First().Cell.Position;
            _collisionSystem = CollisionSystem.Instance;

            HeadCollidable = SnakeBodyPartsScreenSpace.First();
            _collisionSystem.AddCollidable(HeadCollidable);

        }
        public Snake()
        {
            _movementSpeed = SNAKE_SPEED_DEFAULT;
        }

        public int GetSize()
        {
            return _currentSize;

        }
        public Size GetSnakePartSize()
        {
            return _snakeCellSize;
        }
        public Direction GetDirection()
        {
            return _headingDirection;
        }

        public Point GetHeadPosBoardSpace()
        {
            return _headPosBoardIdx;
        }

        public void SetDirection(Direction direction)
        {
            _headingDirection = direction;
        }

        public void SetSize(int size)
        {
            _currentSize = size;
        }


        public void IncreaseSize(int size)
        {
            _currentSize += size;

            for (int i = 0; i < size; i++)
            {
                Point lastCellPos = _snakeBodyPartsBoardIdx.Last();
                Point secondLastCellPos = _snakeBodyPartsBoardIdx[_snakeBodyPartsBoardIdx.Count() - 2];

                Direction direction;

                if (lastCellPos.X == secondLastCellPos.X)
                {
                    if (Math.Abs(lastCellPos.Y - secondLastCellPos.Y) < 1)
                    {
                        if (lastCellPos.Y > secondLastCellPos.Y)
                        {
                            direction = Direction.DOWN;
                        }
                        else
                        {
                            direction = Direction.UP;
                        }
                    }
                    else
                    {
                        if (lastCellPos.Y > secondLastCellPos.Y)
                        {
                            direction = Direction.UP;
                        }
                        else
                        {
                            direction = Direction.DOWN;
                        }
                    }
                }
                else
                {
                    if(Math.Abs(lastCellPos.X - secondLastCellPos.X) < 1)
                    {
                        if (lastCellPos.X > secondLastCellPos.X)
                        {
                            direction = Direction.RIGHT;
                        }
                        else
                        {
                            direction = Direction.LEFT;
                        }
                    }
                    else
                    {
                        if (lastCellPos.X > secondLastCellPos.X)
                        {
                            direction = Direction.LEFT;
                        }
                        else
                        {
                            direction = Direction.RIGHT;
                        }
                    }

                  
                }
                Point newSnakePartLocation = new Point(lastCellPos.X, lastCellPos.Y);
                switch (direction)
                {
                    case Direction.DOWN:

                        if (lastCellPos.Y + 1 > _board.Size.Height)
                        {
                            newSnakePartLocation.Y = 0;
                        }
                        else
                        {
                            newSnakePartLocation.Y = lastCellPos.Y + 1;
                        }
                        _snakeBodyPartsBoardIdx.Add(newSnakePartLocation);
                        SnakeBodyPartsScreenSpace.Add(_collidableFactory.Create<SnakePart>(_board.BoardCells[newSnakePartLocation.X, newSnakePartLocation.Y].Position, _snakeCellSize, Color.RED));
                        break;
                    case Direction.UP:
                        if (lastCellPos.Y - 1 < 0)
                        {
                            newSnakePartLocation.Y = _board.Size.Height - 1;
                        }
                        else
                        {
                            newSnakePartLocation.Y = lastCellPos.Y - 1;
                        }
                        _snakeBodyPartsBoardIdx.Add(newSnakePartLocation);

                        SnakeBodyPartsScreenSpace.Add(_collidableFactory.Create<SnakePart>(_board.BoardCells[newSnakePartLocation.X, newSnakePartLocation.Y].Position, _snakeCellSize, Color.RED));
                        break;
                    case Direction.LEFT:
                        if (lastCellPos.X - 1 < 0)
                        {
                            newSnakePartLocation.X = _board.Size.Width - 1;
                        }
                        else
                        {
                            newSnakePartLocation.X = lastCellPos.X - 1;
                        }
                        _snakeBodyPartsBoardIdx.Add(newSnakePartLocation);
                        SnakeBodyPartsScreenSpace.Add(_collidableFactory.Create<SnakePart>(_board.BoardCells[newSnakePartLocation.X, newSnakePartLocation.Y].Position, _snakeCellSize, Color.RED));
                        break;
                    case Direction.RIGHT:
                        if (lastCellPos.X + 1 > _board.Size.Width)
                        {
                            newSnakePartLocation.X = 0;
                        }
                        else
                        {
                            newSnakePartLocation.X = lastCellPos.X + 1;
                        }
                        _snakeBodyPartsBoardIdx.Add(newSnakePartLocation);
                        SnakeBodyPartsScreenSpace.Add(_collidableFactory.Create<SnakePart>(_board.BoardCells[newSnakePartLocation.X, newSnakePartLocation.Y].Position, _snakeCellSize, Color.RED));
                        break;


                }

            }

        }
        public void DecreaseSize(int size)
        {
            _currentSize -= size;
            for (int i = 0; i < size; i++)
            {
                _snakeBodyPartsBoardIdx.RemoveAt(_snakeBodyPartsBoardIdx.Count - 1);
                SnakeBodyPartsScreenSpace.RemoveAt(SnakeBodyPartsScreenSpace.Count - 1);
            }
        }

        private void MoveBoardIdx()
        {
            Point oldCellPos = new Point(_headPosBoardIdx);
            Point auxCellPos = new Point(0, 0);

            switch (_headingDirection)
            {
                case Direction.UP:
                    if (_headPosBoardIdx.Y - 1 >= 0)
                        _headPosBoardIdx.Y -= 1;
                    else
                    {
                        _headPosBoardIdx.Y = _board.Size.Height - 1;
                    }
                    break;
                case Direction.DOWN:
                    if (_headPosBoardIdx.Y + 1 < _board.Size.Height)
                        _headPosBoardIdx.Y += 1;
                    else
                    {
                        _headPosBoardIdx.Y = 0;
                    }
                    break;
                case Direction.LEFT:
                    if (_headPosBoardIdx.X - 1 >= 0)
                        _headPosBoardIdx.X -= 1;
                    else
                    {
                        _headPosBoardIdx.X = _board.Size.Width - 1;
                    }
                    break;
                case Direction.RIGHT:
                    if (_headPosBoardIdx.X + 1 < _board.Size.Width)
                        _headPosBoardIdx.X += 1;
                    else
                    {
                        _headPosBoardIdx.X = 0;
                    }
                    break;
            }
            for (int i = 1; i < _snakeBodyPartsBoardIdx.Count; i++)
            {
                auxCellPos = _snakeBodyPartsBoardIdx[i];
                _snakeBodyPartsBoardIdx[i] = oldCellPos;
                oldCellPos = auxCellPos;
            }
        }

        private void MoveScreenSpace()
        {
            int idx = 0;
            foreach (var boardIdx in _snakeBodyPartsBoardIdx)
            {
                SnakeBodyPartsScreenSpace[idx].Cell.Position = _board.BoardCells[boardIdx.X, boardIdx.Y].Position;
                idx++;
            }
            HeadCollidable.Cell.Position = SnakeBodyPartsScreenSpace.First().Cell.Position;
        }

        public void Move()
        {
            MoveBoardIdx();
            MoveScreenSpace();
        }

    }
}
