using Microsoft.Xna.Framework;

namespace Game_development_project.Classes.Characters.CharacterDirections
{
    internal class RightDirection : IDirection
    {
        public Vector2 movementDirection { get; set; } = new Vector2(1, 0);
    }
}
