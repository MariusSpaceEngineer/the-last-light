using Game_development_project.Classes.Level_Design.Level1;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_development_project.Classes.Level_Design
{
    abstract internal class Level
    {
        private List<Block> tileList = new List<Block>();
        public List<Block> CollisionTiles
        {
            get { return tileList; }
        }

        private int width, height;
        public int Width { get { return width; } }
        public int Height { get { return height; } }

        public Level() { }

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
                        tileList.Add(new GrassBlock(number, new Rectangle(x * size, y * size, size, size)));
                    }

                    width = (x + 1) * size;
                    height = (y + 1) * size;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Block tile in tileList)
            {
                tile.Draw(spriteBatch);
            }
        }
    }
}

