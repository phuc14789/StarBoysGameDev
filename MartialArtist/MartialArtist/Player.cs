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
    class Player : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Animation _Animation;
        Vector2 _vt2_position;
        Texture2D _t2_Player;
        GraphicsDeviceManager graphics;

        public Player(Game game)
            : base(game)
        {
            graphics = new GraphicsDeviceManager(game);
        }

        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here


            _Animation.Update(gameTime);
            characterControl();
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {

            _t2_Player = Game.Content.Load<Texture2D>("Player");
            
            _Animation = new Animation(_t2_Player, 0, 2, 4,50f);
            _Animation.Vt2_position = _vt2_position;
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            base.LoadContent();     // Load từ cha
        }

        public void characterControl()
        {
            // Allows the game to exit

           
            KeyboardState key = Keyboard.GetState();

            if (key.IsKeyDown(Keys.W))
            {
                _vt2_position.Y -= 3;
                if (_vt2_position.Y < 0)
                    _vt2_position.Y = 0;
            }
            if (key.IsKeyDown(Keys.S))
            {
                _vt2_position.Y += 3;
                if (_vt2_position.Y + _t2_Player.Height > graphics.PreferredBackBufferHeight)
                    _vt2_position.Y = graphics.PreferredBackBufferHeight - _t2_Player.Height;
            }
            if (key.IsKeyDown(Keys.A))
            {
                _vt2_position.X -= 3;
                if (_vt2_position.X < 0)
                    _vt2_position.X = 0;
            }
            if (key.IsKeyDown(Keys.D))
            {
                _vt2_position.X += 3;
                if (_vt2_position.X + _t2_Player.Width > graphics.PreferredBackBufferWidth)
                    _vt2_position.X = graphics.PreferredBackBufferWidth - _t2_Player.Width;

            }
        }

        public override void Draw(GameTime gameTime)
        {
           
            _Animation.Draw(spriteBatch);
            
            base.Draw(gameTime);

        }
    }
}
