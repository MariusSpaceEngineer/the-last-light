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
        //protected Vector2 position;
        //protected Vector2 speed;
        protected Direction direction;
        protected float distance;
        protected float oldDistance;
        //private Vector2 origin;

        public Enemy(Texture2D attackSprite, Texture2D damageSprite, Texture2D deathSprite, Texture2D idleSprite, Texture2D moveSprite, Vector2 position, float speed, float distance) : base(attackSprite, damageSprite, deathSprite, idleSprite, moveSprite)
        {
            this.Position = position;
            this.LinearVelocity = speed;
            this.oldDistance = distance;
        }

        public void Patrol()
        {
            Position.X += LinearVelocity;
            Origin = new Vector2(attackSprite.Width / 2, attackSprite.Height / 2);
            if (distance <= 0)
            {
                direction = new RightDirection();
                LinearVelocity = 1f;
            }
            else if (distance >= oldDistance)
            {
                direction = new LeftDirection();
                LinearVelocity = -1f;
            }
            if (direction is RightDirection)
            {
                distance += 1;
            }
            else
            {
                distance -= 1;
            }

            float heroPosition = Hero.GetHero().Position.X;

            heroPosition = heroPosition - Position.X;

            if (heroPosition >= -10 && heroPosition <= 10)
            {
                if (heroPosition < -1)
                {
                    LinearVelocity = -1f;
                }
                else if (heroPosition > 1)
                {
                    LinearVelocity = 1f;
                }
                else if (heroPosition == 0)
                {
                    LinearVelocity = 0f;
                }
            }
        }
    }
}
