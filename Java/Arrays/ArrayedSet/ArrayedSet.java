/*******************************************************
 *  ArrayedSet.java
 *  Created by Stephen Hall on 11/09/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  Arrayed Set implementation in Java
 ********************************************************/
package Arrays.ArrayedSet;

/**
 * ArrayedSet Class
 * @param <T> Generic type
 */
public class ArrayedSet<T> {

    /**
     * Private Members
     */
    private T[] array;
    private int count;
    private int size;

    /**
     * ArrayedSet default constructor
     */
    public ArrayedSet(){
        this(4);
    }

    /**
     * ArrayedSet constructor initialized to a specific size
     * @param size Size to initialize the array to
     */
    @SuppressWarnings("unchecked")
	public ArrayedSet(int size){
        count = 0;
        if(size > 0)
            this.size = size;
        else
            this.size = 4;
        array = (T[]) new Object[this.size];
    }

    /**
     * Doubles the size of the internal array
     */
    @SuppressWarnings("unchecked")
	private void resize(){
        size *= 2;
        T[] tmp = (T[]) new Object[size];
        System.arraycopy(array, 0, tmp, 0, count);
        array = tmp;
    }

    /**
     * Adds new item into the array
     * @param data Data to add into the array
     * @return Data added into the array
     */
    public T add(T data){
        if(data == null)
            return null;

        if(contains(data))
            return null;

        if(count == size)
            resize();

        array[count] = data;
        count++;
        return data;
    }

    /**
     * Appends the contents of an array to the ArrayedSet
     * @param data: array to append
     * @return boolean: success|fail
     */
    public boolean append(T[] data){
        if(data != null){
            for (T aData : data) {
                if (aData != null)
                    add(aData);
            }
            return true;
        }
        return false;
    }

    /**
     * Sets the data at the given index
     * @param index: index to set
     * @param data: data to set index to
     * @return boolean: success|fail
     */
    public boolean set(int index, T data){
        if(!contains(data)){
            if(index >= 0 && index < size) {
                array[index] = data;
                return true;
            }
        }
        return false;
    }

    /**
     * Gets the data at the arrays given index
     * @param index Index to get data at
     * @return Data at the given index or default value of T if index does not exist
     */
    public T get(int index){
        if(index >= 0 && index < size)
            return array[index];
        return null;
    }

    /**
     * Removes the data at arrays given index
     * @param index Index to remove
     * @return Data removed from the array or default T value if index does not exist
     */
    public T remove(int index){
        if (index < 0 || index > count)
            return null;

        T tmp = array[index];
        array[index] = null;
        count--;
        return tmp;
    }

    /**
     * Resets the internal array to default size with no data
     */
    @SuppressWarnings("unchecked")
	public void reset(){
        count = 0;
        size = 4;
        array = (T[]) new Object[size];
    }

    /**
     * Clears all data in the array leaving size intact
     */
    public void clear(){
        for(int i = 0; i < this.count; i++){
            array[i] = null;
        }
        count = 0;
    }
    
    /**
    * Tests to see if the data exist in the list
    * @param data: data to find
    */
    private boolean contains(T data){
        for(int i = 0; i < size; i++){
            if(array[i] == data)
                return true;
        }
        return false;
    }

    /**
     * Gets the current count of the array
     * @return Number of items in the array
     */
    public int count(){
        return count;
    }

}
