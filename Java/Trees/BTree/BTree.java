/*******************************************************
 *  BTree.java
 *  Created by Stephen Hall on 11/14/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  B-Tree implementation in Java
 ********************************************************/
package DataStructures.Java.Trees.BTree;

/**
 * Btree class
 * @param <Key> Generic type
 * @param <Value> Generic type
 */
public class BTree<Key extends Comparable<Key>, Value>  {
    // max children per B-tree node = M-1
    private static final int Max = 4;
    private Node root;
    private int height;
    // number of key-value pairs in the B-tree
    private int pairs;

    // helper B-tree node data type

    /**
     * Node class
     */
    public class Node {
        // number of children
        public int size;
        // array of children
        public Entry[] children;

        /**
         * Node Constructor
         * @param size: number of children of the node
         */
        @SuppressWarnings("unchecked")
		private Node(int size) {
        	children  = new BTree.Entry[Max];
            this.size = size;
        }
    }

    /**
     * Helper class for BTree Node class
     */
    public class Entry {
        public Key key;
        public Value val;
        public Node next;

        /**
         * Helper class Constructor
         * @param key: key to hold
         * @param val: value of the key
         * @param next: next node in the tree
         */
        public Entry(Key key, Value val, Node next) {
            this.key  = key;
            this.val  = val;
            this.next = next;
        }
    }

    /**
     * B-tree constructor
     */
    public BTree() {
        root = new Node(0);
    }

    /**
     * Determines if the tree is empty
     * @return boolean: true|false
     */
    public boolean isEmpty() {
        return size() == 0;
    }

    /**
     * Returns the number number of pars in the tree
     * @return int: the number of key-value pairs in the tree
     */
    public int size() {
        return pairs;
    }

    /**
     * Gets the height of the tree
     * @return int: height of the tree
     */
    public int height() {
        return height;
    }

    /**
     * Gets the value of the given key
     * @param key: key to find value of
     * @return Value: value of the given key or null
     */
    public Value get(Key key) {
        if (key == null)
            return null;
        return search(root, key, height);
    }

    /**
     * Searches the tree for the key
     * @param node: node to start at
     * @param key: key to find
     * @param ht: height of the tree
     * @return Value: Value of the key or null
     */
    private Value search(Node node, Key key, int ht) {
        Entry[] children = node.children;

        // external node
        if (ht == 0) {
            for (int j = 0; j < node.size; j++) {
                if (equalTo(key, children[j].key))
                    return (Value) children[j].val;
            }
        }
        // internal node
        else {
            for (int j = 0; j < node.size; j++) {
                if (j + 1 == node.size || lessThan(key, children[j + 1].key))
                    return search(children[j].next, key, (ht - 1));
            }
        }
        return null;
    }


    /**
     * Adds a node into the tree or replaces value
     * @param key: key of the node
     * @param val: value of the key
     */
    public void put(Key key, Value val) {
        if (key == null)
            return;

        Node node = insert(root, key, val, height);
        pairs++;
        
        if (node == null)
            return;

        // need to split root
        Node tmp = new Node(2);
        tmp.children[0] = new Entry(root.children[0].key, null, root);
        tmp.children[1] = new Entry(node.children[0].key, null, node);
        root = tmp;
        height++;
    }

    /**
     * Inserts a node into the tree and updates the tree
     * @param node: root to start at
     * @param key: key of the new node
     * @param val: value of the key
     * @param ht: current height
     * @return Node: node added into the tree
     */
    private Node insert(Node node, Key key, Value val, int ht) {
        int j;
        Entry entry = new Entry(key, val, null);

        // external node
        if (ht == 0) {
            for (j = 0; j < node.size; j++) {
                if (lessThan(key, node.children[j].key))
                    break;
            }
        }
        // internal node
        else {
            for (j = 0; j < node.size; j++) {
                if (((j + 1) == node.size) || lessThan(key, node.children[j + 1].key)) {
                    Node tmp = insert(node.children[j++].next, key, val, (ht - 1));

                    if (tmp == null)
                        return null;

                    entry.key = tmp.children[0].key;
                    entry.next = tmp;
                    break;
                }
            }
        }

        for (int i = node.size; i > j; i--)
            node.children[i] = node.children[i - 1];

        node.children[j] = entry;
        node.size++;

        if (node.size < Max)
            return null;
        else
            return split(node);
    }

    /**
     * Splits the given node in half
     * @param node: node to split
     * @return Node: split node
     */
    private Node split(Node node) {
        Node tmp = new Node((Max / 2));
        node.size = (Max / 2);

        for (int j = 0; j < (Max / 2); j++)
            tmp.children[j] = node.children[(Max / 2 + j)];
        return tmp;
    }

    /**
     * Determines if a is less than b
     * @param a: generic type to test
     * @param b: generic type to test
     * @return boolean: true|false
     */
    private boolean lessThan(Key a, Key b) {
        return a.compareTo(b) < 0;
    }

    /**
     * Determines if a is equal to b
     * @param a: generic type to test
     * @param b: generic type to test
     * @return boolean: true|false
     */
    private boolean equalTo(Key a, Key b) {
        return a.compareTo(b) == 0;
    }
}