using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace sneik.States
{
    internal class GameState : State
    {
        private SpriteBatch _spriteBatch;
        private SneikRenderer _sneikRenderer;
        private GraphicsDevice _graphicsDevice;

        public GameState(sneik game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            _graphicsDevice = graphicsDevice;
        }

        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(_graphicsDevice);

            _sneikRenderer = new SneikRenderer(_spriteBatch, _graphicsDevice);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _sneikRenderer.draw();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                _sneikRenderer.StopGame();
                _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
            }

            

        }
    }
}
