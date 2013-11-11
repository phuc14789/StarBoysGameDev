using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MartialArtist
{
    class Camera
    {
        public Matrix transform;
        Viewport view;
        Vector2 centre;

        public Camera(Viewport view)
        {
            this.view = view;
        }

        public void Update(GameTime gameTime, Player player)
        {
            centre = new Vector2(player._vt2_position.X + (player._i_width / 2) - 550, -50);
            transform = Matrix .CreateScale (new Vector3 (1,1,0)) * Matrix .CreateTranslation(new Vector3 (-centre .X,-centre .Y,0));

            if (player._vt2_position.X < 320)
                player._vt2_position.X = 320;

            if (player._vt2_position.X > 1820)
                player._vt2_position.X = 1820;
        }
    }
}
