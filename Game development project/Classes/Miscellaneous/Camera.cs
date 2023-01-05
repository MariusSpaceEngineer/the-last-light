using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_development_project.Classes.Miscellaneous
{
    public class Camera
    {
        #region Private variables

        private Matrix transform;
        private Vector2 centerView;
        private Viewport viewport;

        #endregion

        #region Get/Setters

        public Matrix Transform { get { return transform; } }

        #endregion

        public Camera(Viewport newViewport)
        {
            viewport = newViewport;
        }

        #region Public methods

        public void Update(Vector2 targetPosition, int xOffset)
        {
            if (targetPosition.X < viewport.Width / 2)
            {
                centerView.X = viewport.Width / 2;
            }
            else if (targetPosition.X > xOffset - viewport.Width / 2)
            {
                centerView.X = xOffset - viewport.Width / 2;
            }
            else
            {
                centerView.X = targetPosition.X;
            }

            if (targetPosition.Y < viewport.Height / 2)
            {
                centerView.Y = viewport.Height / 2;
            }

            transform = Matrix.CreateTranslation(new Vector3(-centerView.X + viewport.Width / 2, -centerView.Y + viewport.Height / 2, 0));

        }

        #endregion
    }
}
