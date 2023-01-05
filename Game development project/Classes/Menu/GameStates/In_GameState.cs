using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Default_Level;
using System.Reflection.Metadata;
using Game_development_project.Classes.Sprites;
using Game_development_project.Classes.Sprites.MovableSprites.Characters.Player;
using Game_development_project.Classes.Miscellaneous;
using Game_development_project.Classes.Level_Design.Level1;
using Game_development_project.Classes.Level_Design.Level2;

namespace Game_development_project.Classes.GameStates
{
    internal class In_GameState : GameState
    {

        #region Private variables 

        private static Level level;
        private List<Sprite> spriteList;

        #endregion

        #region Get/Setters

        public static Level Level
        {
            get { return level; }
            set { level = value; }
        }

        public List<Sprite> SpriteList
        {
            get { return spriteList; }
            set { spriteList = value; }
        }

        public Texture2D BackgroundLevel { get; set; }
        public Texture2D HeroAttackSprite { get; private set; }
        public Texture2D HeroDamageSprite { get; private set; }
        public Texture2D HeroDeathSprite { get; private set; }
        public Texture2D HeroIdleSprite { get; private set; }
        public Texture2D HeroJumpSprite { get; private set; }
        public Texture2D HeroMoveSprite { get; private set; }

        public Texture2D SkeletonAttackSprite { get; private set; }
        public Texture2D SkeletonDamageSprite { get; private set; }
        public Texture2D SkeletonDeathSprite { get; private set; }
        public Texture2D SkeletonIdleSprite { get; private set; }
        public Texture2D SkeletonMoveSprite { get; private set; }

        public Texture2D BanditAttackSprite { get; private set; }
        public Texture2D BanditDamageSprite { get; private set; }
        public Texture2D BanditDeathSprite { get; private set; }
        public Texture2D BanditIdleSprite { get; private set; }
        public Texture2D BanditMoveSprite { get; private set; }

        public Texture2D HuntressAttackSprite { get; private set; }
        public Texture2D HuntressDamageSprite { get; private set; }
        public Texture2D HuntressDeathSprite { get; private set; }
        public Texture2D HuntressIdleSprite { get; private set; }
        public Texture2D HuntressMoveSprite { get; private set; }

        public Texture2D ArrowTexture { get; private set; }

        public Camera Camera { get; private set; }

        #endregion

        public In_GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
     : base(game, graphicsDevice, content)
        {
            LoadContent(content);
            Camera = game.GameCamera;

        }

        #region Override methods

        public override void LoadContent(ContentManager content)
        {
            LoadKnight(content);
            LoadSkeleton(content);
            LoadBandit(content);
            LoadHuntress(content);
            ArrowTexture = content.Load<Texture2D>("Textures/Sprites/Projectiles/Arrow");

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            DrawBackground(BackgroundLevel, spriteBatch);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Camera.Transform);

            level.Draw(spriteBatch);

            DrawSprites(spriteList, spriteBatch);
           
            spriteBatch.End();
        }
        public override void Update(GameTime gameTime)
        {
            Hero hero = Hero.GetHero();
            //Debug.WriteLine(hero.Position);

            if (hero.HasDied || hero.IsOnTrigger)
            {
               CheckPlayerState(hero);

            }
            else
            {
                Camera.Update(hero.Position, hero.CurrentLevel.Width);

                foreach (var sprite in spriteList.ToArray())
                {
                    CheckPlayerCollisionWithEnemies(hero, sprite);

                    sprite.Update(gameTime, spriteList);
                }
            }
            PostUpdate();
        }

        public override void PostUpdate()
        {
            if (spriteList != null)
            {
                for (int i = 0; i < spriteList.Count; i++)
                {
                    if (spriteList[i].IsRemoved)
                    {
                        spriteList.RemoveAt(i);
                        i--;
                    }
                }

            }
            
        }

        #endregion

        #region Virtual methods

        public Level GenerateLevel(Level level, int tileSize)
        {
            Level newlevel = null;

            if (level is Level1)
            {
                newlevel = new Level1(new Level1BlockFactory());
                newlevel.Generate(newlevel.Map, tileSize);
            }
            else if (level is Level2)
            {
                newlevel = new Level2(new Level2BlockFactory());
                newlevel.Generate(newlevel.Map, tileSize);
            }


            return newlevel;
        }

        public virtual List<Sprite> GenerateLevelSpriteList() { 
            List<Sprite> spriteList = new List<Sprite>();
            return spriteList;
        }

