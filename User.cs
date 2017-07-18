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
    public class User
    {
        public int maxSpeed;
        public Vector2 position;
        public MouseState mouseState;
        public bool isHolding;
        public Texture2D hand;
        public Rectangle BoundingBox;
        public Texture2D open;
        public Texture2D closed;

        public User()
        {
            open = null;
            closed = null;
            hand = open;
            position = new Vector2(300, 300);
            mouseState = Mouse.GetState();
            isHolding = false;
            maxSpeed = 20;
        }

        public void LoadContent(ContentManager Content)
        {
            closed = Content.Load<Texture2D>("closedhand copy");
            open = Content.Load<Texture2D>("openhand");
            if (isHolding == false) { hand = open; }
            else if (isHolding == true) { hand = closed; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(hand, position, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            if (!GamePad.GetState(PlayerIndex.One).IsConnected)
            {
                position.X = mouseState.X;
                position.Y = mouseState.Y;
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    isHolding = true;
                    hand = closed;
                }
                else
                {
                    isHolding = false;
                    hand = open;
                }
            }
            else
            {
                if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X != 0)
                {
                    position.X += (float)(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X) * maxSpeed;
                }
                if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y != 0)
                {
                    position.Y -= (float)(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y) * maxSpeed;
                }

                if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A))
                {
                    hand = closed;
                    isHolding = true;
                }
                else
                {
                    hand = open;
                    isHolding = false;
                }
            }

            if (position.X + hand.Width > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width)
            { position.X = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - hand.Width; }
            if (position.Y + hand.Height > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height)
            { position.Y = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - hand.Height; }
            if (position.X < 0) { position.X = 0; }
            if (position.Y < 0) { position.Y = 0; }
        }
    }
}
