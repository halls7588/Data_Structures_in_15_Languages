/*******************************************************
 * ArrayedHeap.kt
 * Created by Stephen Hall on 11/08/18.
 * Copyright (c) 2018 Stephen Hall. All rights reserved.
 * Arrayed Heap implementation in Kotlin
 ******************************************************/

import java.util.Arrays

/**
 * ArrayedHeap class
 * @param <T> Generic type
</T> */
class ArrayedHeap<T : Comparable<T?>> {
    /**
     * private members to be used by the heap
     */
    private var array: Array<T?>
    private var size: Int = 0

    /**
     * checks if the heap is empty
     * @return boolean: true|false
     */
    val isEmpty: Boolean
        get() = size == 0

    /**
     * ArrayedHeap Constructor.
     */
    init {
        array = arrayOfNulls<Comparable<*>>(10) as Array<T?>
        size = 0
    }

    /**
     * Adds an item into the heap
     * @param value: value to add
     */
    fun add(value: T) {
        // grow array if needed
        if (size >= array.size - 1) {
            array = resize()
        }
        // place element into heap at bottom
        array[size++] = value
        bubbleUp()
    }

    /**
     * returns the first item on the heap
     * @return T: first element of the heap
     */
    fun peek(): T? {
        return if (this.isEmpty) {
            null
        } else array[1]
    }

    /**
     * Removes and returns the minimum element in the heap.
     * @return T: element removed
     */
    fun remove(): T? {
        val result = peek()
        // get rid of the last element
        array[1] = array[size]
        array[size] = null
        size--
        bubbleDown()
        return result
    }

    /**
     * Gets the size of the heap
     * @return int: number of elements in the heap
     */
    fun size(): Int {
        return size
    }

    /**
     * Bubbles down the root element to the correct placement
     */
    private fun bubbleDown() {
        var index = 1

        while (hasLeftChild(index)) {
            var smallerChild = leftIndex(index)
            if (hasRightChild(index) && array[leftIndex(index)]!!.compareTo(array[rightIndex(index)]) > 0)
                smallerChild = rightIndex(index)
            if (array[index]!!.compareTo(array[smallerChild]) > 0)
                swap(index, smallerChild)
            else
                break
            index = smallerChild
        }
    }

    /**
     * Performs bubble up to place new element in correct position
     */
    private fun bubbleUp() {
        var index = size

        while (hasParent(index) && parent(index)!!.compareTo(array[index]) > 0) {
            swap(index, parentIndex(index))
            index = parentIndex(index)
        }
    }

    /**
     * Determines if the given index has a parent
     * @param i: index to test
     * @return boolean: true|false
     */
    private fun hasParent(i: Int): Boolean {
        return i > 1
    }

    /**
     * Gets the index of the left child
     * @param i: index to test
     * @return int: left child index
     */
    private fun leftIndex(i: Int): Int {
        return i * 2
    }

    /**
     * Gets the index of the right child of given index
     * @param i: index to test
     * @return int: index of right child
     */
    private fun rightIndex(i: Int): Int {
        return i * 2 + 1
    }

    /**
     * Determines if element has a left child or
     * @param i: index to test
     * @return boolean: true|false
     */
    private fun hasLeftChild(i: Int): Boolean {
        return leftIndex(i) <= size
    }

    /**
     * Determines if element has a right child or
     * @param i: index to test
     * @return boolean: true|false
     */
    private fun hasRightChild(i: Int): Boolean {
        return rightIndex(i) <= size
    }

    /**
     * gets the data of the parent
     * @param i: child index
     * @return T: data of the parent index
     */
    private fun parent(i: Int): T? {
        return array[parentIndex(i)]
    }

    /**
     * Gets the parent index of i
     * @param i: index to get parent of
     * @return int: parent index
     */
    private fun parentIndex(i: Int): Int {
        return i / 2
    }

    /**
     * Doubles the size of the internal array
     * @return T[]: new resized array with copy of the data
     */
    private fun resize(): Array<T?> {
        return Arrays.copyOf(array, array.size * 2)
    }

    /**
     * Swaps the data at index a with b
     * @param a: first index to swap
     * @param b: second index to swap
     */
    private fun swap(a: Int, b: Int) {
        val tmp = array[a]
        array[a] = array[b]
        array[b] = tmp
    }
}
