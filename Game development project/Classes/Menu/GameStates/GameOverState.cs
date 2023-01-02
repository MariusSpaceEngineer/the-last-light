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
        public override void InitializeContent()
        {

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

            buttonList = new List<MenuComponent>()
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
            backgroundImage = content.Load<Texture2D>("Textures/Backgrounds/GameOverBackground");

        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
            base.Draw(gameTime, spriteBatch);
            spriteBatch.Begin();
            spriteBatch.Draw(gameOverText, new Vector2(450, 50), Color.White);
            spriteBatch.End();

        }
   
        private void ReloadLevelButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(game._previousState);
          

            if (game._previousState is Level1GameState)
            {
                Debug.WriteLine("Change to level 1");  
                game.ChangeState(new Level1GameState(game, graphicsDevice, content));
            }
            else if (game._previousState is Level2GameState)
            {
                Debug.WriteLine("Change to level 2");
                game.ChangeState(new Level2GameState(game, graphicsDevice, content));

            }

        }
        
        private void MainMenuGameButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Load Main Menu");
            game.ChangeState(new MainMenuState(game,graphicsDevice,content));

        }


    }
}
