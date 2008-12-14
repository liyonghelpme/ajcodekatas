//-----------------------------------------------------------------------
// <copyright file="Field.cs" company="ajlopez.com">
//     Copyright (c) ajlopez.com. All rights reserved.
// </copyright>
// <author>Angel "Java" Lopez</author>
//-----------------------------------------------------------------------

namespace Evolve
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Field has food in a grid
    /// </summary>
    public class Field
    {
        /// <summary>
        /// Internal random values generator
        /// </summary>
        private static Random random = new Random();

        /// <summary>
        /// Food array
        /// </summary>
        private int[][] food;

        /// <summary>
        /// Initializes a new instance of the Field class
        /// composed by a grid of cells
        /// containing 0 food
        /// </summary>
        /// <param name="width">Width of field</param>
        /// <param name="height">Height of field</param>
        public Field(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.ClearFood();
        }

        /// <summary>
        /// Gets width of the field
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Gets height of the field
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Gets total food level
        /// sum of food in each cell
        /// </summary>
        public int FoodLevel
        {
            get
            {
                int level = 0;

                for (int x = 0; x < this.Width; x++)
                {
                    for (int y = 0; y < this.Height; y++)
                    {
                        level += this.food[x][y];
                    }
                }

                return level;
            }
        }

        /// <summary>
        /// Returns food at cell
        /// </summary>
        /// <param name="positionX">X position</param>
        /// <param name="positionY">Y position</param>
        /// <returns>food level at cell</returns>
        public int GetFoodAt(int positionX, int positionY)
        {
            return this.food[positionX][positionY];
        }

        public void ClearFood()
        {
            this.food = new int[this.Width][];

            for (int k = 0; k < this.Width; k++)
            {
                this.food[k] = new int[this.Height];
            }
        }

        /// <summary>
        /// Seed food in field
        /// </summary>
        /// <param name="level">Food to seed</param>
        public void SeedFood(int level)
        {
            int size = this.Width * this.Height;
            int ration = level * 2 / size;

            int x = 0;
            int y = 0;
            int newfood;

            while (level > ration)
            {
                this.ChooseCell(ref x, ref y);
                newfood = random.Next(ration);

                this.food[x][y] += newfood;

                level -= newfood;
            }

            this.ChooseCell(ref x, ref y);
            this.food[x][y] += level;
        }

        public void EatFood(int n, int x, int y)
        {
            this.food[x][y] -= n;
        }

        /// <summary>
        /// Set the coordinates of a random selected cell
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        private void ChooseCell(ref int x, ref int y)
        {
            x = random.Next(this.Width);
            y = random.Next(this.Height);
        }
    }
}
