using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpringandeGris
{
    //Allt i denna klass gjordes av Kilian
    /*
     * De flesta sakerna som finns i denna klass bekrivs redan i startmenyn klassen, det ända som varierar.
     * Det ända som varierar är att det skapas inte lika många knappar och att knapparna retunerar andra värden när man trycker på dem.
     * Så de sakrena som inte är kommenterade i denna klass kommer vara komenterade i Startmenu klassen om kommentarer behövs.
    */

    class Shopmenu :SuperMenu
    {
        //Två variabler som behövs för att skriva ut hur många munkar och liv man har i shopmenyn.
        protected int shopMunkar, upgradeHealth;

        //Konstruktorn
        public Shopmenu(Texture2D shopmenuTexture, Texture2D buyButton, Texture2D buyButtonActive, Texture2D backButton, Texture2D backButtonActive)
        {
            buttonLista.Add(new SuperButtons(buyButton, buyButtonActive, new Vector2(50, 300)));
            buttonLista.Add(new SuperButtons(backButton, backButtonActive, new Vector2(50, 450)));
            menuTexture = shopmenuTexture;
        }

        public Gamestates Update(Player player)
        {
            //Shopmeny variabler får deras startvärde från player klassen.
            shopMunkar = player.munkar;
            upgradeHealth = player.health;

            // Vad metoden gör beskirvs i SuperMenus.
            GettingNewValues();

            if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                keysUsed = true;
            }

            foreach (SuperButtons shopButton in buttonLista)
            {
                if (nowMouseState.Position != lastMouseState.Position)
                {
                    keysUsed = false;
                }

                if (keysUsed == false)
                {
                    shopButton.MouseOnButton();

                    // Vad metoden gör beskrivs i SuperMenus klassen.
                    ButtonListForloop();

                    if (buttonLista[1].MouseOnButton() == ButtonLook.clickingButton)
                    {
                        return Gamestates.startmenu;
                    }

                    //If-satsen gör så att man inte kan köpa något mer när man inte har liräkligt med munkar.
                    if (shopMunkar >= 50)
                    {
                        //If-satsen gör så att man köper ett till liv när man tycker på "köp" knappen i shopmenyn.
                        if (buttonLista[0].MouseOnButton() == ButtonLook.clickingButton && lastMouseState != nowMouseState && lastMouseState.Position == nowMouseState.Position)
                        {
                            player.health++;
                            player.munkar -= 50;

                            //En variabel som skriver ut hur många liv man har i shopmenyn.
                            upgradeHealth++;
                        }
                    }

                    lastMouseState = nowMouseState;
                }
            }


            if (FirtButtonActive() == true)
            {
                valdKnapp++;
                buttonLista[valdKnapp].Update(ButtonLook.lookingButton);
                buttonLista[1].Update(ButtonLook.normalButton);
            }

            if (ClickCombo(nowButtonState, lastButtonState) == ClickCombos.up && valdKnapp >= 0)
            {
                buttonLista[valdKnapp].Update(ButtonLook.normalButton);
                valdKnapp--;

                if (valdKnapp == -1)
                    valdKnapp++;

                buttonLista[valdKnapp].Update(ButtonLook.lookingButton);
            }

            if (ClickCombo(nowButtonState, lastButtonState) == ClickCombos.down && valdKnapp <= 2 && gammalValdKnapp != -1)
            {
                buttonLista[valdKnapp].Update(ButtonLook.normalButton);
                valdKnapp++;

                if (valdKnapp == 2)
                    valdKnapp--;

                buttonLista[valdKnapp].Update(ButtonLook.lookingButton);
            }

            //If-satsen gör så att man inte kan köpa något mer när man inte har liräkligt med munkar.
            if(shopMunkar >= 50)
            {
                //If-satsen gör så att man köper ett till liv när man tycker på "köp" knappen i shopmenyn.
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && valdKnapp == 0 && lastButtonState != nowButtonState)
                {
                    player.health++;
                    player.munkar -= 50;

                    //En variabel som skriver ut hur många liv man har i shopmenyn.
                    upgradeHealth++;
                }
            }
            // Nedan ändras gamstatsen beroende på vilken knapp man "aktiverar"
            #region Gamestates retunering
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && valdKnapp == 1 && lastButtonState != nowButtonState)
            {
                ResetingButtos();
                return Gamestates.startmenu;
            } 
            
            else
            {
                lastButtonState = nowButtonState;
                return Gamestates.shopmenu;
            }
            #endregion

        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont buyJump)
        {
            spriteBatch.Draw(menuTexture, Vector2.Zero, Color.White);

            foreach (SuperButtons pauseButton in buttonLista)
            {
                pauseButton.Draw(spriteBatch);
            }

            //Skriver ut hur många munkar man har.
            spriteBatch.DrawString(buyJump, "Munkar: " + shopMunkar.ToString(), new Vector2(500, 200), Color.White);

            //Skriver ut hur många liv man har.
            spriteBatch.DrawString(buyJump, "Health: " + upgradeHealth.ToString(), new Vector2(500, 320), Color.White);
        }

    }
}
