/*******************************************************
 *  CircularQueue.java
 *  Created by Stephen Hall on 11/09/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  Circular Queue implementation in Java
 ********************************************************/
package DataStructures.Queues;

/**
 * Arrayed Queue Class
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
        array = (T[]) new Object[(size = 10)];
        count = zeroIndex = 0;
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
    public T Enqueue(T data){
        if(!IsFull()) {
            array[(zeroIndex + count) % size] = data;
            count++;
            return Top();
        }
        return null;
    }

    /**
     * Removes item from the queue
     * @return item removed from the queue
     */
    public T Dequeue(){
        if(IsEmpty())
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
    public T Top(){
        if(IsEmpty())
            return null;
        return array[zeroIndex];
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
     * @return True if full, false if not
     */
    public boolean IsFull(){
        return (count == size);
    }
}
