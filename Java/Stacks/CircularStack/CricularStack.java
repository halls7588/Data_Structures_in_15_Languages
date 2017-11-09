/*******************************************************
 *  CircularStack.java
 *  Created by Stephen Hall on 11/09/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  A Circular Stack implementation in Java
 ********************************************************/
package DataStructures.Stacks;

/**
 * Circular Stack Class
 * @param <T> Generic type
 */
public class CircularStack<T> {
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
    public CircularStack(){
        array = (T[]) new Object[(this.size = 10)];
        count = zeroIndex = 0;
    }

    /**
     * Circular Stack Constructor
     * @param size Size to initialize the stack to
     */
    public CircularStack(int size){
        array = (T[]) new Object[(this.size = size)];
        count = zeroIndex = 0;
    }

    /**
     * Pushes given data onto the stack if space is available
     * @param data Data to be added to the stack
     * @return item added to the stack
     */
    public T Push(T data){
        if(!IsFull()) {
            array[(zeroIndex + count) % size] = data;
            count++;
            return Top();
        }
        return null;
    }

    /**
     * Pops item off the stack
     * @return item popped off of the stack
     */
    public T Pop(){
        if(IsEmpty())
            return null;
        T data = array[(count + zeroIndex % size) -1];
        array[(count + zeroIndex % size) -1] = array[zeroIndex];
        array[zeroIndex] = null;
        count--;
        zeroIndex = (zeroIndex + 1) % size;
        return data;
    }

    /**
     * Gets the item onto of the stack
     * @return item on top of the stack
     */
    public T Top(){
        return array[((zeroIndex + count) % size) - 1];
    }

    /**
     * Returns a value indicating if the stack is empty
     * @return true if empty, false if not
     */
    public boolean IsEmpty(){
        return (count == 0);
    }

    /**
     * Returns a value indicating if the stack is full
     * @return True if full, false if not
     */
    public boolean IsFull(){
        return (count == size);
    }
}
