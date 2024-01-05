using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using sneik.Controls;
using System;
using System.Collections.Generic;
using Logic.Factories;
using Logic.Models;
using Color = Microsoft.Xna.Framework.Color;

namespace sneik.States
{
    public class HighscoresState : State
    {
        private List<Component> components;
        private IUseCaseFactory  _useCaseFactory;
        private SpriteFont font;
        private Texture2D backGroundTexture;


        public HighscoresState(sneik game, GraphicsDevice graphicsDevice, ContentManager content, IUseCaseFactory useCaseFactory)
          : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("ButtonFonts/Font");
            _useCaseFactory = useCaseFactory;

            var mainMenuButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 300),
                Text = "Back To Main Menu",
            };

            mainMenuButton.Click += Button_MainMenu_Click;

            components = new List<Component>()
            {
                mainMenuButton,
            };
        }

        public override void LoadContent()
        {
            font = _content.Load<SpriteFont>("ButtonFonts/Font");
            backGroundTexture = _content.Load<Texture2D>("Background/highscoreBackground");
        }


        

        private void Button_MainMenu_Click(object sender, EventArgs args)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Button_MainMenu_Click(this, new EventArgs());

            foreach (var component in components)
                component.Update(gameTime);
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            HighscoresUseCase highscoresUseCase = _useCaseFactory.Create<HighscoresUseCase>() as HighscoresUseCase;
            highscoresUseCase.Execute();
            spriteBatch.Begin(SpriteSortMode.FrontToBack);
            spriteBatch.Draw(backGroundTexture, new Vector2(0, 0), Color.White);

            foreach (var component in components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.DrawString(font, "Highscores:\n", new Vector2(400, 100), Color.Black);
            int lineSpace = 30;
            for (int i = 0; i < highscoresUseCase._highscores.Count; i++)
            {


                spriteBatch.DrawString(font, $"{i+1}. " + highscoresUseCase._highscores[i], new Vector2(400, 100 + (i+1) * lineSpace), Color.Black);


            }

            spriteBatch.End();
        }
    }
}
