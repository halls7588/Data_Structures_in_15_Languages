/*******************************************************
 *  LinkedStack.java
 *  Created by Stephen Hall on 9/22/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  A Linked Stack implementation in Java
 ********************************************************/
package DataStructures.Java.Stacks.Linked_Stack;

/**
 * Linked Stack Class
 * @param <T> Generic Type
 */
public class LinkedStack<T> {
    /**
     * Node Class for Linked Stack
     * @param <T> Generic Type
     */
    public class Node{
        /**
         * Public Member
         */
        public T data;
        public Node next;

        /**
         * Node Class Constructor
         * @param data Data to be held in the node
         */
        public Node(T data){
            this.data = data;
            next = null;
        }
    }

    /**
     * Private Members
     */
    private int count;
    private Node head;

    /**
     * Linked Stack Constructor
     */
    public LinkedStack(){
        count = 0;
        head = null;
    }

    /**
     * Pushes given data onto the stack
     * @param data Data to be added to the stack
     * @return Node added to the stack
     */
    public Node push(T data){
        Node node = new Node(data);
        node.next = head;
        head = node;
        count++;
        return top();
    }

    /**
     * Pops item off the stack
     * @return Node popped off of the stack
     */
    public Node pop(){
        Node node = head;
        head = head.next;
        node.next = null;
        count--;
        return node;
    }

    /**
     * Gets the Node onto of the stack
     * @return Node on top of the stack
     */
    public Node top(){
        return head;
    }

    /**
     * Returns a value indicating if the stack is empty
     * @return true if empty, false if not
     */
    public boolean isEmpty(){
        return (count == 0);
    }

    /**
     * Returns a value indicating if the stack is full
     * @return False, Linked stack is never full
     */
    public boolean isFull(){
        return false;
    }
}
