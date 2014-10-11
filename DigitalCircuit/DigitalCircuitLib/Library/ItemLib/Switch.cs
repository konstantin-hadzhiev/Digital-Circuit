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

namespace DigitalCircuit.Library
{
    /// <summary>
    /// Child of the item class that can be added to the circuit.
    /// </summary>
    public class Switch : Item, IToggleable
    {
        public Switch() : base(0, 1)
        {
        }

        private bool output = false;

        public void toggle()
        {
            output = !output;
        }

        public override bool getOutput()
        {
            return output;
        }
    }
}