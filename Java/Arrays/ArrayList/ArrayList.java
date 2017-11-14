/*******************************************************
 *  ArrayList.java
 *  Created by Stephen Hall on 11/06/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  ArrayList implementation in Java
 ********************************************************/

/**
 * ArrayList Class
 * @param <T> Generic type
 */
public class ArrayList<T> {

    /**
     * Private Members
     */
    private T[] array;
    private int count;
    private int size;

    /**
     * ArrayList default constructor
     */
    public ArrayList(){
        this(4);
    }

    /**
     * ArrayList constructor initialized to a specific size
     * @param size Size to initialize the array to
     */
    public ArrayList(int size){
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
    private void Resize(){
        size *= 2;
        T[] tmp = (T[]) new Object[size];
        for(int i = 0; i < count; i++){
            tmp[i] = array[i];
        }
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

        if(count == size)
            resize();

        array[count] = data;
        count++;

        return data;
    }

    /**
     * Appends the contents of an array to the ArrayList
     * @param data: array to append
     * @return boolean: success|fail
     */
    public boolean append(T[] data){
        if(data != null){
            for (int i = 0; i < data.length; i++) {
                if(data[i] != null )
                    add(data[i]);
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
        if(index >= 0 && index < size) {
            array[index] = data;
            return true;
        }
        return false;
    }

    /**
     * Gets the data at the arrays given index
     * @param index Index to get data at
     * @return Data at the given index or default value of T if index does not exist
     */
    public T get(int index){
        return (index >= 0 && index < size) ? array[index] : null;
    }

    /**
     * Removes the data at arrays given index
     * @param index: Index to remove
     * @return T: Data removed from the array or default T value if index does not exist
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
    public void reset(){
        count = 0;
        size = 4;
        array = (T[]) new Object[size];
    }

    /**
     * Clears all data in the array leaving size intact
     */
    public void clear(){
        for(int i = 0; i < count; i++){
            array[i] = null;
        }
        count = 0;
    }

    /**
     * Gets the current count of the array
     * @return Number of items in the array
     */
    public int count(){
        return count;
    }
}
