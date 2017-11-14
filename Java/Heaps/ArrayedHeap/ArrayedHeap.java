/*******************************************************
 *  ArrayedHeap.java
 *  Created by Stephen Hall on 11/14/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  Arrayed Heap implementation in Java
 ********************************************************/
import java.util.Arrays;

/**
 * ArrayedHeap class
 * @param <T> Generic type
 */
public class ArrayedHeap<T extends Comparable<T>> implements PriorityQueue<T> {
    /**
     * private members to be used by the heap
     */
    private T[] array;
    private int size;

    /**
     * ArrayedHeap Constructor.
     */
    public ArrayedHeap () {
        array = (T[])new Comparable[10];
        size = 0;
    }

    /**
     * Adds an item into the heap
     * @param value: value to add
     */
    public void add(T value) {
        // grow array if needed
        if (size >= array.length - 1) {
            array = resize();
        }
        // place element into heap at bottom
        array[size++] = value;
        bubbleUp();
    }

    /**
     * checks if the heap is empty
     * @return boolean: true|false
     */
    public boolean isEmpty() {
        return size == 0;
    }

    /**
     * returns the first item on the heap
     * @return
     */
    public T peek() {
        if (this.isEmpty()) {
            return null;
        }
        return array[1];
    }

    /**
     * Removes and returns the minimum element in the heap.
     * @return T: element removed
     */
    public T remove() {
        T result = peek();
        // get rid of the last element
        array[1] = array[size];
        array[size] = null;
        size--;

        bubbleDown();

        return result;
    }

    /**
     * Gets the size of the heap
     * @return int: number of elements in the heap
     */
    public int size(){
        return size;
    }

    /**
     * Bubbles down the root element to the correct placement
     */
    private void bubbleDown() {
        int index = 1;

        while (hasLeftChild(index)) {
            int smallerChild = leftIndex(index);
            if (hasRightChild(index) && array[leftIndex(index)].compareTo(array[rightIndex(index)]) > 0) {
                smallerChild = rightIndex(index);
            }
            if (array[index].compareTo(array[smallerChild]) > 0) {
                swap(index, smallerChild);
            } else {
                break;
            }
            index = smallerChild;
        }
    }

    /**
     * Preforms bubble up to place new element in correct position
     */
    private void bubbleUp() {
        int index = size;

        while (hasParent(index) && (parent(index).compareTo(array[index]) > 0)) {
            swap(index, parentIndex(index));
            index = parentIndex(index);
        }
    }

    /**
     * Determines if the given index has a parent
     * @param i: index to test
     * @return boolean: true|false
     */
    private boolean hasParent(int i) {
        return i > 1;
    }

    /**
     * Gets the index of the left child
     * @param i: index to test
     * @return int: left child index
     */
    private int leftIndex(int i) {
        return i * 2;
    }

    /**
     * Gets the index of the right child of given index
     * @param i: index to test
     * @return int: index of right child
     */
    private int rightIndex(int i) {
        return i * 2 + 1;
    }

    /**
     * Determines if element has a left child or
     * @param i: index to test
     * @return boolean: true|false
     */
    private boolean hasLeftChild(int i) {
        return leftIndex(i) <= size;
    }

    /**
     * Determines if element has a right child or
     * @param i: index to test
     * @return boolean: true|false
     */
    private boolean hasRightChild(int i) {
        return rightIndex(i) <= size;
    }

    /**
     * gets the data of the parent
     * @param i: child index
     * @return T: data of the parent index
     */
    private T parent(int i) {
        return array[parentIndex(i)];
    }

    /**
     * Gets the parent index of i
     * @param i: index to get parent of
     * @return int: parent index
     */
    private int parentIndex(int i) {
        return i / 2;
    }

    /**
     * Doubls the size of the internal array
     * @return T[]: new resized array with copy of the data
     */
    private T[] resize() {
        return Arrays.copyOf(array, array.length * 2);
    }

    /**
     * Swaps the data at index a with b
     * @param a: first index to swap
     * @param b: second index to swap
     */
    private void swap(int a, int b) {
        T tmp = array[a];
        array[a] = array[b];
        array[b] = tmp;
    }
}
