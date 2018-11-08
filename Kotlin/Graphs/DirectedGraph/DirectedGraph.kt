/*******************************************************
 * DirectedGraph.kt
 * Created by Stephen Hall on 11/08/18.
 * Copyright (c) 2018 Stephen Hall. All rights reserved.
 * Directed Graph implementation in Kotlin
 ******************************************************/

import java.util.Collections
import java.util.LinkedHashMap

/**
 * Directed Graph class
 */
class DirectedGraph {
    /**
     * private members
     */
    /**
     * gets the number of edges
     * @return int: number of edges
     */
    var numberOfEdges: Int = 0
        private set
    private val parentMap = LinkedHashMap<Int,  MutableMap<Int, Double>>()
    private val childMap = LinkedHashMap<Int,  MutableMap<Int, Double>>()

    /**
     * Returns all nodes of a graph
     * @return Set: set of the nodes in the graph
     */
    val allNodes: Set<Int>
        get() = Collections.unmodifiableSet(childMap.keys)

    /**
     * Gets the size of the graph
     * @return int: size of the graph
     */
    fun size(): Int {
        return parentMap.size
    }

    /**
     * adds a node into the graph
     * @param nodeId: id to add
     * @return boolean: success|fail
     */
    fun addNode(nodeId: Int): Boolean {
        if (parentMap.containsKey(nodeId)) {
            return false
        }

        parentMap[nodeId] = LinkedHashMap()
        childMap[nodeId] = LinkedHashMap()
        return true
    }

    /**
     * test to see if the node exisits
     * @param nodeId: id to find
     * @return boolean: true|false
     */
    fun hasNode(nodeId: Int): Boolean {
        return parentMap.containsKey(nodeId)
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

        val parents = parentMap[nodeId]
        val children = childMap[nodeId]

        if (parents!!.isEmpty() && children!!.isEmpty()) {
            return false
        }

        children!!.keys.forEach { childId -> parentMap[childId]!!.remove(nodeId) }
        parents!!.keys.forEach { parentId -> childMap[parentId]!!.remove(nodeId) }

        numberOfEdges -= parents.size
        numberOfEdges -= children.size
        parents.clear()
        children.clear()
        return true
    }

    /**
     * removes a node from the graph
     * @param nodeId: id to remove
     * @return boolean: success|fail
     */
    fun removeNode(nodeId: Int): Boolean {
        if (!hasNode(nodeId)) {
            return false
        }

        clearNode(nodeId)
        parentMap.remove(nodeId)
        childMap.remove(nodeId)
        return true
    }

    /**
     * helper function to add an edge to the graph
     * @param tailNodeId: tail node
     * @param headNodeId: head node
     * @param weight: weight of the edge
     * @return boolean: success|fail
     */
    private fun addEdge(tailNodeId: Int, headNodeId: Int, weight: Double): Boolean {
        addNode(tailNodeId)
        addNode(headNodeId)

        if (childMap[tailNodeId]!!.containsKey(headNodeId)) {
            val oldWeight = childMap[tailNodeId]!![headNodeId]
            childMap[tailNodeId]!!.put(headNodeId, weight)
            parentMap[headNodeId]!!.put(tailNodeId, weight)
            return oldWeight != weight
        } else {
            childMap[tailNodeId]!!.put(headNodeId, weight)
            parentMap[headNodeId]!!.put(tailNodeId, weight)
            ++numberOfEdges
            return true
        }
    }

    /**
     * Adds an edge to the graph
     * @param tailNodeId: tail node
     * @param headNodeId: head node
     * @return boolean: success|fail
     */
    fun addEdge(tailNodeId: Int, headNodeId: Int): Boolean {
        return addEdge(tailNodeId, headNodeId, 1.0)
    }

    /**
     * tests to see if an edge exist
     * @param tailNodeId: tail node
     * @param headNodeId: head node
     * @return boolean: true|false
     */
    fun hasEdge(tailNodeId: Int, headNodeId: Int): Boolean {
        return childMap.containsKey(tailNodeId) && childMap[tailNodeId]!!.containsKey(headNodeId)
    }

    /**
     * gets the weight of an edge
     * @param tailNodeId: tail node
     * @param headNodeId: head node
     * @return double: weight of the edge
     */
    fun getEdgeWeight(tailNodeId: Int, headNodeId: Int): Double? {
        return if (!hasEdge(tailNodeId, headNodeId)) java.lang.Double.NaN else childMap[tailNodeId]!!.get(headNodeId)
    }

    /**
     * removes an edge from the graph
     * @param tailNodeId: tail node
     * @param headNodeId: head node
     * @return boolean: success|fail
     */
    fun removeEdge(tailNodeId: Int, headNodeId: Int): Boolean {
        if (childMap.containsKey(tailNodeId)) {
            if (childMap[tailNodeId]!!.containsKey(headNodeId)) {
                childMap[tailNodeId]!!.remove(headNodeId)
                parentMap[headNodeId]!!.remove(tailNodeId)
                --numberOfEdges
                return true
            }
        }
        return false
    }

    /**
     * gets the children of the given node
     * @param nodeId: node to test
     * @return Set: set of child nodes
     */
    fun getChildrenOf(nodeId: Int): Set<Int> {
        return if (!childMap.containsKey(nodeId)) emptySet() else Collections.unmodifiableSet(childMap[nodeId]!!.keys)
    }

    /**
     * gets the parents of a node
     * @param nodeId: node to test
     * @return Set: set of parents
     */
    fun getParentsOf(nodeId: Int): Set<Int> {
        return if (!parentMap.containsKey(nodeId)) emptySet() else Collections.unmodifiableSet(parentMap[nodeId]!!.keys)
    }

    /**
     * Clears the graph of all nodes and edges
     */
    fun clear() {
        childMap.clear()
        parentMap.clear()
        numberOfEdges = 0
    }
}