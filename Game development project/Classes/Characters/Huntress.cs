﻿using Game_development_project.Classes.Animations;
using Game_development_project.Classes.Characters.Character_States;
using Game_development_project.Classes.Characters.CharacterDirections;
using Game_development_project.Classes.Characters.Enemies;
using Game_development_project.Classes.GameObjects.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_development_project.Classes.Characters
{
    internal class Huntress : ProjectileEnemy
    {
        private Animation attackAnimation;
        private Animation damageAnimation;
        private Animation deathAnimation;
        private Animation idleAnimation;
        private Animation moveAnimation;

        public Huntress(Texture2D attackSprite, Texture2D damageSprite, Texture2D deathSprite, Texture2D idleSprite, Texture2D moveSprite, Vector2 position, float speed, float distance, Texture2D boundingBoxTexture) : base(attackSprite, damageSprite, deathSprite, idleSprite, moveSprite, position, speed, distance, boundingBoxTexture)
        {

            this.attackAnimation = CreateAnimation(attackSprite, 6, 6, 1);
            this.damageAnimation = CreateAnimation(damageSprite, 3, 3, 1);
            this.deathAnimation = CreateAnimation(deathSprite, 10, 10, 1);
            this.idleAnimation = CreateAnimation(idleSprite, 10, 10, 1);
            this.moveAnimation = CreateAnimation(moveSprite, 8, 8, 1);

            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, 28, 40);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (CharacterState is IdleState)
            {
                CharacterState.Draw(spriteBatch, idleSprite, idleAnimation, Direction, Position, this);
            }
            else if (CharacterState is MoveState)
            {
                CharacterState.Draw(spriteBatch, moveSprite, moveAnimation, Direction, Position, this);
            }
            else if (CharacterState is AttackState)
            {
                CharacterState.Draw(spriteBatch, attackSprite, attackAnimation, Direction, Position, this);

            }
            else if (CharacterState is DamagedState)
            {
                CharacterState.Draw(spriteBatch, damageSprite, damageAnimation, Direction, Position, this);
                IsHit = false;
            }
            else if (CharacterState is DeathState)
            {
                CharacterState.Draw(spriteBatch, deathSprite, deathAnimation, Direction, Position, this);
            }
           
        }


        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Patrol();
            if (CharacterState is AttackState)
            {
                attackAnimation.Update(gameTime);
                ShootProjectile(sprites);
            }
            else if (CharacterState is MoveState)
            {
                moveAnimation.Update(gameTime);

            }
            else if (this.CharacterState is DamagedState)
            {
                damageAnimation.Update(gameTime);

            }
            MoveBoundingBox(Position);

        }

        public override void MoveBoundingBox(Vector2 position)
        {
            boundingBox.X = (int)position.X + 32;
            boundingBox.Y = (int)position.Y + 30;
        }

    }
}

