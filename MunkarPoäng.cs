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


        }

        public override void Update(Player player, GameTime gameTime)
        {
            //Ändrar på playerns position när den träffar översidan av ett objekt
            if (ObjectHitbox.Intersects(player.PlayerHitbox) && hitboxes == Hitboxes.Up)
            {


                player.harhoppat = false;
                player.position.Y = ObjectHitbox.Location.Y - player.PlayerHitbox.Height;
                if (player.ärodödlig == false)
                {

                    player.timer = 1000;
                    //Playern får 1 munk
                    player.munkar += 1;
                }


            }

            //Ändrar på playerns position när den träffar undersidan av ett objekt
            else if (ObjectHitbox.Intersects(player.PlayerHitbox) && hitboxes == Hitboxes.Down)
            {

                player.position.Y = ObjectHitbox.Location.X + player.PlayerHitbox.Height;
                if (player.ärodödlig == false)
                {

                    player.timer = 1000;
                    //Playern får 1 munk
                    player.munkar += 1;
                }
            }
            else if (ObjectHitbox.Intersects(player.PlayerHitbox) && hitboxes == Hitboxes.Left)
            {

                player.position.X = ObjectHitbox.Location.X - player.PlayerHitbox.Width;
                player.harhoppat = true;

                //Playern får 1 munk
                player.munkar += 1;

                if (player.ärodödlig == false)
                {

                    player.timer = 1000;


                }

            }

        }
    }
}
