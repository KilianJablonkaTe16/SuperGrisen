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
    class DownFall
    {
        protected Random randomPositionX = new Random();
        protected Random randomPositionY = new Random();
        protected Vector2 velocity, position;
        protected Texture2D downFallTexture;
        protected float positionX, positionY;
        public bool removeDownFall = false;

        public void Uptade()
        {
            position += velocity;

            if (position.Y >= 1180)
            {
                removeDownFall = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(downFallTexture, position, Color.White);
        }
    }
}
