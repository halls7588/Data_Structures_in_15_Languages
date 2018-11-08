/*******************************************************
 * CircularQueue.kt
 * Created by Stephen Hall on 11/08/18.
 * Copyright (c) 2018 Stephen Hall. All rights reserved.
 * Circular Queue implementation in Java
 ******************************************************/


/**
 * Circular Queue Class
 * @param <T> Generic type
</T> */
class CircularQueue<T>
/**
 * Circular Queue Constructor
 * @param size Size to initialize the queue to
 */
(size: Int) {
    /**
     * Private Members
     */
    private val array: Array<T?>
    private var count: Int = 0
    private var size: Int = 0
    private var zeroIndex: Int = 0

    /**
     * Returns a value indicating if the queue is empty
     * @return true if empty, false if not
     */
    val isEmpty: Boolean
        get() = count == 0

    /**
     * Returns a value indicating if the queue is full
     * @return True if full, false if not
     */
    val isFull: Boolean
        get() = count == size

    /**
     * Default Constructor
     */
    constructor() : this(10) {}

    init {
        this.size = size
        array = arrayOfNulls<Any>(size) as Array<T?>
        zeroIndex = 0
        count = zeroIndex
    }

    /**
     * Pushes given data onto the queue if space is available
     * @param data Data to be added to the queue
     * @return Node added to the queue
     */
    fun enqueue(data: T): T? {
        if (!isFull) {
            array[(zeroIndex + count) % size] = data
            count++
            return top()
        }
        return null
    }

    /**
     * Removes item from the queue
     * @return item removed from the queue
     */
    fun dequeue(): T? {
        if (isEmpty)
            return null

        val tmp = array[zeroIndex]
        array[zeroIndex] = null
        count--
        zeroIndex = (zeroIndex + 1) % size
        return tmp
    }

    /**
     * Gets the top item of the queue
     * @return item on top of the queue
     */
    fun top(): T? {
        return if (isEmpty) null else array[zeroIndex]
    }
}
