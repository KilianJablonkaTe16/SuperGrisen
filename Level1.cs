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
        //Samuel har gjort allt det här
        
        static int  timer = 300;

        //Variabel som används för att kunna lägga blocksen/golvet exakt bredvid varandra
        int positionx = 0;
        int positionx2 = 8100;
        int blockTimer = 50;


        //Listor för alla objekt
        Queue<ObjektBasklassen> groundBlocks = new Queue<ObjektBasklassen>();
        List<FlyingObjects> flyingObjects = new List<FlyingObjects>();
        List<DamageBlock>  damageBlocks = new List<DamageBlock>();
        List<MunkarPoäng> munkar = new List<MunkarPoäng>();
        List<Gren> grenar = new List<Gren>();


        //Hela banan som jag sätter ut steg för steg med alla objekt.
        public Level1(Player  player, GameTime gameTime, Texture2D groundBlockTexture, Texture2D damageBlockTexture, Texture2D munkTexture, Texture2D grenTexture, Texture2D flyingObjectTexture)
        {
           
            for (int i = 0; i < 15; i++)
            {

                

                groundBlocks.Enqueue(new GroundBlock(groundBlockTexture, new Vector2(positionx, 810)));

                positionx += groundBlockTexture.Width;
            }


            grenar.Add(new Gren(grenTexture, new Vector2(1000, 625)));
            grenar.Add(new Gren(grenTexture, new Vector2(1500, 625)));
            damageBlocks.Add(new DamageBlock(damageBlockTexture, new Vector2(2500, 810 - damageBlockTexture.Height)));
            damageBlocks.Add(new DamageBlock(damageBlockTexture, new Vector2(3100, 810 - damageBlockTexture.Height)));
            damageBlocks.Add(new DamageBlock(damageBlockTexture, new Vector2(3700, 810 - damageBlockTexture.Height)));
            damageBlocks.Add(new DamageBlock(damageBlockTexture, new Vector2(4300, 810 - damageBlockTexture.Height)));

            groundBlocks.Enqueue(new GroundBlock(groundBlockTexture, new Vector2(4800, 810)));
            groundBlocks.Enqueue(new GroundBlock(groundBlockTexture, new Vector2(5500, 810)));
            munkar.Add(new MunkarPoäng(munkTexture, new Vector2(5750, 500)));
            groundBlocks.Enqueue(new GroundBlock(groundBlockTexture, new Vector2(6100, 810)));
            groundBlocks.Enqueue(new GroundBlock(groundBlockTexture, new Vector2(6700, 600)));
            groundBlocks.Enqueue(new GroundBlock(groundBlockTexture, new Vector2(7400, 600)));
            grenar.Add(new Gren(grenTexture, new Vector2(7800, 400)));
            damageBlocks.Add(new DamageBlock(damageBlockTexture, new Vector2(8400, 810 - damageBlockTexture.Height)));
            grenar.Add(new Gren(grenTexture, new Vector2(8800, 500)));
            damageBlocks.Add(new DamageBlock(damageBlockTexture, new Vector2(8800 + (damageBlockTexture.Width/2)/2, 500 - damageBlockTexture.Height)));
            damageBlocks.Add(new DamageBlock(damageBlockTexture, new Vector2(9200, 810 - damageBlockTexture.Height)));
            damageBlocks.Add(new DamageBlock(damageBlockTexture, new Vector2(9700, 810 - damageBlockTexture.Height)));
            grenar.Add(new Gren(grenTexture, new Vector2(9700 - (grenTexture.Width/2)/2 , (810 - damageBlockTexture.Height) - grenTexture.Height)));



            for (int i = 0; i < 100; i++)
            {

                groundBlocks.Enqueue(new GroundBlock(groundBlockTexture, new Vector2(positionx2, 810)));
                
                positionx2 += groundBlockTexture.Width;
            }

            //Kollar när värdet på timer är mindre än 0 och då lägger ut blocks i random positioner
            //Annars så tar den timerns värde minus hur lång tid som har gått.
            if (timer < 0)
            {
                flyingObjects.Add(new FlyingObjects(flyingObjectTexture, new Vector2(15000, Game1.rng.Next(50, 650))));
                timer = Game1.rng.Next(3000, 4000);
            }
            else
            {
                timer -= gameTime.ElapsedGameTime.Milliseconds;
            }


        }


        public Gamestates Update(GameTime gameTime, Player player, SoundEffect effect)
        {

            player.Update(gameTime, effect);



            


            //<---- Uppdaterar alla objekt genom att gå igenom alla listor av objekten ---->
            foreach (ObjektBasklassen objekten in groundBlocks)
            {
                objekten.Update(player, gameTime);
                objekten.CheckHitboxes(objekten.ObjectHitbox, player);
            }

            foreach (FlyingObjects flygandeObjekten in flyingObjects)
            {
                flygandeObjekten.Update(player, gameTime);
                flygandeObjekten.CheckHitboxes(flygandeObjekten.ObjectHitbox, player);
            }

            foreach (DamageBlock damageObjekt in damageBlocks)
            {
                damageObjekt.Update(player, gameTime);
                damageObjekt.CheckHitboxes(damageObjekt.ObjectHitbox, player);
            }

            for(int i = 0; i < munkar.Count; i++)
            {
                munkar[i].Update(player, gameTime);
                
                if(munkar[i].removeMe == true)
                {
                    munkar.Remove(munkar[i]);
                }
            }

            foreach(Gren gren in grenar)
            {
                gren.Update(player, gameTime);
                gren.CheckHitboxes(gren.ObjectHitbox, player);
            }
            for (int i = 0; i < flyingObjects.Count; i++)
            {
                flyingObjects[i].Update(player, gameTime);
            }

            //Tar bort groundblicken en efter en.
            if(blockTimer <= 0)
            {
                groundBlocks.Dequeue();
                blockTimer = 200;        
            }
            blockTimer--;


            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                player.position = new Vector2(200, 300);
            }

            //Pausar spelet. 
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                return Gamestates.pausemenu;
            }

            if (player.health == 0)
            {
                return Gamestates.gameOverMenu;
            }

            else
            {
                return Gamestates.inGame;
            }

        }

        // <---- Ritar ut alla objekt genom att gå igenom alla listor med dem i ---- >
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
            foreach(MunkarPoäng Munkar in munkar)
            {
                Munkar.Draw(spriteBatch);
            }
            foreach(Gren Grenar in grenar)
            {
                Grenar.Draw(spriteBatch);
            }


        
        }

        public void ResetLevel()
        {

        }

    }
}
