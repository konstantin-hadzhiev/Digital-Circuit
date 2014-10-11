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
using System.Windows.Forms;

namespace DigitalCircuit.Library
{
    /// <summary>
    /// Abstract class that forces children to implement ‘ execute’ and ‘reverse’ functionality. 
    /// Calling first the ‘execute’ method and after that the ‘reverse’ method should restore 
    /// the circuit in its original state.
    /// </summary>
    public class Action
    {
        /// <summary>
        /// All types of user actions on the circuit.
        /// </summary>
        public enum ActionType { add, delete, toggle };
        object obj { get; set; }

        public Action(Circuit circuit, ActionType actionType, Item item)
        {
            this.circuit = circuit;
            this.actionType = actionType;
            this.obj = item;
        }

        private ActionType actionType
        {
            get;
            set;
        }

        private Circuit circuit
        {
            get;
            set;
        }

        /// <summary>
        /// Executes the action.
        /// </summary>
        /// <returns>True if successful, false if not.</returns>
        public virtual bool execute()
        {
            if (obj is Item)
            {
                switch (actionType)
                {
                    case ActionType.add:
                        if (circuit.addItem((Item)obj))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    case ActionType.delete:
                        circuit.deleteItem((Item)obj);
                        return true;

                    case ActionType.toggle:
                        circuit.toggleItem((IToggleable)obj);
                        return true;
                }
            }
            else if (obj is Connection)
            {
                switch (actionType)
                {
                    case ActionType.add:
                        circuit.addConnection((Connection)obj);
                        return true;

                    case ActionType.delete:
                        circuit.deleteConnection((Connection)obj);
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Undoes the action.
        /// </summary>
        /// <returns>True if successful, false if not.</returns>
        public bool undo()
        {
            if (obj is Item)
            {
                switch (actionType)
                {
                    case ActionType.add:
                        circuit.deleteItem((Item)obj);
                        return true;

                    case ActionType.delete:
                        circuit.addItem((Item)obj);
                        return true;

                    case ActionType.toggle:
                        circuit.toggleItem((IToggleable)obj);
                        return true;
                }
            }
            else if (obj is Connection)
            {
                switch (actionType)
                {
                    case ActionType.add:
                        circuit.deleteConnection((Connection)obj);
                        return true;

                    case ActionType.delete:
                        circuit.addConnection((Connection)obj);
                        return true;
                }
            }

            return false;
        }

    }
}