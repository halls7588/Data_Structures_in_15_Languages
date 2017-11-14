/*******************************************************
 *  DirectedGraph.java
 *  Created by Stephen Hall on 11/14/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  Directed Graph implementation in Java
 ********************************************************/
import java.util.Collections;
import java.util.Iterator;
import java.util.LinkedHashMap;
import java.util.Map;
import java.util.Set;

/**
 * Directed Graph class
 */
public class DirectedGraph extends AbstractGraph {
    /**
     * private members
     */
    private int edges;
    private final Map<Integer, Map<Integer, Double>> parentMap = new LinkedHashMap<>();
    private final Map<Integer, Map<Integer,Double>> childMap = new LinkedHashMap<>();

    /**
     * Gets the size of the graph
     * @return int: size of the graph
     */
    public int size() {
        return parentMap.size();
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
        if (parentMap.containsKey(nodeId)) {
            return false;
        }

        parentMap.put(nodeId, new LinkedHashMap<Integer, Double>());
        childMap.put(nodeId, new LinkedHashMap<Integer, Double>());
        return true;
    }

    /**
     * test to see if the node exisits
     * @param nodeId: id to find
     * @return boolean: true|false
     */
    @Override
    public boolean hasNode(int nodeId) {
        return parentMap.containsKey(nodeId);
    }

    /**
     * clears a node from the graph
     * @param nodeId: id to clear
     * @return boolean: success|fail
     */
    @Override
    public boolean clearNode(int nodeId) {
        if (!hasNode(nodeId)) {
            return false;
        }

        Map<Integer, Double> parents = parentMap.get(nodeId);
        Map<Integer, Double> children = childMap.get(nodeId);

        if (parents.isEmpty() && children.isEmpty()) {
            return false;
        }

        for (Integer childId : children.keySet()) {
            parentMap.get(childId).remove(nodeId);
        }

        for (Integer parentId : parents.keySet()) {
            childMap.get(parentId).remove(nodeId);
        }

        edges -= parents.size();
        edges -= children.size();
        parents.clear();
        children.clear();
        return true;
    }

    /**
     * removes a node from the graph
     * @param nodeId: id to remove
     * @return boolean: success|fail
     */
    @Override
    public boolean removeNode(int nodeId) {
        if (!hasNode(nodeId)) {
            return false;
        }

        clearNode(nodeId);
        parentMap.remove(nodeId);
        childMap.remove(nodeId);
        return true;
    }

    /**
     * helper function to add an edge to the graph
     * @param tailNodeId: tail node
     * @param headNodeId: head node
     * @param weight: weight of the edge
     * @return boolean: success|fail
     */
    @Override
    public boolean addEdge(int tailNodeId, int headNodeId, double weight) {
        addNode(tailNodeId);
        addNode(headNodeId);

        if (childMap.get(tailNodeId).containsKey(headNodeId)) {
            double oldWeight = childMap.get(tailNodeId).get(headNodeId);
            childMap.get(tailNodeId).put(headNodeId, weight);
            parentMap.get(headNodeId).put(tailNodeId, weight);
            return oldWeight != weight;
        } else {
            childMap.get(tailNodeId).put(headNodeId, weight);
            parentMap.get(headNodeId).put(tailNodeId, weight);
            ++edges;
            return true;
        }
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
     * tests to see if an edge exisit
     * @param tailNodeId: tail node
     * @param headNodeId: head node
     * @return boolean: true|false
     */
    @Override
    public boolean hasEdge(int tailNodeId, int headNodeId) {
        if (!childMap.containsKey(tailNodeId)) {
            return false;
        }
        return childMap.get(tailNodeId).containsKey(headNodeId);
    }

    /**
     * gets the weight of an edge
     * @param tailNodeId: tail node
     * @param headNodeId: head node
     * @return double: weight of the edge
     */
    @Override
    public double getEdgeWeight(int tailNodeId, int headNodeId) {
        if (!hasEdge(tailNodeId, headNodeId)) {
            return Double.NaN;
        }
        return childMap.get(tailNodeId).get(headNodeId);
    }

    /**
     * removes an edge from the graph
     * @param tailNodeId: tail node
     * @param headNodeId: head node
     * @return boolean: success|fail
     */
    @Override
    public boolean removeEdge(int tailNodeId, int headNodeId) {
        if (!childMap.containsKey(tailNodeId)) {
            return false;
        }

        if (!childMap.get(tailNodeId).containsKey(headNodeId)) {
            return false;
        }

        childMap.get(tailNodeId).remove(headNodeId);
        parentMap.get(headNodeId).remove(tailNodeId);
        --edges;
        return true;
    }

    /**
     * gets the children of the given node
     * @param nodeId: node to test
     * @return Set: set of child nodes
     */
    @Override
    public Set<Integer> getChildrenOf(int nodeId) {
        if (!childMap.containsKey(nodeId)) {
            return Collections.<Integer>emptySet();
        }
        return Collections.<Integer>unmodifiableSet(childMap.get(nodeId).keySet());
    }

    /**
     * gets the parents of a node
     * @param nodeId: node to test
     * @return Set: set of parrents
     */
    @Override
    public Set<Integer> getParentsOf(int nodeId) {
        if (!parentMap.containsKey(nodeId)) {
            return Collections.<Integer>emptySet();
        }
        return Collections.<Integer>unmodifiableSet(parentMap.get(nodeId).keySet());
    }

    /**
     * retruns all nodes of a graph
     * @return Set: set of the nodes in the graph
     */
    @Override
    public Set<Integer> getAllNodes() {
        return Collections.<Integer>unmodifiableSet(childMap.keySet());
    }

    /**
     * Clears the graph of all nodes and edges
     */
    @Override
    public void clear() {
        childMap.clear();
        parentMap.clear();
        edges = 0;
    }
}