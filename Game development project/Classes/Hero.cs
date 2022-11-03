using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game_development_project.Classes
{
    internal class Hero : IGameObject
    {
        //All the sprites that hero has
        private Texture2D attackSprite;
        private Texture2D damageSprite;
        private Texture2D deathSprite;
        private Texture2D idleSprite;
        private Texture2D jumpSprite;
        private Texture2D jumpFallInBetween;
        private Texture2D moveSprite;
      

        private Animation attackAnimation;
        private Animation damageAnimation;
        private Animation deathAnimation;
        private Animation idleAnimation;
        private Animation jumpAnimation;
        private Animation jumpFallInBetweenAnimation;
        private Animation moveAnimation;
        

        public Hero(Texture2D attackSprite, Texture2D damageSprite, Texture2D deathSprite, Texture2D idleSprite, Texture2D jumpSprite, Texture2D jumpFallInBetween, Texture2D moveSprite)
        {
            this.attackSprite = attackSprite;
            this.damageSprite = damageSprite;
            this.deathSprite = deathSprite;
            this.idleSprite = idleSprite;
            this.jumpSprite = jumpSprite;
            this.jumpFallInBetween = jumpFallInBetween;
            this.moveSprite = moveSprite;
        

            attackAnimation = new Animation(4);
            attackAnimation.GetFramesFromTextureProperties(attackSprite.Width, attackSprite.Height, 4, 1);
           

            damageAnimation = new Animation(1);
            damageAnimation.GetFramesFromTextureProperties(damageSprite.Width, damageSprite.Height, 1, 1);

            deathAnimation = new Animation(10);
            deathAnimation.GetFramesFromTextureProperties(deathSprite.Width, deathSprite.Height, 10, 1);

            idleAnimation = new Animation(10);
            idleAnimation.GetFramesFromTextureProperties(idleSprite.Width, idleSprite.Height, 10, 1);

            jumpAnimation = new Animation(3);
            jumpAnimation.GetFramesFromTextureProperties(idleSprite.Width, idleSprite.Height, 3, 1);

            jumpFallInBetweenAnimation = new Animation(2);
            jumpFallInBetweenAnimation.GetFramesFromTextureProperties(idleSprite.Width, idleSprite.Height, 2, 1);

            moveAnimation = new Animation(10);
            moveAnimation.GetFramesFromTextureProperties(moveSprite.Width, moveSprite.Height, 10, 1);

            //animation = new Animation.Animation();
            //animation.GetFramesFromTextureProperties(texture.Width, texture.Height, 10, 1);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(attackSprite, new Vector2(0, 0), attackAnimation.CurrentFrame.SourceRectangle, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            attackAnimation.Update(gameTime);
        }


    }
}
