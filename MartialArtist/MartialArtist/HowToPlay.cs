using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace MartialArtist
{
    class HowToPlay
    {

        private Texture2D _t_HowToPlay;
        public Button backButton;
        MouseState mouse;
        Rectangle rect_mouse;


        public void LoadContent(ContentManager Content)
        {
            _t_HowToPlay = Content.Load<Texture2D>("Images/Background/Option_menu");
            //Create button
            backButton = new Button(0.7f);
        }

        public void Update(GameTime gameTime, ContentManager Content)
        {
            mouse = Mouse.GetState();
            rect_mouse = new Rectangle(mouse.X, mouse.Y, 1, 1);


            if (rect_mouse.Intersects(backButton.rect_button))
            {
                backButton.Update(gameTime, Content.Load<Texture2D>("Images/Background/Back"), new Vector2(700, 450));
                if (mouse.LeftButton == ButtonState.Pressed) backButton.isClicked = true;
            }
            else
            {
                backButton.Update(gameTime, Content.Load<Texture2D>("Images/Background/Back"), new Vector2(700, 450));
                backButton.isClicked = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_t_HowToPlay, new Rectangle(0, 0, 960, 576), Color.White);

            backButton.Draw(spriteBatch);
            spriteBatch.End();
        }



    }
}
