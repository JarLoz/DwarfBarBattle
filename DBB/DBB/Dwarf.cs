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
        public bool stopped;

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
            stopped = false;
        }


        public void Update(GameTime gameTime)
        {
            direction.Y += 1f;
            if (direction.Y > 15f)
                direction.Y = 15f;

            if(!stopped)
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
            stopped = false;
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

        //Collision works as follows: If the dwarf is found intersecting
        //a solid object, this method is being called. The method repositions
        //the dwarf outside the object and stops movement.

        internal void collideWithStaticObject(Rectangle rectangle)
        {
            //First we move the dwarf back to the position it was in the previous frame.
            position -= direction;
            //Then we calculate the exact time of the collision. This value is between
            //0 and 1.
            float collisionTime = calculateCollisionTime(rectangle);

            //Using the collision time, we can multiply the direction vector with it,
            //And move the dwarf in the correct position (that is, the position just
            //before the collision took place)
            position += Vector2.Multiply(direction, collisionTime);
            stopped = true;

        }

        //collision time calculation seems slightly magic-y, but it's surprisingly simple.
        //Since we are only handling 2D rectangles, we can simply track their projections
        //on X and Y axles. Two rectangles intersect when their projections on both
        //X and Y axles intersect, so we find the maximum time it takes for the projections
        //on both axles to intersect. Since we already know the rectangles will intersect,
        //there is no need to check for that separaterly.

        private float calculateCollisionTime(Rectangle rectangle)
        {
            float collisionTime = 0f;

            // X-axle
            float dwarfXmin = position.X;
            float dwarfXmax = position.X + Width;
            float rectXmin = rectangle.X;
            float rectXmax = rectangle.X + rectangle.Width;

            if (rectXmax < dwarfXmin) //Dwarf is "right" of the rectangle
            {
                float time = (rectXmax - dwarfXmin) / direction.X;
                if (time > collisionTime)
                    collisionTime = time;
            }
            else if (rectXmin > dwarfXmax) //Dwarf is "left" of the rectangle
            {
                float time = (rectXmin - dwarfXmax) / direction.X;
                if (time > collisionTime)
                    collisionTime = time;
            } //If neither, the objects are already overlapping on the X axis!

            // Y-axle

            float dwarfYmin = position.Y;
            float dwarfYmax = position.Y + Height;
            float rectYmin = rectangle.Y;
            float rectYmax = rectangle.Y + rectangle.Height;

            if (rectYmax < dwarfYmin) //Dwarf is "below" the rectangle
            {
                float time = (rectYmax - dwarfYmin) / direction.Y;
                if (time > collisionTime)
                    collisionTime = time;
            }
            else if (rectYmin > dwarfYmax) //Dwarf is "above" the rectangle
            {
                float time = (rectYmin - dwarfYmax) / direction.Y;
                if (time > collisionTime)
                    collisionTime = time;
            } //Again, if neither, then the objects are already overlapping on the Y axis.

            return collisionTime;
        }

        private void intersectionTime(float staticEdge, float movingEdge, float velocityOnAxle, ref float collisionTime)
        {
            float time = (staticEdge - movingEdge) / velocityOnAxle;
            if (time > collisionTime)
                collisionTime = time;
        }
    }
}
