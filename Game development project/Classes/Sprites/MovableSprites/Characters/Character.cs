using Game_development_project.Classes.Characters.Character_States;
using Game_development_project.Classes.Sprites;
using Game_development_project.Classes.Sprites.MovableSprites;
using Microsoft.Xna.Framework.Graphics;

namespace Game_development_project.Classes.Characters
{
    internal abstract class Character : MovableSprite
    {
        #region Get/Setters

        //The sprites of the characters which will be used to make animations
        public Texture2D AttackSprite { get; private set; }
        public Texture2D DamageSprite { get; private set; }
        public Texture2D DeathSprite { get; private set; }
        public Texture2D IdleSprite { get; private set; }
        public Texture2D MoveSprite { get; private set; }

        //The state characters have different states that are changed when something occurs
        public State CharacterState { get; set; }

        //Characters can be attacked and possibly die if they've taken enough damage
        public int Health { get; set; } = 100;
        public bool HasDied { get; set; } = false;
        public bool IsHit { get; set; } = false;

        #endregion

        public Character(Texture2D attackSprite, Texture2D damageSprite, Texture2D deathSprite, Texture2D idleSprite, Texture2D moveSprite)
        {
            this.AttackSprite = attackSprite;
            this.DamageSprite = damageSprite;
            this.DeathSprite = deathSprite;
            this.IdleSprite = idleSprite;
            this.MoveSprite = moveSprite;
        }

        #region Virtual methods

        public virtual void MoveAttackBox()
        {

        }

        public virtual void CheckHitCollision(Sprite enemy)
        {

        }

        public virtual void Attack(Sprite target)
        {

        }

        #endregion





    }
}
