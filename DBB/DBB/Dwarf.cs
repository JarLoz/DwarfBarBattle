using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DBB
{
    class Dwarf
    {
        public Texture2D dwarfTexture;
        public Vector2 position;
        public Vector2 direction;
        public bool facingRight;
        public float dwarfMoveSpeed;
        public bool jumping;
        public bool doublejumping;

        public int Width
        {
            get { return dwarfTexture.Width; }
        }

        public int Height
        {
            get { return dwarfTexture.Height; }
        }


        public void Initialize(Texture2D texture, Vector2 position)
        {
            dwarfTexture = texture;
            this.position = position;
            dwarfMoveSpeed = 8f;
            jumping = true;
            doublejumping = true;
            direction = Vector2.Zero;

            facingRight = true;
        }


        public void Update(GameTime gameTime)
        {
            direction.Y += 1f;
            if (direction.Y > 15f)
                direction.Y = 15f;

            position = position + direction;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if(facingRight)
                spriteBatch.Draw(dwarfTexture, position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            else
                spriteBatch.Draw(dwarfTexture, position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipHorizontally, 0f);
        }

        public Rectangle getBoundingBox()
        {
            return new Rectangle((int)position.X, (int)position.Y,
                (int)dwarfTexture.Width, (int)dwarfTexture.Height);
        }



        internal void moveLeft()
        {
            facingRight = false;
            direction.X = -dwarfMoveSpeed;
        }

        internal void moveRight()
        {
            facingRight = true;
            direction.X = dwarfMoveSpeed;
        }

        internal void jump()
        {
            if (!jumping)
            {
                jumping = true;
                direction.Y = -25f;
            }
            else if (!doublejumping)
            {
                doublejumping = true;
                direction.Y = -25f;
            }
        }


        internal void stop()
        {
            direction.X = 0f;
            
        }
    }
}
