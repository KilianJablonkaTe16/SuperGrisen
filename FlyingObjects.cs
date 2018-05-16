using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringandeGris
{
    class FlyingObjects:ObjektBasklassen
    {
        //Samuel har gjort det här
        Vector2 center;
        float rotation;
        public FlyingObjects(Texture2D texture, Vector2 position):base(texture)
        {
            this.texture = texture;
            this.position = position;
            velocity = new Vector2(-10, 0);
            center = new Vector2(texture.Height / 2, texture.Width / 2);
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





            ////Ändrar på playerns position när den träffar översidan av ett objekt
            //if (ObjectHitbox.Intersects(player.PlayerHitbox) && hitboxes == Hitboxes.Up)
            //{


            //    player.harhoppat = false;
               
            //    if (player.ärodödlig == false)
            //    {
            //        //Playern tar 1 damage
            //        player.health--;
            //        player.timer = 1000;

            //    }

                
            //}

            ////Ändrar på playerns position när den träffar undersidan av ett objekt
            //else if (ObjectHitbox.Intersects(player.PlayerHitbox) && hitboxes == Hitboxes.Down)
            //{

                 
            //    if (player.ärodödlig == false)
            //    {
            //        //Playern tar 1 damage
            //        player.health--;
            //        player.timer = 1000;
            //    }
            //}
            //else if (ObjectHitbox.Intersects(player.PlayerHitbox) && hitboxes == Hitboxes.Left)
            //{

                
            //    player.harhoppat = true;
            //    //Playern tar 1 damage;
            //    player.health--;
            //    if (player.ärodödlig == false)
            //    {

            //        player.timer = 1000;
            //    }

            //}
            //else if (ObjectHitbox.Intersects(player.PlayerHitbox) && hitboxes == Hitboxes.Right)
            //{
                
            //    player.harhoppat = true;
            //    player.health--;
            //    if (player.ärodödlig == false)
            //    {
            //        player.timer = 1000;
            //    }

            //}
        }





        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, rotation, center, 1, SpriteEffects.None, 1);
        }

    }
}
