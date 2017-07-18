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
    public class Window1
    {
        public Vector2 p;
        public Texture2D t;
        public Random rand = new Random();
        public Rectangle boundingBox;
        public double xSpeed;
        public double ySpeed;
        public bool isHeld;
        public bool wasHeld;
        public double next_animate;
        public Folder folder;
        private const int foldXRatio = 35;
        private const int foldYRatio = 180;

        public Window1(Texture2D pic, Vector2 pos, Texture2D folderPic)
        {
            xSpeed = rand.Next(-5, 5);
            ySpeed = rand.Next(-5, 5);
            p = pos;
            t = pic;
            isHeld = false;
            wasHeld = false;
            next_animate = rand.Next(0, 2);
            if (folderPic != null)
            {
                folder = new Folder(folderPic, new Vector2(p.X + foldXRatio, p.Y + foldYRatio));
            }
            else
            {
                folder = null;
            }
        }

        public void Update(GameTime gameTime)
        {
            //Bounding box for being held. Box within which hand can hold windows with.
            boundingBox = new Rectangle((int)p.X, (int)p.Y, t.Width, t.Height);
            if (isHeld == true && wasHeld != true) { wasHeld = true; }
            if (isHeld == false)
            {
                if (p.X < 0) 
                { 
                    p.X = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - t.Width;
                    if (folder != null)
                    {
                        folder.position.X = foldXRatio + p.X;
                    }
                }
                if (p.X + t.Width > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width) 
                {
                    p.X = 0;
                    if (folder != null)
                    {
                        folder.position.X = foldXRatio + p.X;
                    }
 
                }
                if (p.Y + t.Height > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height) 
                { 
                    p.Y = 0;
                    if (folder != null)
                    {
                        folder.position.Y = foldYRatio + p.Y;
                    }
                }
                if (p.Y < 0) 
                { 
                    p.Y = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - t.Height;
                    if (folder != null)
                    {
                        folder.position.Y = foldYRatio + p.Y;
                    }
                }
            }

           float prev = 0;
           prev += (float)gameTime.TotalGameTime.TotalSeconds;

            if (wasHeld == false)
            {
                p.X += (float)xSpeed;
                p.Y += (float)ySpeed;
            }

            if (folder != null && folder.wasHeld == false)
            {
                folder.position.X += (float)xSpeed;
                folder.position.Y += (float)ySpeed;
            }

            if (prev >= next_animate && wasHeld == false)
            {
                next_animate = prev + rand.Next(0, 2);
                int value = rand.Next(1, 4);
                xSpeed = rand.Next(-2, 2);
                ySpeed = rand.Next(-2, 2);
            }

            if(wasHeld == true && isHeld == false)              
            {
                xSpeed = xSpeed * .96;
                p.X += (float)xSpeed;
                ySpeed = ySpeed * .96;
                p.Y += (float)ySpeed;
            }
        }

        public void Draw(SpriteBatch Spritebatch)
        {
            Spritebatch.Draw(t, p, Color.White);
            if (folder != null)
            {
                Spritebatch.Draw(folder.texture, folder.position, Color.White);
            }
        }
    }
}
