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
        public Button optionButton;
        public Button aboutButton;
        public Button playButton;
        public Button exitButton;
        public Button speakerButton;

        Rectangle rect_mouse;
        MouseState mouse;
        KeyboardState key;
        public void LoadContent(ContentManager Content)
        {
            _t_menuBackground = Content.Load<Texture2D>("Images/Background/Backround_01");
            //Create button
            optionButton = new Button(0.7f);
            aboutButton = new Button(0.7f);
            playButton = new Button(0.7f);
            exitButton = new Button(0.7f);
            speakerButton = new Button(0.7f);
        }

        public void Update(GameTime gameTime, ContentManager Content)
        {
            mouse = Mouse.GetState();
            key = Keyboard.GetState();
            //create mouse rectangle
            rect_mouse = new Rectangle(mouse.X, mouse.Y, 1, 1);
            //Check collision between mouse and button, then load new button image
            ////1
            if (rect_mouse.Intersects(optionButton.rect_button))
            {
                optionButton.Update(gameTime, Content.Load<Texture2D>("Images/Button/Optionbar"), new Vector2(750, 20));
                if (mouse.LeftButton == ButtonState.Pressed) optionButton.isClicked = true;
            }
            else
            {
                optionButton.Update(gameTime, Content.Load<Texture2D>("Images/Button/Optionbar" + "_click"), new Vector2(750, 20));
                optionButton.isClicked = false;
            }
            ////2
            if (rect_mouse.Intersects(aboutButton.rect_button))
            {
                aboutButton.Update(gameTime, Content.Load<Texture2D>("Images/Button/Aboutbar"), new Vector2(750, 160));
                if (mouse.LeftButton == ButtonState.Pressed) aboutButton.isClicked = true;
            }
            else
            {
                aboutButton.Update(gameTime, Content.Load<Texture2D>("Images/Button/Aboutbar" + "_click"), new Vector2(750, 160));
                aboutButton.isClicked = false;
            }
            //3
            if (rect_mouse.Intersects(playButton.rect_button))
            {
                playButton.Update(gameTime, Content.Load<Texture2D>("Images/Button/Playbar"), new Vector2(750, 90));
                if (mouse.LeftButton == ButtonState.Pressed) playButton.isClicked = true;
            }
            else
            {
                playButton.Update(gameTime, Content.Load<Texture2D>("Images/Button/Playbar" + "_click"), new Vector2(750, 90));
                playButton.isClicked = false;
            }
            //4
            if (rect_mouse.Intersects(exitButton.rect_button))
            {
                exitButton.Update(gameTime, Content.Load<Texture2D>("Images/Button/Exitbar"), new Vector2(748, 230));
                if (mouse.LeftButton == ButtonState.Pressed) exitButton.isClicked = true;
            }
            else
            {
                exitButton.Update(gameTime, Content.Load<Texture2D>("Images/Button/Exitbar" + "_click"), new Vector2(748, 230));
                exitButton.isClicked = false;
            }

            //Loa
            if (speakerButton.isClicked == false)
            {
               speakerButton.Update(gameTime, Content.Load<Texture2D>("Images/Button/Speakerbar" + "_click"), new Vector2(820, 500));
                
                if (rect_mouse.Intersects(speakerButton.rect_button))
                {
                    speakerButton.Update(gameTime, Content.Load<Texture2D>("Images/Button/Speakerbar"), new Vector2(820, 500));

                    if (mouse.LeftButton == ButtonState.Pressed) speakerButton.isClicked = true; 
                }                
            }
            else
            {
                speakerButton.Update(gameTime, Content.Load<Texture2D>("Images/Button/Speaker_bar_stop"), new Vector2(820, 500));
                if (rect_mouse.Intersects(speakerButton.rect_button))
                {
                    speakerButton.Update(gameTime, Content.Load<Texture2D>("Images/Button/Speakerbar"), new Vector2(820, 500));

                    if (mouse.LeftButton == ButtonState.Pressed) speakerButton.isClicked = false;
                }  
               
            }
            Console.WriteLine(speakerButton.isClicked);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_t_menuBackground, new Rectangle(0, 0, 960, 576), Color.White);
            optionButton.Draw(spriteBatch);
            aboutButton.Draw(spriteBatch);
            playButton.Draw(spriteBatch);
            exitButton.Draw(spriteBatch);
            speakerButton.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}