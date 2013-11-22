using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace MartialArtist
{
    class MainMenu
    {
        private Texture2D _t_menuBackground;
        public Button howtoplayButton;
        public Button aboutButton;
        public Button playButton;
        public Button exitButton;
        public Button speakerButton;
        public Button musicButton;

        

        SoundEffect Selectmenu;
        SoundEffectInstance SelectmenuInstance;
        Rectangle rect_mouse;
        MouseState mouse;
        KeyboardState key;
        public void LoadContent(ContentManager Content)
        {
            _t_menuBackground = Content.Load<Texture2D>("Images/Background/Backround_01");
            //Create button
            howtoplayButton = new Button(0.7f);
            aboutButton = new Button(0.7f);
            playButton = new Button(0.7f);
            exitButton = new Button(0.7f);
            speakerButton = new Button(0.7f);
            musicButton = new Button(0.7f);
            Selectmenu = Content.Load<SoundEffect>("Sounds/MenuSelect");
            SelectmenuInstance = Selectmenu.CreateInstance();
        }

        public void Update(GameTime gameTime, ContentManager Content)
        {
            mouse = Mouse.GetState();
            key = Keyboard.GetState();
            //create mouse rectangle
            rect_mouse = new Rectangle(mouse.X, mouse.Y, 1, 1);
            //Check collision between mouse and button, then load new button image
            ////1
            if (rect_mouse.Intersects(howtoplayButton.rect_button))
            {
                howtoplayButton.Update(gameTime, Content.Load<Texture2D>("Images/Button/How To Play_bar_01"), new Vector2(750, 20));
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    SelectmenuInstance.Volume = 0.5f;
                    SelectmenuInstance.Play();
                    howtoplayButton.isClicked = true;
                }


            }
            else
            {
                howtoplayButton.Update(gameTime, Content.Load<Texture2D>("Images/Button/How To Play_bar"), new Vector2(750, 20));
                howtoplayButton.isClicked = false;
            }
            ////2
            if (rect_mouse.Intersects(aboutButton.rect_button))
            {
                aboutButton.Update(gameTime, Content.Load<Texture2D>("Images/Button/Aboutbar"), new Vector2(750, 160));
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    aboutButton.isClicked = true;
                    SelectmenuInstance.Volume = 0.5f;
                    SelectmenuInstance.Play();
                }
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
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    playButton.isClicked = true;
                    SelectmenuInstance.Volume = 0.5f;
                    SelectmenuInstance.Play();
                }
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
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    exitButton.isClicked = true;
                    SelectmenuInstance.Volume = 0.5f;
                    SelectmenuInstance.Play();
                }
            }
            else
            {
                exitButton.Update(gameTime, Content.Load<Texture2D>("Images/Button/Exitbar" + "_click"), new Vector2(748, 230));
                exitButton.isClicked = false;
            }

            //Loa

            if (speakerButton.isClicked == false)
            {
                speakerButton.Update(gameTime, Content.Load<Texture2D>("Images/Button/Speakerbar_click"), new Vector2(850, 500));

                if (rect_mouse.Intersects(speakerButton.rect_button))
                {
                    speakerButton.Update(gameTime, Content.Load<Texture2D>("Images/Button/Speakerbar"), new Vector2(850, 500));

                    if (mouse.LeftButton == ButtonState.Pressed )
                    {
                        speakerButton.isClicked = true;
                        Global.music = false;

                    }
                }
            }
            else
            {
                speakerButton.Update(gameTime, Content.Load<Texture2D>("Images/Button/Speaker_bar_stop"), new Vector2(850, 500));
                if (rect_mouse.Intersects(speakerButton.rect_button))
                {
                    speakerButton.Update(gameTime, Content.Load<Texture2D>("Images/Button/Speaker_bar_stop_01"), new Vector2(850, 500));

                    if (mouse.LeftButton == ButtonState.Pressed )
                    {
                        Global.music = true;
                        speakerButton.isClicked = false;

                    }
                }

            }

            //Music

            if (musicButton.isClicked == false)
            {
                musicButton.Update(gameTime, Content.Load<Texture2D>("Images/Button/Music_bar"), new Vector2(750, 500));

                if (rect_mouse.Intersects(musicButton.rect_button))
                {
                    musicButton.Update(gameTime, Content.Load<Texture2D>("Images/Button/Music_bar_01"), new Vector2(750, 500));

                    if (mouse.LeftButton == ButtonState.Pressed)
                    {
                        musicButton.isClicked = true;

                    }
                }
            }
            else
            {
                musicButton.Update(gameTime, Content.Load<Texture2D>("Images/Button/Music_bar_stop"), new Vector2(750, 500));
                if (rect_mouse.Intersects(musicButton.rect_button))
                {
                    musicButton.Update(gameTime, Content.Load<Texture2D>("Images/Button/Music_bar_stop_01"), new Vector2(750, 500));

                    if (mouse.LeftButton == ButtonState.Pressed)
                    {

                        musicButton.isClicked = false;
                    }
                }

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_t_menuBackground, new Rectangle(0, 0, 960, 576), Color.White);
            howtoplayButton.Draw(spriteBatch);
            aboutButton.Draw(spriteBatch);
            playButton.Draw(spriteBatch);
            exitButton.Draw(spriteBatch);
            speakerButton.Draw(spriteBatch);
            musicButton.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}