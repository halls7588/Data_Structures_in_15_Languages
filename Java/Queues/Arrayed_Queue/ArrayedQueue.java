/*******************************************************
 *  ArrayedQueue.java
 *  Created by Stephen Hall on 11/01/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  A Arrayed Queue implementation in Java
 ********************************************************/

/**
 * Arrayed Queue Class
 * @param <T> Generic type
 */
public class ArrayedQueue<T> {
    /**
     * Private Members
     */
    private T[] array;
    private int count;
    private int size;

    /**
     * Default Constructor
     */
    public ArrayedQueue(){
       this(10);
    }

    /**
     * Arrayed Queue Constructor
     * @param size Size to initialize the queue to
     */
    public ArrayedQueue(int size){
        array = (T[]) new Object[(this.size = size)];
        count = 0;
    }

    /**
     * Pushes given data onto the queue if space is available
     * @param data Data to be added to the queue
     * @return Node added to the queue
     */
    public T enqueue(T data){
        if(!isFull()) {
            array[count] = data;
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
        T data = array[0];
        T[] tmp = new (T[]) new Object[(this.size)];
        for(int i = 1; i < size-1; i++){
            tmp[i-1] = array[i];
        }
        array = tmp;
        count--;
        return data;
    }

    /**
     * Gets the top item of the queue
     * @return item on top of the queue
     */
    public T top(){
        return (isEmpty()) ? null : array[0];
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
