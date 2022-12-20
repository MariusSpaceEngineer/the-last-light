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
    internal class Enemy : Character, IPatrolBehavior
    {
        //protected Vector2 position;
        //protected Vector2 speed;
        protected Direction direction;
        protected float distance;
        protected float oldDistance;
        //private Vector2 origin;

        //protected Rectangle boundingBox;
        //protected Texture2D blokTexture;

        protected State characterState;

        //public Rectangle BoundingBox
        //{
        //    get { return boundingBox; }
        //    set { boundingBox = value; }
        //}

        //protected Rectangle attackBox;

        //public Rectangle AttackBox
        //{
        //    get { return attackBox; }
        //    set { attackBox = value; }
        //}

        public Enemy(Texture2D attackSprite, Texture2D damageSprite, Texture2D deathSprite, Texture2D idleSprite, Texture2D moveSprite, Vector2 position, float speed, float distance, Texture2D boundingBoxTexture) : base(attackSprite, damageSprite, deathSprite, idleSprite, moveSprite)
        {
            this.Position = position;
            this.LinearVelocity = speed;
            this.oldDistance = distance;

            this.blokTexture = boundingBoxTexture;
            //this.blokTexture.SetData(new[] { Color.White });
        }
        

        public virtual void Patrol()
        {
            Position.X += LinearVelocity;
            Origin = new Vector2(attackSprite.Width / 2, attackSprite.Height / 2);
            if (distance <= 0)
            {
                characterState = new MoveState();
                direction = new RightDirection();
                LinearVelocity = 1f;
            }
            else if (distance >= oldDistance)
            {
                characterState = new MoveState();
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

        }
    }
}
