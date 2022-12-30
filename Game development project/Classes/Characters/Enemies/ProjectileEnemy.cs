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
        public static Direction playerDirection;

        public Arrow projectile;
        int shootingCounter = 0;


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
                CharacterState = new MoveState();
                //the postion is negative so the enemy is in front of the enemy, so the enemy has to move left
                if (heroPosition < -1)
                {
                    playerDirection = new LeftDirection();
                    Direction = new LeftDirection();
                 
                    HorizontalVelocity = Direction.movementDirection.X;

                    if (heroPosition >= -100)
                    {
                        Attack(hero);
                    }

                }

                else if (heroPosition > 1)
                {
                    playerDirection = new RightDirection();
                    Direction = new RightDirection();
                    HorizontalVelocity = Direction.movementDirection.X;

                    if (heroPosition >= 100)
                    {
                        Attack(hero);
                        
                    }
                }
                CheckEnemyHealth(hero);
            }
     
        }

        public override void Attack(Sprite target)
        {
            Debug.WriteLine("Shooting arrow towards player");
            CharacterState = new AttackState();
            HorizontalVelocity = 0f;
        }

       
        protected void ShootProjectile(List<Sprite> sprites)
        {
            
            var arrowProjectile = projectile.Clone() as Arrow;
            arrowProjectile.LifeSpan = 2f;

                if (playerDirection is LeftDirection && CharacterState is AttackState)
                {
                    shootingCounter++;
                    arrowProjectile.movementDirection = playerDirection.movementDirection.X;
                    arrowProjectile.Position = new Vector2(Position.X + 35, Position.Y + 50);
                 
                    if (shootingCounter == 50)
                    {
                        sprites.Add(arrowProjectile);
                        shootingCounter = 0;
                    }

                Debug.WriteLine("Shooting Left");
                }
                else if(playerDirection is RightDirection && CharacterState is AttackState)
                {
                    shootingCounter++;
                    arrowProjectile.movementDirection = playerDirection.movementDirection.X;
                    arrowProjectile.Position = new Vector2(Position.X + 50, Position.Y + 50);
                    if (shootingCounter == 50)
                    {
                        sprites.Add(arrowProjectile);
                        shootingCounter = 0;
                    }

                    Debug.WriteLine("Shooting Right");
                }

        }
    }
}
