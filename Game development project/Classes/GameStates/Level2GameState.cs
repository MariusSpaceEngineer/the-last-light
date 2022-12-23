using Game_development_project.Classes.Characters;
using Game_development_project.Classes.GameObjects.Projectiles;
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
    internal class Level2GameState : GameState
    {

        private Game1 game;
        private List<Sprite> spriteList;
        private Texture2D backgroundLevel;

        public Level2GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            this.game = game;
            LoadContent();
        }

        public override void LoadContent()
        {
            backgroundLevel = game.backgroundCastle;
            game.level2.Generate(game.level2.Map, 64);
            spriteList = new List<Sprite>() {
                 new Huntress(game.huntressAttackSprite,game.huntressDamageSprite, game.huntressDeathSprite, game.huntressIdleSprite, game.huntressMoveSprite, new Vector2(212, 475), 2, 50,game.blokTexture)
                 {
                 Position = new Vector2(100, 350),
                     projectile = new Arrow(game.arrowTexture, game.blokTexture)
             },
                  new Huntress(game.huntressAttackSprite,game.huntressDamageSprite, game.huntressDeathSprite, game.huntressIdleSprite, game.huntressMoveSprite, new Vector2(212, 475), 2, 50,game.blokTexture)
                 {
                 Position = new Vector2(300, 350),
                     projectile = new Arrow(game.arrowTexture, game.blokTexture)
             },
                Hero.GetHero(game.heroAttackSprite, game.heroDamageSprite, game.heroDeathSprite, game.heroIdleSprite, game.heroMoveSprite, game.heroJumpSprite, game.heroJumpFallInBetween, game.blokTexture, game.level2),
                new Skeleton(game.skeletonAttackSprite, game.skeletonDamageSprite, game.skeletonDeathSprite, game.skeletonIdleSprite, game.skeletonMoveSprite, 50, new Vector2(250, 475), 2, game.blokTexture),
                new Bandit(game.banditAttackSprite, game.banditDamageSprite, game.banditDeathSprite, game.banditIdleSprite, game.banditMoveSprite, new Vector2(150, 475), 2, 50, game.blokTexture),
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            spriteBatch.Begin();
            spriteBatch.Draw(game.backgroundCastle, new Vector2(0, 0), Color.White);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, game.camera.Transform);

            game.level2.Draw(spriteBatch);

            foreach (var sprite in spriteList)
            {
                sprite.Draw(spriteBatch);

            }
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            game.camera.Update(Hero.GetHero().Position, game.level2.Width, game.level2.Height);

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

            PostUpdate(gameTime);
            base.Update(gameTime);
        }

        public override void PostUpdate(GameTime gameTime)
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
}
