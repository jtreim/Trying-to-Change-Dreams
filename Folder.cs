using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame3
{
    public class Folder
    {
        public Vector2 position;
        public Texture2D texture;
        public Random random = new Random();
        public Rectangle boundingBox;
        public double xSpeed;
        public double ySpeed;
        public bool isHeld;
        public bool wasHeld;

        public Folder(Texture2D pic, Vector2 pos)
        {
            texture = pic;
            position = pos;
            xSpeed = 0;
            ySpeed = 0;
            isHeld = false;
            wasHeld = false;
        }

        public void Update(GameTime gameTime)
        {
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            if (isHeld == true && wasHeld != true) { wasHeld = true; }
            if (isHeld == false)
            {
                if (position.X < 0) { position.X = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - texture.Width; }
                if (position.X + texture.Width > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width) { position.X = 0; }
                if (position.Y + texture.Height > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height) { position.Y = 0; }
                if (position.Y < 0) { position.Y = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - texture.Height; }

            }

            if (wasHeld == true && isHeld == false)
            {
                xSpeed = xSpeed * .5;
                ySpeed = ySpeed * .5;

                position.X += (float)xSpeed;
                position.Y += (float)ySpeed;
            }
        }

        public void Draw(SpriteBatch Spritebatch)
        {
            Spritebatch.Draw(texture, position, Color.White);
        }
    }

 
}
