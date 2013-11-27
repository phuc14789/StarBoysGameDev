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
    class Effect:Animation
    {
        public Effect(Texture2D texture, Vector2 position, int currentFrame, int rows, int columns, float delay, float scale)
            : base(texture, position, currentFrame, rows, columns, delay, scale) { }

    }
}
