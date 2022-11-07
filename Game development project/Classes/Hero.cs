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

        private Vector2 position;
        private Vector2 speed;
        private IInputReader inputReader;



        public Hero(Texture2D attackSprite, Texture2D damageSprite, Texture2D deathSprite, Texture2D idleSprite, Texture2D jumpSprite, Texture2D jumpFallInBetween, Texture2D moveSprite, IInputReader inputReader)
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

            this.inputReader = inputReader;
            this.position = new Vector2(1, 1);
            this.speed = new Vector2(2, 2);
            


            //animation = new Animation.Animation();
            //animation.GetFramesFromTextureProperties(texture.Width, texture.Height, 10, 1);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(moveSprite, position, moveAnimation.CurrentFrame.SourceRectangle, Color.White);
        }

        public void Update(GameTime gameTime)
        {

            Move();
            moveAnimation.Update(gameTime);
            
        }

        private void Move()
        {
            var direction = inputReader.ReadInput();
            direction *= speed;
            position += direction;

        }

        private Vector2 Limit(Vector2 velocityVector, float maxSpeed)
        {
            if (velocityVector.Length() > maxSpeed)
            {
                var ratio = maxSpeed / velocityVector.Length();
                velocityVector.X *= ratio;
                velocityVector.Y *= ratio;
            }
            return velocityVector;
        }




    }
}
