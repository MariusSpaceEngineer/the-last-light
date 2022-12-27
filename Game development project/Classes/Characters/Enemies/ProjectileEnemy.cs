using Game_development_project.Classes.Characters.Character_States;
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
        private int lifes = 100;
      
        public static Direction playerDirection;
        public ProjectileEnemy(Texture2D attackSprite, Texture2D damageSprite, Texture2D deathSprite, Texture2D idleSprite, Texture2D moveSprite, Vector2 position, float speed, float distance, Texture2D boundingBoxTexture) : base(attackSprite, damageSprite, deathSprite, idleSprite, moveSprite, position, speed, distance, boundingBoxTexture)
        {
        }
        public override void Patrol()
        {
            base.Patrol();

            Hero hero = Hero.GetHero();
            float heroPosition = hero.BoundingBox.X;

            //if heroPosition is negative then is position of the enemy bigger than the one of the hero
            heroPosition = heroPosition - Position.X;


            if (heroPosition >= -200 && heroPosition <= 200)
            {
                this.characterState = new MoveState();
                //heroIsClose = true;
                //the postion is negative so the enemy is in front of the enemy, so the enemy has to move left
                if (heroPosition < -1)
                {
                    playerDirection = new LeftDirection();
                    this.direction = new LeftDirection();
                 
                    LinearVelocity = -1f;

                    if (heroPosition >= -100)
                    {
                        Debug.WriteLine("Shooting arrow left");
                        this.characterState = new AttackState();
                        LinearVelocity = 0f;
                    }

                }

                else if (heroPosition > 1)
                {
                    playerDirection = new RightDirection();
                    this.direction = new RightDirection();
                    LinearVelocity = 1f;

                    if (heroPosition >= 100)
                    {
                        Debug.WriteLine("Shooting arrow right");
                        this.characterState = new AttackState();
                        LinearVelocity = 0f;
                    }
                }
                if (hero.AttackBox.Intersects(this.boundingBox))
                {
                    if (lifes > 0)
                    {
                        this.characterState = new DamagedState();
                        this.lifes--;
                    }
                    else
                    {
                        this.characterState = new DeathState();
                        this.IsRemoved = true;
                    }

                    

                }
                //else if (heroPosition == 0)
                //{
                //    LinearVelocity = 0f;
                //}
            }
            //else
            //{
            //    heroIsClose=false;
            //}
        }

        public Arrow projectile;
        int teller = 0;
        protected void AddBullet(List<Sprite> sprites)
        {
            
            var arrowProjectile = projectile.Clone() as Arrow;
            arrowProjectile.LifeSpan = 2f;


            //shot = true;

                if (playerDirection is LeftDirection && this.characterState is AttackState)
                {
                    teller++;
                    arrowProjectile.Direction.X = -1;
                    arrowProjectile.Position = new Vector2(Position.X + 35, Position.Y + 50);
                    arrowProjectile.Direction.Y = -1;
                    if (teller == 50)
                    {
                        sprites.Add(arrowProjectile);
                        teller = 0;
                    }

                Debug.WriteLine("Shooting Left");
                }
                else if(playerDirection is RightDirection && this.characterState is AttackState)
                {
                    teller++;
                    arrowProjectile.Direction.X = 1;
                    arrowProjectile.Position = new Vector2(Position.X + 50, Position.Y + 50);
                    if (teller == 50)
                    {
                        sprites.Add(arrowProjectile);
                        teller = 0;
                    }

                    Debug.WriteLine("Shooting Right");
                }
                //arrowProjectile.Direction = this.Direction;

               
                //arrowProjectile.LinearVelocity = this.LinearVelocity * 2;
                //arrowProjectile.Position = this.Position;
                //arrowProjectile.LinearVelocity = this.LinearVelocity * 2;
                //arrowProjectile.Parent = this;


            
            //else if (shot )
            //{
            //    shot=false;
            //    teller=0;
            //}
            
          
        }
    }
}
