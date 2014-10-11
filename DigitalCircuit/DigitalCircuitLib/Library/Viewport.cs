﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace DigitalCircuit.Library
{
    /// <summary>
    /// Displays (a part of) the circuit and is responsible for actions such as panning and zooming.
    /// </summary>
    public class Viewport
    {
        /// <summary>
        /// The level of zooming.
        /// </summary>
        public decimal zoomingLevel
        {
            get;
            set;
        }
        /// <summary>
        /// The exact coordinates of the viewport.
        /// </summary>
        private Point location
        {
            get;
            set;
        }
        /// <summary>
        /// Width of the viewport.
        /// </summary>
        private int pixelWidth
        {
            get;
            set;
        }

        private int absoluteWidth
        {
            get
            {
                return getAbsoluteLength(pixelWidth);
            }
        }

        private int absoluteHeight
        {
            get
            {
                return getAbsoluteLength(pixelHeight);
            }
        }

        /// <summary>
        /// Height of the viewport.
        /// </summary>
        private int pixelHeight
        {
            get;
            set;
        }

        private Rectangle ViewportRectangle
        {
            get
            {
                return new Rectangle(location.X, location.Y, absoluteWidth, absoluteHeight);
            }
        }

        public List<Item> getVisibleItems()
        {
            List<Item> visibleItems = new List<Item>();
            foreach (Item item in circuit.Items)
            {
                if (item.BoundingBox.IntersectsWith(this.ViewportRectangle))
                {
                    visibleItems.Add(item);
                }
            }
            return visibleItems;
        }

        private Circuit circuit;

        public Viewport(Circuit circuit, int width, int height)
        {
            this.circuit = circuit;
            this.location = new Point(0, 0);
            this.pixelWidth = width;
            this.pixelHeight = height;
            this.zoomingLevel = 1;
        }

        public void changeSize(int width, int height)
        {
            this.pixelWidth = width;
            this.pixelHeight = height;
        }

        /// <summary>
        /// Zooms out the viewport.
        /// </summary>
        public void zoomOut()
        {
            this.zoomingLevel = zoomingLevel / (decimal)1.5;
        }

        /// <summary>
        /// Zooms in  the viewport.
        /// </summary>
        public void zoomIn()
        {
            this.zoomingLevel = zoomingLevel * (decimal)1.5;
        }

        /// <summary>
        /// Calculates the absolute coordinates.
        /// </summary>
        /// <param name="coordinates"></param>
        /// <returns></returns>
        public Point getAbsoluteCoordinates(Point relativeCoordinates)
        {
            Point absoluteCoordinates = new Point(Convert.ToInt32((location.X + relativeCoordinates.X / zoomingLevel)),
                                                  Convert.ToInt32(location.Y + relativeCoordinates.Y / zoomingLevel));

            return absoluteCoordinates;
        }

        public void changeLocationRelatively(Point vector)
        {
            this.location = new Point(location.X + vector.X, location.Y + vector.Y);
        }

        public int getPixelLength(int length)
        {
            return Convert.ToInt32(Math.Round(length * zoomingLevel));
        }

        public int getAbsoluteLength(int length)
        {
            return Convert.ToInt32(Math.Round(length / zoomingLevel));
        }

        public Point getPixelLocation(Point location)
        {
            return new Point(getPixelLength(location.X - this.location.X), getPixelLength(location.Y - this.location.Y));
        }

        public void focus(Point pixelLocation)
        {
            Point absoluteLocation = getAbsoluteCoordinates(pixelLocation);

            this.location = new Point(absoluteLocation.X - (pixelWidth / 2), absoluteLocation.Y - (pixelHeight / 2));
        }
    }
}