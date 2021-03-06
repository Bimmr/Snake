﻿using BimmCore.Misc;
using BimmCore.MonoGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Snakez.Screens;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Snakez
{
    public class SnakeAI : Snake
    {
        private int maxRandom = 75;
        private int randomCounter;
        private int randomDelay;
        private List<Direction> prevDirections;

        public SnakeAI(GameScreen gameScreen, Vector2 location)
        : this("Mr. Snake", gameScreen, location)
        {
            
        }
        public SnakeAI(string name, GameScreen gameScreen, Vector2 location)
        : base(name, gameScreen, location, Keys.A, Keys.A, Keys.A, Keys.A)
        {
            randomDelay = new Random().Next(moveDelay + 1, maxRandom);
            prevDirections = new List<Direction>();
            getDirection();
            /*this.moveDelay = 1;
            this.removeDelay = 3;
            this.spacing = 2;
            */
        }

        /// <summary>
        /// Check if the given point will cause a collision
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool willCollide(Vector2 point)
        {
            if (hitWalls(point))
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
                Debug.WriteLine(name + ": Hit Self");
                Vector2 willHit = getNextPoint();

                try
                {
                    direction = Directions.getOpposite(findDirection(willHit, body[findPos(willHit) + 1]));

                    if (willCollide(getNextPoint()))
                        direction = Directions.getOpposite(direction);

                    return;
                }
                catch (Exception) { }
            }
            if (hitWalls(getNextPoint()) && prevDirections.Count > 2)
            {
                Debug.WriteLine(name + ": Hit Wall");

                direction = (prevDirections[1]);
                if (willCollide(getNextPoint()))
                    direction = Directions.getOpposite(direction);
               
                return;

            }
            if (hitOthers(getNextPoint()))
            {

                Debug.WriteLine(name + ": Hit Other");
                try
                {
                    direction = (prevDirections[1]);
                    if (willCollide(getNextPoint()))
                        direction = Directions.getOpposite(direction);

                    return;
                }
                catch (Exception) { }
            }

            //If not hitting self, then choose random direction

            Debug.WriteLine(name + ": Random Direction");
            //Try going all 4 directions(In a random order), and if nothing works give up
            Random r = new Random(Guid.NewGuid().GetHashCode());
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
        public new void update()
        {

            //Randomly move
            randomCounter++;
            if (randomCounter > randomDelay)
            {
                randomCounter = 0;
                randomDelay = new Random().Next(moveDelay + 1, maxRandom);
                getDirection();
                addLastDirection(direction);
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
                randomCounter = 0;
                randomDelay = new Random().Next(200);
                getDirection();
                addLastDirection(direction);
            }
            //Check if it did collide
            if (moveCounter == 0)
                if (checkCollisions())
                    die();
        }

        /// <summary>
        /// Add the previous direction
        /// </summary>
        /// <param name="dir"></param>
        private void addLastDirection(Direction dir)
        {
            if (prevDirections.Count == 0 || prevDirections[0] != dir)
            {
                Debug.WriteLine(dir);
                prevDirections.Insert(0, direction);
                if (prevDirections.Count > 4)
                    prevDirections.RemoveAt(4);
            }
        }

        /// <summary>
        /// Remove the last element of the body
        /// </summary>
        public new void removeTail()
        {
            body.RemoveAt(body.Count - 1);
        }
        private int findPos(Vector2 vec)
        {
            int pos = -1;
            for (int i = 0; i < body.Count; i++)
                if (body[i] == vec)
                    pos = i;
            return pos;
        }

        private bool isBox(Vector2 point1, Vector2 point2)
        {
            int p1 = findPos(point1);
            int p2 = findPos(point2);
            Debug.WriteLine(p1 + " " + p2 + " " + (p2 - p1));
            for (int i = 0; i <= p2 - p1; i++)
            {
                if (body[p1 + i] == body[p2 - i])
                    return true;
            }
            return false;

        }
    }
}
