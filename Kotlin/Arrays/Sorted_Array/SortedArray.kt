/*******************************************************
 * SortedArray.kt
 * Created by Stephen Hall on 11/08/18.
 * Copyright (c) 2018 Stephen Hall. All rights reserved.
 * SortedArray implementation in Kotlin
 *******************************************************/

/**
 * SortedArray Class
 * @param <T> Generic type</T>
 */
class SortedArray<T : Comparable<T?>>
/**
 * SortedArray constructor initialized to a specific size
 * @param size Size to initialize the array to
 */
(size: Int) {
    /**
     * Private Members
     */
    var array: Array<T?>
    var count: Int = 0
    var size: Int = 0

    /**
     * SortedArray default constructor
     */
    constructor() : this(4) {}

    init {
        count = 0
        if (size > 0)
            this.size = size
        else
            this.size = 4
        array = arrayOfNulls<Comparable<*>>(this.size) as Array<T?>
    }

    /**
     * Doubles the size of the internal array
     */
    private fun resize() {
        size *= 2
        val tmp = arrayOfNulls<Comparable<*>>(size) as Array<T?>
        System.arraycopy(array!!, 0, tmp, 0, count)
        array = tmp
    }

    /**
     * Adds new item into the array
     * @param data Data to add into the array
     * @return Data added into the array
     */
    fun add(data: T?): T? {
        if (data == null)
            return null

        if (this.count == size)
            resize()

        array[count] = data
        count++

        return data
    }

    /**
     * Appends the contents of an array to the SortedArray
     * @param data: array to append
     * @return boolean: success|fail
     */
    fun append(data: Array<T?>): Boolean {
        if (data != null) {
            for (aData in data) {
                if (aData != null)
                    add(aData)
            }
            return true
        }
        return false
    }

    /**
     * Sets the data at the given index
     * @param index: index to set
     * @param data: data to set index to
     * @return boolean: success|fail
     */
    operator fun set(index: Int, data: T): Boolean {
        if (index >= 0 && index < size) {
            array[index] = data
            return true
        }
        return false
    }

    /**
     * Gets the data at the arrays given index
     * @param index Index to get data at
     * @return Data at the given index or default value of T if index does not exist
     */
    operator fun get(index: Int): T? {
        return if (index in 0..(size - 1)) array[index] else null
    }

    /**
     * Removes the data at arrays given index
     * @param index Index to remove
     * @return Data removed from the array or default T value if index does not exist
     */
    fun remove(index: Int): T? {
        if (index < 0 || index > count)
            return null

        val tmp = array[index]
        array[index] = null
        count--
        return tmp
    }

    /**
     * Resets the internal array to default size with no data
     */
    fun reset() {
        count = 0
        size = 4
        array = arrayOfNulls<Comparable<*>>(this.size) as Array<T?>
    }

    /**
     * Clears all data in the array leaving size intact
     */
    fun clear() {
        for (i in 0 until count) {
            array[i] = null
        }
        this.count = 0
    }


    /**
     * Gets the current count of the array
     * @return Number of items in the array
     */
    fun count(): Int {
        return count
    }

    /**
     * Private helper method for Merger Sort. Merges two sub-arrays of arr[].
     * @param arr: array to be sorted
     * @param l: index of first sub array
     * @param m: merge point
     * @param r: index of second sub array
     */
    private fun merge(arr: Array<T?>, l: Int, m: Int, r: Int) {
        var i: Int
        var j: Int
        var k: Int
        val n1 = m - l + 1
        val n2 = r - m

        // create temp arrays
        val L = arrayOfNulls<Comparable<*>>(n1) as Array<T?>
        val R = arrayOfNulls<Comparable<*>>(n2) as Array<T?>

        // Copy data to temp arrays L[] and R[]
        i = 0
        while (i < n1) {
            L[i] = arr[l + i]
            i++
        }
        j = 0
        while (j < n2) {
            R[j] = arr[m + 1 + j]
            j++
        }

        // Merge the temp arrays back into arr[l..r]
        i = 0 // Initial index of first sub-array
        j = 0 // Initial index of second sub-array
        k = l // Initial index of merged sub-array
        while (i < n1 && j < n2) {
            if (lessThan(L[i], R[j]) || equalTo(L[i], R[j])) {
                arr[k] = L[i]
                i++
            } else {
                arr[k] = R[j]
                j++
            }
            k++
        }

        // Copy the remaining elements of L[], if there are any
        while (i < n1) {
            arr[k] = L[i]
            i++
            k++
        }

        // Copy the remaining elements of R[], if there are any
        while (j < n2) {
            arr[k] = R[j]
            j++
            k++
        }
    }

    /**
     * Recursive helper method for merge sort
     * @param arr: sub array to be sorted
     * @param l: left index
     * @param r: right index
     */
    private fun mergeSortHelper(arr: Array<T?>, l: Int, r: Int) {
        if (l < r) {
            // Same as (l+r)/2, but avoids overflow for
            // large l and h
            val m = l + (r - l) / 2

            // Sort first and second halves
            mergeSortHelper(arr, l, m)
            mergeSortHelper(arr, m + 1, r)

            merge(arr, l, m, r)
        }
    }

    /**
     * Performs Merge Sort on internal array
     * @return T[]: sorted copy of the internal array
     */
    fun mergeSort(): Array<T?> {
        val tmp = arrayOfNulls<Comparable<*>>(count) as Array<T?>
        System.arraycopy(array, 0, tmp, 0, count)
        mergeSortHelper(tmp, 0, count - 1)
        return tmp
    }

    /**
     * Performs Bubble sort on internal array
     * @return T[]: sorted copy of the internal array
     */
    fun bubbleSort(): Array<T?> {
        val tmp = arrayOfNulls<Comparable<T?>>(count) as Array<T?>
        System.arraycopy(array, 0, tmp, 0, count)

        for (i in 0 until count - 1)
            for (j in 0 until count - i - 1) {
                if (greaterThan(tmp[j], tmp[j + 1])) {
                    // swap temp and arr[i]
                    val temp = tmp[j]
                    tmp[j] = tmp[j + 1]
                    tmp[j + 1] = temp
                }
            }
        return tmp
    }

    /**
     * Helper method for Quick Sort. Swaps data in the partition
     * @param arr: array to be sorted
     * @param low: low index
     * @param high: high index
     * @return int: pivot index
     */
    private fun partition(arr: Array<T?>, low: Int, high: Int): Int {
        val pivot = arr[high]
        var i = low - 1 // index of smaller element
        for (j in low until high) {
            // If current element is smaller than or
            // equal to pivot
            if (lessThan(arr[j], pivot) || equalTo(arr[j], pivot)) {
                i++
                // swap arr[i] and arr[j]
                val temp = arr[i]
                arr[i] = arr[j]
                arr[j] = temp
            }
        }

        // swap arr[i+1] and arr[high] (or pivot)
        val temp = arr[i + 1]
        arr[i + 1] = arr[high]
        arr[high] = temp

        return i + 1
    }

    /**
     * Helper method for recursive Quick Sort
     * @param arr: array to be sorted
     * @param low: low index
     * @param high: high index
     */
    private fun quickSortHelper(arr: Array<T?>, low: Int, high: Int) {
        if (low < high) {
            // pivot is partitioning index, arr[pivot] is now at right place
            val pivot = partition(arr, low, high)

            // Recursively sort elements before and after pivot
            quickSortHelper(arr, low, pivot - 1)
            quickSortHelper(arr, pivot + 1, high)
        }
    }

    /**
     * Performs Quick Sort on the internal array
     * @return T[]: sorted copy of the internal array
     */
    fun quickSort(): Array<T?> {
        val tmp = arrayOfNulls<Comparable<T?>>(count) as Array<T?>
        System.arraycopy(array, 0, tmp, 0, count)
        quickSortHelper(tmp, 0, count - 1)

        return tmp
    }

    /**
     * Performs Insertion sort on the internal array
     * @return T[]: sorted copy of the internal array
     */
    fun insertionSort(): Array<T?> {
        val tmp = arrayOfNulls<Comparable<T?>>(count) as Array<T?>
        System.arraycopy(array, 0, tmp, 0, count)

        for (i in 1 until count) {
            val key = tmp[i]
            var j = i - 1

            // Move elements of arr[0..i-1], that are greater than key, to one position ahead
            while (j >= 0 && lessThan(tmp[j], key)) {
                tmp[j + 1] = tmp[j]
                j = j - 1
            }
            tmp[j + 1] = key
        }
        return tmp
    }

    /**
     * Performs Selection Sort on internal array
     * @return T[]: Sorted copy of the internal data
     */
    fun selectionSort(): Array<T?> {
        val tmp = arrayOfNulls<Comparable<T?>>(count) as Array<T?>
        System.arraycopy(array, 0, tmp, 0, count)
        // One by one move boundary of unsorted sub-array
        for (i in 0 until count - 1) {
            // Find the minimum element in unsorted array
            var min = i
            for (j in i + 1 until count)
                if (lessThan(tmp[j], tmp[min]))
                    min = j

            // Swap the found minimum element with the first
            // element
            val temp = tmp[min]
            tmp[min] = tmp[i]
            tmp[i] = temp
        }
        return tmp
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

    /**
     * Determines if a is greater than b
     * @param a: generic type to test
     * @param b: generic type to test
     * @return boolean: true|false
     */
    private fun greaterThan(a: T?, b: T?): Boolean {
        return a!!.compareTo(b) > 0
    }
}
