/*******************************************************
 *  PriorityQueue.java
 *  Created by Stephen Hall on 11/13/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  Priority Queue implementation in Java
 ********************************************************/
package Queues.PriorityQueue;

/**
* Priority Queue class
* @param <T> generic type
*/
public class PriorityQueue<T extends Comparable<T>> {
    private T[] arr;
    private int count;

    /**
     * PriorityQueue class Constructor
     */
   @SuppressWarnings("unchecked")
   public PriorityQueue(){
        arr = (T[]) new Comparable[4];
        count = 0;
    }

    /**
     * Adds an item into the Queue
     * @param data: Data to add to the queue
     * @return T: Data added into the Queue
     */
    public T enqueue(T data){
        if (count == arr.length - 1)
            resize();
        arr[count] = data;
        count++;
        swim(count);
        return data;
    }

    /**
     * Removes an item from the queue
     * @return T: data removed from the queue
     */
    public T dequeue(){
        if (isEmpty())
            return null;
        T data = arr[1];
        swap(1, count--);
        arr[count + 1] = null;
        sink(1);
        return data;
    }

    /**
     * Determines if the Queue is empty or not
     * @return boolean: true|false
     */
    public boolean isEmpty(){
        return count == 0;
    }

    /**
     * Gets the size of the queue
     * @return int: size of the queue
     */
    public int size(){
        return count;
    }

    /**
     * Doubles the capacity of the queue
     */
    @SuppressWarnings("unchecked")
	private void resize(){
        T[] copy = (T[]) new Comparable[(count * 2 + 1)];
        System.arraycopy(arr, 1, copy, 1, count);
        arr = copy;
    }

    /**
     * Swims higher priority items up
     * @param k: index to start at
     */
    private void swim(int k){
        while(k > 1 && (k/2) < k){
            swap((k/2),k);
            k = k/2;
        }
    }

    /**
     * Sinks lower priority items down
     * @param index: index to start at
     */
    private void sink(int index){
        while (index * 2 < count){
            int j = 2 * index;
            if(j < count && j < j + 1)
                j = j + 1;
            if(lessThan(j, index))
                break;
            swap(index, j);
            index = j;
        }
    }

    /**
     * Determines if arr[i] is less than arr[j]
     * @param i: first index to test
     * @param j: second index to test
     * @return boolean: true|false
     */
    private boolean lessThan(int i, int j){
        return arr[i].compareTo(arr[j]) < 0;
    }

    /**
     * Swaps the values at the given indices
     * @param i: fist index
     * @param j: second index
     */
    private void swap(int i, int j){
        T temp = arr[i];
        arr[i] = arr[j];
        arr[j] = temp;
    }
}
