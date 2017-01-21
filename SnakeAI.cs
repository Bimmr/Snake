using BimmCore.MonoGame;
using BimmCore.MonoGame.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Snakez.Screens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snakez
{
    public class SnakeAI : Snake
    {
        private int maxRandom = 75;
        private int randomCounter;
        private int randomDelay;
        private List<Direction> prevDirections;

        public SnakeAI(GameScreen gameScreen, Vector2 location)
        : base("AI", gameScreen, location, Keys.A, Keys.A, Keys.A, Keys.A)
        {
            randomDelay = new Random().Next(moveDelay + 1, maxRandom);
            prevDirections = new List<Direction>();

        }

        /// <summary>
        /// Check if the given point will cause a collision
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool willCollide(Vector2 point)
        {
            if (point.X < 0 || point.X > MonoHelper.Size.X || point.Y < 0 || point.Y > MonoHelper.Size.Y)
                return true;

            if (alive && hitSelf(point))
                return true;

            else if (alive && hitOthers(point))
                return true;


            return false;
        }

        /// <summary>
        /// Find the direction that the snake was going between these two points
        /// </summary>
        private Direction findDirection(Vector2 prev, Vector2 next)
        {
            Direction dir = Direction.Up;
            if (prev.X == next.X)
            {
                if (prev.Y < next.Y)
                    dir = Direction.Up;
                else
                    dir = Direction.Down;
            }
            else
            {
                if (prev.X < next.X)
                    dir = Direction.Left;
                else
                    dir = Direction.Right;

            }

            return dir;

        }

        /// <summary>
        /// Choose a direction to go
        /// </summary>
        public void getDirection()
        {
            //If will hit self, make sure to not trap self
            if (hitSelf(getNextPoint()))
            {
                Vector2 willHit = getNextPoint();
                for (int i = 1; i < body.Count; i++)
                {
                    if (body[i - 1] == willHit)
                    {
                        direction = getOpposite(findDirection(willHit, body[i]));
                    }
                }
            }

            //If not hitting self, then choose random direction
            else
            {

                //Try going all 4 directions(In a random order), and if nothing works give up
                Random r = new Random();
                List<Direction> dirs = new List<Direction> { Direction.Up, Direction.Down, Direction.Left, Direction.Right };
                for (int i = 0; i < 4; i++)
                {
                    direction = dirs[r.Next(dirs.Count)];
                    if (!willCollide(getNextPoint()))
                        break;
                    else
                        dirs.Remove(direction);
                }
            }
            //Make sure to not trap self by going opposite way of last turn
            if (prevDirections.Count > 2)
            {
                //Iterate through previous directions till it finds the previous previous direction, then make sure it doesn't go the opposite directoon
                Direction d = prevDirections[0];
                for (int i = 0; i < prevDirections.Count; i++)
                {
                    if(d != prevDirections[i])
                    {
                        if(getOpposite(direction) == prevDirections[i])
                        {
                            direction = prevDirections[i];
                        }
                        break;
                    }
                }
            }

        }
        public new void update()
        {

            //Randomly move
            randomCounter++;
            if (randomCounter > randomDelay)
            {
                randomCounter = 0;
                randomDelay = new Random().Next(moveDelay + 1, maxRandom);
                prevDirections.Insert(0, direction);
                getDirection();
            }

            //Move
            moveCounter++;
            if (moveCounter > moveDelay)
            {
                moveCounter = 0;
                move();
            }

            //Remove tail
            removeCounter++;
            if (removeCounter > removeDelay)
            {
                removeCounter = 0;
                removeTail();
            }

            //Check if it will collide
            if (willCollide(getNextPoint()))
            {
                prevDirections.Insert(0, direction);
                randomCounter = 0;
                randomDelay = new Random().Next(200);
                getDirection();
            }

            //Check if it did collide
            if (checkCollisions())
                die();
        }

        /// <summary>
        /// Remove the last element of the body
        /// </summary>
        public new void removeTail()
        {
            score++;
            if(prevDirections.Count > 1)
                prevDirections.RemoveAt(prevDirections.Count - 1);
            body.RemoveAt(body.Count - 1);
        }
        /// <summary>
        /// Get the opposite of the direction given
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public Direction getOpposite(Direction d)
        {
            switch (d)
            {
                case Direction.Up:
                    return Direction.Down;
                case Direction.Down:
                    return Direction.Up;
                case Direction.Left:
                    return Direction.Right;
                case Direction.Right:
                    return Direction.Left;
                default:
                    return Direction.Up;
            }
        }
    }
}
