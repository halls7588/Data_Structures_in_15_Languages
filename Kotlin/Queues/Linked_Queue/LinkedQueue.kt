/*******************************************************
 * LinkedQueue.kt
 * Created by Stephen Hall on 11/08/18.
 * Copyright (c) 2018 Stephen Hall. All rights reserved.
 * A Linked Queue implementation in Kotlin
 */


/**
 * Linked Queue Class
 * @param <T> Generic Type
</T> */
class LinkedQueue<T> {

    /**
     * Private Members
     */
    private var count: Int = 0
    private var head: Node? = null
    private var tail: Node? = null

    /**
     * Returns a value indicating if the queue is empty
     * @return true if empty, false if not
     */
    val isEmpty: Boolean
        get() = count == 0

    /**
     * Returns a value indicating if the queue is full
     * @return False, Linked queue is never full
     */
    val isFull: Boolean
        get() = false

    /**
     * Node Class for Linked Queue
     */
    inner class Node
    /**
     * Node Class Constructor
     * @param data Data to be held in the node
     */
    (
            /**
             * private Member
             */
            var data: T?) {
            var next: Node?

        init {
            next = null
        }
    }

    /**
     * Linked Queue Constructor
     */
    init {
        count = 0
        tail = null
        head = tail
    }

    /**
     * Adds given data onto the queue
     * @param data Data to be added to the queue
     * @return Node added to the queue
     */
    fun enqueue(data: T): Node? {
        if (isEmpty) {
            val node = Node(data)
            tail = node
            head = tail
            count++
            return node
        }
        val node = Node(data)
        node.next = tail
        tail = node
        count++
        return node
    }

    /**
     * Removes item off the queue
     * @return Node popped off of the queue
     */
    fun dequeue(): Node? {
        val node = head
        head = head!!.next
        node!!.next = null
        count--
        return node
    }

    /**
     * Gets the Node onto of the queue
     * @return Node on top of the queue
     */
    fun top(): Node? {
        return head
    }
}
