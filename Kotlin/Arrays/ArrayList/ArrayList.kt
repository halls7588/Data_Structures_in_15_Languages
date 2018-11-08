/*******************************************************
 * ArrayList.kt
 * Created by Stephen Hall on 11/08/18.
 * Copyright (c) 2018 Stephen Hall. All rights reserved.
 * ArrayList implementation in Kotlin
 ********************************************************/


/**
 * ArrayList Class
 * @param <T> Generic type
</T> */
class ArrayList<T>
/**
 * ArrayList constructor initialized to a specific size
 * @param size Size to initialize the array to
 */
(size: Int) {

    /**
     * Private Members
     */
    private var array: Array<T?>
    private var count: Int = 0
    private var size: Int = 0

    /**
     * ArrayList default constructor
     */
    constructor() : this(4) {}

    init {
        count = 0
        if (size > 0)
            this.size = size
        else
            this.size = 4
        array = arrayOfNulls<Any>(this.size) as Array<T?>
    }

    /**
     * Doubles the size of the internal array
     */
    private fun resize() {
        size *= 2
        val tmp = arrayOfNulls<Any>(size) as Array<T?>
        System.arraycopy(array, 0, tmp, 0, count)
        array = tmp
    }

    /**
     * Adds new item into the array
     * @param data Data to add into the array
     * @return Data added into the array
     */
    fun add(data: T?): T? {
        if (data == null)
            return null

        if (count == size)
            resize()

        array[count] = data
        count++

        return data
    }

    /**
     * Appends the contents of an array to the ArrayList
     * @param data: array to append
     * @return boolean: success|fail
     */
    fun append(data: Array<T>?): Boolean {
        if (data != null) {
            for (aData in data) {
                if (aData != null)
                    add(aData)
            }
            return true
        }
        return false
    }

    /**
     * Sets the data at the given index
     * @param index: index to set
     * @param data: data to set index to
     * @return boolean: success|fail
     */
    operator fun set(index: Int, data: T): Boolean {
        if (index >= 0 && index < size) {
            array[index] = data
            return true
        }
        return false
    }

    /**
     * Gets the data at the arrays given index
     * @param index Index to get data at
     * @return Data at the given index or default value of T if index does not exist
     */
    operator fun get(index: Int): T? {
        return if (index >= 0 && index < size) array[index] else null
    }

    /**
     * Removes the data at arrays given index
     * @param index: Index to remove
     * @return T: Data removed from the array or default T value if index does not exist
     */
    fun remove(index: Int): T? {
        if (index < 0 || index > count)
            return null

        val tmp = array[index]
        array[index] = null
        count--
        return tmp
    }

    /**
     * Resets the internal array to default size with no data
     */
    fun reset() {
        count = 0
        size = 4
        array = arrayOfNulls<Any>(size) as Array<T?>
    }

    /**
     * Clears all data in the array leaving size intact
     */
    fun clear() {
        for (i in 0 until count) {
            array[i] = null
        }
        count = 0
    }

    /**
     * Gets the current count of the array
     * @return Number of items in the array
     */
    fun count(): Int {
        return count
    }
}