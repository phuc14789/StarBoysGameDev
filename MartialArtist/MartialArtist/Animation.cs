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
    public abstract class Animation
    {
        //2. khai bao bien dung de tao Image
        public Texture2D _t_Image;

        //3. khai bao bien dung de xac dinh toa do
        public Vector2 _vt2_position;

        //6.7 khai bao bien hang va cot
        protected int _i_Rows;
        protected int _i_Columns;

        //8.9 khai bao bien hinh anh tong va hien tai
        protected int _i_totalFrame;
        protected int _i_currentFrame;

        //10.11 khai bao chieu cao va chieu rong
        public int _i_width;
        protected int _i_heigth;

        //12.13 khai bao bien khung hinh va diem den cua khung hinh
        protected Rectangle _rect_sourceRectangle;
        //private Rectangle _rect_destinationRectangle;

        protected float _f_elapse;
        //Speed of frame
        protected float _f_delay;

        //Scale of image
        protected float _f_scale;    

        ///khai bao ham khoi tao voi 4 bien so
        public Animation(Texture2D texture, Vector2 position, int currentFrame, int rows, int columns,float delay, float scale)
        {
            //Initialize variable
            _t_Image = texture;
            _i_Rows = rows;
            _i_Columns = columns;
            _i_currentFrame = currentFrame;
            _f_delay = delay;
            _f_scale = scale;
            _vt2_position = position;

            //Calculate variable
            _i_totalFrame = _i_Rows * _i_Columns;
            _i_width = _t_Image.Width / _i_Columns;
            _i_heigth = _t_Image.Height / _i_Rows;

            animationCharacter();
        }

        ///khai bao ham Update
        public virtual void Update(GameTime gameTime)
        {
            //Moving frame of character
            _f_elapse += (float)gameTime.ElapsedGameTime.Milliseconds;
            if (_f_elapse >= _f_delay)
            {
                if (_i_currentFrame >= _i_totalFrame -1)
                    _i_currentFrame = 0;
                else
                    _i_currentFrame++;
                _f_elapse = 0;
            }
            //animationCharacter();
            //_rect_destinationRectangle = new Rectangle((int)_vt2_position.X, (int)_vt2_position.Y, _i_width, _i_heigth); -->Khong can?
        }

        public void animationCharacter()
        {
            //Calculate current frame
            int row = (int)((float)_i_currentFrame / (float)_i_Columns);
            int column = _i_currentFrame % _i_Columns;

            //Update frame
            _rect_sourceRectangle = new Rectangle(_i_width * column, _i_heigth * row, _i_width, _i_heigth);
        }

        ///khai bao ham Draw
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_t_Image, _vt2_position, _rect_sourceRectangle, Color.White, 0f, Vector2.Zero, _f_scale, SpriteEffects.None, 0f);
        }
    }
}

