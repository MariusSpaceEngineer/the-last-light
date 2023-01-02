using Game_development_project.Classes.Menu.Components;
using Game_development_project.Classes.Sprites.MovableSprites.Characters.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_development_project.Classes.GameStates
{
    internal class MainMenuState : MenuState
    {
        public MainMenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {

        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            backgroundImage = content.Load<Texture2D>("Background/mainMenu");
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Load new game");
            base.game.ChangeState(new Level1GameState(base.game, graphicsDevice, content));
            Hero.GetHero().CurrentLevel = Level1GameState.level;

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

            buttonList = new List<MenuComponent>()
            {
             newGameButton,
             level1GameButton,
             level2GameButton,
             quitGameButton,
            };
        }
    }
}
