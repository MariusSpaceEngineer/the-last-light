using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game_development_project.Classes.Characters;
using Game_development_project.Classes.Menu.Components;

namespace Game_development_project.Classes.GameStates
{
    internal class MenuState : GameState
    {
        protected List<MenuComponent> buttonList;
        protected Texture2D buttonTexture;
        protected SpriteFont buttonFont;
        protected Texture2D backgroundImage;

        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        { 
            LoadContent(content);
            InitializeContent();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundImage, new Vector2(0, 0), Color.White);
            spriteBatch.End();

            spriteBatch.Begin();

            if (buttonList != null)
            {
                foreach (var button in buttonList)
                    button.Draw(gameTime, spriteBatch);
            }
           
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            if (buttonList != null)
            {
                foreach (var button in buttonList)
                    button.Update(gameTime);
            }
            
        }

        protected void QuitGameButton_Click(object sender, EventArgs e)
        {
            game.Exit();
        }

        public override void LoadContent(ContentManager content)
        {
            buttonTexture = content.Load<Texture2D>("Textures/Menu/Button");
            buttonFont = content.Load<SpriteFont>("Fonts/ButtonFont");
            backgroundImage = content.Load<Texture2D>("Textures/Backgrounds/MainMenuBackground");
        }

       
    }
}

