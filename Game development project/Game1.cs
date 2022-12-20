using Default_Block;
using Default_Level;
using Game_development_project.Classes;
using Game_development_project.Classes.Characters;
using Game_development_project.Classes.GameObjects.Projectiles;
using Game_development_project.Classes.Level_Design;
using Game_development_project.Classes.Level_Design.Level;
using Game_development_project.Classes.Level_Design.Level1;
using Game_development_project.Classes.Level_Design.Level2;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
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

        private Bandit bandit;
        private Texture2D banditAttackSprite;
        private Texture2D banditDamageSprite;
        private Texture2D banditDeathSprite;
        private Texture2D banditIdleSprite;
        private Texture2D banditMoveSprite;

        private Huntress huntress;
        private Texture2D huntressAttackSprite;
        private Texture2D huntressDamageSprite;
        private Texture2D huntressDeathSprite;
        private Texture2D huntressIdleSprite;
        private Texture2D huntressMoveSprite;

        //Level1 level1;
        Level2 level2;

        BlockFactory blockFactory;

        Texture2D blokTexture;
        Rectangle block;

        Camera camera;

        private List<Sprite> _sprites;
        private Texture2D arrowTexture;

        private Texture2D backgroundVillage;







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
            //level1 = new Level1(new Level1_BlockFactory());
            level2 = new Level2(new Level2_BlockFactory());
            //level1 = new Level(new Level_1_BlockFactory(), _graphics.GraphicsDevice);

            base.Initialize();
            //hero = new Hero(heroAttackSprite, heroDamageSprite, heroDeathSprite, heroIdleSprite, heroJumpSprite, heroJumpFallInBetween, heroMoveSprite, new KeyboardReader());
            //hero = Hero.GetHero(heroBlokTexture,heroAttackSprite, heroDamageSprite, heroDeathSprite, heroIdleSprite, heroJumpSprite, heroJumpFallInBetween, heroMoveSprite, new KeyboardReader());
           
            //huntress = new Huntress(huntressAttackSprite,huntressDamageSprite, huntressDeathSprite, huntressIdleSprite, huntressMoveSprite, new Vector2(212, 475), 2, 50);
            block = new Rectangle(250, 400,32 , 32);
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            blokTexture = new Texture2D(GraphicsDevice, 1, 1);
            blokTexture.SetData(new[] { Color.White });
            LoadHuntress();
            LoadHero();
            LoadSkeleton();
            LoadBandit();
            _sprites = new List<Sprite>()
            {
                 new Huntress(huntressAttackSprite,huntressDamageSprite, huntressDeathSprite, huntressIdleSprite, huntressMoveSprite, new Vector2(212, 475), 2, 50,this.blokTexture)
             {
                 Position = new Vector2(100, 350),
                 projectile = new Arrow(Content.Load<Texture2D>("Sprites/Projectile/Arrow"), blokTexture)
             },
            Hero.GetHero(heroAttackSprite, heroDamageSprite, heroDeathSprite, heroIdleSprite, heroMoveSprite, heroJumpSprite, heroJumpFallInBetween, blokTexture, level2),
            new Skeleton(skeletonAttackSprite, skeletonDamageSprite, skeletonDeathSprite, skeletonIdleSprite, skeletonMoveSprite, 50, new Vector2(250, 475), 2, blokTexture),
            new Bandit(banditAttackSprite, banditDamageSprite, banditDeathSprite, banditIdleSprite, banditMoveSprite, new Vector2(150, 475), 2, 50, blokTexture)
        };

            heroBlokTexture = new Texture2D(GraphicsDevice, 1, 1);
          
            backgroundVillage = Content.Load<Texture2D>("Background/village_level1");


            camera = new Camera(GraphicsDevice.Viewport);


            //arrowTexture = Content.Load<Texture2D>("Sprites/Projectile/Arrow");
           

            // TODO: use this.Content to load your game content here
            Block.Content = Content;
            //Tiles.Content = Content;

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

            //level1.Generate(level1.Map, 64);
            level2.Generate(level2.Map, 64);
            
            //level1.Generate(new int[,] {
            //    { 0,0,0,0,0,0,0,0,0},
            //    { 0,0,0,0,0,0,0,0,0},
            //    { 0,0,0,0,0,0,0,0,0},
            //    { 0,0,0,0,0,0,0,0,0},
            //    { 0,0,0,1,0,0,0,0,0},
            //    { 0,0,1,1,0,0,0,0,0},
            //    { 0,1,1,1,1,1,1,1,1},
            //    { 0,1,1,1,1,1,1,1,1 }

            //}, 64);

           
            _spriteBatch = new SpriteBatch(GraphicsDevice);


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //hero.Update(gameTime, level1);
            //hero.Update(gameTime, level2);
            //foreach (Block block in level1.TileList)
            //{
            //    hero.Collision(block.Rectangle, level1.Width, level1.Height);
            //}
            //skeleton.Update(gameTime);
            //bandit.Update(gameTime);
            //huntress.Update(gameTime, _sprites);
            //camera.Update(Hero.Position, level1.Width, level1.Height);
            camera.Update(Hero.GetHero().Position, level2.Width, level2.Height);

            foreach (var sprite in _sprites.ToArray())
            {
                if (Hero.GetHero().BoundingBox.TouchLeftOf(sprite.BoundingBox))
                {
                    Hero.GetHero().Position.X = sprite.BoundingBox.X - Hero.GetHero().BoundingBox.Width - 55;     
                }
                else if (Hero.GetHero().BoundingBox.TouchRightOf(sprite.BoundingBox))
                {
                    Hero.GetHero().Position.X = sprite.BoundingBox.Right - Hero.GetHero().BoundingBox.Width - 20;                           
                }  
                sprite.Update(gameTime, _sprites);

            }
           
               

            PostUpdate();
            base.Update(gameTime);
        }

        private void PostUpdate()
        {
            for (int i = 0; i < _sprites.Count; i++)
            {
                if (_sprites[i].IsRemoved)
                {
                    _sprites.RemoveAt(i);
                    i--;
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawi
            // ng code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(backgroundVillage, new Vector2(0, 0), Color.White);
            _spriteBatch.End();
            _spriteBatch.Begin(SpriteSortMode.Deferred,null,null,null,null,null,camera.Transform);
            //hero.Draw(_spriteBatch);
            //map.Draw(_spriteBatch);
            //level1.Draw(_spriteBatch);
            level2.Draw(_spriteBatch);
            _spriteBatch.Draw(blokTexture, block, Color.Red);
           // skeleton.Draw(_spriteBatch);
            //bandit.Draw(_spriteBatch);
            //huntress.Draw(_spriteBatch);
            foreach (var sprite in _sprites)
            {
         
                sprite.Draw(_spriteBatch);


            }
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

        private void LoadBandit()
        {
            banditAttackSprite = Content.Load<Texture2D>("Sprites/Heavy Bandit/_Attack");
            //banditDamageSprite = Content.Load<Texture2D>("Sprites/Heavy Bandit/_Hit");
            banditDeathSprite = Content.Load<Texture2D>("Sprites/Heavy Bandit/_Death");
            banditIdleSprite = Content.Load<Texture2D>("Sprites/Heavy Bandit/_Idle");
            banditMoveSprite = Content.Load<Texture2D>("Sprites/Heavy Bandit/_Run");
        }

        private void LoadHuntress() 
        {
            huntressAttackSprite = Content.Load<Texture2D>("Sprites/Huntress/_Attack");
            huntressDamageSprite = Content.Load<Texture2D>("Sprites/Huntress/_Hit");
            huntressDeathSprite = Content.Load<Texture2D>("Sprites/Huntress/_Death");
            huntressIdleSprite = Content.Load<Texture2D>("Sprites/Huntress/_Idle");
            huntressMoveSprite = Content.Load<Texture2D>("Sprites/Huntress/_Run");

        }
    }
}
