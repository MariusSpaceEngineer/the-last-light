using Game_development_project.Classes.Menu;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_development_project.Classes.GameStates
{
    internal class MenuState : State
    {
        private List<Component> _components;
        private Texture2D backgroundImage;

        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Menu/Button/Button_style");
            var buttonFont = _content.Load<SpriteFont>("Menu/Button/Button_Font");

            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(500, 200),
                Text = "New Game",
            };

            newGameButton.Click += NewGameButton_Click;

            var level1GameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(500, 250),
                Text = "Load First Level",
            };

            level1GameButton.Click += LoadLevel1GameButton_Click;

            var level2GameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(500, 300),
                Text = "Load Second Level",
            };

            level2GameButton.Click += LoadLevel2GameButton_Click;

            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(500, 350),
                Text = "Quit Game",
            };

            quitGameButton.Click += QuitGameButton_Click;

            _components = new List<Component>()
            {
             newGameButton,
             level1GameButton,
             level2GameButton,
             quitGameButton,
            };

            this.backgroundImage = _content.Load<Texture2D>("Background/mainMenu");
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundImage, new Vector2(0, 0), Color.White);
            spriteBatch.End();

            spriteBatch.Begin();

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        private void LoadLevel1GameButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Load Level1");
            _game.ChangeState(new Level1GameState(_game, _graphicsDevice, _content));

        }

        private void LoadLevel2GameButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Load Level2");
            _game.ChangeState(new Level2GameState(_game, _graphicsDevice, _content));

        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Load new game");
            _game.ChangeState(new Level1GameState(_game, _graphicsDevice, _content));
        }

        public override void PostUpdate(GameTime gameTime)
        {
            // remove sprites if they're not needed
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }

        public override void LoadContent()
        {
            
        }
    }
}

