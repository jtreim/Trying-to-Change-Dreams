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
    public class Window2
    {
        public int speed;
        public Vector2 position;
        public Texture2D texture;
        public Random r = new Random();
        public Rectangle boundingBox;

        public Window2()
        {
            speed = r.Next(0, 5);
            position = new Vector2(r.Next(0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width), r.Next(0, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height));
            texture = null;
        }

        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("Window2");
        }

        public void Update(GameTime gameTime)
        {
            //bounding box for being held.
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            if (position.X < 0) { position.X = 0; }
            if (position.X + texture.Width > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width) { position.X = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - texture.Width; }
            if (position.Y + texture.Height > GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height) { position.Y = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - texture.Height; }
            if (position.Y < 0) { position.Y = 0; }
        }

        public void Draw(SpriteBatch Spritebatch)
        {
            Spritebatch.Draw(texture, position, Color.White);
        }
    }
}
