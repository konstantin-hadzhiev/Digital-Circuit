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
using System.Windows;
using System.Drawing;

namespace DigitalCircuit.Library
{
    /// <summary>
    /// An abstract thing that can be added to a circuit, except connections. 
    /// It has a location on the circuit and has a bounding box.
    /// </summary>
    public abstract class Item
    {

        /// <summary>
        /// The exact location specified by absolute coordinates.
        /// </summary>
        private Point location;
        public virtual Point Location
        {
            get
            {
                return this.location;
            }
            set
            {
                this.location = value;
            }
        }

        public int Width = 100;
        public int Height = 100;
        /// <summary>
        /// The boundingBox is used to check whether two items are intersecting, or whether an item is clicked.
        /// </summary>
        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle(location.X, location.Y, Width, Height);
            }
        }

        /// <summary>
        /// Array of inputs
        /// </summary>
        private Port[] inputs;
        public Port[] Inputs
        {
            get
            {
                return this.inputs;
            }
            set
            {
                this.inputs = value;
            }
        }

        /// <summary>
        /// Array of outputs
        /// </summary>
        private Port[] outputs;
        public Port[] Outputs
        {
            get
            {
                return this.outputs;
            }
            set
            {
                this.outputs = value;
            }
        }

        /// <summary>
        /// Calculates the output based on the input.
        /// </summary>
        /// <returns></returns>
        public abstract bool getOutput();

        /// <summary>
        /// Check if a point collides with the item.
        /// </summary>
        /// <param name="coordinates"></param>
        /// <returns>Return true if it collides, false if not.</returns>
        public virtual bool checkCollision(Point coordinates)
        {
            throw new System.NotImplementedException();
        }


        public Item(int inputs, int outputs)
        {
            Inputs = new Port[inputs];
            Outputs = new Port[outputs];
        }

    }
}