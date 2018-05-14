using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringandeGris
{
    public class Player
    {
        //Gabriel har gjort playern

       public bool harhoppat;
       public Texture2D texture, whichTexture, crouchTexture, healthTexture;
       public Vector2 position, velocity, gravity;
       float rotation, sonicSpeed;
       public float jumpHeight;
        public int health;
       KeyboardState nowbuttonpressed,lastbuttonpressed;
       public bool ärodödlig = true;
       public int timer;
       public int munkar = 0;
       int xPosition = 0;



        //Konstruktor
        public Player(Texture2D texture, Texture2D crouchTexture, Texture2D healthTexture)
        {
            this.healthTexture = healthTexture;
            this.texture = texture;
            this.crouchTexture = texture;
            position = new Vector2(200, 320);
            velocity = new Vector2(3, 0);
            harhoppat = true;
            whichTexture = texture;
            jumpHeight = -18;
            gravity = new Vector2(0, 0.4f);
            health = 3;
            xPosition = -400;
        }

        public Rectangle PlayerHitbox
        {
            get
            {
                Rectangle playerhitbox = new Rectangle();
                playerhitbox.Location = position.ToPoint();

                playerhitbox.Width = texture.Width;
                playerhitbox.Height = texture.Height;

                return playerhitbox;
            }
        }


        public void Update(GameTime gametime, SoundEffect effect)
        {
            if (timer <= 0)
            {
                ärodödlig = false;
            }
            else
            {
                timer -= gametime.ElapsedGameTime.Milliseconds;
            }

            if (velocity.X >= 20)
            {
                if (velocity.X >= 20)
                    sonicSpeed = -25;

                if (velocity.X >= 30)
                    sonicSpeed = -15;

                if (velocity.X >= 40)
                    sonicSpeed = -5;
                rotation -= MathHelper.TwoPi / sonicSpeed;
            }

            nowbuttonpressed = Keyboard.GetState();

            //Gravitation
            velocity += gravity;
            //Playern rör sig
            position += velocity;


            //gör så att bilden rör sig uppåt när jag håller in space.
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && harhoppat == false && nowbuttonpressed != lastbuttonpressed)
            {
                velocity.Y = jumpHeight;
                position.Y += velocity.Y;
                harhoppat = true;
                effect.Play();             
            }

            if (Keyboard.GetState().IsKeyDown(Keys.K))
                {
                    health = 0;
                }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    whichTexture = crouchTexture;
                    harhoppat = true;
                }
                else
            {
                
                whichTexture = texture;
            }

            lastbuttonpressed = nowbuttonpressed;
        }


    public void Draw(SpriteBatch spriteBatch)
        {
            for(int i = 0; i < health; i++)
            { 
                spriteBatch.Draw(healthTexture, new Vector2(position.X - 400 + i * 100, 10), null, Color.White);
            }
            spriteBatch.Draw(whichTexture, position, null, Color.White);
        }
    }

}
