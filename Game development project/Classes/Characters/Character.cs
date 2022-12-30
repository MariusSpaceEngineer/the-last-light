using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game_development_project.Classes.Animations;
using Microsoft.Xna.Framework;
using Game_development_project.Classes.Characters.Character_States;
using Game_development_project.Classes.Characters.CharacterDirections;

namespace Game_development_project.Classes.Characters
{
    internal abstract class Character : MovableSprite
    {
        protected Texture2D attackSprite;
        protected Texture2D damageSprite;
        protected Texture2D deathSprite;
        protected Texture2D idleSprite;
        protected Texture2D moveSprite;

        public State CharacterState;
        //public Direction Direction;

        public bool HasDied = false;
        public int Health = 100;
        public bool IsHit = false;

        //protected Rectangle boundingBox;
        //public Rectangle BoundingBox
        //{
        //    get { return boundingBox; }
        //    set { boundingBox = value; }
        //}

        //protected static Vector2 position;
        //public static Vector2 Position
        //{
        //    get { return position; }
        //    set { position = value; }

        //protected Rectangle boundingBox;
        //protected Texture2D blokTexture;
        //public Rectangle BoundingBox
        //{
        //    get { return boundingBox; }
        //    set { boundingBox = value; }
        //}

        public Character(Texture2D attackSprite, Texture2D damageSprite, Texture2D deathSprite, Texture2D idleSprite, Texture2D moveSprite)
        {
            this.attackSprite = attackSprite;
            this.damageSprite = damageSprite;
            this.deathSprite = deathSprite;
            this.idleSprite = idleSprite;
            this.moveSprite = moveSprite;
        }

        public virtual void MoveAttackBox()
        {

        }

        public virtual void Attack(Sprite target)
        {

        }



       


    }
}
