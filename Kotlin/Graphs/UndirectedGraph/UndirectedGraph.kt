/*******************************************************
 * UndirectedGraph.kt
 * Created by Stephen Hall on 11/08/18.
 * Copyright (c) 2018 Stephen Hall. All rights reserved.
 * Undirected Graph implementation in Kotlin
 *****************************************************/

import java.util.Collections
import java.util.LinkedHashMap

/**
 * Undirected Graph class
 */
class UndirectedGraph {
    /**
     * private members
     */
    /**
     * gets the number of edges
     * @return int: number of edges
     */
    private var numberOfEdges: Int = 0
    private val map = LinkedHashMap<Int, MutableMap<Int, Double>>()

    /**
     * Returns all nodes of a graph
     * @return Set: set of the nodes in the graph
     */
    val allNodes: Set<Int>
        get() = Collections.unmodifiableSet(map.keys)

    /**
     * Gets the size of the graph
     * @return int: size of the graph
     */
    fun size(): Int {
        return map.size
    }

    /**
     * adds a node into the graph
     * @param nodeId: id to add
     * @return boolean: success|fail
     */
    fun addNode(nodeId: Int): Boolean {
        if (map.containsKey(nodeId)) {
            return false
        }

        map[nodeId] = LinkedHashMap()
        return true
    }

    /**
     * test to see if the node exisits
     * @param nodeId: id to find
     * @return boolean: true|false
     */
    fun hasNode(nodeId: Int): Boolean {
        return map.containsKey(nodeId)
    }

    /**
     * clears a node from the graph
     * @param nodeId: id to clear
     * @return boolean: success|fail
     */
    fun clearNode(nodeId: Int): Boolean {
        if (!hasNode(nodeId)) {
            return false
        }

        val neighbors = map[nodeId]
        if (neighbors!!.isEmpty()) {
            return false
        }

        neighbors.keys.forEach { neighborId -> map[neighborId]!!.remove(nodeId) }

        numberOfEdges -= neighbors.size
        neighbors.clear()
        return true
    }

    /**
     * removes a node from the graph
     * @param nodeId: id to remove
     * @return boolean: success|fail
     */
    fun removeNode(nodeId: Int): Boolean {
        if (hasNode(nodeId)) {
            clearNode(nodeId)
            map.remove(nodeId)
            return true
        }
        return false
    }

    /**
     * helper function to add an edge to the graph
     * @param tailNodeId: tail node
     * @param headNodeId: head node
     * @param weight: weight of the edge
     * @return boolean: success|fail
     */
    @JvmOverloads
    fun addEdge(tailNodeId: Int, headNodeId: Int, weight: Double = 1.0): Boolean {
        if (tailNodeId != headNodeId) {

            addNode(tailNodeId)
            addNode(headNodeId)

            if (!map[tailNodeId]!!.containsKey(headNodeId)) {
                map[tailNodeId]!![headNodeId] = weight
                map[headNodeId]!![tailNodeId] = weight
                ++numberOfEdges
                return true
            } else {
                val oldWeight = map[tailNodeId]!![headNodeId]
                map[tailNodeId]!![headNodeId] = weight
                map[headNodeId]!![tailNodeId] = weight
                return oldWeight != weight
            }
        }
        // Undirected graph are not allowed to contain self-loops.
        return false
    }

    /**
     * tests to see if an edge exisit
     * @param tailNodeId: tail node
     * @param headNodeId: head node
     * @return boolean: true|false
     */
    fun hasEdge(tailNodeId: Int, headNodeId: Int): Boolean {
        return map.containsKey(tailNodeId) && map[tailNodeId]!!.containsKey(headNodeId)
    }

    /**
     * gets the weight of an edge
     * @param tailNodeId: tail node
     * @param headNodeId: head node
     * @return double: weight of the edge
     */
    fun getEdgeWeight(tailNodeId: Int, headNodeId: Int): Double? {
        return if (!hasEdge(tailNodeId, headNodeId)) java.lang.Double.NaN else map[tailNodeId]!!.get(headNodeId)

    }

    /**
     * removes an edge from the graph
     * @param tailNodeId: tail node
     * @param headNodeId: head node
     * @return boolean: success|fail
     */
    fun removeEdge(tailNodeId: Int, headNodeId: Int): Boolean {
        if (!(map.containsKey(tailNodeId) || !map[tailNodeId]!!.containsKey(headNodeId)))
            return false

        map[tailNodeId]!!.remove(headNodeId)
        map[headNodeId]!!.remove(tailNodeId)
        --numberOfEdges
        return true
    }

    /**
     * gets the children of the given node
     * @param nodeId: node to test
     * @return Set: set of child nodes
     */
    fun getChildrenOf(nodeId: Int): Set<Int> {
        return if (map.containsKey(nodeId)) Collections.unmodifiableSet(map[nodeId]!!.keys) else emptySet()

    }

    /**
     * gets the parents of a node
     * @param nodeId: node to test
     * @return Set: set of parents
     */
    fun getParentsOf(nodeId: Int): Set<Int> {
        return if (map.containsKey(nodeId)) Collections.unmodifiableSet(map[nodeId]!!.keys) else emptySet()

    }

    /**
     * Clears the graph of all nodes and edges
     */
    fun clear() {
        map.clear()
        numberOfEdges = 0
    }
}
