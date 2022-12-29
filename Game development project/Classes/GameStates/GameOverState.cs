﻿using Game_development_project.Classes.Characters;
using Game_development_project.Classes.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_development_project.Classes.GameStates
{
    internal class GameOverState : State
    {
        private List<Component> _components;
        private Texture2D gameOverText;
        private Texture2D backgroundImage;


        public GameOverState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
        

            var buttonTexture = _content.Load<Texture2D>("Menu/Button/Button_style");
            var buttonFont = _content.Load<SpriteFont>("Menu/Button/Button_Font");

            var reloadLevelButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(500, 200),
                Text = "Reload level",
            };

            reloadLevelButton.Click += ReloadLevelButton_Click;

            var mainMenuButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(500, 250),
                Text = "Back to main menu",
            };

            mainMenuButton.Click += MainMenuGameButton_Click;

            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(500, 300),
                Text = "Quit Game",
            };

            quitGameButton.Click += QuitGameButton_Click;

            _components = new List<Component>()
            {
             reloadLevelButton,
             mainMenuButton,
             quitGameButton,
            };

            this.gameOverText = _content.Load<Texture2D>("Menu/GameOver_Text");
            this.backgroundImage = _content.Load<Texture2D>("Background/gameOver");

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundImage, new Vector2(0, 0), Color.White);
            spriteBatch.End();

            spriteBatch.Begin();
            spriteBatch.Draw(gameOverText, new Vector2(450, 50), Color.White);
            spriteBatch.End();

            spriteBatch.Begin();

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        private void ReloadLevelButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(_game._previousState);
          

            if (_game._previousState is Level1GameState)
            {
                Debug.WriteLine("Change to level 1");  
                _game.ChangeState(new Level1GameState(_game, _graphicsDevice, _content));
            }
            else if (_game._previousState is Level2GameState)
            {
                Debug.WriteLine("Change to level 2");
                _game.ChangeState(new Level2GameState(_game, _graphicsDevice, _content));

            }

        }
        
        private void MainMenuGameButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Load Main Menu");
            _game.ChangeState(new MenuState(_game,_graphicsDevice,_content));

        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
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

        public override void LoadContent()
        {
            
        }
    }
}