/*******************************************************
 *  Deque.java
 *  Created by Stephen Hall on 11/06/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  A Deque implementation in Java
 ********************************************************/
package DataStructures.Queues;

/**
 * Linked Queue Class
 * @param <T> Generic Type
 */
public class Deque<T> {

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
    public Node<T> previous;

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
    private Node<T> head;
    private Node<T> tail;

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
    public Node<T> EnqueueFront(T data){
        if(IsEmpty()){
            Node<T> node = new Node(data);
            head = tail = node;
            count++;
            return node;
        }

        Node<T> node = new Node(data);
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
    public Node<T> EnqueueBack(T data){
        if(IsEmpty()){
            Node<T> node = new Node(data);
            head = tail = node;
            count++;
            return node;
        }

        Node<T> node = new Node(data);
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
    public Node<T> DequeueFront(){
        if(IsEmpty())
            return null;
        Node<T> node = head;
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
    public Node<T> DequeueBack(){
        if(IsEmpty())
            return null;
        Node<T> node = tail;
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
    public Node<T> PeekFront(){
        return head;
    }

    /**
     * Gets the Node at the back of the deque
     * @return Node on bottom of the deque
     */
    public Node<T> PeekBack(){
        return tail;
    }

    /**
     * Returns a value indicating if the deque is empty
     * @return true if empty, false if not
     */
    public boolean IsEmpty(){
        return (count == 0);
    }

    /**
     * Returns a value indicating if the deque is full
     * @return False, deque is never full
     */
    public boolean IsFull(){
        return false;
    }