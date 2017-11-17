using System;
using System.Collections.Generic;

namespace DataStructures.Graphs.UndirecetedGraph
{
    /// <summary>
    /// Undirected Graph class
    /// </summary>
    public class UndirectedGraph
    {
        /// <summary>
        /// private members
        /// </summary>
        private int _edges;
        private readonly Dictionary<Int32, Dictionary<Int32, Double>> _map = new Dictionary<Int32, Dictionary<Int32, Double>>();

        /// <summary>
        /// Gets the size of teh graph
        /// </summary>
        /// <returns>size of the graph</returns>
        public int Size() => _map.Count;

        /// <summary>
        /// gets the number of edges
        /// </summary>
        /// <returns>number of edges</returns>
        public int GetNumberOfEdges() => _edges;

        /// <summary>
        ///  adds a node into the graph
        /// </summary>
        /// <param name="nodeId">id to add</param>
        /// <returns>success|fail</returns>
        public bool AddNode(int nodeId)
        {
            if (_map.ContainsKey(nodeId))
                return false;

            _map.Add(nodeId, new Dictionary<Int32, Double>());
            return true;
        }

        /// <summary>
        /// Adds an edge to the graph
        /// </summary>
        /// <param name="tailNodeId">tail node</param>
        /// <param name="headNodeId">head node</param>
        /// <returns>success|fail</returns>
        public bool AddEdge(int tailNodeId, int headNodeId) => AddEdge(tailNodeId, headNodeId, 1.0);

        /// <summary>
        /// test to see if the node exisits
        /// </summary>
        /// <param name="nodeId">id to find</param>
        /// <returns>true|false</returns>
        public bool HasNode(int nodeId) => _map.ContainsKey(nodeId);

        /// <summary>
        /// clears a node from the graph
        /// </summary>
        /// <param name="nodeId">id to clear</param>
        /// <returns> success|fail</returns>
        public bool ClearNode(int nodeId)
        {
            if (!HasNode(nodeId))
                return false;

            Dictionary<Int32, Double> neighbors = _map[nodeId];
            if (neighbors.Count == 0)
                return false;

            foreach (Int32 neighborId in neighbors.Keys)
                _map[neighborId].Remove(nodeId);

            _edges -= neighbors.Count;
            neighbors.Clear();
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
                _map.Remove(nodeId);
                return true;
            }
            return false;
        }

        /// <summary>
        /// helper function to add an edge to the graph
        /// </summary>
        /// <param name="tailNodeId">tail node</param>
        /// <param name="headNodeId">head node</param>
        /// <param name="weight">weight of the edge</param>
        /// <returns>success|fail</returns>
        public bool AddEdge(int tailNodeId, int headNodeId, double weight)
        {
            if (tailNodeId != headNodeId)
            {
                AddNode(tailNodeId);
                AddNode(headNodeId);

                if (!_map[tailNodeId].ContainsKey(headNodeId))
                {
                    _map[tailNodeId].Add(headNodeId,weight);
                    _map[headNodeId].Add(tailNodeId, weight);
                    ++_edges;
                    return true;
                }
                double oldWeight = _map[tailNodeId][headNodeId];
                _map[tailNodeId][headNodeId] = weight;
                _map[headNodeId][tailNodeId] = weight;
                return oldWeight != weight;
            }
            // Undirected graph are not allowed to contain self-loops.
            return false;
        }

        /// <summary>
        /// tests to see if an edge exisit
        /// </summary>
        /// <param name="tailNodeId">tail node</param>
        /// <param name="headNodeId">head node</param>
        /// <returns>true|false</returns>
        public bool HasEdge(int tailNodeId, int headNodeId) => _map.ContainsKey(tailNodeId) && _map[tailNodeId].ContainsKey(headNodeId);

        /// <summary>
        /// gets the weight of an edge
        /// </summary>
        /// <param name="tailNodeId">tail node</param>
        /// <param name="headNodeId">head node</param>
        /// <returns>weight of the edge</returns>
        public double GetEdgeWeight(int tailNodeId, int headNodeId) => !HasEdge(tailNodeId, headNodeId) ? Double.NaN : _map[tailNodeId][headNodeId];

        /// <summary>
        /// removes an edge from the graph
        /// </summary>
        /// <param name="tailNodeId">tail node</param>
        /// <param name="headNodeId">head node</param>
        /// <returns>success|fail</returns>
        public bool RemoveEdge(int tailNodeId, int headNodeId)
        {
            if (!(_map.ContainsKey(tailNodeId) || (!_map[tailNodeId].ContainsKey(headNodeId))))
                return false;

            _map[tailNodeId].Remove(headNodeId);
            _map[headNodeId].Remove(tailNodeId);
            --_edges;
            return true;
        }

        /// <summary>
        /// gets the children of the given node
        /// </summary>
        /// <param name="nodeId">node to test</param>
        /// <returns>set of child nodes</returns>
        public HashSet<Int32> GetChildrenOf(int nodeId) => _map.ContainsKey(nodeId) ? new HashSet<Int32>(_map[nodeId].Keys) : new HashSet<Int32>();

        /// <summary>
        /// gets the parents of a node
        /// </summary>
        /// <param name="nodeId">node to test</param>
        /// <returns>set of parents</returns>
        public HashSet<Int32> GetParentsOf(int nodeId) => _map.ContainsKey(nodeId) ? new HashSet<Int32>(_map[nodeId].Keys) : new HashSet<Int32>();

        /// <summary>
        ///  Returns all nodes of a graph
        /// </summary>
        /// <returns>set of the nodes in the graph</returns>
        public HashSet<Int32> GetAllNodes() => new HashSet<Int32>(_map.Keys);

        /// <summary>
        /// Clears the graph of all nodes and edges
        /// </summary>
        public void Clear()
        {
            _map.Clear();
            _edges = 0;
        }
    }
}
