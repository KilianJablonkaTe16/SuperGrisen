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

    class Pausemenu :SuperMenu
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

                    //Nedan ändrar if-satserna på gamestatsen beroende på vilken knapp man trycker på. 

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


            usingKeys(2);
           
            //När den första/övre knappen är markerad och man trycker på enter går man till startmenyn och lämnar spelet.
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && valdKnapp == 0)
            {
                //Innan if-satsenretunerar sitt värde nollstänner den alla knappar i pausmenyn
                valdKnapp = -1;
                gammalValdKnapp = -1;
                buttonLista[0].Update(ButtonLook.normalButton);
                buttonLista[1].Update(ButtonLook.normalButton);
                return Gamestates.startmenu;
            }

            //När den andra/undre knappen är markerad och man trycker på enter går man tillbaka in i spelet.
            else if (Keyboard.GetState().IsKeyDown(Keys.Enter) && valdKnapp == 1)
            {
                //Innan if-satsenretunerar sitt värde nollstänner den alla knappar i pausmenyn
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
