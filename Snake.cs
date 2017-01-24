using BimmCore.MonoGame;
using BimmCore.MonoGame.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Snakez.Screens;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Snakez
{
    public class Snake
    {

        public GameScreen gameScreen;

        public string name;
        public Keys up, down, left, right;
        public Color color;
        public Direction direction;
        public List<Vector2> body;

        public static int size = 10;
        public int moveDelay = 3;
        public int moveCounter;
        public int removeDelay = 5;
        public int removeCounter;

        public Action<Snake> onDeath;
        public bool alive;

        public Vector2 head
        {
            get
            {
                return body[0];
            }
        }

        public Snake(string name, GameScreen gameScreen, Vector2 location, Keys up, Keys down, Keys left, Keys right)
        {
            this.name = name;
            this.body = new List<Vector2> { location };
            this.gameScreen = gameScreen;
            this.color = getRandomColor();
            this.direction = Direction.Up;
            this.alive = true;

            this.up = up;
            this.down = down;
            this.left = left;
            this.right = right;
            removeCounter = removeDelay;
            moveCounter = moveDelay;
        }
        public Snake(string name, GameScreen gameScreen, Vector2 location, Keys up, Keys down, Keys left, Keys right, Action<Snake> onDeath)
        : this(name, gameScreen, location, up, down, left, right)
        {
            this.onDeath = onDeath;
        }

        /// <summary>
        /// Remove last item of body
        /// </summary>
        public void removeTail()
        {
            body.RemoveAt(body.Count - 1);
        }

        /// <summary>
        /// Move the snake by adding a point infront
        /// </summary>
        public void move()
        {
            body.Insert(0, getNextPoint());
        }

        /// <summary>
        /// Get the next point in front of the snake
        /// </summary>
        /// <returns></returns>
        public Vector2 getNextPoint()
        {
            Vector2 next = head;
            switch (direction)
            {
                case Direction.Up:
                    next.Y -= size + 1;
                    break;
                case Direction.Down:
                    next.Y += size + 1;
                    break;
                case Direction.Left:
                    next.X -= size + 1;
                    break;
                case Direction.Right:
                    next.X += size + 1;
                    break;
            }
            return next;
        }

        /// <summary>
        /// Check if the snake hit itself
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool hitSelf(Vector2 point)
        {
            for (int i = 1; i < body.Count; i++)
            {
                if (getBox(point).Intersects(getBox(body[i])))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Check if the snake hit another snake
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool hitOthers(Vector2 point)
        {
            foreach (Snake snake in gameScreen.getSnakes())
            {
                if (snake.isAlive() && !this.name.Equals(snake.name))
                {
                        foreach (Vector2 vec in snake.body)
                            if (getBox(point).Intersects(getBox(vec)))
                            {
                                return true;
                            }
                    if (!alive)
                        break;
                }
            }
            return false;
        }
        public bool hitWalls(Vector2 point)
        {
            return point.X < 0 || point.X > MonoHelper.Size.X || point.Y < 0 || point.Y > MonoHelper.Size.Y;
        }

        /// <summary>
        /// Check if the snake collided with anything
        /// </summary>
        /// <returns></returns>
        public bool checkCollisions()
        {
            if (hitWalls(head))
                return true;

            else if (alive && hitSelf(head))
                return true;

            else if (alive && hitOthers(head))
                return true;


            return false;
        }

        public void update()
        {
            //Listen to keys
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(up))
                direction = Direction.Up;
            else if (ks.IsKeyDown(down))
                direction = Direction.Down;
            else if (ks.IsKeyDown(left))
                direction = Direction.Left;
            else if (ks.IsKeyDown(right))
                direction = Direction.Right;

            //Move
            moveCounter++;
            if (moveCounter > moveDelay)
            {
                moveCounter = 0;
                move();
            }
            //Remove
            removeCounter++;
            if (removeCounter > removeDelay)
            {
                removeCounter = 0;
                removeTail();
            }
            //Check collisions
            if (moveCounter == 0)
                if (checkCollisions())
                    die();
        }

        /// <summary>
        /// Get the rectangle box surounding the given point
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public Rectangle getBox(Vector2 point)
        {
            return new Rectangle((int)point.X - (size / 2), (int)point.Y - (size / 2), size, size);
        }

        /// <summary>
        /// Draw the snake
        /// </summary>
        public void draw()
        {
            MonoHelper.SpriteBatch.DrawString(FontHandler.menuFont, "" + name, Utils.centerText(FontHandler.menuFont, name, new Rectangle((int)head.X - 25, 10, 50, 20)), color);

            foreach (Vector2 vec in body)
            {
                Drawer.drawRectangle(getBox(vec), color);
            }

        }
        /// <summary>
        /// Get a random color for the snake
        /// </summary>
        /// <returns></returns>
        public Color getRandomColor()
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            return new Color((byte)random.Next(100, 200), (byte)random.Next(100, 200), (byte)random.Next(100, 200));
        }

        /// <summary>
        /// Snake dies
        /// </summary>
        public void die()
        {
            alive = false;
            onDeath?.Invoke(this);
        }

        /// <summary>
        /// Check if the snake is alive
        /// </summary>
        /// <returns></returns>
        public bool isAlive() { return alive; }
    }
}
