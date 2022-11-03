using Game_development_project.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game_development_project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Hero hero;
        private Texture2D attackSprite;
        private Texture2D damageSprite;
        private Texture2D deathSprite;
        private Texture2D idleSprite;
        private Texture2D jumpSprite;
        private Texture2D jumpFallInBetween;
        private Texture2D moveSprite;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            hero = new Hero(attackSprite,damageSprite,deathSprite,idleSprite, jumpSprite, jumpFallInBetween,moveSprite);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            LoadHero();

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            hero.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            hero.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void LoadHero()
        {
            attackSprite = Content.Load<Texture2D>("Sprites/Knight/_Attack");
            damageSprite = Content.Load<Texture2D>("Sprites/Knight/_Hit");
            deathSprite = Content.Load<Texture2D>("Sprites/Knight/_Death");
            idleSprite = Content.Load<Texture2D>("Sprites/Knight/_Idle");
            jumpSprite = Content.Load<Texture2D>("Sprites/Knight/_Jump");
            jumpFallInBetween = Content.Load<Texture2D>("Sprites/Knight/_JumpFallInbetween");
            moveSprite = Content.Load<Texture2D>("Sprites/Knight/_Run");
        }
    }
}