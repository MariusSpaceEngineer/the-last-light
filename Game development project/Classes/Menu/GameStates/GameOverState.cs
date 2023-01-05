using Game_development_project.Classes.Characters;
using Game_development_project.Classes.Menu.Components;
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
    internal class GameOverState : MenuState
    {

        private Texture2D gameOverText;

        public GameOverState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {

        }

        #region Public methods

        public override void InitializeContent()
        {

            var reloadLevelButton = new Button(ButtonTexture, ButtonFont)
            {
                Position = new Vector2(500, 200),
                Text = "Reload level",
            };

            reloadLevelButton.Click += ReloadLevelButton_Click;

            var mainMenuButton = new Button(ButtonTexture, ButtonFont)
            {
                Position = new Vector2(500, 250),
                Text = "Back to main menu",
            };

            mainMenuButton.Click += MainMenuGameButton_Click;

            var quitGameButton = new Button(ButtonTexture, ButtonFont)
            {
                Position = new Vector2(500, 300),
                Text = "Quit Game",
            };

            quitGameButton.Click += QuitGameButton_Click;

            ButtonList = new List<MenuComponent>()
            {
             reloadLevelButton,
             mainMenuButton,
             quitGameButton,
            };

        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            gameOverText = content.Load<Texture2D>("Textures/Menu/GameOverText");
            BackgroundImage = content.Load<Texture2D>("Textures/Backgrounds/GameOverBackground");

        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
            base.Draw(gameTime, spriteBatch);
            spriteBatch.Begin();
            spriteBatch.Draw(gameOverText, new Vector2(450, 50), Color.White);
            spriteBatch.End();

        }

        #endregion

        #region Private methods

        private void ReloadLevelButton_Click(object sender, EventArgs e)
        {
            //Debug.WriteLine(Game._previousState);
          

            if (Game._previousState is Level1GameState)
            {
                //Debug.WriteLine("Change to level 1");  
                Game.ChangeState(new Level1GameState(Game, GraphicsDevice, Content));
            }
            else if (Game._previousState is Level2GameState)
            {
                //Debug.WriteLine("Change to level 2");
                Game.ChangeState(new Level2GameState(Game, GraphicsDevice, Content));

            }

        }
        
        private void MainMenuGameButton_Click(object sender, EventArgs e)
        {
            Game.ChangeState(new MainMenuState(Game,GraphicsDevice,Content));

        }

        #endregion


    }
}
