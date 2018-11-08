/*******************************************************
 * CircularStack.kt
 * Created by Stephen Hall on 11/08/18.
 * Copyright (c) 2018 Stephen Hall. All rights reserved.
 * A Circular Stack implementation in Kotlin
 *******************************************************/

/**
 * Circular Stack Class
 * @param <T> Generic type
</T> */
class CircularStack<T>
/**
 * Circular Stack Constructor
 * @param size Size to initialize the stack to
 */
(size: Int) {
    /**
     * Private Members
     */
    private val array: Array<T?> = arrayOfNulls<Any>(size) as Array<T?>
    private var count: Int = 0
    private var size: Int = 0
    private var zeroIndex: Int = 0

    /**
     * Returns a value indicating if the stack is empty
     * @return true if empty, false if not
     */
    val isEmpty: Boolean
        get() = count == 0

    /**
     * Returns a value indicating if the stack is full
     * @return True if full, false if not
     */
    val isFull: Boolean
        get() = count == size

    /**
     * Default Constructor
     */
    constructor() : this(10) {}

    init {
        zeroIndex = 0
        count = zeroIndex
    }

    /**
     * Pushes given data onto the stack if space is available
     * @param data Data to be added to the stack
     * @return item added to the stack
     */
    fun push(data: T): T? {
        if (!isFull) {
            array[(zeroIndex + count) % size] = data
            count++
            return top()
        }
        return null
    }

    /**
     * Pops item off the stack
     * @return item popped off of the stack
     */
    fun pop(): T? {
        if (isEmpty)
            return null
        val data = array[count + zeroIndex % size - 1]
        array[count + zeroIndex % size - 1] = array[zeroIndex]
        array[zeroIndex] = null
        count--
        zeroIndex = (zeroIndex + 1) % size
        return data
    }

    /**
     * Gets the item onto of the stack
     * @return item on top of the stack
     */
    fun top(): T? {
        return array[(zeroIndex + count) % size - 1]
    }
}
