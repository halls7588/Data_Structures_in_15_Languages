/*******************************************************
 *  UndirectedGraph.java
 *  Created by Stephen Hall on 11/14/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  Undirected Graph implementation in Java
 ********************************************************/
package DataStructures.Java.Graphs.UndirectedGraph;

import java.util.Collections;
import java.util.LinkedHashMap;
import java.util.Map;
import java.util.Set;

/**
 * Undirected Graph class
 */
public class UndirectedGraph {
    /**
     * private members
     */
    private int edges;
    private final Map<Integer, Map<Integer, Double>> map = new LinkedHashMap<>();

    /**
     * Gets the size of the graph
     * @return int: size of the graph
     */
    public int size() {
        return map.size();
    }

    /**
     * gets the number of edges
     * @return int: number of edges
     */
    public int getNumberOfEdges() {
        return edges;
    }

    /**
     * adds a node into the graph
     * @param nodeId: id to add
     * @return boolean: success|fail
     */
    public boolean addNode(int nodeId) {
        if (map.containsKey(nodeId)) {
            return false;
        }

        map.put(nodeId, new LinkedHashMap<Integer, Double>());
        return true;
    }

    /**
     * Adds an edge to the graph
     * @param tailNodeId: tail node
     * @param headNodeId: head node
     * @return boolean: success|fail
     */
    public boolean addEdge(int tailNodeId, int headNodeId) {
        return addEdge(tailNodeId, headNodeId, 1.0);
    }

    /**
     * test to see if the node exisits
     * @param nodeId: id to find
     * @return boolean: true|false
     */
    public boolean hasNode(int nodeId) {
        return map.containsKey(nodeId);
    }

    /**
     * clears a node from the graph
     * @param nodeId: id to clear
     * @return boolean: success|fail
     */
    public boolean clearNode(int nodeId) {
        if (!hasNode(nodeId)) {
            return false;
        }

        Map<Integer, Double> neighbors = map.get(nodeId);

        if (neighbors.isEmpty()) {
            return false;
        }

        for (Integer neighborId : neighbors.keySet()) {
            map.get(neighborId).remove(nodeId);
        }

        edges -= neighbors.size();
        neighbors.clear();
        return true;
    }

    /**
     * removes a node from the graph
     * @param nodeId: id to remove
     * @return boolean: success|fail
     */
    public boolean removeNode(int nodeId) {
        if (!hasNode(nodeId)) {
            return false;
        }

        clearNode(nodeId);
        map.remove(nodeId);
        return true;
    }

    /**
     * helper function to add an edge to the graph
     * @param tailNodeId: tail node
     * @param headNodeId: head node
     * @param weight: weight of the edge
     * @return boolean: success|fail
     */
    public boolean addEdge(int tailNodeId, int headNodeId, double weight) {
        if (tailNodeId == headNodeId) {
            // Undirected graph are not allowed to contain self-loops.
            return false;
        }

        addNode(tailNodeId);
        addNode(headNodeId);

        if (!map.get(tailNodeId).containsKey(headNodeId)) {
            map.get(tailNodeId).put(headNodeId, weight);
            map.get(headNodeId).put(tailNodeId, weight);
            ++edges;
            return true;
        } else {
            double oldWeight = map.get(tailNodeId).get(headNodeId);
            map.get(tailNodeId).put(headNodeId, weight);
            map.get(headNodeId).put(tailNodeId, weight);
            return oldWeight != weight;
        }
    }

    /**
     * tests to see if an edge exisit
     * @param tailNodeId: tail node
     * @param headNodeId: head node
     * @return boolean: true|false
     */
    public boolean hasEdge(int tailNodeId, int headNodeId) {
        if (!map.containsKey(tailNodeId)) {
            return false;
        }

        return map.get(tailNodeId).containsKey(headNodeId);
    }

    /**
     * gets the weight of an edge
     * @param tailNodeId: tail node
     * @param headNodeId: head node
     * @return double: weight of the edge
     */
    public double getEdgeWeight(int tailNodeId, int headNodeId) {
        if (!hasEdge(tailNodeId, headNodeId)) {
            return Double.NaN;
        }

        return map.get(tailNodeId).get(headNodeId);
    }

    /**
     * removes an edge from the graph
     * @param tailNodeId: tail node
     * @param headNodeId: head node
     * @return boolean: success|fail
     */
    public boolean removeEdge(int tailNodeId, int headNodeId) {
        if (!map.containsKey(tailNodeId)) {
            return false;
        }

        if (!map.get(tailNodeId).containsKey(headNodeId)) {
            return false;
        }

        map.get(tailNodeId).remove(headNodeId);
        map.get(headNodeId).remove(tailNodeId);
        --edges;
        return true;
    }

    /**
     * gets the children of the given node
     * @param nodeId: node to test
     * @return Set: set of child nodes
     */
    public Set<Integer> getChildrenOf(int nodeId) {
        if (!map.containsKey(nodeId)) {
            return Collections.<Integer>emptySet();
        }

        return Collections.<Integer>unmodifiableSet(map.get(nodeId).keySet());
    }

    /**
     * gets the parents of a node
     * @param nodeId: node to test
     * @return Set: set of parents
     */
    public Set<Integer> getParentsOf(int nodeId) {
        if (!map.containsKey(nodeId)) {
            return Collections.<Integer>emptySet();
        }

        return Collections.<Integer>unmodifiableSet(map.get(nodeId).keySet());
    }

    /**
     * Returns all nodes of a graph
     * @return Set: set of the nodes in the graph
     */
    public Set<Integer> getAllNodes() {
        return Collections.<Integer>unmodifiableSet(map.keySet());
    }

    /**
     * Clears the graph of all nodes and edges
     */
    public void clear() {
        map.clear();
        edges = 0;
    }
}