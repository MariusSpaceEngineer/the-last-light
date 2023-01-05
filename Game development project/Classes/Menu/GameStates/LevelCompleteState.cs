﻿using Game_development_project.Classes.Menu.Components;
using Game_development_project.Classes.Sprites.MovableSprites.Characters.Player;
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
    internal class LevelCompleteState : MenuState
    {
        //private Texture2D MainText;

        public LevelCompleteState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            Debug.WriteLine(Game._currentState);
        }

        public override void InitializeContent()
        {
            ButtonList = new List<MenuComponent>();
            if (Game._currentState is Level1GameState)
            {
                var nextLevelButton = new Button(ButtonTexture, ButtonFont)
                {
                    Position = new Vector2(500, 200),
                    Text = "Next Level",
                };

                nextLevelButton.Click += NextLevelButton_Click;
                
                ButtonList.Add(nextLevelButton);
            }

            var mainMenuButton = new Button(ButtonTexture, ButtonFont)
            {
                Position = new Vector2(500, 250),
                Text = "Back to main menu",
            };

            ButtonList.Add(mainMenuButton);

            mainMenuButton.Click += MainMenuGameButton_Click;

            var quitGameButton = new Button(ButtonTexture, ButtonFont)
            {
                Position = new Vector2(500, 300),
                Text = "Quit Game",
            };

            quitGameButton.Click += QuitGameButton_Click;

            ButtonList.Add(quitGameButton);

        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            if (Game._currentState is Level1GameState)
            {
                MainText = content.Load<Texture2D>("Textures/Menu/Level1CompletedText");
                BackgroundImage = content.Load<Texture2D>("Textures/Backgrounds/Level1CompleteBackground");

            }
            else if (Game._currentState is Level2GameState)
            {
                MainText = content.Load<Texture2D>("Textures/Menu/Level2CompletedText");
                BackgroundImage = content.Load<Texture2D>("Textures/Backgrounds/Level2CompleteBackground");
            }
    }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            spriteBatch.Begin();
            if (Game._previousState is Level1GameState)
            {
                //Debug.WriteLine("Using level 1 text");
                spriteBatch.Draw(MainText, new Vector2(100, 100), Color.White);
            
            }
            else if (Game._previousState is Level2GameState)
            {
                //Debug.WriteLine("Using level 2 text");
                spriteBatch.Draw(MainText, new Vector2(250, 150), Color.White);
            }

            spriteBatch.End();
        }

        private void NextLevelButton_Click(object sender, EventArgs e)
        {
                Debug.WriteLine("Change to level 2");
                
                Game.ChangeState(new Level2GameState(Game, GraphicsDevice, Content));
                Hero.GetHero().CurrentLevel = Level2GameState.Level;

        }

        private void MainMenuGameButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Load Main Menu");
            Game.ChangeState(new MainMenuState(Game, GraphicsDevice, Content));

        }
    }
}