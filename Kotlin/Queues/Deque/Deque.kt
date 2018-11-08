/*******************************************************
 * Deque.kt
 * Created by Stephen Hall on 11/08/18.
 * Copyright (c) 2018 Stephen Hall. All rights reserved.
 * A Deque implementation in Kotlin
 *******************************************************/


/**
 * Linked Queue Class
 * @param <T> Generic Type
</T> */
class Deque<T> {

    /**
     * Private Members
     */
    private var count: Int = 0
    private var head: Node? = null
    private var tail: Node? = null

    /**
     * Returns a value indicating if the deque is empty
     * @return true if empty, false if not
     */
    val isEmpty: Boolean
        get() = count == 0

    /**
     * Returns a value indicating if the deque is full
     * @return False, deque is never full
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
            var previous: Node?

        init {
            next = null
            previous = null
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
     * Adds given data onto the front of the deque
     * @param data Data to be added to the deque
     * @return Node added to the deque
     */
    fun enqueueFront(data: T): Node? {
        if (isEmpty) {
            val node = Node(data)
            tail = node
            head = tail
            count++
            return node
        }
        val node = Node(data)
        node.next = head
        head!!.previous = node
        head = node
        count++
        return node
    }

    /**
     * Adds given data onto the back of the deque
     * @param data Data to be added to the deque
     * @return Node added to the deque
     */
    fun enqueueBack(data: T): Node? {
        if (isEmpty) {
            val node = Node(data)
            tail = node
            head = tail
            count++
            return node
        }
        val node = Node(data)
        node.next = tail
        tail!!.previous = node
        tail = node
        count++
        return node
    }


    /**
     * Removes item off the front of the deque
     * @return Node popped off of the deque
     */
    fun dequeueFront(): Node? {
        if (isEmpty)
            return null
        val node = head
        head = head!!.next
        head!!.previous = null
        node!!.next = null
        count--
        return node
    }

    /**
     * Removes item off the back of the deque
     * @return Node removes from the back of the deque
     */
    fun dequeueBack(): Node? {
        if (isEmpty)
            return null
        val node = tail
        tail = tail!!.previous
        tail!!.next = null
        node!!.previous = null
        count--
        return node
    }

    /**
     * Gets the Node at the front of the deque
     * @return Node on top of the deque
     */
    fun peekFront(): Node? {
        return head
    }

    /**
     * Gets the Node at the back of the deque
     * @return Node on bottom of the deque
     */
    fun peekBack(): Node? {
        return tail
    }
}
