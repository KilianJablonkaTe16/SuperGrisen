using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;

namespace SpringandeGris
{
    class Snow:DownFall
    {
        public Snow(Texture2D snowTexture)
        {
            downFallTexture = snowTexture;
            
            velocity = new Vector2(randomPositionX.Next(-5, -1), randomPositionX.Next(3, 6));

            positionX = randomPositionX.Next(0, 15000);
            positionY = randomPositionY.Next(-1000, -100);
            position = new Vector2(positionX, positionY);
        }
    }
}
