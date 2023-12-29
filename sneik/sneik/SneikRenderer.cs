using Logic.Models;
using sneikTools;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace sneik
{
    public class SneikRenderer
    {
        private SneikGame _sneikGame;
        private SpriteBatch _spriteBatch;
        private GraphicsDevice _graphicsDevice;

        private Texture2D _gameBoardCellTexture;
        private Texture2D _gameBoardObstacleTexture;
        private Texture2D _sneikTexture;

        private bool shouldUpdate = true;

        private void initTextures()
        {
            Size CellSize = _sneikGame.round.Board.BoardCells[0, 0].Size;

            Color gameBoardCellColor = _sneikGame.round.Board.BoardCells[0, 0].Color;
            Color gameBoardObstacleColor = Color.TEA_GREEN;
            Color sneikColor = Color.PINK;

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

            _gameBoardCellTexture = new Texture2D(_graphicsDevice, CellSize.Width, CellSize.Height);
            _gameBoardObstacleTexture = new Texture2D(_graphicsDevice, CellSize.Width, CellSize.Height);
            _sneikTexture = new Texture2D(_graphicsDevice, CellSize.Width, CellSize.Height);

            Microsoft.Xna.Framework.Color[] cellTextureColor = new Microsoft.Xna.Framework.Color[CellSize.Width * CellSize.Height];
            Microsoft.Xna.Framework.Color[] obstacleTextureColor = new Microsoft.Xna.Framework.Color[CellSize.Width * CellSize.Height];
            Microsoft.Xna.Framework.Color[] sneikTextureColor = new Microsoft.Xna.Framework.Color[CellSize.Width * CellSize.Height];

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

        }
        private void onGameUpdate(object sender, EventArgs args)
        {
            //wanted to use this to call draw() (see comment in draw)
            //currently unused, maybe usefull down the line
            //could be used for input handling?
        }
        public SneikRenderer(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            _spriteBatch = spriteBatch;
            _sneikGame = SneikGame.Instance;
            _sneikGame.DrawUpdateDelegate += onGameUpdate;
            _graphicsDevice = graphicsDevice;

            initTextures();
        }

        public void draw()
        {
            //apparently monogame doesnt like when you dont draw from the UI thread(draw from sneik.cs), which is understanable.
            _spriteBatch.Begin();

            foreach (var boardCell in _sneikGame.round.Board.BoardCells)
            {
                _spriteBatch.Draw(_gameBoardCellTexture, Tools.PointToVector2(boardCell.Position), Tools.ModelsColorToFrameworkColor(boardCell.Color)); //draw board
            }

            foreach (var boardObstacle in _sneikGame.round.Board.BoardObstacles)
            {
                if (boardObstacle != null)
                {
                    _spriteBatch.Draw(_gameBoardObstacleTexture, Tools.PointToVector2(boardObstacle.Cell.Position), Tools.ModelsColorToFrameworkColor(boardObstacle.Cell.Color)); //draw obstacles
                }
            }

            //this is where the snek should be drawn
            foreach (var SnakeCell in _sneikGame.round.Snake.SnakeBodyPartsScreenSpace)
            {
                if (SnakeCell != null)
                {
                    _spriteBatch.Draw(_gameBoardObstacleTexture, Tools.PointToVector2(SnakeCell.Collider.Position), Tools.ModelsColorToFrameworkColor(Color.RED)); //draw snake
                }
            }
            _spriteBatch.End();
        }
    }
}
