using Default_Block;
using Game_development_project.Classes;
using Game_development_project.Classes.Characters;
using Game_development_project.Classes.Level_Design;
using Game_development_project.Classes.Level_Design.Level;
using Game_development_project.Classes.Level_Design.Level1;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Formats.Asn1.AsnWriter;

namespace Game_development_project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Hero hero;
        private Texture2D heroAttackSprite;
        private Texture2D heroDamageSprite;
        private Texture2D heroDeathSprite;
        private Texture2D heroIdleSprite;
        private Texture2D heroJumpSprite;
        private Texture2D heroJumpFallInBetween;
        private Texture2D heroMoveSprite;
        private Texture2D heroBlokTexture;

        private Skeleton skeleton;
        private Texture2D skeletonAttackSprite;
        private Texture2D skeletonDamageSprite;
        private Texture2D skeletonDeathSprite;
        private Texture2D skeletonIdleSprite;
        private Texture2D skeletonMoveSprite;

        // Level level1;

        //Map map;
        Level1 level1;
        BlockFactory blockFactory;

        int[,] gameboard = new int[,] {
            { 1,1,1,1,1,1,1,1 },
            { 0,0,1,1,0,1,1,1 },
            { 1,0,0,0,0,0,0,1 },
            { 1,1,1,1,1,1,0,1 },
            { 1,0,0,0,0,0,0,2 },
            { 1,0,1,1,1,1,1,2 },
            { 1,0,0,0,0,0,0,0 },
            { 1,1,1,1,1,1,1,1 }
        };

        Texture2D blokTexture;
        Rectangle block;







        //private void CreateBlocks()
        //{
        //    for (int l = 0; l < gameboard.GetLength(0); l++)
        //    {
        //        for (int k = 0; k < gameboard.GetLength(1); k++)
        //        {
        //            blocks.Add(BlockFactory.CreateBlock(gameboard[l, k]));
        //        }
        //    }
        //}



        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //map = new Map();
            level1 = new Level1(new Level1_BlockFactory());
            //level1 = new Level(new Level_1_BlockFactory(), _graphics.GraphicsDevice);

            base.Initialize();
            //hero = new Hero(heroAttackSprite, heroDamageSprite, heroDeathSprite, heroIdleSprite, heroJumpSprite, heroJumpFallInBetween, heroMoveSprite, new KeyboardReader());
            //hero = Hero.GetHero(heroBlokTexture,heroAttackSprite, heroDamageSprite, heroDeathSprite, heroIdleSprite, heroJumpSprite, heroJumpFallInBetween, heroMoveSprite, new KeyboardReader());
            hero = Hero.GetHero(heroAttackSprite, heroDamageSprite, heroDeathSprite, heroIdleSprite, heroMoveSprite, heroJumpSprite, heroJumpFallInBetween, heroBlokTexture);
            skeleton = new Skeleton(skeletonAttackSprite, skeletonDamageSprite, skeletonDeathSprite, skeletonIdleSprite, skeletonMoveSprite, 50, new Vector2(0,1), new Vector2 (2,0));
            block = new Rectangle(250, 400,32 , 32);
        }

        protected override void LoadContent()
        {
            
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            heroBlokTexture = new Texture2D(GraphicsDevice, 1, 1);
            LoadHero();
            LoadSkeleton();

            // TODO: use this.Content to load your game content here
            Block.Content = Content;
            //Tiles.Content = Content;

            //level1.GenerateLevel(new int[,] {
            //    { 0,0,0,1},
            //    { 0,0,0,1},
            //    { 0,0,1,1},
            //    { 0,1,1,1}
            //}, 64);

            //map.Generate(new int[,] {
            //    { 0,0,0,1},
            //    { 0,0,0,1},
            //    { 0,0,1,1},
            //    { 0,1,1,1}
            //}, 12);

            //level1.Generate(new int[,] {
            //    { 0,0,0,0,0,0,0,0,0},
            //    { 0,0,0,0,0,0,0,0,0},
            //    { 0,0,0,0,0,0,0,0,0},
            //    { 0,0,0,0,0,0,0,0,0},
            //    { 0,0,0,0,0,0,0,0,0},
            //    { 0,0,0,0,0,0,0,0,0},
            //    { 0,0,0,0,0,0,0,0,0},
            //    { 0,0,0,0,0,0,0,0,0},
            //    { 0,0,0,0,0,0,0,0,0},
            //    { 0,0,0,0,0,0,0,0,0},
            //    { 0,0,0,0,0,0,0,0,0},
            //    { 0,0,0,1,0,0,0,0,0},
            //    { 0,0,1,1,0,0,0,0,0},
            //    { 0,1,1,1,1,1,1,1,1}

            //}, 32);

            level1.Generate(new int[,] {
                { 0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0},
                { 0,0,0,0,0,0,0,0,0},
                { 0,0,0,1,0,0,0,0,0},
                { 0,0,1,1,0,0,0,0,0},
                { 0,1,1,1,1,1,1,1,1}

            }, 64);

            blokTexture = new Texture2D(GraphicsDevice, 1, 1);
            blokTexture.SetData(new[] { Color.White });
            _spriteBatch = new SpriteBatch(GraphicsDevice);


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
         
            hero.Update(gameTime, level1);
            //foreach (Block block in level1.TileList)
            //{
            //    hero.Collision(block.Rectangle, level1.Width, level1.Height);
            //}
            skeleton.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            hero.Draw(_spriteBatch);
            //map.Draw(_spriteBatch);
            level1.Draw(_spriteBatch);
            _spriteBatch.Draw(blokTexture, block, Color.Red);
            skeleton.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void LoadHero()
        {

            heroAttackSprite = Content.Load<Texture2D>("Sprites/Knight/_Attack");
            heroDamageSprite = Content.Load<Texture2D>("Sprites/Knight/_Hit");
            heroDeathSprite = Content.Load<Texture2D>("Sprites/Knight/_Death");
            heroIdleSprite = Content.Load<Texture2D>("Sprites/Knight/_Idle");
            heroJumpSprite = Content.Load<Texture2D>("Sprites/Knight/_Jump");
            heroJumpFallInBetween = Content.Load<Texture2D>("Sprites/Knight/_JumpFallInbetween");
            heroMoveSprite = Content.Load<Texture2D>("Sprites/Knight/_Run");
           
        }

        private void LoadSkeleton()
        {
            skeletonAttackSprite = Content.Load<Texture2D>("Sprites/Skeleton/_Attack");
            skeletonDamageSprite = Content.Load<Texture2D>("Sprites/Skeleton/_Hit");
            skeletonDeathSprite = Content.Load<Texture2D>("Sprites/Skeleton/_Death");
            skeletonIdleSprite = Content.Load<Texture2D>("Sprites/Skeleton/_Idle");
            skeletonMoveSprite = Content.Load<Texture2D>("Sprites/Skeleton/_Run");
        }
    }
    }
