using Game_development_project.Classes.Characters.Character_States;
using Game_development_project.Classes.Characters.CharacterDirections;
using Game_development_project.Classes.GameObjects.Projectiles;
using Game_development_project.Classes.Sprites.MovableSprites.Characters.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_development_project.Classes.Sprites.MovableSprites.Characters.Enemies.ProjectileEnemies
{
    internal class ProjectileEnemy : Enemy
    {
        #region Private variables
        //The projectile that will be shot
        private Projectile projectile;
        //The counter for the projectile, if it reaches a certain value it will be removed
        private int shootingCounter = 0;

        #endregion

        #region Get/Setters

        //Needed to determine which way the projectile will face
        public static IDirection PlayerDirection { get; private set; }

        public Projectile Projectile
        {
            get { return projectile; }
            set { projectile = value; }
        }

        #endregion

        public ProjectileEnemy(Texture2D attackSprite, Texture2D damageSprite, Texture2D deathSprite, Texture2D idleSprite, Texture2D moveSprite, Vector2 position, float speed, float distance, Texture2D boundingBoxTexture) : base(attackSprite, damageSprite, deathSprite, idleSprite, moveSprite, position, speed, distance, boundingBoxTexture)
        {
            ChasingSpeed = speed;
        }

        #region Override methods

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
                    PlayerDirection = new LeftDirection();
                    Direction = new LeftDirection();

                    HorizontalVelocity = Direction.movementDirection.X * ChasingSpeed;

                    if (heroPosition >= -100)
                    {
                        Attack(hero);
                    }

                }

                else if (heroPosition > 1)
                {
                    PlayerDirection = new RightDirection();
                    Direction = new RightDirection();
                    HorizontalVelocity = Direction.movementDirection.X * ChasingSpeed;

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
            //Debug.WriteLine("Shooting arrow towards player");
            CharacterState = new AttackState();
            HorizontalVelocity = 0f;
        }

        #endregion


        #region Methods

        protected void ShootProjectile(List<Sprite> sprites)
        {

            var arrowProjectile = Projectile.Clone() as Arrow;
            arrowProjectile.LifeSpan = 2f;

            if (PlayerDirection is LeftDirection && CharacterState is AttackState)
            {
                shootingCounter++;
                arrowProjectile.MovementDirection = PlayerDirection.movementDirection.X;
                arrowProjectile.Position = new Vector2(Position.X + 35, Position.Y + 50);

                if (shootingCounter == 50)
                {
                    sprites.Add(arrowProjectile);
                    shootingCounter = 0;
                }

                //Debug.WriteLine("Shooting Left");
            }
            else if (PlayerDirection is RightDirection && CharacterState is AttackState)
            {
                shootingCounter++;
                arrowProjectile.MovementDirection = PlayerDirection.movementDirection.X;
                arrowProjectile.Position = new Vector2(Position.X + 50, Position.Y + 50);
                if (shootingCounter == 50)
                {
                    sprites.Add(arrowProjectile);
                    shootingCounter = 0;
                }

                //Debug.WriteLine("Shooting Right");
            }

        }

        #endregion
    }
}
