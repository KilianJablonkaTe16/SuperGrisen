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
    enum ButtonLook { normalButton, lookingButton, clickingButton, releasedButton}

    //Allt i denna klass gjordes av Kilian


    class SuperButtons
    {
        protected Texture2D normalButtonTexture, whichButtonTexture, HoverButtonTexture;
        protected Vector2 buttonPotisiton;
        public MouseState nowMousestate, lastMousestate;
        public ButtonLook mouseButtonLook = new ButtonLook();
        protected string keyButtonLook = "";
        protected bool wasButtomPressed = false;

        public SuperButtons(Texture2D normalButtonTexture, Texture2D HoverButtonTexture, Vector2 position)
        {
            this.normalButtonTexture = normalButtonTexture;
            this.HoverButtonTexture = HoverButtonTexture;
            whichButtonTexture = normalButtonTexture;
            buttonPotisiton = position;
            mouseButtonLook = ButtonLook.normalButton;
        }


        // Button hitbox.
        public Rectangle ButtonHitbox
        {
            get
            {
                Rectangle hitbox = new Rectangle();
                hitbox.Location = buttonPotisiton.ToPoint();

                hitbox.Width = whichButtonTexture.Width;
                hitbox.Height = whichButtonTexture.Height;

                return hitbox;
            }
        }


        //Muspekarens hitbox. 
        public Rectangle MouseHitbox
        {
            get
            {
                Rectangle hitbox = new Rectangle();
                hitbox.Location = nowMousestate.Position;

                hitbox.Width = 1;
                hitbox.Height = 1;
                return hitbox;
            }
        }
        

        public ButtonLook MouseOnButton()
        {
            nowMousestate = Mouse.GetState();
            mouseButtonLook = ButtonLook.normalButton;

            if (MouseHitbox.Intersects(ButtonHitbox))
            {
                mouseButtonLook = ButtonLook.lookingButton;

                if (mouseButtonLook == ButtonLook.lookingButton && nowMousestate.LeftButton == ButtonState.Pressed)
                {
                    lastMousestate = nowMousestate;
                    wasButtomPressed = true;
                    return ButtonLook.clickingButton;
                }

                if(wasButtomPressed == true && lastMousestate.LeftButton == ButtonState.Released)
                {
                    lastMousestate = nowMousestate;
                    wasButtomPressed = false;
                    return ButtonLook.releasedButton;
                }

                else
                {
                    lastMousestate = nowMousestate;
                    return ButtonLook.lookingButton;
                }                
            }

            else
            {
                lastMousestate = nowMousestate;
                return ButtonLook.normalButton;
            }
        }

        public void Update(ButtonLook thisButtonInFocus)
        {
            mouseButtonLook = thisButtonInFocus;

            if (mouseButtonLook == ButtonLook.lookingButton)
            {
                whichButtonTexture = HoverButtonTexture;
            }

            else
            {
                whichButtonTexture = normalButtonTexture;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(whichButtonTexture, buttonPotisiton, Color.White);
        }

    }
}
