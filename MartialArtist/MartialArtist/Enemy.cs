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
    class Enemy:Animation
    {

        #region Field

        private SpriteBatch _p_SpriteBatch;                             // Dùng để load hình ảnh
        private GraphicsDevice _g_GraphicsDevice;                       // GraphicsDevice

        private Texture2D _t_Enemy;                                     // Texture để load Enemy    Có thế kế thừa từ Animation
        private Vector2 _vt2_PositionEnemy;                             // Vị trí Enemy

        private int  _f_Speed;                                          // Tốc độ Enemy
        private bool _b_Life;                                           // Mạng sống của Enemy
        private int _i_Damage;                                          // Sức mạnh của Enemy
        private int _i_Heatlh;                                          // Máu của Enemy

        private Random _r_Random;                                       // Cho Enemy xuất hiện ngẫu nhiên



        // Các thuộc tính cần phải kết thừa từ Animation

        // _CurrentFrame, _TotalFrame, _Width, _Height, _srcRectangle,_desRectangle, _Timer, _Interal 


        #endregion


        #region Properties

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

        #endregion




        #region Constructor

        public Enemy()
            : base()
        { }


        #endregion

        #region Method


        /// <summary>
        /// Khởi tạo cho Enemy
        /// </summary>
        public void f_Initialize()
        {
            _r_Random = new Random();                                                                                       // Khởi tạo Ramdom
            _vt2_PositionEnemy = new Vector2( _r_Random.Next(200, 2500), _r_Random.Next(200, 300) );                        // Vị trí để Enemy xuất hiện
            _b_Life = true;                                                                                                 // Trạng thái Enemy còn sống
            _i_Heatlh = 100;                                                                                                // Máu Enemy
            _i_Damage = 100;                                                                                                // Sức mạnh Enemy   
 
            // Khởi tạo các biến lấy từ Animation qua
 
        }

        /// <summary>
        /// Load Enemy
        /// </summary>
        /// <param name="Content">Khởi tạo cho graphicsDevice</param>
        public void f_LoadContent(ContentManager Content)
        {
            _t_Enemy = Content.Load<Texture2D>("Enemy");                                                                // Load hình ảnh Enemy
           
        }




        public void f_Update(GameTime gameTime)
        {
            // Gọi hàm Animation từ class Animation

        }


        public void f_Draw(SpriteBatch spriteBatch)
        {
            // 2 biến này lấy từ Animation
            Rectangle dest = new Rectangle() ;
            Rectangle source  = new Rectangle() ;


            spriteBatch.Draw(_t_Enemy, Vector2.Zero, Color.White);

        }

        #endregion








    }
}
