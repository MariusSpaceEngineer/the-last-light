using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Default_Block
{
    abstract public class Block
    {
        #region Private variables

        protected Texture2D texture;

        private Rectangle rectangle;

        public bool isTrigger;

        private static ContentManager content;

        #endregion

        #region Get/Setters

        public Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }

        public static ContentManager Content
        {
            protected get { return content; }
            set { content = value; }
        }

        public Block(Rectangle newrectangle)
        {
            this.Rectangle = newrectangle;
            this.isTrigger = false;
        }

        #endregion

        #region Public methods

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }

        #endregion
    }
}
