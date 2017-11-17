/*******************************************************
 *  DirectedGraph.cs
 *  Created by Stephen Hall on 11/20/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  Directed Graph implementation in C#
 ********************************************************/
using System;
using System.Collections.Generic;

namespace DataStructures.Graphs.DirectedGraph
{
    /// <summary>
    /// Directed Graph Class
    /// </summary>
    public class DirectedGraph
    {
        private int _edges;
        private readonly Dictionary<Int32, Dictionary<Int32, Double>> _parentMap = new Dictionary<Int32, Dictionary<Int32, Double>>();
        private readonly Dictionary<Int32, Dictionary<Int32, Double>> _childMap = new Dictionary<Int32, Dictionary<Int32, Double>>();

        /// <summary>
        /// Gets the size of the graph
        /// </summary>
        /// <returns>size of the graph</returns>
        public int Size() => _parentMap.Count;

        /// <summary>
        /// gets the number of edges
        /// </summary>
        /// <returns>number of edges</returns>
        public int GetNumberOfEdges() => _edges;

        /// <summary>
        /// adds a node into the graph
        /// </summary>
        /// <param name="nodeId">id to add</param>
        /// <returns>success|fail</returns>
        public bool AddNode(int nodeId)
        {
            if (!_parentMap.ContainsKey(nodeId))
            {
                _parentMap.Add(nodeId, new Dictionary<Int32, Double>());
                _childMap.Add(nodeId, new Dictionary<Int32, Double>());
                return true;
            }
            return false;
        }

        /// <summary>
        /// test to see if the node exisits
        /// </summary>
        /// <param name="nodeId">id to find</param>
        /// <returns>true|false</returns>
        public bool HasNode(int nodeId) => _parentMap.ContainsKey(nodeId);

        /// <summary>
        /// clears a node from the graph
        /// </summary>
        /// <param name="nodeId">id to clear</param>
        /// <returns>success|fail</returns>
        public bool ClearNode(int nodeId)
        {
            if (!HasNode(nodeId))
            {
                return false;
            }

            Dictionary<Int32, Double> parents = _parentMap[nodeId];
            Dictionary<Int32, Double> children = _childMap[nodeId];

            if (parents.Count == 0 && children.Count == 0)
            {
                return false;
            }

            foreach (Int32 childId in children.Keys)
            {
                _parentMap[childId].Remove(nodeId);
            }
            
            foreach (Int32 parentId in parents.Keys)
            {
                _childMap[parentId].Remove(nodeId);
            }

            _edges -= parents.Count;
            _edges -= children.Count;
            parents.Clear();
            children.Clear();
            return true;
        }

        /// <summary>
        /// removes a node from the graph
        /// </summary>
        /// <param name="nodeId">id to remove</param>
        /// <returns>success|fail</returns>
        public bool RemoveNode(int nodeId)
        {
            if (HasNode(nodeId))
            {
                ClearNode(nodeId);
                _parentMap.Remove(nodeId);
                _childMap.Remove(nodeId);
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// helper function to add an edge to the graph
        /// </summary>
        /// <param name="tailNodeId"> tail node</param>
        /// <param name="headNodeId">head node</param>
        /// <param name="weight">weight of the edge</param>
        /// <returns>success|fail</returns>
        private bool AddEdge(int tailNodeId, int headNodeId, double weight)
        {
            AddNode(tailNodeId);
            AddNode(headNodeId);

            if (_childMap[tailNodeId].ContainsKey(headNodeId))
            {
                double oldWeight = _childMap[tailNodeId][headNodeId];
                _childMap[tailNodeId][headNodeId] =  weight;
                _parentMap[headNodeId][tailNodeId] = weight;
                return oldWeight != weight;
            }
            else
            {
                _childMap[tailNodeId][headNodeId] = weight;
                _parentMap[headNodeId][tailNodeId] = weight;
                ++_edges;
                return true;
            }
        }

        /// <summary>
        /// Adds an edge to the graph
        /// </summary>
        /// <param name="tailNodeId">tail node</param>
        /// <param name="headNodeId">head node</param>
        /// <returns>success|fail</returns>
        public bool AddEdge(int tailNodeId, int headNodeId) => AddEdge(tailNodeId, headNodeId, 1.0);

        /// <summary>
        /// tests to see if an edge exist
        /// </summary>
        /// <param name="tailNodeId">tail node</param>
        /// <param name="headNodeId">head node</param>
        /// <returns>true|false</returns>
        public bool HasEdge(int tailNodeId, int headNodeId) => _childMap.ContainsKey(tailNodeId) && _childMap[tailNodeId].ContainsKey(headNodeId);

        /// <summary>
        /// gets the weight of an edge
        /// </summary>
        /// <param name="tailNodeId">tail node</param>
        /// <param name="headNodeId">head node</param>
        /// <returns>weight of the edge</returns>
        public double GetEdgeWeight(int tailNodeId, int headNodeId) => !HasEdge(tailNodeId, headNodeId) ? Double.NaN : _childMap[tailNodeId][headNodeId];

        /**
         * removes an edge from the graph
         * @param tailNodeId: tail node
         * @param headNodeId: head node
         * @return boolean: success|fail
         */
        public bool RemoveEdge(int tailNodeId, int headNodeId)
        {
            if (_childMap.ContainsKey(tailNodeId))
            {
                if (_childMap[tailNodeId].ContainsKey(headNodeId))
                {
                    _childMap[tailNodeId].Remove(headNodeId);
                    _parentMap[headNodeId].Remove(tailNodeId);
                    --_edges;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// gets the children of the given node
        /// </summary>
        /// <param name="nodeId">node to test</param>
        /// <returns>set of child nodes</returns>
        public HashSet<Int32> GetChildrenOf(int nodeId) => _childMap.ContainsKey(nodeId) ? new HashSet<Int32>(_childMap[nodeId].Keys) : new HashSet<Int32>();

        /// <summary>
        /// gets the parents of a node
        /// </summary>
        /// <param name="nodeId"> node to test</param>
        /// <returns>set of parents</returns>
        public HashSet<Int32> GetParentsOf(int nodeId) => _parentMap.ContainsKey(nodeId) ? new HashSet<Int32>(_parentMap[nodeId].Keys) : new HashSet<Int32>();
      
        /// <summary>
        /// all nodes of a graph
        /// </summary>
        /// <returns>set of the nodes in the graph</returns>
        public HashSet<Int32> GetAllNodes() => new HashSet<Int32>(_childMap.Keys);

        /// <summary>
        /// Clears the graph of all nodes and edges
        /// </summary>
        public void Clear()
        {
            _childMap.Clear();
            _parentMap.Clear();
            _edges = 0;
        }
    }
}
