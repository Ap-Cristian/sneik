﻿
using Logic.Systems;
using System.Collections.Generic;
using System.Linq;

namespace Logic.Models
{
    public class Snake
    {
        const int SNAKE_SPEED_DEFAULT = 2;

        private int _currentSize;
        private int _movementSpeed;

        private GameBoard _board;
        private List<Point> _snakeBodyPartsBoardIdx;
        public List<SnakePart> SnakeBodyPartsScreenSpace { get; set; }

        private int _boardHeight;
        private int _boardWidth;
        private Point _headPosBoardIdx;
        private readonly Point _headPosScreenSpace;

        private Size _snakeCellSize = new Size(2, 2);
        private Direction _headingDirection = Direction.DOWN;
        private CollisionSystem _collisionSystem;

        public SnakePart HeadCollidable { get; set; }

        public Snake(int size, GameBoard gameBoard, int movementSpeed = SNAKE_SPEED_DEFAULT)
        {
            this._currentSize = size;


            this._movementSpeed = movementSpeed;
            this._board = gameBoard;

            _boardHeight = gameBoard.Size.Height;
            _boardWidth = gameBoard.Size.Width;

            this._snakeBodyPartsBoardIdx = new List<Point>();
            this.SnakeBodyPartsScreenSpace = new List<SnakePart>();

            for (int i = 0; i < _currentSize; i++)
            {
                Point cellCoordsBoardIdx = new Point(0, size - i);
                Point cellCoordsScreenSpace = new Point(cellCoordsBoardIdx);

                this._snakeBodyPartsBoardIdx.Add(cellCoordsBoardIdx);
                this.SnakeBodyPartsScreenSpace.Add(new SnakePart(cellCoordsScreenSpace, _snakeCellSize));
            }
            this._headPosBoardIdx = this._snakeBodyPartsBoardIdx.First();
            this._headPosScreenSpace = this.SnakeBodyPartsScreenSpace.First().Cell.Position;
            this._collisionSystem = CollisionSystem.Instance;

            this.HeadCollidable = this.SnakeBodyPartsScreenSpace.First();
            this._collisionSystem.AddCollidable(this.HeadCollidable);

        }


        public Snake(int size, int boardWidth, int boardHeight, int movementSpeed = SNAKE_SPEED_DEFAULT)
        {
            this._currentSize = size;

            this._movementSpeed = movementSpeed;

            this._snakeBodyPartsBoardIdx = new List<Point>();
            this.SnakeBodyPartsScreenSpace = new List<SnakePart>();

            _boardHeight = boardHeight;
            _boardWidth = boardWidth;

            for (int i = 0; i < _currentSize; i++)
            {
                Point cellCoordsBoardIdx = new Point(0, size - i);
                Point cellCoordsScreenSpace = new Point(this._board.BoardCells[cellCoordsBoardIdx.X, cellCoordsBoardIdx.Y].Position);

                this._snakeBodyPartsBoardIdx.Add(cellCoordsBoardIdx);
                this.SnakeBodyPartsScreenSpace.Add(new SnakePart(cellCoordsScreenSpace, _snakeCellSize, Color.RED));
            }
            this._headPosBoardIdx = this._snakeBodyPartsBoardIdx.First();
            this._headPosScreenSpace = this.SnakeBodyPartsScreenSpace.First().Cell.Position;
            this._collisionSystem = CollisionSystem.Instance;

            this.HeadCollidable = this.SnakeBodyPartsScreenSpace.First();
            this._collisionSystem.AddCollidable(this.HeadCollidable);

        }


        public Snake()
        {
            this._movementSpeed = SNAKE_SPEED_DEFAULT;
        }

        public int GetSize()
        {
            return this._currentSize;

        }
        public List<SnakePart> SnakeBodyPartsBoardIdx()
        {
            return this.SnakeBodyPartsScreenSpace;
        }
        public Direction GetDirection()
        {
            return this._headingDirection;
        }

        public Point GetHeadPosBoardSpace()
        {
            return _headPosBoardIdx;
        }

        public void SetDirection(Direction direction)
        {
            this._headingDirection = direction;
        }

        public void SetSize(int size)
        {
            this._currentSize = size;
        }

