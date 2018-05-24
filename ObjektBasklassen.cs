using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringandeGris
{
    //Samuel har gjort det här 

    public enum Hitboxes { Left, Right, Up, Down }
    public class ObjektBasklassen
    {
        protected Vector2 position, velocity;
        protected Texture2D texture;
        //Instans av hitboxes
        protected Hitboxes hitboxes;
        //När den blir true så ska objektet tas bort från listan(Funkar ej)
        public bool removeMe = false;

        public ObjektBasklassen(Texture2D texture)
        {
            this.texture = texture;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }


        //Skapar hitbox som alla andra subklasser kommer ärva utav.
        public Rectangle ObjectHitbox
        {
            get
            {
                Rectangle objecthitbox = new Rectangle();
                objecthitbox.Location = position.ToPoint();

                objecthitbox.Width = texture.Width;
                objecthitbox.Height = texture.Height;

                return objecthitbox;
            }
        }



        public virtual void Update(Player player, GameTime gameTime)
        {
            //Boolen (harhoppat) är en bool som sätts beroende på vilken sida man är på som t.ex.
            //om man är uppe på ett objekt så sätts den till false och då kan playern fortfarande hoppa.
            //<-- Ändrar på playerns position beroende på vilken sida av objekten playern befinner sig på -->
            if (ObjectHitbox.Intersects(player.PlayerHitbox) && hitboxes == Hitboxes.Up)
            {
                player.position.Y = ObjectHitbox.Location.Y - player.texture.Height;
                player.velocity.Y = 0;
                player.harhoppat = false;
            }

            
            if (ObjectHitbox.Intersects(player.PlayerHitbox) && hitboxes == Hitboxes.Down)
            {
                player.position.Y = ObjectHitbox.Location.Y + ObjectHitbox.Height + player.PlayerHitbox.Height;
            }

            if (ObjectHitbox.Intersects(player.PlayerHitbox) && hitboxes == Hitboxes.Left)
            {
                player.position.X = ObjectHitbox.Location.X - player.PlayerHitbox.Width;
            }

            if (ObjectHitbox.Intersects(player.PlayerHitbox) && hitboxes == Hitboxes.Right)
            {
                player.position.X = ObjectHitbox.Location.X + player.PlayerHitbox.Width;
                player.harhoppat = true;
            }


        }
        //Använder enums för att se vilken sida om objektet som spelaren befinner sig om
        //Skapar även nya rektanglar så att se om man är innuti den rektangeln
        public Hitboxes CheckHitboxes(Rectangle collision, Player player)
        {
            if (player.PlayerHitbox.Intersects(new Rectangle(collision.X + ObjectHitbox.Width, collision.Y, ObjectHitbox.Width, ObjectHitbox.Height)))
            {
                hitboxes = Hitboxes.Right;
                return Hitboxes.Right;
            }

            if (player.PlayerHitbox.Intersects(new Rectangle(collision.X, collision.Y - ObjectHitbox.Height, ObjectHitbox.Width, ObjectHitbox.Height)))
            {
                hitboxes = Hitboxes.Up;
                return Hitboxes.Up;
            }

            if (player.PlayerHitbox.Intersects(new Rectangle(collision.X - ObjectHitbox.Width, collision.Y, ObjectHitbox.Width, ObjectHitbox.Height)))
            {
                hitboxes = Hitboxes.Left;
                return Hitboxes.Left;
            }

            else 
            {
                hitboxes = Hitboxes.Down;
                return Hitboxes.Down;
            }
        }

    }

}