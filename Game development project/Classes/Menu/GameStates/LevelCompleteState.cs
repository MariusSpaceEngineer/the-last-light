using Game_development_project.Classes.Menu.Components;
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
        private Texture2D levelCompletedText;



        public LevelCompleteState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            Debug.WriteLine(game._currentState);

        }

        public override void InitializeContent()
        {
            buttonList = new List<MenuComponent>();
            if (game._currentState is Level1GameState)
            {
                var nextLevelButton = new Button(buttonTexture, buttonFont)
                {
                    Position = new Vector2(500, 200),
                    Text = "Next Level",
                };

                nextLevelButton.Click += NextLevelButton_Click;
                
                buttonList.Add(nextLevelButton);
            }

            var mainMenuButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(500, 250),
                Text = "Back to main menu",
            };

            buttonList.Add(mainMenuButton);

            mainMenuButton.Click += MainMenuGameButton_Click;

            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(500, 300),
                Text = "Quit Game",
            };

            quitGameButton.Click += QuitGameButton_Click;

            buttonList.Add(quitGameButton);

        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            if (game._currentState is Level1GameState)
            {
                levelCompletedText = content.Load<Texture2D>("Textures/Menu/Level1CompletedText");
                backgroundImage = content.Load<Texture2D>("Textures/Backgrounds/Level1CompleteBackground");

            }
            else if (game._currentState is Level2GameState)
            {
                levelCompletedText = content.Load<Texture2D>("Textures/Menu/Level2CompletedText");
                backgroundImage = content.Load<Texture2D>("Textures/Backgrounds/Level2CompleteBackground");
            }
    }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            spriteBatch.Begin();
            if (game._previousState is Level1GameState)
            {
                //Debug.WriteLine("Using level 1 text");
                spriteBatch.Draw(levelCompletedText, new Vector2(100, 100), Color.White);
            
            }
            else if (game._previousState is Level2GameState)
            {
                //Debug.WriteLine("Using level 2 text");
                spriteBatch.Draw(levelCompletedText, new Vector2(250, 150), Color.White);
            }

            spriteBatch.End();
        }

        private void NextLevelButton_Click(object sender, EventArgs e)
        {
                Debug.WriteLine("Change to level 2");
                
                game.ChangeState(new Level2GameState(game, graphicsDevice, content));
                Hero.GetHero().CurrentLevel = Level2GameState.level;

        }

        private void MainMenuGameButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Load Main Menu");
            game.ChangeState(new MainMenuState(game, graphicsDevice, content));

        }
    }
}
