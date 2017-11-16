/*******************************************************
 *  BinaryTree.java
 *  Created by Stephen Hall on 9/23/17.
 *  Copyright (c) 2016 Stephen Hall. All rights reserved.
 *  A Binary Tree implementation in Java
 ********************************************************/
package Trees.Binary_Tree;

import java.util.PriorityQueue;
import java.util.Stack;

/**
 * Binary Tree Class
 * @param <T> Generic Type
 */
public class BinaryTree<T extends Comparable<T>> {
    /**
     * Node class for the binary tree
     */
    public class Node{
        /**
         * private members
         */
        private Node left;
        private Node right;
        private Node parent;
        private T data;

        /**
         * Node class constructor
         * @param data: data of the node
         */
        public Node(T data){
            left = right = null;
            this.data = data;
        }
    }

    /**
     * Private members
     */
    private Node root;

    /**
     * Binary tree class constructor
     */
    public BinaryTree(){
        root = null;
    }

    /**
     * Compares the data in given node to the give data for equality
     * @param data data to compare
     * @param node Node to compare to
     * @return 1 if greater then, 0 if equal to, -1 if less then
     */
    private int compare(T data, Node node){
       return node.data.compareTo(data);
    }

    /**
     * Inserts a new node into the tree
     * @param data data to insert into the tree
     * @return Node inserted into the tree
     */
    public Node insert(T data){
        return (root == null) ? (root = new Node(data)) : insertHelper(data, root);
    }

    /**
     * Recursive helper function to insert a new node into the tree
     * @param data Data to insert into the tree
     * @param node Current node in the tree
     * @return Node inserted into the tree
     */
    private Node insertHelper(T data, Node node){
        try {
            int cmp = compare(data, node);
            switch(cmp){
                case 1:
                    if (node.right == null) {
                        Node newNode = new Node(data);
                        node.right = newNode;
                        newNode.parent = node;
                        return newNode;
                    } else
                        return insertHelper(data, node.right);
                default:
                    if (node.left == null) {
                        Node newNode = new Node(data);
                        node.left = newNode;
                        newNode.parent = node;
                        return newNode;
                    } else
                        return insertHelper(data, node.left);
            }
        }catch(Exception e) {
            return null;
        }
    }

    /**
     * Removes a Node from the tree
     * @param data Data to remove from the tree
     * @return Node removed from the tree
     */
    public Node remove(T data){
        return (root == null) ? null : removeHelper(data, root);
    }

    /**
     * Recursive helper function to remove a node from the tree
     * @param data Data to remove
     * @param node Current node
     * @return Node removed from the tree
     */
    private Node removeHelper(T data, Node node){
        try{
            int cmp = compare(data, node);
            if(cmp == 1)
                return removeHelper(data, node.right);
            if(cmp == -1)
                return removeHelper(data, node.left);
            if(cmp == 0){
                //has no children
                Node tempNode;

                if(node.left == null && node.right == null) {
                    node = node.parent;
                }

                //has one child
                if(node.parent != null) {
                    tempNode = node.parent;
                }
                else { // this is the root node
                    if(node.right != null) {
                        tempNode = node.right;
                        tempNode.parent = null;
                        root.right = null;
                        root = tempNode;
                        return node;
                    }
                    else {
                        tempNode = node.left;
                        tempNode.parent = null;
                        root.left = null;
                        root = tempNode;
                        return node;
                    }
                }
                if(tempNode.left == node) {
                    if(node.left != null) {
                        tempNode.left = node.left;
                        node.left.parent = tempNode;
                        return node;
                    }
                    else if(node.right != null) {
                        tempNode.right = node.right;
                        node.right.parent = tempNode;
                        return node;
                    }
                }
                if(tempNode.right == node) {
                    if(node.left != null) {
                        tempNode.left = node.left;
                        node.left.parent = tempNode;
                        return node;
                    }
                    else if(node.right != null) {
                        tempNode.right = node.right;
                        node.right.parent = tempNode;
                        return node;
                    }
                }
                else if (node.left != null && node.right != null) { // test for 2 children
                    tempNode = node.right; // find min value of right child tree to replace the node
                    while(tempNode.left != null)
                        tempNode = tempNode.left;
                    T temp = node.data;
                    node.data = tempNode.data; // replace the node with the tempNode
                    tempNode.data = temp;
                    tempNode.parent.left = null;
                    tempNode.parent = null;
                    return tempNode;
                }
            }
        }catch(Exception e){ return null;}
            return null;
    }

    /**
     * Returns the smallest node in the tree
     * @return Smallest Node int he tree
     */
    public Node getMin(){
        if(root == null)
            return null;

        Node node = root;

        while(node.left != null)
            node = node.left;

        return node;
    }

    /**
     * Return the largest node in the tree
     * @return Largest Node in the tree
     */
    public Node getMax(){
        if(root == null)
            return null;

        Node node = root;

        while(node.right != null)
            node = node.right;

        return node;
    }

    /**
     * Prints out the tree using Pre-Order Traversal
     * @param node Node to start the Pre-Order Traversal at
     */
    public void preOrederTraversal(Node node){
        if(node != null){
            System.out.println(node.data);
            preOrederTraversal(node.left);
            preOrederTraversal(node.right);
        }
    }

    /**
     * Prints out the Tree using Post Order Traversal
     * @param node Node to start the Post Order Traversal at
     */
    public void postPrderTraversal(Node node){
        if(node != null){
            postPrderTraversal(node.left);
            postPrderTraversal(node.right);
            System.out.println(node.data);
        }
    }

    /**
     * Pints out the tree using in order Traversal
     * @param node Node to start the In Order Traversal at
     */
    public void inOrderedTraversal(Node node){
        if(node != null){
            inOrderedTraversal(node.left);
            System.out.println(node.data);
            inOrderedTraversal(node.right);
        }
    }

    /**
     *  Prints out the tree using Depth First Search
     * @param node Node to start the Depth First Search at
     */
    public void depthFirstSearch(Node node){
        if(node == null)
            return;

        Stack<Node> stack = new Stack<Node>();
        stack.push(node);

        while (!stack.empty()) {
            System.out.println(node = stack.pop());
            if(node.right!=null)
                stack.push(node.right);
            if(node.left!=null)
                stack.push(node.left);
        }
    }

    /**
     * Prints out the tree using breadth first search
     * @param node Node to start Breadth First Search at
     */
    public void breadthFirstSearch(Node node){
        if(node == null)
            return;

        PriorityQueue<Node> queue = new PriorityQueue<Node>();
        queue.add(node);

        while (queue.size() > 0) {
            System.out.println(node = queue.remove());
            if(node.left != null)
                queue.add(node.left);
            if(node.right != null)
                queue.add(node.right);
        }
    }
}
