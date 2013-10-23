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
        private Texture2D _t_menuBackground;

        public void LoadContent(ContentManager Content)
        {
            _t_menuBackground = Content.Load<Texture2D>("Images/Background");            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_t_menuBackground, new Rectangle(0, 0, 800, 480), Color.White);     
        }
    }
}
