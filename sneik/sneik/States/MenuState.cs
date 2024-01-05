using System;
using System.Collections.Generic;
using Logic.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using sneik.Controls;

namespace sneik.States
{
    public class MenuState : State
    {
        private List<Component> components;

        private Texture2D menuBackGroundTexture;

        public MenuState(sneik game, GraphicsDevice graphicsDevice, ContentManager content)
      : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("ButtonFonts/Font");

            var startGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(100, 200),
                Text = "Start",
            };

            var highscoresButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(100, 250),
                Text = "Highscores",
            };

            highscoresButton.Click += Button_Highscores_Click;

            startGameButton.Click += Button_StartGame_Click;

            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(100, 300),
                Text = "Quit Game",
            };

            quitGameButton.Click += Button_Quit_Clicked;

            components = new List<Component>()
            {
                startGameButton,
                highscoresButton,
                quitGameButton,
            };
        }

        public override void LoadContent()
        {
            menuBackGroundTexture = _content.Load<Texture2D>("Background/MainMenu");
        }

        private void Button_StartGame_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new SelectDifficultyState(_game, _graphicsDevice, _content));
        }

       

        private void Button_Quit_Clicked(object sender, EventArgs args)
        {
            _game.Exit();
        }

        private void Button_Highscores_Click(object sender, EventArgs args)
        {
            _game.ChangeState(new HighscoresState(_game, _graphicsDevice, _content, new UseCaseFactory()));
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in components)
                component.Update(gameTime);
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(menuBackGroundTexture, new Vector2(0, 0), Color.White);

            foreach (var component in components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
    }
}
