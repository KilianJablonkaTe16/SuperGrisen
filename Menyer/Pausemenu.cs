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
    class Pausemenu:SuperMenu
    {
        //Konstruktorn
        public Pausemenu(Texture2D pausemenuTexture, Texture2D resumeButton, Texture2D resumeButtonActive, Texture2D leaveButton,Texture2D leaveButtonActive)
        {
            buttonLista.Add(new SuperButtons(leaveButton, leaveButtonActive, new Vector2(400, 100)));
            buttonLista.Add(new SuperButtons(resumeButton, resumeButtonActive, new Vector2(400, 250)));

            menuTexture = pausemenuTexture;
        }


        public Gamestates Update()
        {
            // Vad metoden gör beskirvs i SuperMenus.
            GettingNewValues();


            if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                keysUsed = true;
            }

            foreach (SuperButtons pauseButton in buttonLista)
            {
                if (nowMouseState.Position != lastMouseState.Position)
                {
                    keysUsed = false;
                }

                if (keysUsed == false)
                {
                    pauseButton.MouseOnButton();

                    // Vad metoden gör beskrivs i SuperMenus klassen.
                    ButtonListForloop();

                    //Nedan ändrar if sattserna på gamestatsen beroende på vilken knapp man trycker på. 

                    if (buttonLista[0].MouseOnButton() == ButtonLook.clickingButton)
                    {
                    
                        return Gamestates.startmenu;
                    }

                    if (buttonLista[1].MouseOnButton() == ButtonLook.clickingButton)
                    {
                        return Gamestates.inGame;
                    }

                    lastMouseState = nowMouseState;
                }
            }



            // Markerar första knappen när man kommer in i pausskärmen. 
            if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.Down) && valdKnapp == -1 && gammalValdKnapp == -1)
            {
                valdKnapp = 0;
                buttonLista[0].Update(ButtonLook.lookingButton);
                buttonLista[1].Update(ButtonLook.normalButton);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up) && valdKnapp == 1 && lastButtonState != nowButtonState)
            {
                valdKnapp = 0;
                buttonLista[0].Update(ButtonLook.lookingButton);
                buttonLista[1].Update(ButtonLook.normalButton);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down) && valdKnapp == 0 && gammalValdKnapp != -1 && lastButtonState != nowButtonState)
            {
                valdKnapp = 1;
                buttonLista[1].Update(ButtonLook.lookingButton);
                buttonLista[0].Update(ButtonLook.normalButton);
            }

            lastButtonState = nowButtonState;

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && valdKnapp == 0)
            {
                valdKnapp = -1;
                gammalValdKnapp = -1;
                buttonLista[0].Update(ButtonLook.normalButton);
                buttonLista[1].Update(ButtonLook.normalButton);
                return Gamestates.startmenu;
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Enter) && valdKnapp == 1)
            {
                valdKnapp = -1;
                gammalValdKnapp = -1;
                buttonLista[0].Update(ButtonLook.normalButton);
                buttonLista[1].Update(ButtonLook.normalButton);
                return Gamestates.inGame;
            }

            else
            {
                return Gamestates.pausemenu;
            }

        }
    }
}
