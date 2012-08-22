using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DBB
{
    class Box
    {
        public Texture2D boxTexture;
        public Vector2 position;

        public void Initialize(Texture2D boxTexture, Vector2 position)
        {
            this.boxTexture = boxTexture;
            this.position = position;
        }

        public int Width
        {
            get { return boxTexture.Width; }
        }

        public int Height
        {
            get { return boxTexture.Height; }
        }

        public Rectangle getBoundingBox()
        {
            return new Rectangle((int)position.X, (int)position.Y, boxTexture.Width, boxTexture.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(boxTexture, position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}
