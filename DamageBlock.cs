using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringandeGris
{

    //Samuel har gjort det här
    class DamageBlock : ObjektBasklassen
    {
        public DamageBlock(Texture2D texture, Vector2 position) : base(texture)
        {
            this.texture = texture;
            this.position = position;

        }

        public override void Update(Player player, GameTime gameTime)
        {
            
            if (ObjectHitbox.Intersects(player.PlayerHitbox) && hitboxes == Hitboxes.Up)
            {

                player.harhoppat = false;
                player.position.Y = ObjectHitbox.Location.Y - player.texture.Height;

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


            
            else if (ObjectHitbox.Intersects(player.PlayerHitbox) && hitboxes == Hitboxes.Down)
            {

                player.position.Y = ObjectHitbox.Location.X + player.PlayerHitbox.Height;
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


            else if (ObjectHitbox.Intersects(player.PlayerHitbox) && hitboxes == Hitboxes.Left)
            {

                player.position.X = ObjectHitbox.Location.X - player.PlayerHitbox.Width;
                
                //Playern tar 1 damage;
                if (player.ärodödlig == false)
                {
                    player.health--;
                    player.timer = 5000;
                    player.ärodödlig = true;
                }
              
                
                 
                    
              
                
            }


            else if (ObjectHitbox.Intersects(player.PlayerHitbox) && hitboxes == Hitboxes.Right)
            {
                player.position.X = ObjectHitbox.Location.X + player.PlayerHitbox.Width;
                player.harhoppat = true;
                player.health--;
                if (player.ärodödlig == false)
                {
                    player.health--;
                    player.timer = 5000;
                    player.ärodödlig = true;
                }

            }


        }



        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, position, Color.White);
        }

    }
}
