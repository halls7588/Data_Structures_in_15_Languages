/*******************************************************
 * PriorityQueue.kt
 * Created by Stephen Hall on 11/08/18.
 * Copyright (c) 2018 Stephen Hall. All rights reserved.
 * Priority Queue implementation in Kotlin
 ******************************************************/

/**
 * Priority Queue class
 * @param <T> generic type
</T> */
class PriorityQueue<T : Comparable<T?>> {
    private var arr: Array<T?>
    private var count: Int = 0

    /**
     * Determines if the Queue is empty or not
     * @return boolean: true|false
     */
    val isEmpty: Boolean
        get() = count == 0

    /**
     * PriorityQueue class Constructor
     */
    init {
        arr = arrayOfNulls<Comparable<*>>(4) as Array<T?>
        count = 0
    }

    /**
     * Adds an item into the Queue
     * @param data: Data to add to the queue
     * @return T: Data added into the Queue
     */
    fun enqueue(data: T): T? {
        if (count == arr.size - 1)
            resize()
        arr[count] = data
        count++
        swim(count)
        return data
    }

    /**
     * Removes an item from the queue
     * @return T: data removed from the queue
     */
    fun dequeue(): T? {
        if (isEmpty)
            return null
        val data = arr[1]
        swap(1, count--)
        arr[count + 1] = null
        sink(1)
        return data
    }

    /**
     * Gets the size of the queue
     * @return int: size of the queue
     */
    fun size(): Int {
        return count
    }

    /**
     * Doubles the capacity of the queue
     */
    private fun resize() {
        val copy = arrayOfNulls<Comparable<*>>(count * 2 + 1) as Array<T?>
        System.arraycopy(arr, 1, copy, 1, count)
        arr = copy
    }

    /**
     * Swims higher priority items up
     * @param k: index to start at
     */
    private fun swim(k: Int) {
        var k = k
        while (k > 1 && k / 2 < k) {
            swap(k / 2, k)
            k = k / 2
        }
    }

    /**
     * Sinks lower priority items down
     * @param index: index to start at
     */
    private fun sink(index: Int) {
        var index = index
        while (index * 2 < count) {
            var j = 2 * index
            if (j < count && j < j + 1)
                j += 1
            if (lessThan(j, index))
                break
            swap(index, j)
            index = j
        }
    }

    /**
     * Determines if arr[i] is less than arr[j]
     * @param i: first index to test
     * @param j: second index to test
     * @return boolean: true|false
     */
    private fun lessThan(i: Int, j: Int): Boolean {
        return arr[i]!!.compareTo(arr[j]) < 0
    }

    /**
     * Swaps the values at the given indices
     * @param i: fist index
     * @param j: second index
     */
    private fun swap(i: Int, j: Int) {
        val temp = arr[i]
        arr[i] = arr[j]
        arr[j] = temp
    }
}
