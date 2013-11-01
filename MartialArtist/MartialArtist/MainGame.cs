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
        Camera camera;
        Player player;
        //Begin gameState, begin with Main Menu
        GameState currentGameMenu = GameState.MainMenu;

        //States for game
        enum GameState
        {
            MainMenu,
            Option,
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
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;

            //Add Mouse
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            //Create MainMenu Object
            mainMenu = new MainMenu();
            camera = new Camera(GraphicsDevice.Viewport);
            player = new Player(Content.Load<Texture2D>("Images/Player/Player_Standing"), new Vector2(320, 200), 100, 3, 0, 1, 8, 50f, 0.8f);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
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
                    camera.Update(gameTime, player);
                    //Change gameState to Playing when button is clicked
                    if (mainMenu.playButton.isClicked) { currentGameMenu = GameState.Playing; }

                    //Change gameState to Playing when button is clicked
                    if (mainMenu.exitButton.isClicked) { currentGameMenu = GameState.Exit; }
                    break;

                //Playing State
                case GameState.Playing:
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
            spriteBatch.Begin(SpriteSortMode.Deferred ,BlendState .AlphaBlend ,null,null,null,null,camera .transform);
            switch (currentGameMenu)
            {
                //MainMenu State
                case GameState.MainMenu:
                    //Draw Main Menu
                    mainMenu.Draw(spriteBatch);
                    break;

                //Playing State
                case GameState.Playing:
                    break;

                //Exit State
                case GameState.Exit:
                    break;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
