using Game_development_project.Classes.Sprites.MovableSprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game_development_project.Classes.GameObjects
{
    internal abstract class GameObject : MovableSprite
    {
        protected Texture2D texture;

        public GameObject(Texture2D texture)
        {
            this.texture = texture;
            Origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }
    }
}
