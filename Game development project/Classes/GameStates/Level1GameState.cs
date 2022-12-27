using Default_Block;
using Default_Level;
using Game_development_project.Classes.Characters;
using Game_development_project.Classes.GameObjects.Projectiles;
using Game_development_project.Classes.Level_Design.Level1;
using Game_development_project.Classes.Level_Design.Level2;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Game_development_project.Classes.GameStates
{
    internal class Level1GameState : GameState
    {
        private Game1 game;
        private List<Sprite> spriteList;
        private Texture2D backgroundLevel;

        
        public Level1GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            this.game = game;
            LoadContent();
            

        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            spriteBatch.Draw(game.backgroundVillage, new Vector2(0, 0), Color.White);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, game.camera.Transform);

            game.level1.Draw(spriteBatch);
          
            foreach (var sprite in spriteList)
            {
                sprite.Draw(spriteBatch);

            }
            spriteBatch.End();



        }

        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            if (Hero.GetHero().hasDied)
            {

                game.ChangeState(new GameOverState(game, game.GraphicsDevice, game.Content));
                foreach (var sprite in spriteList)
                {
                    if (sprite is Hero)
                    {
                        Debug.WriteLine("Hero found");
                        Hero.GetHero().ResetHero();
                        Hero.GetHero().hasDied = false;
                        
                    }
                }
                
            }
            else
            {
                game.camera.Update(Hero.GetHero().Position, game.level1.Width, game.level1.Height);

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
        public override void LoadContent()
        {
            backgroundLevel = game.backgroundVillage;
            game.level1.Generate(game.level1.Map, 64);
            spriteList = new List<Sprite>() {
                 new Huntress(game.huntressAttackSprite,game.huntressDamageSprite, game.huntressDeathSprite, game.huntressIdleSprite, game.huntressMoveSprite, new Vector2(212, 475), 2, 50,game.blokTexture)
                 {
                 Position = new Vector2(500, 400),
                     projectile = new Arrow(game.arrowTexture, game.blokTexture)
             },
                  new Huntress(game.huntressAttackSprite,game.huntressDamageSprite, game.huntressDeathSprite, game.huntressIdleSprite, game.huntressMoveSprite, new Vector2(212, 475), 2, 50,game.blokTexture)
                 {
                 Position = new Vector2(300, 350),
                     projectile = new Arrow(game.arrowTexture, game.blokTexture)
             },
                Hero.GetHero(game.heroAttackSprite, game.heroDamageSprite, game.heroDeathSprite, game.heroIdleSprite, game.heroMoveSprite, game.heroJumpSprite, game.heroJumpFallInBetween, game.blokTexture, game.level1),
                new Skeleton(game.skeletonAttackSprite, game.skeletonDamageSprite, game.skeletonDeathSprite, game.skeletonIdleSprite, game.skeletonMoveSprite, 50, new Vector2(250, 475), 2, game.blokTexture),
                new Bandit(game.banditAttackSprite, game.banditDamageSprite, game.banditDeathSprite, game.banditIdleSprite, game.banditMoveSprite, new Vector2(150, 475), 2, 50, game.blokTexture),
            };

           
        }

    }
    
}
