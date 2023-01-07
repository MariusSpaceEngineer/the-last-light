using Microsoft.Xna.Framework.Graphics;

namespace Game_development_project.Classes.Sprites.MovableSprites.Characters.Player.PlayerBehaviors
{
    internal interface IJumpBehavior
    {
        public Texture2D JumpSprite { get; set; }
        public bool HasJumped { get; set; }
        public float FallVelocity { get; set; }
        void Jump(float jumpHeight, float fallSpeed);
    }
}
