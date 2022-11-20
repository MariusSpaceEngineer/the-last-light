using Game_development_project.Classes.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_development_project.Classes.Level_Design.Level1
{
    internal class Level1
    {
        //Maybe make a abstract level class and for every level an subclass

        private List<Block> collisionTiles = new List<Block>();
        public List<Block> CollisionTiles
        {
            get { return collisionTiles; }
        }

        private int width, height;
        public int Width { get { return width; } }
        public int Height { get { return height; } }

        public Level1() { }

        public void Generate(int[,] map, int size)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                for (int y = 0; y < map.GetLength(0); y++)
                {
                    int number = map[y, x];


                    //Maybe use a blockFactory
                    if (number > 0)
                    {
                        collisionTiles.Add(new GrassBlock(number, new Rectangle(x * size, y * size, size, size)));
                    }

                    width = (x + 1) * size;
                    height = (y + 1) * size;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Block tile in collisionTiles)
            {
                tile.Draw(spriteBatch);
            }
        }
    }
}

