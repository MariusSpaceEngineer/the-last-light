using Game_development_project.Classes.Characters.Behaviors;
using Game_development_project.Classes.Characters.CharacterDirections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_development_project.Classes.Characters
{
    internal class Enemy : Character, IPatrolBehavior
    {
        protected Vector2 position;
        protected Vector2 speed;
        protected Direction direction;
        protected float distance;
        protected float oldDistance;
        private Vector2 origin;

        public Enemy(Texture2D attackSprite, Texture2D damageSprite, Texture2D deathSprite, Texture2D idleSprite, Texture2D moveSprite, Texture2D boundingBoxTexture, Vector2 position, Vector2 speed, float distance) : base(attackSprite, damageSprite, deathSprite, idleSprite, moveSprite, boundingBoxTexture)
        {
            this.position = position;
            this.speed = speed;
            this.oldDistance = distance;

        
        }

        public void Patrol()
        {
            position += speed;
            origin = new Vector2(attackSprite.Width / 2, attackSprite.Height / 2);
            if (distance <= 0)
            {
                direction = new RightDirection();
                speed.X = 1f;
            }
            else if (distance >= oldDistance)
            {
                direction = new LeftDirection();
                speed.X = -1f;
            }
            if (direction is RightDirection)
            {
                distance += 1;
            }
            else
            {
                distance -= 1;
            }

            float heroPosition = Hero.Position.X;

            heroPosition = heroPosition - position.X;

            if (heroPosition >= -10 && heroPosition <= 10)
            {
                if (heroPosition < -1)
                {
                    speed.X = -1f;
                }
                else if (heroPosition > 1)
                {
                    speed.X = 1f;
                }
                else if (heroPosition == 0)
                {
                    speed.X = 0f;
                }
            }
        }
    }
}
