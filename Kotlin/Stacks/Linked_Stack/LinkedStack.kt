/*******************************************************
 * LinkedStack.kt
 * Created by Stephen Hall on 11/08/18.
 * Copyright (c) 2018 Stephen Hall. All rights reserved.
 * A Linked Stack implementation in Kotlin
 ******************************************************/

/**
 * Linked Stack Class
 * @param <T> Generic Type</T> */
class LinkedStack<T> {

    /**
     * Private Members
     */
    private var count: Int = 0
    private var head: Node? = null

    /**
     * Returns a value indicating if the stack is empty
     * @return true if empty, false if not
     */
    val isEmpty: Boolean
        get() = count == 0

    /**
     * Returns a value indicating if the stack is full
     * @return False, Linked stack is never full
     */
    val isFull: Boolean
        get() = false

    /**
     * Node Class for Linked Stack
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
            var data: T) {
            var next: Node?

        init {
            next = null
        }
    }

    /**
     * Linked Stack Constructor
     */
    init {
        count = 0
        head = null
    }

    /**
     * Pushes given data onto the stack
     * @param data Data to be added to the stack
     * @return Node added to the stack
     */
    fun push(data: T): Node? {
        val node = Node(data)
        node.next = head
        head = node
        count++
        return top()
    }

    /**
     * Pops item off the stack
     * @return Node popped off of the stack
     */
    fun pop(): Node {
        val node = head
        head = head!!.next
        node!!.next = null
        count--
        return node
    }

    /**
     * Gets the Node onto of the stack
     * @return Node on top of the stack
     */
    fun top(): Node? {
        return head
    }
}
