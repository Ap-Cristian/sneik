using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Difficulties;
using Logic.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using sneik.Controls;

namespace sneik.States
{
    public class SelectDifficultyState : State
    {
        private string _path = @"../../../../Logic/Helpers/Difficulty.txt";
        private List<Component> components;
        private SpriteFont font;
        private Texture2D backGroundTexture;
        public SelectDifficultyState(sneik game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<SpriteFont>("ButtonFonts/Font");

            var easyButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(100, 200),
                Text = "Easy",
            };

            var mediumButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(100, 250),
                Text = "Medium",
            };

            var hardButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(100, 300),
                Text = "Hard",
            };

            var veryHardButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(100, 350),
                Text = "Very Hard",
            };

            var nightmareButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(100, 400),
                Text = "Nightmare",
            };

            var backButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(100, 450),
                Text = "Back",
            };


            easyButton.Click += Button_Easy_Click;
            mediumButton.Click += Button_Medium_Click;
            hardButton.Click += Button_Hard_Click;
            veryHardButton.Click += Button_VeryHard_Click;
            nightmareButton.Click += Button_Nightmare_Click;
            backButton.Click += Button_Back_Click;

            components = new List<Component>
            {
                easyButton,
                mediumButton,
                hardButton,
                veryHardButton,
                nightmareButton,
                backButton,
            };

        }

        private void Button_Back_Click(object sender, EventArgs e)
        {

            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }

        private void Button_Nightmare_Click(object sender, EventArgs e)
        {
            File.WriteAllText(_path, "Nightmare");
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }

        private void Button_VeryHard_Click(object sender, EventArgs e)
        {
            File.WriteAllText(_path, "VeryHard");
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }

        private void Button_Hard_Click(object sender, EventArgs e)
        {
            File.WriteAllText(_path, "Hard");
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }

        private void Button_Medium_Click(object sender, EventArgs e)
        {
            File.WriteAllText(_path, "Medium");
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }

        private void Button_Easy_Click(object sender, EventArgs e)
        {
            File.WriteAllText(_path, "Easy");

            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }


        public override void LoadContent()
        {
            font = _content.Load<SpriteFont>("ButtonFonts/Font");
            backGroundTexture = _content.Load<Texture2D>("Background/highscoreBackground");
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack);
            spriteBatch.Draw(backGroundTexture, new Vector2(0, 0), Color.White);

            foreach (var component in components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Button_Back_Click(this, new EventArgs());

            foreach (var component in components)
                component.Update(gameTime);
        }
    }
}
