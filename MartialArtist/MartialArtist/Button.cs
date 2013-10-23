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

        public void Update(GameTime gameTime, Texture2D button, Vector2 position)
        {
            this.button = button;
            this.position = position;
            rect_button = new Rectangle((int)position.X, (int)position.Y, button.Width, button.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(button, position, new Rectangle(0, 0, button.Width, button.Height), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
        }
    }
}
