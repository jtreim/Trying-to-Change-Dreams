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
    public class Window3
    {
        public int spd;
        public Vector2 pos;
        public Texture2D text;
        public Random random = new Random();
        public Rectangle bB;
        public enum Direction { Up, Down, Left, Right, Neutral };
        public Direction direction;
        public Window3()
        {
            int value = random.Next(1, 5);
            if (value == 1) { direction = Direction.Up; }
            else if (value == 2) { direction = Direction.Right; }
            else if (value == 3) { direction = Direction.Down; }
            else if (value == 4) { direction = Direction.Left; }
            else if (value == 5) { direction = Direction.Neutral; }

            spd = random.Next(0, 5);
            pos = new Vector2(random.Next(0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width), random.Next(0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height));
            text = null;
        }

        public void LoadContent(ContentManager Content)
        {
            text = Content.Load<Texture2D>("Window3");
        }

        public void Update(GameTime gameTime)
        {
            TimeSpan change = gameTime.TotalGameTime;
            int value = random.Next(0, 4);

            if ((int)change.TotalSeconds % 5 == 0 && value == 0) { direction = Direction.Neutral; }
            else if ((int)change.TotalSeconds % 5 == 0 && value == 1) { direction = Direction.Down; }
            else if ((int)change.TotalSeconds % 5 == 0 && value == 2) { direction = Direction.Up; }
            else if ((int)change.TotalSeconds % 5 == 0 && value == 3) { direction = Direction.Left; }
            else if ((int)change.TotalSeconds % 5 == 0 && value == 4) { direction = Direction.Right; }

            if (direction == Direction.Down) { pos.Y = pos.Y + spd; }
            if (direction == Direction.Left) { pos.X = pos.X - spd; }
            if (direction == Direction.Right) { pos.X = pos.X + spd; }
            if (direction == Direction.Up) { pos.Y = pos.Y - spd; }

            //bounding box for being held.
            bB = new Rectangle((int)pos.X, (int)pos.Y, text.Width, text.Height);
            if (pos.X < 0) { pos.X = 0; }
            if (pos.X + text.Width > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width) { pos.X = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - text.Width; }
            if (pos.Y + text.Height > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height) { pos.Y = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - text.Height; }
            if (pos.Y < 0) { pos.Y = 0; }
        }

        public void Draw(SpriteBatch Spritebatch)
        {
            Spritebatch.Draw(text, pos, Color.White);
        }
    }
}
