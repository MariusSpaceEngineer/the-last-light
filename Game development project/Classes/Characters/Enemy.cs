using Game_development_project.Classes.Characters.Behaviors;
using Game_development_project.Classes.Characters.Character_States;
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
    internal abstract class Enemy : Character, IPatrolBehavior
    {
        public float CurrentDistance { get; set; }
        public float PatrolDistance { get; set; }

        public Enemy(Texture2D attackSprite, Texture2D damageSprite, Texture2D deathSprite, Texture2D idleSprite, Texture2D moveSprite, Vector2 position, float speed, float patrolDistance, Texture2D boundingBoxTexture) : base(attackSprite, damageSprite, deathSprite, idleSprite, moveSprite)
        {
            this.Position = position;
            this.HorizontalVelocity = speed;
            this.PatrolDistance = patrolDistance;

            this.boundingBoxTexture = boundingBoxTexture;
        }
        
        public virtual void Patrol()
        {
            Position.X += HorizontalVelocity;
            Origin = new Vector2(attackSprite.Width / 2, attackSprite.Height / 2);
            
            if (CurrentDistance <= 0)
            {
                CharacterState = new MoveState();
                Direction = new RightDirection();
                HorizontalVelocity = Direction.movementDirection.X;
            }
            else if (CurrentDistance >= PatrolDistance)
            {
                CharacterState = new MoveState();
                Direction = new LeftDirection();
                HorizontalVelocity = Direction.movementDirection.X;
            }
            if (Direction is RightDirection)
            {
                CurrentDistance += 1;
            }
            else
            {
                CurrentDistance -= 1;
            }

           
        }

        public void CheckEnemyHealth(Hero hero)
        {
            if (hero.AttackBox.Intersects(boundingBox))
            {
                if (Health > 0)
                {
                    CharacterState = new DamagedState();
                    Health--;
                }
                else
                {
                    CharacterState = new DeathState();
                    IsRemoved = true;
                }



            }

        }
    }
}
