/*******************************************************
 *  Deque.java
 *  Created by Stephen Hall on 11/06/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  A Deque implementation in Java
 ********************************************************/
package Queues.Deque;

/**
 * Linked Queue Class
 * @param <T> Generic Type
 */
public class Deque<T> {
    /**
     * Node Class for Linked Queue
     */
    public class Node{
        /**
         * private Member
         */
        private T data;
        private Node next;
        private Node previous;

        /**
         * Node Class Constructor
         * @param data Data to be held in the node
         */
        public Node(T data){
            this.data = data;
            next = null;
            previous = null;
        }
    }

    /**
     * Private Members
     */
    private int count;
    private Node head;
    private Node tail;

    /**
     * Linked Queue Constructor
     */
    public Deque(){
        count = 0;
        head = tail = null;
    }

    /**
     * Adds given data onto the front of the deque
     * @param data Data to be added to the deque
     * @return Node added to the deque
     */
    public Node enqueueFront(T data){
        if(isEmpty()){
            Node node = new Node(data);
            head = tail = node;
            count++;
            return node;
        }
        Node node = new Node(data);
        node.next = head;
        head.previous = node;
        head = node;
        count++;
        return node;
    }

    /**
     * Adds given data onto the back of the deque
     * @param data Data to be added to the deque
     * @return Node added to the deque
     */
    public Node enqueueBack(T data){
        if(isEmpty()){
            Node node = new Node(data);
            head = tail = node;
            count++;
            return node;
        }
        Node node = new Node(data);
        node.next = tail;
        tail.previous = node;
        tail = node;
        count++;
        return node;
    }


    /**
     * Removes item off the front of the deque
     * @return Node popped off of the deque
     */
    public Node dequeueFront(){
        if(isEmpty())
            return null;
        Node node = head;
        head = head.next;
        head.previous = null;
        node.next = null;
        count--;
        return node;
    }

    /**
     * Removes item off the back of the deque
     * @return Node removes from the back of the deque
     */
    public Node dequeueBack(){
        if(isEmpty())
            return null;
        Node node = tail;
        tail = tail.previous;
        tail.next = null;
        node.previous = null;
        count--;
        return node;
    }

    /**
     * Gets the Node at the front of the deque
     * @return Node on top of the deque
     */
    public Node peekFront(){
        return head;
    }

    /**
     * Gets the Node at the back of the deque
     * @return Node on bottom of the deque
     */
    public Node peekBack(){
        return tail;
    }

    /**
     * Returns a value indicating if the deque is empty
     * @return true if empty, false if not
     */
    public boolean isEmpty(){
        return (count == 0);
    }

    /**
     * Returns a value indicating if the deque is full
     * @return False, deque is never full
     */
    public boolean isFull(){
        return false;
    }
}
