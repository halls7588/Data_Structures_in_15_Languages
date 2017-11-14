/*******************************************************
 *  SelfBalancingBinaryTree.java
 *  Created by Stephen Hall on 11/14/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  SelfBalancingBinaryTree implementation in Java
 ********************************************************/

/**
 * Self Balancing Binary Tree Class
 * @param <T> Generic Type
 */
public class SelfBalancingBinaryTree<T extends Comparable<T>>{
    /**
     * Node class for SelfBalancingBinaryTree
     */
    public class Node{
        public Node left, right;
        public T data;
        public int height;

        /**
         * Node Constructor
         */
        public Node(){
            left = null;
            right = null;
            data = null;
            height = 0;
        }
        /**
         * Node Constructor
         */
        public Node(T n)
        {
            left = null;
            right = null;
            data = n;
            height = 0;
        }
    }

    private Node root;
    private int count;

    /**
     * SelfBalancingBinaryTree Constructor
     */
    /
    public SelfBalancingBinaryTree(){
        root = null;
        count = 0;
    }

    /**
     * Insert data into the tree
     * @param data: data to insert
     */
    public void insert(T data) {
        root = insert(data, root);
    }

    /**
     * Inserts data into the tree
     * @param data: data to insert
     * @param node: node to start at
     * @return Node: new root
     */
    private Node insert(T data, Node node) {
        if (node == null)
            node = new Node(data);

        else if (lessThan(data,node.data)) {
            node.left = insert(data, node.left );
            if (height(node.left) - height(node.right ) == 2)
                if (lessThan(data, node.left.data))
                    node = rotateLeft(node);
                else
                    node = doubleLeft(node);
        }
        else if (greaterThan(data, node.data)) {
            node.right = insert(data, node.right );
            if (height(node.right) - height(node.left) == 2)
                if (greaterThan(data, node.right.data))
                    node = rotateRight(node);
                else
                    node = doubleRight(node);
        }

        node.height = max(height(node.left), height(node.right)) + 1;
        count++;
        return node;
    }

    /**
     * rotates the given root node left
     * @param node: node to rotate
     * @return Node: new root
     */
    private Node rotateLeft(Node node)  {
        Node tmp = node.left;
        node.left = tmp.right;
        tmp.right = node;
        node.height = max(height(node.left), height(node.right)) + 1;
        tmp.height = max(height(tmp.left), node.height) + 1;
        return tmp;
    }

    /**
     * Rotates the given root node right
     * @param node: node to rotate
     * @return Node: new root
     */
    private Node rotateRight(Node node) {
        Node tmp = node.right;
        node.right = tmp.left;
        tmp.left = node;
        node.height = max(height(node.left), height(node.right)) + 1;
        tmp.height = max(height(tmp.right), node.height) + 1;
        return tmp;
    }

    /**
     * Rotates the left child right than the root node left
     * @param node: root node to rotate
     * @return Node: new root
     */
    private Node doubleLeft(Node node) {
        node.left = rotateRight(node.left);
        return rotateLeft(node);
    }

    /**
     * rotates the right child left than the root right
     * @param node: node to rotate
     * @return Node: new root
     */
    private Node doubleRight(Node node) {
        node.right = rotateLeft(node.right);
        return rotateRight(node);
    }

    /**
     * Gets the size of the tree
     * @return int: number of node in the tree
     */
    public int size(){
        return count;
    }

    /**
     * Gets the height of the node
     * @param node: node to test
     * @return int: height of the node
     */
    private int height(Node node) {
        return node == null ? -1 : t.height;
    }

    /**
     * Function to max of left/right node
     * @param left:
     * @param right:
     * @return int: max
     */
    private int max(int left, int right){
        return left > right ? left : right;
    }
    /**
     * check if tree is empty
     * @return boolean: true|false
     */
    public boolean isEmpty(){
        return root == null;
    }

    /**
     * Clears the data from the tree
     */
    public void clear(){
        root = null;
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

    /**
     * Determines if a is greater than b
     * @param a: generic type to test
     * @param b: generic type to test
     * @return boolean: true|false
     */
    private boolean greaterThan(T a, T b) {
        return a.compareTo(b) > 0;
    }

    //TODO: add search and remove
}