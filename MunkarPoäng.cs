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
        //Samuel har gjort det här
        public MunkarPoäng(Texture2D texture, Vector2 position) : base(texture)
        {
            this.texture = texture;
            this.position = position;

        }

        //Kollar om playern intersectar med Munken och då ger playern +1 munk.
        public override void Update(Player player, GameTime gameTime)
        {

            if (ObjectHitbox.Intersects(player.PlayerHitbox))
            {
                player.munkar++;
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position,Color.White);
        }
    }
}
