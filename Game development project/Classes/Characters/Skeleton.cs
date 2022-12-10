using Game_development_project.Classes.Characters.CharacterDirections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game_development_project.Classes.Animations;
using Game_development_project.Classes.Characters.Enemies;

namespace Game_development_project.Classes.Characters
{
    internal class Skeleton : MeleeEnemy, IGameObject
    {
      //The sprites and variables needed for the patrol are assigned in the enemy class


        private Animation attackAnimation;
        private Animation damageAnimation;
        private Animation deathAnimation;
        private Animation idleAnimation;
        private Animation moveAnimation;

        //Depending on the distance and speed, the skeleton will patrol in a different way
        public Skeleton(Texture2D attackSprite, Texture2D damageSprite, Texture2D deathSprite, Texture2D idleSprite, Texture2D moveSprite, float distance, Vector2 position, float speed): base(attackSprite, damageSprite, deathSprite, idleSprite, moveSprite, position, speed, distance)
        {
           

            this.attackAnimation = CreateAnimation(attackSprite, 18, 18, 1);
            this.damageAnimation = CreateAnimation(damageSprite, 8, 8,1);
            this.deathAnimation = CreateAnimation(deathSprite, 15, 15 ,1);
            this.idleAnimation = CreateAnimation(idleSprite, 11, 11 ,1);
            this.moveAnimation = CreateAnimation(moveSprite, 13, 13, 1);
            
            oldDistance = distance;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (LinearVelocity > 0)
            {
                spriteBatch.Draw(moveSprite, Position, moveAnimation.CurrentFrame.SourceRectangle, Color.White);

            }
            else
            {
                
                spriteBatch.Draw(moveSprite, Position, moveAnimation.CurrentFrame.SourceRectangle, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.FlipHorizontally, 0);


            }
        }

        public void Update(GameTime gameTime)
        {
            Patrol();
            moveAnimation.Update(gameTime);
        }
    }
}
