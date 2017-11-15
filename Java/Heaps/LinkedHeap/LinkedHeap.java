/*******************************************************
 *  ArrayedHeap.java
 *  Created by Stephen Hall on 11/14/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  Arrayed Heap implementation in Java
 ********************************************************/
package DataStructures.Java.Heaps.LinkedHeap;

/**
 * Linked Heap class
 * @param <T> Generic type
 */
public class LinkedHeap<T extends Comparable<T>> {
    /**
     * Node Class
     */
    public class Node {
        public T data;
        public Node left;
        public Node right;
        public int npl;

        /**
         * Node Constructor
         * @param data: data for node to hold
         */
        Node(T data){
            this( data, null, null );
        }

        /**
         * Node Constructor
         * @param data: data for node to hold
         * @param leftt: left child
         * @param right: right child
         */
        Node(T data, Node leftt, Node right ) {
            this.data = data;
            this.left = leftt;
            this.right = right;
            npl = 0;
        }

    }

    private Node root;

    /**
     * Gets the root of the heap
     * @return Node: root node of the heap
     */
    public Node root(){
        return root;
    }

    /**
     * Construct the linked heap.
     */
    public LinkedHeap() {
        root = null;
    }

    /**
     * Merges two heaps together
     * @param heap: heap to merge with
     */
    public void merge(LinkedHeap<T> heap) {
        if(this == heap)
            return;

        root = merge(root, heap.root());
        heap.root = null;
    }

    /**
     * Merges two roots together
     * @param n1: first root
     * @param n2: second root
     * @return Node: merged roots
     */
    private Node merge(Node n1, Node n2 ) {
        if( n1 == null )
            return n2;
        if( n2 == null )
            return n1;
        if(n1.data.compareTo( n2.data ) < 0)
            return merge1(n1, n2);
        else
            return merge1(n2, n1);
    }

    /**
     * Helper method to merge()
     * @param n1: first root
     * @param n2: second root
     * @return Node: merged roots
     */
    private Node merge1(Node n1, Node n2) {
        if( n1.left == null )
            n1.left = n2;
        else {
            n1.right = merge(n1.right, n2 );
            if(n1.left.npl < n1.right.npl )
                swapChildren(n1);
            n1.npl = n1.right.npl + 1;
        }
        return n1;
    }

    /**
     * Swaps the children of the given node
     * @param node: node with two children to swap
     */
    private void swapChildren(Node node) {
        Node tmp = node.left;
        node.left = node.right;
        node.right = tmp;
    }

    /**
     * Insert into the priority queue, maintaining heap order.
     * @param data: the item to insert.
     */
    public void insert(T data) {
        root = merge(new Node(data), root);
    }

    /**
     * Find the smallest item in the priority queue.
     * @return T: smallest item or null.
     */
    public T findMin() {
        return (isEmpty()) ? null : root.data;
    }

    /**
     * Remove the smallest item from the heap
     * @return T: item removed or null
     */
    public T deleteMin() {
        if(isEmpty())
            return null;

        T minItem = root.data;
        root = merge(root.left, root.right);

        return minItem;
    }

    /**
     * Test if heap is empty.
     * @return boolean: true|false.
     */
    public boolean isEmpty(){
        return root == null;
    }

    /**
     * Test if the heap is full.
     * @return false
     */
    public boolean isFull(){
        return false;
    }

    /**
     * Clears the data in the heap
     */
    public void makeEmpty(){
        root = null;
    }
}