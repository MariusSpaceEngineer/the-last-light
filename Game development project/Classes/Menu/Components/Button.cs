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

namespace Game_development_project.Classes.Menu.Components
{
    internal class Button : MenuComponent
    {
        #region Private variables
        //To determine if the user touches the button
        private IInputReader inputReader;

        private MouseState currentMouseState;
        private MouseState previousMouseState;

        private bool mouseHooveringOverButton;


        private SpriteFont buttonFont;
        private Texture2D texture;

        #endregion

        //The events that can occur if the button is clicked
        public event EventHandler Click;

        #region Get/Setters

        public Color FontColor { get; set; }

        public Vector2 Position { get; set; }

        //The button will have a rectangle around it to see if the user input intersects with it
        private Rectangle positionRectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
            }
        }

        //The content of the button
        public string Text { get; set; }

        #endregion

        public Button(Texture2D buttonTexture, SpriteFont font)
        {
            inputReader = new MouseReader();

            texture = buttonTexture;

            buttonFont = font;

            FontColor = Color.Black;
        }

        #region Public methods

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Adjustes the color of the button
            var buttonColor = Color.White;

            if (mouseHooveringOverButton)
                buttonColor = Color.Gray;

            spriteBatch.Draw(texture, positionRectangle, buttonColor);

            //Positions the text in the button's center
            if (!string.IsNullOrEmpty(Text))
            {
                var x = positionRectangle.X + positionRectangle.Width / 2 - buttonFont.MeasureString(Text).X / 2;
                var y = positionRectangle.Y + positionRectangle.Height / 2 - buttonFont.MeasureString(Text).Y / 2;

                spriteBatch.DrawString(buttonFont, Text, new Vector2(x, y), FontColor);
            }
        }

        public override void Update(GameTime gameTime)
        {
            previousMouseState = currentMouseState;
            currentMouseState = (MouseState)inputReader.GetInputState();

            Vector2 mousePosition = inputReader.ReadInput();
            //Draws rectangle around the mouse to see if it intersects with the button's rectangle
            var mousePositionRectangle = new Rectangle((int)mousePosition.X, (int)mousePosition.Y, 1, 1);
            //The bool used to check the collision between the input and the button
            
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

        #endregion


    }
}

