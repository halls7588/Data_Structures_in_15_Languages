/*******************************************************
 *  SkipList.java
 *  Created by Stephen Hall on 11/09/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  Skip List implementation in Java
 ********************************************************/
package DataStructures.Java.Lists.Skip_List;

import java.util.List;
import java.util.ArrayList;

/**
 * Skip list class
 * @param <T> Generic type
 */
public class SkipList<T extends Comparable<T>> {
    private Node head;
    private int max;
    private int size;
    private static final double PROBABILITY = 0.5;

    /**
     * Node for Skip List class
     */
    public class Node {
        public T data;
        public List<Node> nodeList;

        /**
         * Node Constructor
         * @param data: data for the node to hold
         */
        public Node(T data) {
            this.data = data;
            nodeList = new ArrayList<Node>();
        }

        /**
         * Gets the level of the Node
         * @return int: level of the node
         */
        public int Level() {
            return nodeList.size() - 1;
        }
    }

    /**
     * Skip List Constructor
     */
    public SkipList() {
        size = 0;
        max = 0;
        // a Node with value null marks the beginning
        head = new Node(null);
        // null marks the end
        head.nodeList.add(null);
    }

    /**
     * Adds a node into the Skip List
     * @param data: data to add into the list
     * @return boolean: success|fail
     */
    public boolean add(T data) {
        if(contains(data))
            return false;
        
        size++;        
        int level = 0;
        
        // random number from 0 to max + 1 (inclusive)
        while (Math.random() < PROBABILITY)
            level++;

        while(level > max) {
            // should only happen once
            head.nodeList.add(null);
            max++;
        }

        Node node = new Node(data);
        Node current = head;

        do {
            current = findNext(data, current, level);
            node.nodeList.add(0, current.nodeList.get(level));
            current.nodeList.set(level, node);
        } while ((level--) > 0);
        return true;
    }

    /**
     * Finds a node in the list with the same data
     * @param data: data to find
     * @return Node: Node found
     */
    private Node find(T data) {
        return find(data, head, max);
    }

    /**
     * Returns node with the greatest value
     * @param data: data to find
     * @param current: current Node
     * @param level: level to start form
     * @return Node: current node
     */
    private Node find(T data, Node current, int level) {
        do {
            current = findNext(data, current, level);
        } while((level--) > 0);
        return current;
    }

    /**
     * Returns the node at a given level with highest value less than data
     * @param data: data to find
     * @param current: current node
     * @param level: current level
     * @return Node: highest node
     */
    private Node findNext(T data, Node current, int level) {
        Node next = (Node)current.nodeList.get(level);

        while(next != null) {
            T value = (T) next.data;
            if(lessThan(data, value))
                break;
            
            current = next;
            next = (Node)current.nodeList.get(level);
        }
        return current;
    }

    /**
     * gets the size of the list
     * @return int: size of the list
     */
    public int size() {
        return size;
    }

    /**
     * Determines if the object is in the list or not
     * @param o: object to test
     * @return boolean: true|false
     */
    @SuppressWarnings("unchecked")
	public boolean contains(Object o) {
        T data = (T)o;
        Node node = find(data);
        return (node != null && node.data != null && equalTo((T)node.data, data));
    }

    /**
     * Determines if a is less than b
     * @param a: generic type to test
     * @param b: generic type to test
     * @return boolean: true|false
     */
    private boolean lessThan(T a, T b) {
        return a.compareTo(b) < 0;
    }

    /**
     * Determines if a is equal to b
     * @param a: generic type to test
     * @param b: generic type to test
     * @return boolean: true|false
     */
    private boolean equalTo(T a, T b) {
        return a.compareTo(b) == 0;
    }
}
