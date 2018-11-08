/*******************************************************
 * ArrayedSet.kt
 * Created by Stephen Hall on 11/08/18.
 * Copyright (c) 2018 Stephen Hall. All rights reserved.
 * Arrayed Set implementation in Kotlin
 *****************************************************/

/**
 * ArrayedSet Class
 * @param <T> Generic type
</T> */
class ArrayedSet<T>
/**
 * ArrayedSet constructor initialized to a specific size
 * @param size Size to initialize the array to
 */
(size: Int) {

    /**
     * Private Members
     */
    private var arr: Array<T?>? = null
    private var count: Int = 0
    private var size: Int = 0

    /**
     * ArrayedSet default constructor
     */
    constructor() : this(4) {}

    init {
        count = 0
        if (size > 0)
            this.size = size
        else
            this.size = 4
        arr = arrayOfNulls<Any>(this.size) as Array<T?>
    }

    /**
     * Doubles the size of the internal array
     */
    private fun resize() {
        size *= 2
        val tmp = arrayOfNulls<Any>(size) as Array<T?>
        System.arraycopy(arr, 0, tmp, 0, count)
        arr = tmp
    }

    /**
     * Adds new item into the array
     * @param data Data to add into the array
     * @return Data added into the array
     */
    fun add(data: T?): T? {
        if (data == null)
            return null

        if (contains(data))
            return null

        if (count == size)
            resize()

        arr!![count] = data
        count++
        return data
    }

    /**
     * Appends the contents of an array to the ArrayedSet
     * @param data: array to append
     * @return boolean: success|fail
     */
    fun append(data: Array<T?>?): Boolean {
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
    operator fun set(index: Int, data: T?): Boolean {
        if (!contains(data)) {
            if (index >= 0 && index < size) {
                arr!![index] = data
                return true
            }
        }
        return false
    }

    /**
     * Gets the data at the arrays given index
     * @param index Index to get data at
     * @return Data at the given index or default value of T if index does not exist
     */
    operator fun get(index: Int): T? {
        return if (index >= 0 && index < size) arr!![index] else null
    }

    /**
     * Removes the data at arrays given index
     * @param index Index to remove
     * @return Data removed from the array or default T value if index does not exist
     */
    fun remove(index: Int): T? {
        if (index < 0 || index > count)
            return null

        val tmp = arr!![index]
        arr!![index] = null
        count--
        return tmp
    }

    /**
     * Resets the internal array to default size with no data
     */
    fun reset() {
        count = 0
        size = 4
        arr = arrayOfNulls<Any>(size) as Array<T?>
    }

    /**
     * Clears all data in the array leaving size intact
     */
    fun clear() {
        for (i in 0 until this.count) {
            arr!![i] = null
        }
        count = 0
    }

    /**
     * Tests to see if the data exist in the list
     * @param data: data to find
     */
    private operator fun contains(data: T?): Boolean {
        for (i in 0 until size) {
            if (arr!![i] === data)
                return true
        }
        return false
    }

    /**
     * Gets the current count of the array
     * @return Number of items in the array
     */
    fun count(): Int {
        return count
    }

}
