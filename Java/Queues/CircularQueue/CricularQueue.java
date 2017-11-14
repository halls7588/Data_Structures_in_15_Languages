/*******************************************************
 *  CircularQueue.java
 *  Created by Stephen Hall on 11/09/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  Circular Queue implementation in Java
 ********************************************************/

/**
 * Circular Queue Class
 * @param <T> Generic type
 */
public class CircularQueue<T> {
    /**
     * Private Members
     */
    private T[] array;
    private int count;
    private int size;
    private int zeroIndex;

    /**
     * Default Constructor
     */
    public CircularQueue(){
        this(10);
    }

    /**
     * Circular Queue Constructor
     * @param size Size to initialize the queue to
     */
    public CircularQueue(int size){
        array = (T[]) new Object[(this.size = size)];
        count = zeroIndex = 0;
    }

    /**
     * Pushes given data onto the queue if space is available
     * @param data Data to be added to the queue
     * @return Node added to the queue
     */
    public T enqueue(T data){
        if(!isFull()) {
            array[(zeroIndex + count) % size] = data;
            count++;
            return top();
        }
        return null;
    }

    /**
     * Removes item from the queue
     * @return item removed from the queue
     */
    public T dequeue(){
        if(isEmpty())
            return null;

        T tmp = array[(zeroIndex)];
        array[zeroIndex] = null;
        count--;
        zeroIndex = (zeroIndex + 1) % size;
        return tmp;
    }

    /**
     * Gets the top item of the queue
     * @return item on top of the queue
     */
    public T top(){
        return (IsEmpty()) ? null : array[zeroIndex];
    }

    /**
     * Returns a value indicating if the queue is empty
     * @return true if empty, false if not
     */
    public boolean isEmpty(){
        return (count == 0);
    }

    /**
     * Returns a value indicating if the queue is full
     * @return True if full, false if not
     */
    public boolean isFull(){
        return (count == size);
    }
}
