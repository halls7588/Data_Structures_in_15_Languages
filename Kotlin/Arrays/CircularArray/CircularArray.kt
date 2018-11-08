/*******************************************************
 * CircularArray.kt
 * Created by Stephen Hall on 11/08/18.
 * Copyright (c) 2018 Stephen Hall. All rights reserved.
 * A Circular Array implementation in Kotlin
 ******************************************************/

/**
 * Circular Array Class
 * @param <T> Generic type
</T> */
class CircularArray<T>
/**
 * CircularArray constructor initialized to a specific size
 * @param size Size to initialize the array to
 */
(private var size: Int) {

    /**
     * Private Members
     */
    private var array: Array<T?>
    private var count: Int = 0
    private var zeroIndex: Int = 0

    /**
     * CircularArray default constructor
     */
    constructor() : this(10) {}

    init {
        zeroIndex = 0
        count = zeroIndex
        array = arrayOfNulls<Any>(this.size) as Array<T?>
    }

    /**
     * Adds new item into the array
     * @param data Data to add into the array
     * @return Data added into the array
     */
    fun add(data: T): T? {
        if (count == size - 1) {
            resize()
        }

        val tmp = (zeroIndex + count) % size
        array[tmp] = data
        count++
        return array[tmp]
    }

    /**
     * Gets the data at the arrays given index
     * @param index Index to get data at
     * @return Data at the given index or default value of T if index does not exist
     */
    fun dataAt(index: Int): T? {
        return if ((index + zeroIndex) % size < count && array[(index + zeroIndex) % size] != null) {
            array[index + zeroIndex % size]
        } else null
    }

    /**
     * Removes the data at arrays given index
     * @param index Index to remove
     * @return Data removed from the array or default T value if index does not exist
     */
    fun remove(index: Int): T? {
        if (index > size)
            return null

        val tmp = array[index + zeroIndex % size]
        array[index + zeroIndex % size] = array[zeroIndex]
        array[zeroIndex] = null
        count--
        zeroIndex = (zeroIndex + 1) % size
        return tmp
    }

    /**
     * Gets the current count of the array
     * @return Number of items in the array
     */
    fun count(): Int {
        return count
    }

    /**
     * Private method to resize the array if capacity has been reached
     */
    private fun resize() {
        val arr = arrayOfNulls<Any>(size * 2) as Array<T?>
        for (i in 0 until count) {
            arr[i] = array[(zeroIndex + i) % size]
        }
        size *= 2
        zeroIndex = 0
        array = arr
    }

    fun print() {
        for (i in 0 until count) {
            println(array[zeroIndex + i % size])
        }
    }

}
