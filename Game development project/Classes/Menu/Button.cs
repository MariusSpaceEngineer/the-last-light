using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game_development_project.Classes.Input;
using System.Diagnostics;

namespace Game_development_project.Classes.Menu
{
    internal class Button : Component
    {
     
        private IInputReader inputReader;

        private MouseState currentMouseState;
        private MouseState previousMouseState;

        private bool mouseHooveringOverButton;


        private SpriteFont buttonFont;
        private Texture2D texture;


        public event EventHandler Click;

        public Color FontColor { get; set; }

        public Vector2 Position { get; set; }

        private Rectangle positionRectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
            }
        }

        public string Text { get; set; }

     

        public Button(Texture2D buttonTexture, SpriteFont font)
        {
            this.inputReader = new MouseReader();

            this.texture = buttonTexture;

            buttonFont = font;

            FontColor = Color.Black;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var buttonColor = Color.White;

            if (mouseHooveringOverButton)
                buttonColor = Color.Gray;

            spriteBatch.Draw(texture, positionRectangle, buttonColor);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = (positionRectangle.X + (positionRectangle.Width / 2)) - (buttonFont.MeasureString(Text).X / 2);
                var y = (positionRectangle.Y + (positionRectangle.Height / 2)) - (buttonFont.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(buttonFont, Text, new Vector2(x, y), FontColor);
            }
        }

        public override void Update(GameTime gameTime)
        {
            previousMouseState = currentMouseState;
            currentMouseState = (MouseState)inputReader.GetInputState();
            Vector2 mousePosition = inputReader.ReadInput();
            var mousePositionRectangle = new Rectangle((int)mousePosition.X, (int)mousePosition.Y,1,1);

            mouseHooveringOverButton = false;

            if (mousePositionRectangle.Intersects(positionRectangle))
            {
                Debug.WriteLine("Mouse intersects");
                mouseHooveringOverButton = true;

                if (currentMouseState.LeftButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Pressed)
                {
                    Click.Invoke(this, new EventArgs());
                  
                }

               
            }
        
        }


    }
}

