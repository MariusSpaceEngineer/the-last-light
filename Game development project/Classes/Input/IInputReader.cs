using Microsoft.Xna.Framework;

namespace Game_development_project.Classes
{
    internal interface IInputReader
    {
        Vector2 ReadInput();

        object GetInputState();
    }
}
