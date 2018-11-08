/*******************************************************
 * SkipList.kt
 * Created by Stephen Hall on 11/09/18.
 * Copyright (c) 2018 Stephen Hall. All rights reserved.
 * Skip List implementation in Kotlin
 ******************************************************/


import java.util.ArrayList

/**
 * Skip list class
 * @param <T> Generic type
</T> */
class SkipList<T : Comparable<T?>> {
    private val head: Node?
    private var max: Int = 0
    private var size: Int = 0

    /**
     * Node for Skip List class
     */
    inner class Node
    /**
     * Node Constructor
     * @param data: data for the node to hold
     */
    (var data: T?) {
     var nodeList: MutableList<Node?>

        init {
            nodeList = ArrayList()
        }

        /**
         * Gets the level of the Node
         * @return int: level of the node
         */
        fun Level(): Int {
            return nodeList.size - 1
        }
    }

    /**
     * Skip List Constructor
     */
    init {
        size = 0
        max = 0
        head = Node(null) // a Node with value null marks the beginning
        head.nodeList.add(null) // null marks the end
    }

    /**
     * Adds a node into the Skip List
     * @param data: data to add into the list
     * @return boolean: success|fail
     */
    fun add(data: T?): Boolean {
        if (contains(data))
            return false

        size++
        var level = 0
        // random number from 0 to max + 1 (inclusive)
        while (Math.random() < PROBABILITY)
            level++
        while (level > max) {
            // should only happen once
            head!!.nodeList.add(null)
            max++
        }

        var node = Node(data)
        var current = head

        do {
            current = findNext(data, current, level)
            node.nodeList.add(0, current!!.nodeList[level])
            current.nodeList.set(level, node)
        } while (level-- > 0)
        return true
    }

    /**
     * Returns node with the greatest value
     * @param data: data to find
     * @param current: current Node
     * @param level: level to start form
     * @return Node: current node
     */
    private fun find(data: T?, current: Node? = head, level: Int = max): Node? {
        var current = current
        var level = level
        do {
            current = findNext(data, current, level)
        } while (level-- > 0)
        return current
    }

    /**
     * Returns the node at a given level with highest value less than data
     * @param data: data to find
     * @param current: current node
     * @param level: current level
     * @return Node: highest node
     */
    private fun findNext(data: T?, current: Node?, level: Int): Node? {
        var current = current
        var next: Node? = current!!.nodeList[level]

        while (next != null) {
            val value = next.data
            if (lessThan(data, value))
                break
            current = next
            next = current.nodeList[level]
        }
        return current
    }

    /**
     * gets the size of the list
     * @return int: size of the list
     */
    fun size(): Int {
        return size
    }

    /**
     * Determines if the object is in the list or not
     * @param data: object to test
     * @return boolean: true|false
     */
    operator fun contains(data: T?): Boolean {
        val node = find(data)
        return node != null && node.data != null && equalTo(node.data, data)
    }

    /**
     * Determines if a is less than b
     * @param a: generic type to test
     * @param b: generic type to test
     * @return boolean: true|false
     */
    private fun lessThan(a: T?, b: T?): Boolean {
        return a!!.compareTo(b) < 0
    }

    /**
     * Determines if a is equal to b
     * @param a: generic type to test
     * @param b: generic type to test
     * @return boolean: true|false
     */
    private fun equalTo(a: T?, b: T?): Boolean {
        return a!!.compareTo(b) == 0
    }

    companion object {
        private val PROBABILITY = 0.5
    }
}
/**
 * Finds a node in the list with the same data
 * @param data: data to find
 * @return Node: Node found
 */
