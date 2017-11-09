/*******************************************************
 *  ArrayedSet.java
 *  Created by Stephen Hall on 11/09/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  Arrayed Set implementation in Java
 ********************************************************/

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
     * CircularArray default constructor
     */
    public ArrayedSet(){
        count = 0;
        size = 4;
        array = (T[]) new Object[size];
    }

    /**
     * CircularArray constructor initialized to a specific size
     * @param size Size to initialize the array to
     */
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
    private void Resize(){
        size *= 2;
        T[] tmp = (T[]) new Object[this.size];
        for(int i = 0; i < this.count; i++){
            tmp[i] = this.array[i];
        }
        this.array = tmp;
    }

    /**
     * Adds new item into the array
     * @param data Data to add into the array
     * @return Data added into the array
     */
    public T Add(T data){
        if(data == null)
            return null;

        if(Contains(data))
            return null;

        if(this.count == size)
            this.Resize();

        this.array[this.count] = data;
        this.count++;

        return data;
    }

    /**
     * Appends the contents of an array to the ArrayedSet
     * @param data: array to append
     * @return boolean: success|fail
     */
    public boolean Append(T[] data){
        if(data != null){
            for (int i = 0; i < data.length; i++) {
                if(data[i] != null )
                    this.Add(data[i]);
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
    public boolean Set(int index, T data){
        if(!Contains(data)){
            if(index >= 0 && index < this.size) {
                this.array[index] = data;
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
    public T Get(int index){
        if(index >= 0 && index < this.size)
            return this.array[index];
        return null;
    }

    /**
     * Removes the data at arrays given index
     * @param index Index to remove
     * @return Data removed from the array or default T value if index does not exist
     */
    public T Remove(int index)
    {
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
    public void Reset(){
        this.count = 0;
        this.size = 4;
        this.array = (T[]) new Object[this.size];
    }

    /**
     * Clears all data in the array leaving size intact
     */
    public void Clear(){
        for(int i = 0; i < this.count; i++){
            this.array[i] = null;
        }
        this.count = 0;
    }

    public boolean Contains(T data){
        for(int i = 0; i < this.size; i++){
            if(this.array[i] == data)
                return true;
        }
        return false;
    }

    /**
     * Gets the current count of the array
     * @return Number of items in the array
     */
    public int Count(){
        return count;
    }
}
