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
    class Shopmenu:SuperMenu
    {
        protected int munkar = 100, upgradeHealht;

        //Konstruktorn
        public Shopmenu(Texture2D shopmenuTexture, Texture2D buyButton, Texture2D buyButtonActive, Texture2D backButton, Texture2D backButtonActive)
        {
            buttonLista.Add(new SuperButtons(buyButton, buyButtonActive, new Vector2(50, 300)));
            buttonLista.Add(new SuperButtons(backButton, backButtonActive, new Vector2(50, 450)));
            menuTexture = shopmenuTexture;
        }

        public Gamestates Update(Player player)
        {
            // Vad metoden gör beskirvs i SuperMenus.
            upgradeHealht = player.health;
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

                    //Nedan ändras gamestatens

                    if (buttonLista[1].MouseOnButton() == ButtonLook.clickingButton)
                    {
                        return Gamestates.startmenu;
                    }

                    if(munkar != 0)
                    {
                        if (buttonLista[0].MouseOnButton() == ButtonLook.clickingButton && lastMouseState != nowMouseState && lastMouseState.Position == nowMouseState.Position)
                        {
                            player.health++;
                            munkar -= 50;
                            upgradeHealht++;
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
            if(munkar != 0)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) && valdKnapp == 0 && lastButtonState != nowButtonState)
                {
                    player.health++;
                    munkar -= 50;
                    upgradeHealht++;
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
            spriteBatch.DrawString(buyJump, "Munkar: " + munkar.ToString(), new Vector2(500, 200), Color.White);
            spriteBatch.DrawString(buyJump, "Health: " + upgradeHealht.ToString(), new Vector2(500, 320), Color.White);
        }

    }
}
