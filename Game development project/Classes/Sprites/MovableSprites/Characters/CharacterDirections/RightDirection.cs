using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_development_project.Classes.Characters.CharacterDirections
{
    internal class RightDirection : IDirection
    {
        public Vector2 movementDirection { get; set; } = new Vector2(1, 0);
    }
}