        #endregion

        #region Private methods

        private void DrawBackground(Texture2D backgroundTexture, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, new Vector2(0, 0), Color.White);
            spriteBatch.End();
        }

        private void DrawSprites(List<Sprite> spriteList, SpriteBatch spriteBatch)
        {
            foreach (var sprite in spriteList)
            {
                sprite.Draw(spriteBatch);

            }
        }


        private void CheckPlayerState(Hero player)
        {
            if (player.HasDied)
            {
                Game.ChangeState(new GameOverState(Game, Game.GraphicsDevice, Game.Content));
                Hero.GetHero().HasDied = false;
            }
            else if (player.IsOnTrigger)
            {
                Game.ChangeState(new LevelCompleteState(Game, Game.GraphicsDevice, Game.Content));
                Hero.GetHero().IsOnTrigger = false;

            }

            foreach (var sprite in spriteList)
            {
                if (sprite is Hero)
                {
                    //Debug.WriteLine("Hero found");
                    player.ResetHero();

                }
            }

        }

        private void CheckPlayerCollisionWithEnemies(Hero player, Sprite enemy)
        {
            if (player.BoundingBox.TouchLeftOf(enemy.BoundingBox))
            {
                player.Position.X = enemy.BoundingBox.X - player.BoundingBox.Width - 55;
            }
            else if (player.BoundingBox.TouchRightOf(enemy.BoundingBox))
            {
                player.Position.X = enemy.BoundingBox.Right - player.BoundingBox.Width - 20;
            }

        }
        private void LoadKnight(ContentManager content)
        {

            HeroAttackSprite = content.Load<Texture2D>("Textures/Sprites/Knight/Knight_Attack");
            HeroDamageSprite = content.Load<Texture2D>("Textures/Sprites/Knight/Knight_Hit");
            HeroDeathSprite = content.Load<Texture2D>("Textures/Sprites/Knight/Knight_Death");
            HeroIdleSprite = content.Load<Texture2D>("Textures/Sprites/Knight/Knight_Idle");
            HeroJumpSprite = content.Load<Texture2D>("Textures/Sprites/Knight/Knight_Jump");
            HeroMoveSprite = content.Load<Texture2D>("Textures/Sprites/Knight/Knight_Run");

        }

        private void LoadSkeleton(ContentManager content)
        {
            SkeletonAttackSprite = content.Load<Texture2D>("Textures/Sprites/Skeleton/Skeleton_Attack");
            SkeletonDamageSprite = content.Load<Texture2D>("Textures/Sprites/Skeleton/Skeleton_Hit");
            SkeletonDeathSprite = content.Load<Texture2D>("Textures/Sprites/Skeleton/Skeleton_Death");
            SkeletonIdleSprite = content.Load<Texture2D>("Textures/Sprites/Skeleton/Skeleton_Idle");
            SkeletonMoveSprite = content.Load<Texture2D>("Textures/Sprites/Skeleton/Skeleton_Run");
        }

        private void LoadBandit(ContentManager content)
        {
            BanditAttackSprite = content.Load<Texture2D>("Textures/Sprites/HeavyBandit/HeavyBandit_Attack");
            BanditDamageSprite = content.Load<Texture2D>("Textures/Sprites/HeavyBandit/HeavyBandit_Hit");
            BanditDeathSprite = content.Load<Texture2D>("Textures/Sprites/HeavyBandit/HeavyBandit_Death");
            BanditIdleSprite = content.Load<Texture2D>("Textures/Sprites/HeavyBandit/HeavyBandit_Idle");
            BanditMoveSprite = content.Load<Texture2D>("Textures/Sprites/HeavyBandit/HeavyBandit_Run");
        }

        private void LoadHuntress(ContentManager content)
        {
            HuntressAttackSprite = content.Load<Texture2D>("Textures/Sprites/Huntress/Huntress_Attack");
            HuntressDamageSprite = content.Load<Texture2D>("Textures/Sprites/Huntress/Huntress_Hit");
            HuntressDeathSprite = content.Load<Texture2D>("Textures/Sprites/Huntress/Huntress_Death");
            HuntressIdleSprite = content.Load<Texture2D>("Textures/Sprites/Huntress/Huntress_Idle");
            HuntressMoveSprite = content.Load<Texture2D>("Textures/Sprites/Huntress/Huntress_Run");


        }

        #endregion




     

    }
}
