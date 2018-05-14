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
    enum ButtonLook { normalButton, lookingButton, clickingButton}

    class SuperButtons
    {
        protected Texture2D normalButtonTexture, whichButtonTexture, lookingButtonTexture;
        protected Vector2 buttonPotisiton;
        public MouseState nowMousestate, lastmousestate;
        public ButtonLook mouseButtonLook = new ButtonLook();
        protected string keyButtonLook = "";

        public SuperButtons(Texture2D normalButtonTexture, Texture2D lookingButtonTexture, Vector2 position)
        {
            this.normalButtonTexture = normalButtonTexture;
            this.lookingButtonTexture = lookingButtonTexture;
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

                if (mouseButtonLook == ButtonLook.lookingButton && nowMousestate.LeftButton == ButtonState.Pressed && nowMousestate.LeftButton != lastmousestate.LeftButton)
                {
                    return ButtonLook.clickingButton;
                }
                lastmousestate = nowMousestate;
                return ButtonLook.lookingButton;
            }
            lastmousestate = nowMousestate;
            return ButtonLook.normalButton;
        }

        public void Update(ButtonLook thisButtonInFocus)
        {
            mouseButtonLook = thisButtonInFocus;

            if (mouseButtonLook == ButtonLook.lookingButton)
            {
                whichButtonTexture = lookingButtonTexture;
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
