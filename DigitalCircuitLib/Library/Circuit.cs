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
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
namespace DigitalCircuit.Library
{
    /// <summary>
    /// Contains all the data about the current circuit.
    /// </summary>
    [Serializable]
    public class Circuit : ISerializable
    {
        /// <summary>
        /// The file path of the saved circuit. If it was not saved then it is NULL.
        /// </summary>
        public virtual string filepath
        {
            get;
            set;
        }

        /// <summary>
        /// The viewport that is attached to the circuit
        /// </summary>
        public Viewport Viewport
        {
            get;
            private set;
        }

        /// <summary>
        /// The list of connections between components.
        /// </summary>
        private List<Connection> connections;
        public List<Connection> Connections
        {
            get
            {
                return this.connections;
            }
        }

        /// <summary>
        /// The list of items.
        /// </summary>
        private List<Item> items;
        public List<Item> Items
        {
            get
            {
                return this.items;
            }
            set
            {
                this.items = value;
            }
        }

        public Circuit()
        {
            items = new List<Item>();
            connections = new List<Connection>();
            this.Viewport = new Viewport(this);
        }

        public Circuit(SerializationInfo info, StreamingContext context)
        {
            this.connections = (List<Connection>)info.GetValue("connections", typeof(List<Connection>));
            this.items = (List<Item>)info.GetValue("items", typeof(List<Item>));
            this.Viewport = (Viewport)info.GetValue("viewport", typeof(Viewport));
            this.Viewport.SetCircuit(this);
        }

        /// <summary>
        /// Add a connection to the circuit
        /// </summary>
        /// <param name="port1">First port of the connection</param>
        /// <param name="port2">Second port of the connection</param>
        public void addConnection(Connection connection)
        {
            if (!connection.connected)
            {
                connection.Connect();
            }

            connections.Add(connection);
        }

        /// <summary>
        /// Checks if a connection is possible between two ports.
        /// </summary>
        /// <param name="port1">First port of connection</param>
        /// <param name="port2">Second port of connection</param>
        /// <returns>True if a connection is possible, false if not.</returns>
        public bool connectionPossible(Port port1, Port port2)
        {
            if (port1.isInput == port2.isInput)
            {
                return false;
            }

            if (port1.item == port2.item)
            {
                return false;
            }

            if (port1.isInput && port1.isUsed || port2.isInput && port2.isUsed)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Deletes an item from the circuit
        /// </summary>
        /// <param name="item">The item that should be removed</param>
        public void deleteItem(Item item)
        {
            items.Remove(item);
        }

        /// <summary>
        /// Returns a list of the connections that are associated with an item
        /// </summary>
        /// <param name="item">The concerning item</param>
        /// <returns>The list of associated connections</returns>
        public List<Connection> getAssociatedConnections(Item item)
        {
            List<Port> associatedPorts = item.getPorts();
            List<Connection> associatedConnections = new List<Connection>();

            foreach (Connection connection in connections)
            {
                if (item.Inputs.Contains(connection.inputPort) || item.Inputs.Contains(connection.outputPort) || item.Outputs.Contains(connection.inputPort) || item.Outputs.Contains(connection.outputPort))
                {
                    associatedConnections.AddRange(getAssociatedConnections(connection.inputPort));
                    associatedConnections.AddRange(getAssociatedConnections(connection.outputPort));
                }
            }

            return associatedConnections;
        }

        public List<Connection> getAssociatedConnections(Port port)
        {
            List<Connection> associatedConnections = new List<Connection>();

            foreach (Connection connection in connections)
            {
                if (connection.inputPort == port || connection.outputPort == port)
                {
                    associatedConnections.Add(connection);
                }
            }

            return associatedConnections;
        }

        /// <summary>
        /// Toggles a source
        /// </summary>
        /// <param name="item">Source</param>
        public virtual void toggleItem(IToggleable item)
        {
            item.toggle();
        }

        /// <summary>
        /// Adds an item to the circuit
        /// </summary>
        /// <param name="item">The concerning item</param>
        /// <returns>True if successful, false if not.</returns>
        public virtual bool addItem(Item item)
        {
            if (!checkCollision(item))
            {
                items.Add(item);
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// Deletes a connection from the circuit
        /// </summary>
        /// <param name="connection">The connection that should be deleted</param>
        /// <returns>True if successful, false if not.</returns>
        public bool deleteConnection(Connection connection)
        {
            connections.Remove(connection);
            connection.Disconnect();
            return true;
        }

        /// <summary>
        /// Checks if an item collides with another item
        /// </summary>
        /// <param name="item">The item that will </param>
        /// <returns>True if there is a collision, false if not.</returns>
        public bool checkCollision(Item item)
        {
            return checkCollision(item.BoundingBox);
        }

        /// <summary>
        /// Checks if an item collides with a rectangle
        /// </summary>
        /// <param name="rectangle">The concerning rectangle</param>
        /// <returns>True if there is a collision, false if not. </returns>
        public bool checkCollision(Rectangle rectangle)
        {
            foreach (Item currentItem in items)
            {
                if (currentItem.BoundingBox.IntersectsWith(rectangle))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Save the circuit to a filestream
        /// </summary>
        /// <param name="fileStream">The filestream that the circuit will be saved to</param>
        public void SaveToFile(FileStream fileStream)
        {
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(fileStream, this);
        }

        /// <summary>
        /// Initialize all objects, only used for deserializing
        /// </summary>
        public void Initialize()
        {
            foreach (Item item in items)
            {
                item.Initialize();
            }

            foreach (Connection connection in connections)
            {
                connection.Connect();
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("connections", this.connections);
            info.AddValue("items", this.items);
            info.AddValue("viewport", this.Viewport);
        }
    }
}