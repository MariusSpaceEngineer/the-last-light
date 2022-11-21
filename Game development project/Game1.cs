﻿using Default_Block;
using Game_development_project.Classes;
using Game_development_project.Classes.Level_Design;
using Game_development_project.Classes.Level_Design.Level;
using Game_development_project.Classes.Level_Design.Level1;
using Game_development_project.Classes.Map;
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
        private Texture2D heroAttackSprite;
        private Texture2D heroDamageSprite;
        private Texture2D heroDeathSprite;
        private Texture2D heroIdleSprite;
        private Texture2D heroJumpSprite;
        private Texture2D heroJumpFallInBetween;
        private Texture2D heroMoveSprite;

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
            hero = new Hero(heroAttackSprite, heroDamageSprite, heroDeathSprite, heroIdleSprite, heroJumpSprite, heroJumpFallInBetween, heroMoveSprite, new KeyboardReader());

            //skeleton = new Skeleton(skeletonAttackSprite, skeletonDamageSprite, skeletonDeathSprite, skeletonIdleSprite, skeletonMoveSprite);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            LoadHero();
            //LoadSkeleton();

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

            level1.Generate(new int[,] {
                { 0,0,0,1},
                { 0,0,0,1},
                { 0,0,1,1},
                { 0,1,1,1}
            }, 12);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            hero.Update(gameTime);
            //skeleton.Update(gameTime);

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
            //skeleton.Draw(_spriteBatch);
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
