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
    class Enemy
    {
        private SpriteBatch _p_SpriteBatch;                             // Dùng để load hình ảnh
        private GraphicsDevice _g_GraphicsDevice;                       // GraphicsDevice

        private Texture2D _t_Enemy;                                     // Texture để load Enemy    Có thế kế thừa từ Animation
        private Vector2 _vt2_PositionEnemy;                             // Vị trí Enemy

        private int  _f_Speed;                                          // Tốc độ Enemy
        private bool _b_Life;                                           // Mạng sống của Enemy
        private int _i_Damage;                                          // Sức mạnh của Enemy
        private int _i_Heatlh;                                          // Máu của Enemy

        private Random _r_Random;                                       // Cho Enemy xuất hiện ngẫu nhiên

        public GraphicsDevice G_GraphicsDevice
        {
            get { return _g_GraphicsDevice; }
            set { _g_GraphicsDevice = value; }
        }

        public SpriteBatch P_SpriteBatch
        {
            get { return _p_SpriteBatch; }
            set { _p_SpriteBatch = value; }
        }

        public Texture2D T_Enemy
        {
            get { return _t_Enemy; }
            set { _t_Enemy = value; }
        }

        public Vector2 Vt2_PositionEnemy
        {
            get { return _vt2_PositionEnemy; }
            set { _vt2_PositionEnemy = value; }
        }

        public int F_Speed
        {
            get { return _f_Speed; }
            set { _f_Speed = value; }
        }

        public bool B_Life
        {
            get { return _b_Life; }
            set { _b_Life = value; }
        }

        public int I_Damage
        {
            get { return _i_Damage; }
            set { _i_Damage = value; }
        }

        public int I_Heatlh
        {
            get { return _i_Heatlh; }
            set { _i_Heatlh = value; }
        }

        public Enemy(Texture2D enemy)           
        {
            this._t_Enemy = enemy;
        }

        /// <summary>
        /// Khởi tạo cho Enemy
        /// </summary>
        public void Initialize()
        {
            _r_Random = new Random();
            // Vị trí để Enemy xuất hiện                                                             
            _vt2_PositionEnemy = new Vector2( _r_Random.Next(200, 2500), _r_Random.Next(200, 300) );

            // Trạng thái Enemy còn sống           
            _b_Life = true;

            // Máu Enemy                                                                                 
            _i_Heatlh = 100;

            // Sức mạnh Enemy                                                                                
            _i_Damage = 100;                                                                                                  

        }
        
        /// <summary>
        /// Load Enemy
        /// </summary>
        /// <param name="Content">Khởi tạo cho graphicsDevice</param>
        public void LoadContent(ContentManager Content)
        {
            _t_Enemy = Content.Load<Texture2D>("Enemy");                                                                // Load hình ảnh Enemy
           
        }

        public void Update(GameTime gameTime)
        {
            // Gọi hàm Animation từ class Animation

        }

        public void Draw(SpriteBatch spriteBatch)
        {          
            spriteBatch.Draw(_t_Enemy, Vector2.Zero, Color.White);
        }
    }
}
