using Default_Block;
using Game_development_project.Classes.Level_Design.Level;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Default_Level
{
    abstract public class Level
    {
        #region Private variables

        //The list which will hold the tiles of the tilemap
        private List<Block> tileList = new List<Block>();

        //The width and height of the level
        private int width;
        private int height;
        
        //The map in matrix-form
        private int[,] map;

        //The factory that will be used to create tiles
        private IBlockFactory blockFactory;

        #endregion

        #region Get/Setters

        public List<Block> TileList
        {
            get { return tileList; }
        }

        public int Width { get { return width; } }
        public int Height { get { return height; } }

        public int[,] Map { get { return map; } set { map = value; } }

        #endregion

        public Level(IBlockFactory blockFactory) {
            this.blockFactory = blockFactory;
        }

        #region Public methods

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
                        tileList.Add(blockFactory.CreateBlock(number, new Rectangle(x * size, y*size, size, size)));
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

        #endregion
    }
}

