using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringandeGris
{
    class MunkarPoäng : ObjektBasklassen
    {

        public MunkarPoäng(Texture2D texture, Vector2 position) : base(texture)
        {
            this.texture = texture;
            this.position = position;

        }

        public override void Update(Player player, GameTime gameTime)
        {
            if (ObjectHitbox.Intersects(player.PlayerHitbox))
            {
                player.munkar++;
                removeMe = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position,Color.White);
        }
    }
}
