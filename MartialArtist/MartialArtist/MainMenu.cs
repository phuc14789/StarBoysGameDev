using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MartialArtist
{
    class MainMenu
    {
        private Texture2D _t_menuBackground;
        public Button playButton;
        public Button exitButton;
        Rectangle rect_mouse;
        MouseState mouse;

        public void LoadContent(ContentManager Content)
        {
            _t_menuBackground = Content.Load<Texture2D>("Images/Background/Background");

            //Create button
            playButton = new Button(1f);
            exitButton = new Button(1f);
        }

        public void Update(GameTime gameTime, ContentManager Content)
        {
            mouse = Mouse.GetState();
            //create mouse rectangle
            rect_mouse = new Rectangle(mouse.X, mouse.Y, 1, 1);

            //Check collision between mouse and button, then load new button image
            if (rect_mouse.Intersects(playButton.rect_button))
            {
                playButton.Update(gameTime, Content.Load<Texture2D>("Images/Button/play"), new Vector2(300, 120));
                if (mouse.LeftButton == ButtonState.Pressed) playButton.isClicked = true;
            }
            else
            {
                playButton.Update(gameTime, Content.Load<Texture2D>("Images/Button/play" + "_click"), new Vector2(300, 120));
                playButton.isClicked = false;
            }

            if (rect_mouse.Intersects(exitButton.rect_button))
            {
                exitButton.Update(gameTime, Content.Load<Texture2D>("Images/Button/exit"), new Vector2(300, 200));
                if (mouse.LeftButton == ButtonState.Pressed) exitButton.isClicked = true;
            }
            else
            {
                exitButton.Update(gameTime, Content.Load<Texture2D>("Images/Button/exit" + "_click"), new Vector2(300, 200));
                exitButton.isClicked = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_t_menuBackground, new Rectangle(0, 0, 800, 480), Color.White);
            playButton.Draw(spriteBatch);
            exitButton.Draw(spriteBatch);
        }
    }
}
