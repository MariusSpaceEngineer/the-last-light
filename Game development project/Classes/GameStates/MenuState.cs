using Game_development_project.Classes.Menu;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game_development_project.Classes.Characters;

namespace Game_development_project.Classes.GameStates
{
    internal class MenuState : State
    {
        private Game1 game;

        private List<Component> buttonList;
        private Texture2D buttonTexture;
        private SpriteFont buttonFont;
        private Texture2D backgroundImage;

        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        { 
            LoadContent(content);
            InitializeContent();

            this.game = game;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundImage, new Vector2(0, 0), Color.White);
            spriteBatch.End();

            spriteBatch.Begin();

            foreach (var button in buttonList)
                button.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        private void LoadLevel1GameButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Load Level1");
            base.game.ChangeState(new Level1GameState(base.game, graphicsDevice, content));
            Hero.GetHero().CurrentLevel = Level1GameState.level;
        }

        private void LoadLevel2GameButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Load Level2");
            base.game.ChangeState(new Level2GameState(base.game, graphicsDevice, content));
            Hero.GetHero().CurrentLevel = Level2GameState.level;

        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Load new game");
            base.game.ChangeState(new Level1GameState(base.game, graphicsDevice, content));
            Hero.GetHero().CurrentLevel = Level1GameState.level;

        }


        public override void Update(GameTime gameTime)
        {
            foreach (var button in buttonList)
                button.Update(gameTime);
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            base.game.Exit();
        }

        public override void LoadContent(ContentManager content)
        {
            buttonTexture = content.Load<Texture2D>("Menu/Button/Button_style");
            buttonFont = content.Load<SpriteFont>("Menu/Button/Button_Font");
            backgroundImage = base.content.Load<Texture2D>("Background/mainMenu");
        }

        public override void InitializeContent()
        {
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

            buttonList = new List<Component>()
            {
             newGameButton,
             level1GameButton,
             level2GameButton,
             quitGameButton,
            };
        }
    }
}

