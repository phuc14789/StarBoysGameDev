using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MartialArtist
{
    public class Button
    {
        public Texture2D button;
        public Rectangle rect_button;
        public Vector2 position;
        public bool isClicked = false;
        private float _f_scale;

        public Button(float scale)
        {
            this._f_scale = scale;
            //rect_button = new Rectangle((int)position.X, (int)position.Y, (int)(button.Width * this._f_scale), (int)(button.Height * this._f_scale));
        }

        public void Update(GameTime gameTime, Texture2D button, Vector2 position)
        {
            this.button = button;
            this.position = position;
            rect_button = new Rectangle((int)position.X, (int)position.Y, (int)(button.Width * this._f_scale), (int)(button.Height * this._f_scale));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(button, position, new Rectangle(0, 0, button.Width, button.Height), Color.White, 0, Vector2.Zero, _f_scale, SpriteEffects.None, 0);
        }
    }
}
