using Logic.Models;
using sneikTools;
using Microsoft.Xna.Framework.Graphics;
using System;
using Logic.Factories;
using Logic.Interfaces;
using sneik.Interfaces;
using System.Net.Mime;
using Microsoft.Xna.Framework.Content;

namespace sneik
{
    public class SneikRenderer : IUpdateable
    {
        public SneikGameUseCase _sneikGame;
        private SpriteBatch _spriteBatch;
        private GraphicsDevice _graphicsDevice;
        private IUseCaseFactory _useCaseFactory;
        private Texture2D _gameBoardCellTexture;
        private Texture2D _gameBoardObstacleTexture;
        private Texture2D _gameBoardFoodPalletTexture;
        private Texture2D _sneikTexture;
        private SpriteFont _scoreFont;
        public bool stopGame = false;
        private void initTextures()
        {

            
            Size CellSize = _sneikGame.round.Board.BoardCells[0, 0].Size;

            Color gameBoardCellColor = _sneikGame.round.Board.BoardCells[0, 0].Color;
            Color gameBoardObstacleColor = Color.TEA_GREEN;
            Color sneikColor = Color.PINK;
            Color gameBoardFoodPallet = Color.YELLOW;

            for (int i = 0; i < _sneikGame.round.Board.BoardObstacles.GetLength(0); i++)
            {
                for (int j = 0; j < _sneikGame.round.Board.BoardObstacles.GetLength(1); j++)
                {
                    if (_sneikGame.round.Board.BoardObstacles[i, j] != null)
                    {
                        gameBoardObstacleColor = _sneikGame.round.Board.BoardObstacles[i, j].Cell.Color;
                    }
                }
            }
            for (int i = 0; i < _sneikGame.round.Board.BoardFood.GetLength(0); i++)
            {
                for (int j = 0; j < _sneikGame.round.Board.BoardFood.GetLength(1); j++)
                {
                    if (_sneikGame.round.Board.BoardFood[i, j] != null)
                    {
                        gameBoardFoodPallet = _sneikGame.round.Board.BoardFood[i, j].Cell.Color;
                    }
                }
            }


            _gameBoardCellTexture = new Texture2D(_graphicsDevice, CellSize.Width, CellSize.Height);
            _gameBoardObstacleTexture = new Texture2D(_graphicsDevice, CellSize.Width, CellSize.Height);
            _gameBoardFoodPalletTexture = new Texture2D(_graphicsDevice, CellSize.Width, CellSize.Height);
            _sneikTexture = new Texture2D(_graphicsDevice, CellSize.Width, CellSize.Height);

            Microsoft.Xna.Framework.Color[] cellTextureColor = new Microsoft.Xna.Framework.Color[CellSize.Width * CellSize.Height];
            Microsoft.Xna.Framework.Color[] obstacleTextureColor = new Microsoft.Xna.Framework.Color[CellSize.Width * CellSize.Height];
            Microsoft.Xna.Framework.Color[] sneikTextureColor = new Microsoft.Xna.Framework.Color[CellSize.Width * CellSize.Height];
            Microsoft.Xna.Framework.Color[] foodPalletTextureColor = new Microsoft.Xna.Framework.Color[CellSize.Width * CellSize.Height];

            for (int i = 0; i < cellTextureColor.Length; i++)
            {
                cellTextureColor[i] = Tools.ModelsColorToFrameworkColor(gameBoardCellColor);
            }
            _gameBoardCellTexture.SetData(cellTextureColor);

            for (int i = 0; i < obstacleTextureColor.Length; i++)
            {
                obstacleTextureColor[i] = Tools.ModelsColorToFrameworkColor(gameBoardObstacleColor);
            }
            _gameBoardObstacleTexture.SetData(obstacleTextureColor);

            for (int i = 0; i < sneikTextureColor.Length; i++)
            {
                sneikTextureColor[i] = Tools.ModelsColorToFrameworkColor(sneikColor);
            }
            _sneikTexture.SetData(sneikTextureColor);

            for(int i = 0; i < foodPalletTextureColor.Length; i++)
            {
                foodPalletTextureColor[i] = Tools.ModelsColorToFrameworkColor(gameBoardFoodPallet);
            }
            _gameBoardFoodPalletTexture.SetData(foodPalletTextureColor);

        }
        private void onGameUpdate(object sender, EventArgs args)
        {
            //wanted to use this to call draw() (see comment in draw)
            //currently unused, maybe usefull down the line
            //could be used for input handling?
        }
        public SneikRenderer(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, IUseCaseFactory useCaseFactory, ContentManager _content)
        {
            _useCaseFactory = useCaseFactory;
            _spriteBatch = spriteBatch;
            _sneikGame = _useCaseFactory.Create<SneikGameUseCase>() as SneikGameUseCase;
            _sneikGame.Execute();
            _sneikGame.DrawUpdateDelegate += onGameUpdate;
            _graphicsDevice = graphicsDevice;
            _scoreFont = _content.Load<SpriteFont>("Fonts/ScoreFont");
            initTextures();
        }

        public void StopGame()
        {
            _sneikGame.DrawUpdateDelegate -= onGameUpdate;
            _sneikGame.StopGameLoop();

        }
        public void Update()
        {
            if (_sneikGame.round != null)
            {
                //apparently monogame doesnt like when you dont draw from the UI thread(draw from sneik.cs), which is understanable.
                _spriteBatch.Begin();


                int width = _graphicsDevice.Viewport.Width;
                int height = _graphicsDevice.Viewport.Height;
                Point point;

                int paddingWidth = width / 4 - (_sneikGame.round.Board.Size.Width * _sneikGame.round.Board.CellSize.Width) / 4;
                int paddingHeight = height / 4 - (_sneikGame.round.Board.Size.Height * _sneikGame.round.Board.CellSize.Height) / 4;

                point = new Point(paddingWidth, paddingHeight);


                foreach (var boardCell in _sneikGame.round.Board.BoardCells)
                {
                    _spriteBatch.Draw(_gameBoardCellTexture, Tools.PointToVector2(boardCell.Position + point), Tools.ModelsColorToFrameworkColor(boardCell.Color)); //draw board
                }

                foreach (var boardObstacle in _sneikGame.round.Board.BoardObstacles)
                {
                    if (boardObstacle != null)
                    {
                        _spriteBatch.Draw(_gameBoardObstacleTexture, Tools.PointToVector2(boardObstacle.Cell.Position + point), Tools.ModelsColorToFrameworkColor(boardObstacle.Cell.Color)); //draw obstacles
                    }
                }
                foreach (var boardFood in _sneikGame.round.Board.BoardFood)
                {
                    if (boardFood != null)
                    {
                        _spriteBatch.Draw(_gameBoardFoodPalletTexture, Tools.PointToVector2(boardFood.Cell.Position + point), Tools.ModelsColorToFrameworkColor(boardFood.Cell.Color)); //draw food
                    }
                }

                //this is where the snek should be drawn
                foreach (var SnakeCell in _sneikGame.round.Snake.SnakeBodyPartsScreenSpace)
                {
                    if (SnakeCell != null)
                    {
                        _spriteBatch.Draw(_gameBoardObstacleTexture, Tools.PointToVector2(SnakeCell.Cell.Position + point), Tools.ModelsColorToFrameworkColor(SnakeCell.Cell.Color)); //draw snake
                    }
                }

                _spriteBatch.DrawString(_scoreFont, "Score " + _sneikGame.round.GetScore(), Tools.PointToVector2(new Point(10, 10)), Tools.ModelsColorToFrameworkColor(Color.GREEN));
                _spriteBatch.End();
            }
            
        }
    }
}
