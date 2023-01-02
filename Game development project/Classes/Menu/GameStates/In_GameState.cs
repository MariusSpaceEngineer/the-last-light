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

namespace Game_development_project.Classes.GameStates
{
    internal class In_GameState : GameState
    {
        protected List<Sprite> spriteList;
        protected Texture2D backgroundLevel;
        public static Level level;

        public Texture2D heroAttackSprite;
        public Texture2D heroDamageSprite;
        public Texture2D heroDeathSprite;
        public Texture2D heroIdleSprite;
        public Texture2D heroJumpSprite;
        public Texture2D heroJumpFallInBetween;
        public Texture2D heroMoveSprite;
        public Texture2D heroBlokTexture;

        public Texture2D skeletonAttackSprite;
        public Texture2D skeletonDamageSprite;
        public Texture2D skeletonDeathSprite;
        public Texture2D skeletonIdleSprite;
        public Texture2D skeletonMoveSprite;

        public Texture2D banditAttackSprite;
        public Texture2D banditDamageSprite;
        public Texture2D banditDeathSprite;
        public Texture2D banditIdleSprite;
        public Texture2D banditMoveSprite;

        public Texture2D huntressAttackSprite;
        public Texture2D huntressDamageSprite;
        public Texture2D huntressDeathSprite;
        public Texture2D huntressIdleSprite;
        public Texture2D huntressMoveSprite;

        public Texture2D arrowTexture;

        public Camera camera;

        public In_GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
     : base(game, graphicsDevice, content)
        {
            LoadContent(content);
            camera = game.camera;

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundLevel, new Vector2(0, 0), Color.White);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.Transform);

            level.Draw(spriteBatch);

            foreach (var sprite in spriteList)
            {
                sprite.Draw(spriteBatch);

            }
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            Debug.WriteLine(Hero.GetHero().Position);

            if (Hero.GetHero().HasDied || Hero.GetHero().isOnTrigger)
            {
                if (Hero.GetHero().HasDied)
                {
                    game.ChangeState(new GameOverState(game, game.GraphicsDevice, game.Content));
                    Hero.GetHero().HasDied = false;
                }
                else if (Hero.GetHero().isOnTrigger)
                {
                    game.ChangeState(new LevelCompleteState(game, game.GraphicsDevice, game.Content));
                    Hero.GetHero().isOnTrigger = false;

                }

                foreach (var sprite in spriteList)
                {
                    if (sprite is Hero)
                    {
                        Debug.WriteLine("Hero found");
                        Hero.GetHero().ResetHero();

                    }
                }

            }
            else
            {
                camera.Update(Hero.GetHero().Position, Hero.GetHero().CurrentLevel.Width, Hero.GetHero().CurrentLevel.Height);
                if (spriteList != null)
                {
                    foreach (var sprite in spriteList.ToArray())
                    {
                        if (Hero.GetHero().BoundingBox.TouchLeftOf(sprite.BoundingBox))
                        {
                            Hero.GetHero().Position.X = sprite.BoundingBox.X - Hero.GetHero().BoundingBox.Width - 55;
                        }
                        else if (Hero.GetHero().BoundingBox.TouchRightOf(sprite.BoundingBox))
                        {
                            Hero.GetHero().Position.X = sprite.BoundingBox.Right - Hero.GetHero().BoundingBox.Width - 20;
                        }
                        sprite.Update(gameTime, spriteList);

                    }

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

        public override void LoadContent(ContentManager content)
        {
            LoadKnight(content);
            LoadSkeleton(content);
            LoadBandit(content);
            LoadHuntress(content);
            arrowTexture = content.Load<Texture2D>("Textures/Sprites/Projectiles/Arrow");

        }



        private void LoadKnight(ContentManager content)
        {

            heroAttackSprite = content.Load<Texture2D>("Textures/Sprites/Knight/Knight_Attack");
            heroDamageSprite = content.Load<Texture2D>("Textures/Sprites/Knight/Knight_Hit");
            heroDeathSprite = content.Load<Texture2D>("Textures/Sprites/Knight/Knight_Death");
            heroIdleSprite = content.Load<Texture2D>("Textures/Sprites/Knight/Knight_Idle");
            heroJumpSprite = content.Load<Texture2D>("Textures/Sprites/Knight/Knight_Jump");
            heroMoveSprite = content.Load<Texture2D>("Textures/Sprites/Knight/Knight_Run");

        }

        private void LoadSkeleton(ContentManager content)
        {
            skeletonAttackSprite = content.Load<Texture2D>("Textures/Sprites/Skeleton/Skeleton_Attack");
            skeletonDamageSprite = content.Load<Texture2D>("Textures/Sprites/Skeleton/Skeleton_Hit");
            skeletonDeathSprite = content.Load<Texture2D>("Textures/Sprites/Skeleton/Skeleton_Death");
            skeletonIdleSprite = content.Load<Texture2D>("Textures/Sprites/Skeleton/Skeleton_Idle");
            skeletonMoveSprite = content.Load<Texture2D>("Textures/Sprites/Skeleton/Skeleton_Run");
        }

        private void LoadBandit(ContentManager content)
        {
            banditAttackSprite = content.Load<Texture2D>("Textures/Sprites/HeavyBandit/HeavyBandit_Attack");
            banditDamageSprite = content.Load<Texture2D>("Textures/Sprites/HeavyBandit/HeavyBandit_Hit");
            banditDeathSprite = content.Load<Texture2D>("Textures/Sprites/HeavyBandit/HeavyBandit_Death");
            banditIdleSprite = content.Load<Texture2D>("Textures/Sprites/HeavyBandit/HeavyBandit_Idle");
            banditMoveSprite = content.Load<Texture2D>("Textures/Sprites/HeavyBandit/HeavyBandit_Run");
        }

        private void LoadHuntress(ContentManager content)
        {
            huntressAttackSprite = content.Load<Texture2D>("Textures/Sprites/Huntress/Huntress_Attack");
            huntressDamageSprite = content.Load<Texture2D>("Textures/Sprites/Huntress/Huntress_Hit");
            huntressDeathSprite = content.Load<Texture2D>("Textures/Sprites/Huntress/Huntress_Death");
            huntressIdleSprite = content.Load<Texture2D>("Textures/Sprites/Huntress/Huntress_Idle");
            huntressMoveSprite = content.Load<Texture2D>("Textures/Sprites/Huntress/Huntress_Run");


        }

    }
}
