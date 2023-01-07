using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Game_development_project.Classes.Input
{
    internal class MouseReader : IInputReader
    {
        public Vector2 ReadInput()
        {
            MouseState state = (MouseState)GetInputState();
            //Debug.WriteLine(state);

            //gets the coordinations of the mouse
            Vector2 directionMouse = new Vector2(state.X, state.Y);
            return directionMouse;
        }

        public object GetInputState()
        {
            return Mouse.GetState();
        }
    }
}
