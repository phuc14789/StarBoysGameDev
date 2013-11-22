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
    public class MainGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        MainMenu mainMenu;
        LevelManager levelManager;
        HowToPlay howtoPlay;
        //Begin gameState, begin with Main Menu
        GameState currentGameMenu = GameState.MainMenu;
        SoundEffect MenuSong;
        SoundEffectInstance MenuSongInstance;
        SoundEffect MainSong;
        SoundEffectInstance MainSongInstance;
        //States for game
        enum GameState
        {
            MainMenu,
            HowToPlay,
            Sorce,
            About,
            Help,
            Playing,
            GameOver,
            Exit,
        }

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //Screen size
            graphics.PreferredBackBufferWidth = 960;
            graphics.PreferredBackBufferHeight = 576;

            //Add Mouse
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //Create MainMenu Object
            mainMenu = new MainMenu();
            howtoPlay = new HowToPlay();
            howtoPlay.LoadContent(Content);
            levelManager = new LevelManager(this, Content);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            MenuSong = Content.Load<SoundEffect>("Sounds/Menu");
            MenuSongInstance = MenuSong.CreateInstance();
            MainSong = Content.Load<SoundEffect>("Sounds/MainTheme5");
            MainSongInstance = MainSong.CreateInstance();
            mainMenu.LoadContent(Content);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            switch (currentGameMenu)
            {
                //MainMenu State
                case GameState.MainMenu:

                    mainMenu.Update(gameTime, Content);
                    howtoPlay.Update(gameTime, Content);
                    if (Global.music == true)
                    {
                        MenuSongInstance.Volume = 1f;
                        MenuSongInstance.Play();
                    }
                    else
                    {
                        MenuSongInstance.Stop();
                    }
                    ////Change gameState to HowToPlay when button is clicked
                    if (mainMenu.howtoplayButton.isClicked) { currentGameMenu = GameState.HowToPlay; }
                    ////Change gameState to About when button is clicked
                    if (mainMenu.aboutButton.isClicked) { currentGameMenu = GameState.About; }
                    //Change gameState to Playing when button is clicked
                    if (mainMenu.playButton.isClicked) { currentGameMenu = GameState.Playing; }

                    //Change gameState to Playing when button is clicked
                    if (mainMenu.exitButton.isClicked) { currentGameMenu = GameState.Exit; }
                    break;

                //HowToPlay State
                case GameState.HowToPlay:
                    howtoPlay.Update(gameTime, Content);
                    if (howtoPlay.backButton.isClicked)
                    {
                        currentGameMenu = GameState.MainMenu;
                    }
                    break;

                //Playing State
                case GameState.Playing:
                    MenuSongInstance.Stop();
                    MainSongInstance.Volume = 0.7f;
                    MainSongInstance.Play();
                    if (!levelManager.GameOver)
                        levelManager.Update(gameTime);
                    else
                        currentGameMenu = GameState.Exit;
                    break;

                //Exit State
                case GameState.Exit:
                    this.Exit();
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            switch (currentGameMenu)
            {
                //MainMenu State
                case GameState.MainMenu:
                    //Draw Main Menu
                    mainMenu.Draw(spriteBatch);
                    break;

                //HowToPlay State
                case GameState.HowToPlay:
                    //Draw Main Menu
                    howtoPlay.Draw(spriteBatch);
                    break;

                //Playing State
                case GameState.Playing:
                    levelManager.Draw(spriteBatch);
                    break;

                //Exit State
                case GameState.Exit:
                    break;
            }

            base.Draw(gameTime);
        }
    }
}
