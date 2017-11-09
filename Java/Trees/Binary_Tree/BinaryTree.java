/*******************************************************
 *  BinaryTree.java
 *  Created by Stephen Hall on 9/23/17.
 *  Copyright (c) 2016 Stephen Hall. All rights reserved.
 *  A Binary Tree implementation in Java
 ********************************************************/
package DataStructures.Trees;

import java.util.PriorityQueue;
import java.util.Stack;

/**
 * Binary Tree Class
 * @param <T> Generic Type
 */
public class BinaryTree<T> {
    /**
     * Node class for the binary tree
     * @param <T> Generic Type
     */
    public class Node<T>{
        /**
         * public members
         */
        public Node<T> left;
        public Node<T> right;
        public Node<T> parent;
        public T data;

        /**
         * Node class constructor
         * @param data
         */
        public Node(T data){
            left = right = null;
            this.data = data;
        }
    }

    /**
     * Private members
     */
    private Node<T> root;

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
    private int compare(T data, Node<T> node){
       return ((Comparable)node).compareTo(data);
    }

    /**
     * Inserts a new node into the tree
     * @param data data to insert into the tree
     * @return Node inserted into the tree
     */
    public Node<T> Insert(T data){
        if(root == null){
            return (root = new Node(data));
        }
        return this.insertHelper(data, root);
    }

    /**
     * Recursive helper function to inset a new node into the tree
     * @param data Data to insert into the tree
     * @param node Current node in the tree
     * @return Node inserted into the tree
     */
    private Node<T> insertHelper(T data, Node<T> node){
        try {
            int cmp = this.compare(data, node);
            switch(cmp){
                case 1:
                    if (node.right == null) {
                        Node<T> newNode = new Node(data);
                        node.right = newNode;
                        newNode.parent = node;
                        return newNode;
                    } else
                        return this.insertHelper(data, node.right);

                default:
                    if (node.left == null) {
                        Node<T> newNode = new Node(data);
                        node.left = newNode;
                        newNode.parent = node;
                        return newNode;
                    } else
                        return this.insertHelper(data, node.left);
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
    public Node<T> Remove(T data){
        if(root != null)
            return null;
        return this.removeHelper(data, root);
    }

    /**
     * Recursive helper function to remove a node from the tree
     * @param data Data to remove
     * @param node Current node
     * @return Node removed from the tree
     */
    private Node<T> removeHelper(T data, Node<T> node){

        try{
            int cmp = this.compare(data, node);
            if(cmp == 1)
                return this.removeHelper(data, node.right);
            if(cmp == -1)
                return this.removeHelper(data, node.left);
            if(cmp == 0){
                //has no children
                Node<T> tempNode;

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
                        this.root.right = null;
                        this.root = tempNode;
                        return node;
                    }
                    else {
                        tempNode = node.left;
                        tempNode.parent = null;
                        this.root.left = null;
                        this.root = tempNode;
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
        }catch(Exception e){}
            return null;
    }

    /**
     * Returns the smallest node in the tree
     * @return Smallest Node int he tree
     */
    public Node<T> GetMin(){
        if(root == null)
            return null;

        Node<T> node = this.root;

        while(node.left != null)
            node = node.left;

        return node;
    }

    /**
     * Return the largest node in the tree
     * @return Largest Node in the tree
     */
    public Node<T> GetMax(){
        if(root == null)
            return null;

        Node<T> node = this.root;

        while(node.right != null)
            node = node.right;

        return node;
    }

    /**
     * Prints out the tree using Pre Order Traversal
     * @param node Node to start the Pre Order Traversal at
     */
    public void PreOrederTraversal(Node<T> node){
        if(node != null){
            System.out.println(node.data);
            PreOrederTraversal(node.left);
            PreOrederTraversal(node.right);
        }
    }

    /**
     * Prints out the Tree using Post Order Traversal
     * @param node Node to start the Post Order Traversal at
     */
    public void PostPrderTraversal(Node<T> node){
        if(node != null){
            PostPrderTraversal(node.left);
            PostPrderTraversal(node.right);
            System.out.println(node.data);
        }
    }

    /**
     * Pints out the tree using in order Traversal
     * @param node Node to start the In Order Traversal at
     */
    public void InOrderedTraversal(Node<T> node){
        if(node != null){
            InOrderedTraversal(node.left);
            System.out.println(node.data);
            InOrderedTraversal(node.right);
        }
    }

    /**
     *  Prints out the tree using Depth First Search
     * @param node Node to start the Depth First Search at
     */
    public void DepthFirstSearch(Node<T> node){
        if(node == null)
            return;

        Stack<Node<T>> stack = new Stack();
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
    public void BreadthFirstSearch(Node<T> node){
        if(node == null)
            return;

        PriorityQueue<Node<T>> queue = new PriorityQueue();
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
