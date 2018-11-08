/*******************************************************
 * ArrayedStack.kt
 * Created by Stephen Hall on 11/08/18.
 * Copyright (c) 2018 Stephen Hall. All rights reserved.
 * A Arrayed Stack implementation in Kotlin
 ******************************************************/

/**
 * Arrayed Stack Class
 * @param <T> Generic type
</T> */
class ArrayedStack<T>
/**
 * Arrayed Stack Constructor
 * @param size Size to initialize the stack to
 */
(size: Int) {
    /**
     * Private Members
     */
    private val array: Array<T> = arrayOfNulls<Any>(size) as Array<T>
    private var count: Int = 0
    private var size: Int = 0

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
        count = 0
    }

    /**
     * Pushes given data onto the stack if space is available
     * @param data Data to be added to the stack
     * @return Node added to the stack
     */
    fun push(data: T): T? {
        if (!isFull) {
            array[count] = data
            count++
            return top()
        }
        return null
    }

    /**
     * Pops item off the stack
     * @return Node popped off of the stack
     */
    fun pop(): T {
        val data = array[count - 1]
        count--
        return data
    }

    /**
     * Gets the Node onto of the stack
     * @return Node on top of the stack
     */
    fun top(): T {
        return array[count - 1]
    }
}