        public void IncreaseSize(int size)
        {
            _currentSize += size;

            for (int i = 0; i < size; i++)
            {
                Point lastCellPos = _snakeBodyPartsBoardIdx.Last();
                Point secondLastCellPos = _snakeBodyPartsBoardIdx[_snakeBodyPartsBoardIdx.Count - 2];

                Direction direction;

                if (lastCellPos.X == secondLastCellPos.X)
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
                    if (lastCellPos.X > secondLastCellPos.X)
                    {
                        direction = Direction.RIGHT;
                    }
                    else
                    {
                        direction = Direction.LEFT;
                    }
                }

                switch(direction)
                    {
                    case Direction.DOWN:
                        _snakeBodyPartsBoardIdx.Add(new Point(lastCellPos.X, lastCellPos.Y + 1));
                        SnakeBodyPartsScreenSpace.Add(new SnakePart(_board.BoardCells[lastCellPos.X, lastCellPos.Y + 1].Position, _snakeCellSize));
                        break;
                    case Direction.UP:
                        _snakeBodyPartsBoardIdx.Add(new Point(lastCellPos.X, lastCellPos.Y - 1));
                        SnakeBodyPartsScreenSpace.Add(new SnakePart(_board.BoardCells[lastCellPos.X, lastCellPos.Y - 1].Position, _snakeCellSize));
                        break;
                    case Direction.LEFT:
                        _snakeBodyPartsBoardIdx.Add(new Point(lastCellPos.X - 1, lastCellPos.Y));
                        SnakeBodyPartsScreenSpace.Add(new SnakePart(_board.BoardCells[lastCellPos.X - 1, lastCellPos.Y].Position, _snakeCellSize));
                        break;
                    case Direction.RIGHT:
                        _snakeBodyPartsBoardIdx.Add(new Point(lastCellPos.X + 1, lastCellPos.Y));
                        SnakeBodyPartsScreenSpace.Add(new SnakePart(_board.BoardCells[lastCellPos.X + 1, lastCellPos.Y].Position, _snakeCellSize));
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
            Point oldCellPos = new Point(this._headPosBoardIdx);
            Point auxCellPos = new Point(0, 0);

            switch (this._headingDirection)
            {
                case Direction.UP:
                    if (this._headPosBoardIdx.Y - 1 >= 0)
                        this._headPosBoardIdx.Y -= 1;
                    else
                    {
                        this._headPosBoardIdx.Y = _boardHeight - 1;
                    }
                    break;
                case Direction.DOWN:
                    if (this._headPosBoardIdx.Y + 1 < _boardHeight)
                        this._headPosBoardIdx.Y += 1;
                    else
                    {
                        this._headPosBoardIdx.Y = 0;
                    }
                    break;
                case Direction.LEFT:
                    if (this._headPosBoardIdx.X - 1 >= 0)
                        this._headPosBoardIdx.X -= 1;
                    else
                    {
                        this._headPosBoardIdx.X = _boardWidth - 1;
                    }
                    break;
                case Direction.RIGHT:
                    if (this._headPosBoardIdx.X + 1 < _boardWidth)
                        this._headPosBoardIdx.X += 1;
                    else
                    {
                        this._headPosBoardIdx.X = 0;
                    }
                    break;
            }
            for (int i = 1; i < _snakeBodyPartsBoardIdx.Count; i++)
            {
                auxCellPos = this._snakeBodyPartsBoardIdx[i];
                _snakeBodyPartsBoardIdx[i] = oldCellPos;
                oldCellPos = auxCellPos;
            }
        }

        //private void MoveScreenSpace()
        //{
        //    int idx = 0;
        //    foreach (var boardIdx in _snakeBodyPartsBoardIdx)
        //    {
        //        this.SnakeBodyPartsScreenSpace[idx].Cell.Position = this._board.BoardCells[boardIdx.X, boardIdx.Y].Position;
        //        idx++;
        //    }
        //    this.HeadCollidable.Cell.Position =SnakeBodyPartsScreenSpace.First().Cell.Position;
        //}

        public void Move()
        {
            MoveBoardIdx();
            //MoveScreenSpace();
        }

    }
}
