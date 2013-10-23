using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace MartialArtist
{
    class MainMenu
    {
        private Texture2D menuBackground;

        public void LoadContent(ContentManager Content)
        {
            menuBackground = Content.Load<Texture2D>("Images/Background");            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(menuBackground, new Rectangle(0, 0, 800, 480), Color.White);     
        }
    }
}
