using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_development_project.Classes.Characters.Enemies
{
    internal class MeleeEnemy : Enemy
    {
        public MeleeEnemy(Texture2D attackSprite, Texture2D damageSprite, Texture2D deathSprite, Texture2D idleSprite, Texture2D moveSprite, Vector2 position, float speed, float distance) : base(attackSprite, damageSprite, deathSprite, idleSprite, moveSprite, position, speed, distance)
        {
        }
        public override void Patrol()
        {
            base.Patrol();

            float heroPosition = Hero.GetHero().BoundingBox.X;

            heroPosition = heroPosition - Position.X;

            if (heroPosition >= -150 && heroPosition <= 150)
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
