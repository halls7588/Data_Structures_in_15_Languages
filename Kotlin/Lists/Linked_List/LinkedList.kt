/*******************************************************
 * LinkedList.kt
 * Created by Stephen Hall on 11/08/18.
 * Copyright (c) 2018 Stephen Hall. All rights reserved.
 * A Linked List implementation in Kotlin
 ******************************************************/

/**
 * Singly linked list class
 * @param <T> Generic type
</T> */
class LinkedList<T : Comparable<T>> {

    /**
     * Private Members
     */
    private var head: Node? = null
    private var tail: Node? = null
    private var count: Int = 0

    /**
     * Node class for singly linked list
     */
    inner class Node
    /**
     * Node Class Constructor
     * @param data Data to be held in the Node
     */
    (
            /**
             * private Members
             */
            var data: T) {
            var next: Node?

        init {
            next = null
        }
    }

    /**
     * Linked List Constructor
     */
    init {
        tail = null
        head = tail
        count = 0
    }

    /**
     * Adds a new node into the list with the given data
     * @param data Data to add into the list<
     * @return Node added into the list
     */
    fun add(data: T?): Node? {
        // No data to insert into list
        if (data != null) {
            var node = Node(data)
            // The Linked list is empty
            if (head == null) {
                head = node
                tail = head
                count++
                return node
            }
            // Add to the end of the list
            tail!!.next = node
            tail = node
            count++
            return node
        }
        return null
    }

    /**
     * Removes the first node in the list matching the data
     * @param data Data to remove from the list
     * @return Node removed from the list
     */
    fun remove(data: T?): Node? {
        // List is empty or no data to remove
        if (head != null && data != null) {
            var tmp = head
            // The data to remove what found in the first Node in the list
            if (equalTo(tmp!!.data, data)) {
                head = head!!.next
                count--
                return tmp
            }
            // Try to find the node in the list
            while (tmp!!.next != null) {
                // Node was found, Remove it from the list
                if (equalTo(tmp.next!!.data, data)) {
                    val node = tmp.next
                    tmp.next = tmp.next!!.next
                    count--
                    return node
                }
                tmp = tmp.next
            }
        }
        // The data was not found in the list
        return null
    }

    /**
     * Gets the first node that has the given data
     * @param data Data to find in the list
     * @return Node First node with matching data or null if no node was found
     */
    fun find(data: T?): Node? {
        // No list or data to find
        if (head != null || data != null) {
            var tmp = head
            // Try to find the data in the list
            while (tmp != null) {
                // Data was found
                if (equalTo(tmp.data, data))
                    return tmp
                tmp = tmp.next
            }
        }
        // Data was not found in the list
        return null
    }

    /**
     * Gets the node at the given index
     * @param index Index of the Node to get
     * @return Node at passed in index
     */
    fun indexAt(index: Int): Node? {
        //Index was negative or larger then the amount of Nodes in the list
        if (index < 0 || index > size())
            return null

        var tmp = head
        // Move to index
        for (i in 0 until index) {
            tmp = tmp!!.next
        }
        // return the node at the index position
        return tmp
    }

    /**
     * Gets the current count of the array
     * @return Number of items in the array
     */
    fun size(): Int {
        return count
    }

    /**
     * Determines if a is equal to b
     * @param a: generic type to test
     * @param b: generic type to test
     * @return boolean: true|false
     */
    private fun equalTo(a: T, b: T?): Boolean {
        return a.compareTo(b!!) == 0
    }
}
