using Game_development_project.Classes.Animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_development_project.Classes
{
    internal class Hero : IGameObject
    {
        private Texture2D texture;
        private Rectangle deelRectangle;
        private Animation.Animation animation;

        private int schuifOp_X = 0;

        public Hero(Texture2D texture)
        {
            this.texture = texture;
            animation = new Animation.Animation();
            animation.GetFramesFromTextureProperties(texture.Width, texture.Height, 10, 1);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(0, 0), animation.CurrentFrame.SourceRectangle, Color.White);
        }

        public void Update()
        {
            animation.Update();
        }
    }
}
