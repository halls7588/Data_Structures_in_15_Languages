/*******************************************************
 *  ArrayedStack.java
 *  Created by Stephen Hall on 9/23/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  A Arrayed Stack implementation in Java
 ********************************************************/
package DataStructures.Stacks;

/**
 * Arrayed Stack Class
 * @param <T> Generic type
 */
public class ArrayedStack<T> {
    /**
     * Private Members
     */
    private T[] array;
    private int count;
    private int size;

    /**
     * Default Constructor
     */
    public ArrayedStack(){
        array = (T[]) new Object[(size = 10)];
        count = 0;
    }

    /**
     * Arrayed Stack Constructor
     * @param size Size to initialize the stack to
     */
    public ArrayedStack(int size){
        array = (T[]) new Object[(this.size = size)];
        count = 0;
    }

    /**
     * Pushes given data onto the stack if space is available
     * @param data Data to be added to the stack
     * @return Node added to the stack
     */
    public T Push(T data){
        if(!IsFull()) {
            array[count] = data;
            count++;
            return Top();
        }
        return null;
    }

    /**
     * Pops item off the stack
     * @return Node popped off of the stack
     */
    public T Pop(){
        T data = array[count-1];
        count--;
        return data;
    }

    /**
     * Gets the Node onto of the stack
     * @return Node on top of the stack
     */
    public T Top(){
        return array[count-1];
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
