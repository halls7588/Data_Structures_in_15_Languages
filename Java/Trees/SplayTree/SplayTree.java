/*******************************************************
 *  SplayTree.java
 *  Created by Stephen Hall on 11/14/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  Splay Tree implementation in Java
 ********************************************************/
package DataStructures.Java.Trees.SplayTree;

/**
 * Splay Tree Class
 * @param <Key> generic type
 * @param <Value> generic type
 */
public class SplayTree<Key extends Comparable<Key>, Value> {

    private Node root;   // root of the BST

    /**
     * Node class for splay tree
     */
    public class Node {
        /**
         * Data members of the node
         */
        public Key key;
        public Value value;
        public Node left;
        public Node right;

        /**
         * Node Constructor
         * @param key
         * @param value
         */
        public Node(Key key, Value value) {
            this.key = key;
            this.value = value;
        }
    }

    /**
     * Tests to see if a key exist in the tree
     * @param key: key to test
     * @return boolean: yes|no
     */
    public boolean contains(Key key) {
        return get(key) != null;
    }

    /**
     * Gets the value of the key if it exists
     * @param key: key to test
     * @return Value: value of the key of null
     */
    public Value get(Key key) {
        root = splay(root, key);
        return (key.compareTo(root.key) == 0) ? root.value : null;
    }

    /**
     * adds or updates an item into the tree
     * @param key: key to insert
     * @param value: value of the key
     */
    public void put(Key key, Value value) {
        // splay key to root
        if (root == null) {
            root = new Node(key, value);
            return;
        }

        root = splay(root, key);

        int tmp = key.compareTo(root.key);

        // Insert new node at root
        if (tmp < 0) {
            Node node = new Node(key, value);
            node.left = root.left;
            node.right = root;
            root.left = null;
            root = node;
        }
        // Insert new node at root
        else if (tmp > 0) {
            Node node = new Node(key, value);
            node.right = root.right;
            node.left = root;
            root.right = null;
            root = node;
        }
        // Duplicate key. Update value
        else {
            root.value = value;
        }

    }

    /**
     * Removes a node from the tree
     * @param key: key to remove
     */
    public void remove(Key key) {
        if (root == null)
            return;

        root = splay(root, key);

        int tmp = key.compareTo(root.key);

        if (tmp == 0) {
            if (root.left == null) {
                root = root.right;
            } else {
                Node node = root.right;
                root = root.left;
                splay(root, key);
                root.right = node;
            }
        }
    }

    /**
     * Rotates the given node to the right
     * @param node: node to rotate
     * @return Node: new root of the rotate
     */
    private Node rotateRight(Node node) {
        Node tmp = node.left;
        node.left = tmp.right;
        tmp.right = node;
        return tmp;
    }

    /**
     * Rotates the node to the left
     * @param node: node to rotate
     * @return Node: new root of the rotate
     */
    private Node rotateLeft(Node node) {
        Node tmp = node.right;
        node.right = tmp.left;
        tmp.left = node;
        return tmp;
    }

    /**
     * Splays the node containing key to the root node given
     * @param node: root to splay to
     * @param key: key to find
     * @return Node: Node found or null
     */
    private Node splay(Node node, Key key) {
        if (node == null)
            return null;

        int tmp = key.compareTo(node.key);

        if (tmp < 0) {
            // key not in tree, so we're done
            if (node.left == null) {
                return node;
            }
            int tmp2 = key.compareTo(node.left.key);
            if (tmp2 < 0) {
                node.left.left = splay(node.left.left, key);
                node = rotateRight(node);
            } else if (tmp2 > 0) {
                node.left.right = splay(node.left.right, key);
                if (node.left.right != null)
                    node.left = rotateLeft(node.left);
            }

            if (node.left == null)
                return node;
            else
                return rotateRight(node);
        }
        else if (tmp > 0) {
            // key not in tree, so we're done
            if (node.right == null) {
                return node;
            }

            int tmp2 = key.compareTo(node.right.key);

            if (tmp2 < 0) {
                node.right.left = splay(node.right.left, key);

                if (node.right.left != null)
                    node.right = rotateRight(node.right);
            }
            else if (tmp2 > 0) {
                node.right.right = splay(node.right.right, key);
                node = rotateLeft(node);
            }

            if (node.right == null)
                return node;
            else
                return rotateLeft(node);
        }
        else
            return node;
    }

    /**
     * Gets the height of the tree
     * @return int: height of the tree
     */
    public int height() {
        return height(root);
    }

    /**
     * Gets the height of the given node
     * @param node: node to test
     * @return int: height of the node
     */
    private int height(Node node) {
        return (node == null) ? -1 : Math.max(height(node.left), height(node.right)) + 1;
    }

    /**
     * Gets the size of the tree
     * @return int: size of the tree
     */
    public int size() {
        return size(root);
    }

    /**
     * Gets the size of the given node
     * @param node: nose to test
     * @return int: size of the node
     */
    private int size(Node node) {
        return (node == null) ? 0 : size(node.left) + size(node.right) + 1;
    }
}