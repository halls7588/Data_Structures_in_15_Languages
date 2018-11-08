/*******************************************************
 * ArrayedHeap.kt
 * Created by Stephen Hall on 11/08/18.
 * Copyright (c) 2018 Stephen Hall. All rights reserved.
 * Arrayed Heap implementation in Kotlin
 ******************************************************/

/**
 * Linked Heap class
 * @param <T> Generic type</T>
 */
class LinkedHeap<T : Comparable<T>> {

    private var root: Node? = null

    /**
     * Test if heap is empty.
     * @return boolean: true|false.
     */
    val isEmpty: Boolean
        get() = root == null

    /**
     * Test if the heap is full.
     * @return false
     */
    val isFull: Boolean
        get() = false

    /**
     * Node Class
     */
    inner class Node
    /**
     * Node Constructor
     * @param data: data for node to hold
     * @param left: left child
     * @param right: right child
     */
    internal constructor(var data: T, var left: Node? = null, var right: Node? = null) {
        var npl: Int = 0

    }
    /**
     * Node Constructor
     * @param data: data for node to hold
     */

    /**
     * Gets the root of the heap
     * @return Node: root node of the heap
     */
    fun root(): Node? {
        return root
    }

    /**
     * Construct the linked heap.
     */
    init {
        root = null
    }

    /**
     * Merges two heaps together
     * @param heap: heap to merge with
     */
    fun merge(heap: LinkedHeap<T>) {
        if (this != heap) {
            root = merge(root, heap.root())
            heap.root = null
        }
    }

    /**
     * Merges two roots together
     * @param n1: first root
     * @param n2: second root
     * @return Node: merged roots
     */
    private fun merge(n1: Node?, n2: Node?): Node? {
        if (n1 == null)
            return n2
        if (n2 == null)
            return n1
        return if (n1.data.compareTo(n2.data) < 0) merge1(n1, n2) else merge1(n2, n1)
    }

    /**
     * Helper method to merge()
     * @param n1: first root
     * @param n2: second root
     * @return Node: merged roots
     */
    private fun merge1(n1: Node?, n2: Node?): Node? {
        if (n1!!.left == null)
            n1.left = n2
        else {
            n1.right = merge(n1.right, n2)
            if (n1.left!!.npl < n1.right!!.npl)
                swapChildren(n1)
            n1.npl = n1.right!!.npl + 1
        }
        return n1
    }

    /**
     * Swaps the children of the given node
     * @param node: node with two children to swap
     */
    private fun swapChildren(node: Node) {
        val tmp = node.left
        node.left = node.right
        node.right = tmp
    }

    /**
     * Insert into the priority queue, maintaining heap order.
     * @param data: the item to insert.
     */
    fun insert(data: T) {
        root = merge(Node(data), root)
    }

    /**
     * Find the smallest item in the priority queue.
     * @return T: smallest item or null.
     */
    fun findMin(): T? {
        return if (isEmpty) null else root!!.data
    }

    /**
     * Remove the smallest item from the heap
     * @return T: item removed or null
     */
    fun deleteMin(): T? {
        if (!isEmpty) {
            val minItem = root!!.data
            root = merge(root!!.left, root!!.right)
            return minItem
        }
        return null
    }

    /**
     * Clears the data in the heap
     */
    fun makeEmpty() {
        root = null
    }
}