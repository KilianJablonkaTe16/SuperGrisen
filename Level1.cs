using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace SpringandeGris
{
     class Level1
    {
        
        static int  timer = 300;
        int positionx = 0;
        List<ObjektBasklassen> groundBlocks = new List<ObjektBasklassen>();
        List<FlyingObjects> flyingObjects = new List<FlyingObjects>();
        List<DamageBlock>  damageBlocks = new List<DamageBlock>();

        public Level1(Player  player, Texture2D groundBlockTexture, Texture2D damageBlockTexture)
        {
           
            for (int i = 0; i < 100; i++)
            {

                //Game1.Objekten.Add(new Block(Game1.objectSprite, new Vector2(player.position.X + Game1.rng.Next(100,10000), Game1.rng.Next(Convert.ToInt32(player.position.X) + player.PlayerHitbox.Height - 50, Convert.ToInt32(player.position.X) + player.PlayerHitbox.Height + 50))));

                groundBlocks.Add(new GroundBlock(groundBlockTexture, new Vector2(positionx, 810)));

                positionx += groundBlockTexture.Width;
            }
            groundBlocks.Add(new GroundBlock(groundBlockTexture, new Vector2(2000, 540)));
            groundBlocks.Add(new GroundBlock(groundBlockTexture, new Vector2(2500, 270)));
            groundBlocks.Add(new GroundBlock(groundBlockTexture, new Vector2(3000, 540)));
            groundBlocks.Add(new GroundBlock(groundBlockTexture, new Vector2(3500, 540)));

            damageBlocks.Add(new DamageBlock(damageBlockTexture, new Vector2(4500, 810 - damageBlockTexture.Height)));
        }


        public Gamestates Update(GameTime gameTime, Player player, SoundEffect effect)
        {

            player.Update(gameTime, effect);



            //Kollar när värdet på timer är mindre än 0 och då lägger ut blocks i random positioner
            //Annars så tar den timerns värde minus hur lång tid som har gått.
            if (timer < 0)
            {
                flyingObjects.Add(new FlyingObjects(Game1.flyingsprite, new Vector2(3000, Game1.rng.Next(100, 300))));
                timer = Game1.rng.Next(3000, 4000);
            }
            else
            {
                timer -= gameTime.ElapsedGameTime.Milliseconds;
            }


            foreach (ObjektBasklassen objekten in groundBlocks)
            {
                objekten.Update(player, gameTime);
                objekten.CheckHitboxes(objekten.DirectionBlockHitbox, player);
            }

            foreach (FlyingObjects flygandeObjekten in flyingObjects)
            {
                flygandeObjekten.Update(player, gameTime);
                flygandeObjekten.CheckHitboxes(flygandeObjekten.DirectionBlockHitbox, player);
            }

            foreach (FlyingObjects flygandeObjekten in flyingObjects)
            {
                if (flygandeObjekten.removeMe == true)
                {
                    groundBlocks.Remove(flygandeObjekten);
                }
            }
            foreach (DamageBlock damageObjekt in damageBlocks)
            {
                damageObjekt.Update(player, gameTime);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                player.position = new Vector2(200, 300);
            }
            //Pausar spelet. 
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                return Gamestates.pausemenu;
            }

            if (player.health <= 0)
            {
                return Gamestates.gameOverMenu;
            }

            else
            {
                return Gamestates.inGame;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (ObjektBasklassen objekten in groundBlocks)
            {
                objekten.Draw(spriteBatch);
            }
            foreach (FlyingObjects flygandeObjekten in flyingObjects)
            {
                flygandeObjekten.Draw(spriteBatch);
            }
            foreach (DamageBlock damageObjekt in damageBlocks)
            {
                damageObjekt.Draw(spriteBatch);
            }
        }

        public void ResetLevel()
        {

        }

    }
}
