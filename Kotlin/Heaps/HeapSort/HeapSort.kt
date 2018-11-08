/*******************************************************
 * HeapSort.kt
 * Created by Stephen Hall on 11/08/18.
 * Copyright (c) 2018 Stephen Hall. All rights reserved.
 * HeapSort implementation in Kotlin
 */

/**
 * HeapSort Class
 * @param <T> Generic Type</T>
 */
class HeapSort<T : Comparable<T>> {

    /**
     * Sorts the given array
     * @param heap; array to sort
     */
    fun sort(heap: Array<T>) {
        for (i in heap.size / 2 - 1 downTo 0)
            heapify(heap, heap.size, i)
        for (i in heap.indices.reversed()) {
            val temp = heap[0]
            heap[0] = heap[i]
            heap[i] = temp
            heapify(heap, i, 0)
        }
    }

    /**
     * Builds a heap out of the array
     * @param arr: array to heapify
     * @param length: length allowed
     * @param index: starting index
     */
    private fun heapify(arr: Array<T>, length: Int, index: Int) {
        val left = 2 * index + 1
        val right = 2 * index + 2
        var largest = index

        if (left < length && arr[left].compareTo(arr[largest]) > 0)
            largest = left

        if (right < length && arr[right].compareTo(arr[largest]) > 0)
            largest = right

        if (largest != index) {
            val swap = arr[index]
            arr[index] = arr[largest]
            arr[largest] = swap
            heapify(arr, length, largest)
        }
    }
}
