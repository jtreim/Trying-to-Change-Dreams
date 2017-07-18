using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame3
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Random rand = new Random();
        User user = new User();
        List<Window1> dreamList = new List<Window1>();
        List<Folder> folderList = new List<Folder>();
        bool holding_something = false;
        int holdIndex = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = true;
            //graphics.IsFullScreen = false;
            //graphics.PreferredBackBufferHeight = 700;
            //graphics.PreferredBackBufferWidth = 700;
        }

        protected override void Initialize() { base.Initialize(); }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            user.LoadContent(Content);
        }

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.F12) && Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.LeftControl))
                this.Exit();
            foreach (Window1 dream in dreamList) { dream.Update(gameTime); }
            LoadDream();
            user.Update(gameTime);
            carryStuff();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            if (Keyboard.GetState().IsKeyDown(Keys.H) 
                
                || Keyboard.GetState().IsKeyDown(Keys.L) 
                || Keyboard.GetState().IsKeyDown(Keys.OemQuotes) 
                || Keyboard.GetState().IsKeyDown(Keys.D0) 
                || Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                GraphicsDevice.Clear(Color.White);
                spriteBatch.Begin();
                foreach (Window1 dream in dreamList) { dream.Draw(spriteBatch); }
                user.Draw(spriteBatch);
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }

        public void LoadDream()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.H) 
                || Keyboard.GetState().IsKeyDown(Keys.L) 
                || Keyboard.GetState().IsKeyDown(Keys.OemQuotes) 
                || Keyboard.GetState().IsKeyDown(Keys.D0) 
                || Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                int randY = rand.Next(0, GraphicsDevice.DisplayMode.Width);
                int randX = rand.Next(0, GraphicsDevice.DisplayMode.Height);
                int randTexture = rand.Next(0, 44);
                if (dreamList.Count() < 15 && randTexture % 3 == 0)
                {
                    dreamList.Add(new Window1(Content.Load<Texture2D>("window 1"), new Vector2(randX, randY), null));
                }
                else if (dreamList.Count() <= 5 && randTexture % 3 == 1)
                {
                    int folderX = randX + 50;
                    int folderY = randY + 100;
                    dreamList.Add(new Window1(Content.Load<Texture2D>("window2"), new Vector2(randX, randY), Content.Load<Texture2D>("folder")));
                }
                else if (dreamList.Count() <= 5 && randTexture % 3 == 2)
                {
                    dreamList.Add(new Window1(Content.Load<Texture2D>("Window3"), new Vector2(randX, randY), null));
                }
            }
            else
            {
                for (int n = 0; n < dreamList.Count; n++)
                {
                    dreamList.RemoveAt(n);
                    n--;
                }
            }
        }

        public void carryStuff()
        {
            if (holding_something == false)
            {
                for (int i = dreamList.Count - 1; i >= 0; i--)
                {
                    if ((dreamList[i].folder != null && dreamList[i].folder.boundingBox.Contains((int)user.position.X, (int)user.position.Y)) && user.isHolding == true)
                    {
                        dreamList[i].folder.isHeld = true;
                        holdIndex = i;
                        holding_something = true;
                        break;
                    }
                    else if ((dreamList[i].boundingBox.Contains((int)user.position.X, (int)user.position.Y) && user.isHolding == true))
                    {
                        dreamList[i].isHeld = true;
                        holdIndex = i;
                        holding_something = true;
                        break;
                    }
                }
            }

            //Holding a folder.
            if ((user.isHolding == true && dreamList.Count > holdIndex) && (dreamList[holdIndex].folder != null && dreamList[holdIndex].folder.isHeld == true))
            {
                dreamList[holdIndex].folder.position.X = user.position.X - dreamList[holdIndex].folder.position.X;
                dreamList[holdIndex].folder.position.Y = user.position.Y - dreamList[holdIndex].folder.position.Y;
            }

            //Holding something else.
            else if (dreamList.Count > holdIndex && dreamList[holdIndex].isHeld == true && holding_something == true)
            {
                double prevX = dreamList[holdIndex].p.X; 
                dreamList[holdIndex].p.X = user.position.X - dreamList[holdIndex].p.X;
                dreamList[holdIndex].xSpeed = (dreamList[holdIndex].p.X - prevX) * .05;

                double prevY = dreamList[holdIndex].p.Y;
                dreamList[holdIndex].p.Y = user.position.Y - dreamList[holdIndex].p.Y;
                dreamList[holdIndex].ySpeed = (dreamList[holdIndex].p.Y - prevY) * .05;
            }

            //Letting go of a folder.
            if (user.isHolding == false && dreamList.Count > holdIndex && (dreamList[holdIndex].folder != null && dreamList[holdIndex].folder.isHeld == true))
            {
                dreamList[holdIndex].folder.isHeld = false;
                holding_something = false;
            }
            else if((user.isHolding == false && dreamList.Count > holdIndex) && dreamList[holdIndex].isHeld == true)
            {
                dreamList[holdIndex].isHeld = false;
                holding_something = false;
            }
           
        }
    }
}