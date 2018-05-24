using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringandeGris
{
    //Samuel har gjort allt det här förutom där det står att kilian har gjort något.
    class FlyingObjects:ObjektBasklassen
    {
        //<-- Kilian -->
        Vector2 center;
        //<-- Kilian -->
        float rotation;
        public FlyingObjects(Texture2D texture, Vector2 position):base(texture)
        {
            this.texture = texture;
            this.position = position;
            velocity = new Vector2(-10, 0);
            //<-- Kilian har gjort det här -- >
            center = new Vector2(texture.Height / 2, texture.Width / 2);
            //<-- -->
        }

        public override void Update(Player player, GameTime gameTime)
        {

            rotation -= MathHelper.TwoPi / -50f;
            position += velocity;

            if (ObjectHitbox.Intersects(player.PlayerHitbox))
            {
                removeMe = true;
                if (player.ärodödlig == false)
                {
                    //Playern tar 1 damage
                    player.health--;
                    //Timern till hur länge man är odödlig sätts till fem sekunder
                    player.timer = 5000;
                    //Sätter så att man är odödlig
                    player.ärodödlig = true;
                }
            }
        }





        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, rotation, center, 1, SpriteEffects.None, 1);
        }

    }
}
