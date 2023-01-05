using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_development_project.Classes.Animations
{
    internal class Animation
    {
        #region Private variables

        private List<AnimationFrame> frames;
        private int counter;
        private double secondCounter = 0;
        private int fps;

        #endregion

        #region Get/Setters

        public AnimationFrame CurrentFrame { get; set; }

        #endregion

        public Animation(int fps)
        {
            frames = new List<AnimationFrame>();
            this.fps = fps;
        }

        #region Public methods

        //public void AddFrame(AnimationFrame frame)
        //{
        //    frames.Add(frame);
        //    CurrentFrame = frames[0];
        //}


    

        public void Update(GameTime gameTime)
        {
            CurrentFrame = frames[counter];

            secondCounter += gameTime.ElapsedGameTime.TotalSeconds;


            if (secondCounter >= 1d / fps)
            {
                counter++;
                secondCounter = 0;
            }

            if (counter >= frames.Count)
            {
                counter = 0;
            }
        }


        public void GetFramesFromTextureProperties(int width, int height, int numberOfWidthSprites, int numberOfHeightSprites)
        {
            int widthOfFrame = width / numberOfWidthSprites;
            int heightOfFrame = height / numberOfHeightSprites;

            for (int y = 0; y <= height - heightOfFrame; y += heightOfFrame)
            {
                for (int x = 0; x <= width - widthOfFrame; x += widthOfFrame)
                {
                    frames.Add(new AnimationFrame(new Rectangle(x, y, widthOfFrame, heightOfFrame)));
                }
            }
        }

        #endregion
    }
}

