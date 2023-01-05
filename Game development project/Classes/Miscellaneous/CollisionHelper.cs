using Microsoft.Xna.Framework;

namespace Game_development_project.Classes.Miscellaneous
{
    static internal class CollisionHelper
    {
        public static bool TouchTopOf(this Rectangle rectangle1, Rectangle rectangle2)
        {
            return rectangle1.Bottom >= rectangle2.Top - 1 && rectangle1.Bottom <= rectangle2.Top + rectangle2.Height / 2 && rectangle1.Right >= rectangle2.Left + rectangle2.Width / 5 && rectangle1.Left <= rectangle2.Right - rectangle2.Width / 5;

        }

        public static bool TouchBottomOf(this Rectangle rectangle1, Rectangle rectangle12)
        {
            return rectangle1.Top <= rectangle12.Bottom + rectangle12.Height / 5 && rectangle1.Top >= rectangle12.Bottom - 1 && rectangle1.Right >= rectangle12.Left + rectangle12.Width / 5 && rectangle1.Left <= rectangle12.Right - rectangle12.Width / 5;
        }

        public static bool TouchLeftOf(this Rectangle rectangle1, Rectangle rectangle2)
        {
            return rectangle1.Right <= rectangle2.Right && rectangle1.Right >= rectangle2.Left - 5 && rectangle1.Top <= rectangle2.Bottom - rectangle2.Width / 4 && rectangle1.Bottom >= rectangle2.Top + rectangle2.Width / 4;
        }

        public static bool TouchRightOf(this Rectangle rectangle1, Rectangle rectangle2)
        {
            return rectangle1.Left >= rectangle2.Left && rectangle1.Left <= rectangle2.Right + 5 && rectangle1.Top <= rectangle2.Bottom - rectangle2.Width / 4 && rectangle1.Bottom >= rectangle2.Top + rectangle2.Width / 4;
        }
    }
}
