using Game_development_project.Classes.Characters.CharacterDirections;
using Game_development_project.Classes.GameObjects.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_development_project.Classes.Characters.Enemies
{
    internal class ProjectileEnemy : Enemy
    {
        public bool heroIsClose = false;
        public bool shot = false;
        private Direction playerDirection;
        public ProjectileEnemy(Texture2D attackSprite, Texture2D damageSprite, Texture2D deathSprite, Texture2D idleSprite, Texture2D moveSprite, Vector2 position, float speed, float distance) : base(attackSprite, damageSprite, deathSprite, idleSprite, moveSprite, position, speed, distance)
        {
        }
        public override void Patrol()
        {
            base.Patrol();

            float heroPosition = Hero.GetHero().BoundingBox.X;

            heroPosition = heroPosition - Position.X;

            if (heroPosition >= -150 && heroPosition <= 150)
            {
                heroIsClose = true;
                if (heroPosition < -1)
                {
                    playerDirection = new LeftDirection();
                    LinearVelocity = -1f;
                }
                else if (heroPosition > 1)
                {
                    playerDirection = new RightDirection();
                    LinearVelocity = 1f;
                }
                else if (heroPosition == 0)
                {
                    LinearVelocity = 0f;
                }
            }
            else
            {
                heroIsClose=false;
            }
        }

        public Arrow projectile;
        int teller = 0;
        protected void AddBullet(List<Sprite> sprites)
        {
            
            var arrowProjectile = projectile.Clone() as Arrow;

            if (heroIsClose && !shot && teller<= 2 )
            {
                
                shot = true;
                
                if (playerDirection is LeftDirection)
                {
                    teller++;
                    arrowProjectile.Direction.X = -1;
                    arrowProjectile.Position = new Vector2(Position.X + 35, Position.Y + 50);
                    Debug.WriteLine("Shooting Left");
                }
                else
                {
                    teller++;
                    arrowProjectile.Direction.X = 1;
                    arrowProjectile.Position = new Vector2(Position.X + 50, Position.Y + 50);
                    Debug.WriteLine("Shooting Right");
                }
                //arrowProjectile.Direction = this.Direction;

               
                //arrowProjectile.LinearVelocity = this.LinearVelocity * 2;
                //arrowProjectile.Position = this.Position;
                //arrowProjectile.LinearVelocity = this.LinearVelocity * 2;
                arrowProjectile.LifeSpan = 2f;
                //arrowProjectile.Parent = this;

                sprites.Add(arrowProjectile);

            }
            else if (shot )
            {
                shot=false;
                teller=0;
            }
            
          
        }
    }
}
