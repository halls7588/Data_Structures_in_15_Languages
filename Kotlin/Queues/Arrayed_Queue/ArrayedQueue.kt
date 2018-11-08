/*******************************************************
 * ArrayedQueue.kt
 * Created by Stephen Hall on 11/08/18.
 * Copyright (c) 2018 Stephen Hall. All rights reserved.
 * A Arrayed Queue implementation in Kotlin
 ******************************************************/

/**
 * Arrayed Queue Class
 * @param <T> Generic type
</T> */
class ArrayedQueue<T>
/**
 * Arrayed Queue Constructor
 * @param size Size to initialize the queue to
 */
(size: Int) {
    /**
     * Private Members
     */
    private var array: Array<T?>
    private var count: Int = 0
    private var size: Int = 0

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
        count = 0
    }

    /**
     * Pushes given data onto the queue if space is available
     * @param data Data to be added to the queue
     * @return Node added to the queue
     */
    fun enqueue(data: T): T? {
        if (!isFull) {
            array[count] = data
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
        val data = array[0]
        val tmp = arrayOfNulls<Any>(size) as Array<T?>
        System.arraycopy(array, 1, tmp, 0, size - 1 - 1)
        array = tmp
        count--
        return data
    }

    /**
     * Gets the top item of the queue
     * @return item on top of the queue
     */
    fun top(): T? {
        return if (isEmpty) null else array[0]
    }
}
