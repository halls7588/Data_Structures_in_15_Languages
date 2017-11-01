/*******************************************************
 *  LinkedQueue.java
 *  Created by Stephen Hall on 11/01/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  A Linked Queue implementation in Java
 ********************************************************/
package DataStructures.Queues;

/**
 * Linked Queue Class
 * @param <T> Generic Type
 */
public class LinkedQueue<T> {

    /**
     * Node Class for Linked Queue
     * @param <T> Generic Type
     */
    public class Node<T>{

        /**
         * Public Member
         */
        public T data;
        public Node<T> next;

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
    private Node<T> head;
    private Node<T> tail;

    /**
     * Linked Queue Constructor
     */
    public LinkedQueue(){
        count = 0;
        head = tail = null;
    }

    /**
     * Adds given data onto the queue
     * @param data Data to be added to the queue
     * @return Node added to the queue
     */
    public Node<T> Enqueue(T data){
        if(IsEmpty()){
            Node<T> node = new Node(data);
            head = tail = node;
            count++;
            return node;
        }

        Node<T> node = new Node(data);
        node.next = tail;
        tail = node;
        count++;
        return node;
    }

    /**
     * Removes item off the queue
     * @return Node popped off of the queue
     */
    public Node<T> Dequeue(){
        Node<T> node = head;
        head = head.next;
        node.next = null;
        count--;
        return node;
    }

    /**
     * Gets the Node onto of the queue
     * @return Node on top of the queue
     */
    public Node<T> Top(){
        return head;
    }

    /**
     * Returns a value indicating if the queue is empty
     * @return true if empty, false if not
     */
    public boolean IsEmpty(){
        return (count == 0);
    }

    /**
     * Returns a value indicating if the queue is full
     * @return False, Linked queue is never full
     */
    public boolean IsFull(){
        return false;
    }
}
