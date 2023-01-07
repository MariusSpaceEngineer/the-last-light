using Game_development_project.Classes.Animations;
using Game_development_project.Classes.Characters.CharacterDirections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game_development_project.Classes.Sprites.MovableSprites
{
    internal abstract class MovableSprite : Sprite
    {

        public IDirection Direction { get; set; }

        public float HorizontalVelocity { get; set; }

        public Animation CreateAnimation(Texture2D sprite, int fps, int numberOfWidthSprites, int numberOfHeightSprites)
        {
            Animation animation = new Animation(fps);
            animation.GetFramesFromTextureProperties(sprite.Width, sprite.Height, numberOfWidthSprites, numberOfHeightSprites);
            return animation;
        }

        public virtual void MoveBoundingBox(Vector2 position)
        {

        }
    }
}
