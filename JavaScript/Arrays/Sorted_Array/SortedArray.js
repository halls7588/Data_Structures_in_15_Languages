/*******************************************************
 *  SortedArray.js
 *  Created by Stephen Hall on 11/15/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  SortedArray implementation in JavaScript
 ********************************************************/

/**
 * SortedArray Class
 * @param size: size to initialize the array to
 * @constructor
 */
function SortedArray(size) {
    if(size !== undefined && size !== null && size > 0)
        this.array = [(this.size = size)];
    else
        this.array = [(this.size = 4)];
    this.count = 0;

    /**
     * Doubles the size of the internal array
     */
    function resize() {
        this.size *= 2;
        var tmp = [size];
        for(var i = 0; i < this.count; i++){
            tmp[i] = this.array[i];
        }
        this.array = tmp;
    }

    /**
     * Recursive helper method for merge sort
     * @param arr: sub array to be sorted
     * @param l: left index
     * @param r: right index
     */
    function mergeSortHelper(arr, l, r) {
        if (l < r){
            // Same as (l+r)/2, but avoids overflow for
            // large l and h
            var m = l+(r-l)/2;

            // Sort first and second halves
            this.mergeSortHelper(arr, l, m);
            this.mergeSortHelper(arr, (m + 1), r);

            this.merge(arr, l, m, r);
        }
    }

    /**
     * Private helper method for Merger Sort. Merges two sub-arrays of arr[].
     * @param arr: array to be sorted
     * @param l: index of first sub array
     * @param m: merge point
     * @param r: index of second sub array
     */
    function merge(arr, l, m, r) {
        var i, j, k;
        var n1 = m - l + 1;
        var n2 =  r - m;

        // create temp arrays
        var L = [n1];
        var R = [n2];

        // Copy data to temp arrays L[] and R[]
        for (i = 0; i < n1; i++)
            L[i] = arr[l + i];
        for (j = 0; j < n2; j++)
            R[j] = arr[m + 1+ j];

        // Merge the temp arrays back into arr[l..r]
        i = 0; // Initial index of first sub-array
        j = 0; // Initial index of second sub-array
        k = l; // Initial index of merged sub-array
        while (i < n1 && j < n2) {
            if (L[i] <= R[j]) {
                arr[k] = L[i];
                i++;
            }
            else {
                arr[k] = R[j];
                j++;
            }
            k++;
        }

        // Copy the remaining elements of L[], if there are any
        while (i < n1){
            arr[k] = L[i];
            i++;
            k++;
        }

        // Copy the remaining elements of R[], if there are any
        while (j < n2){
            arr[k] = R[j];
            j++;
            k++;
        }
    }

    /**
     * Helper method for recursive Quick Sort
     * @param arr: array to be sorted
     * @param low: low index
     * @param high: high index
     */
    function quickSortHelper(arr, low, high) {
        if (low < high) {
            // pivot is partitioning index, arr[pivot] is now at right place
            var pivot = this.partition(arr, low, high);

            // Recursively sort elements before and after pivot
            this.quickSortHelper(arr, low, (pivot - 1));
            this.quickSortHelper(arr, (pivot + 1), high);
        }
    }

    /**
     * Helper method for Quick Sort. Swaps data in the partition
     * @param arr: array to be sorted
     * @param low: low index
     * @param high: high index
     * @return int: pivot index
     */
    function partition(arr, low, high) {
        var pivot = arr[high];
        var i = (low - 1); // index of smaller element
        for(var j = low; j < high; j++) {
            // If current element is smaller than or
            // equal to pivot
            if(arr[j] <= pivot) {
                i++;
                // swap arr[i] and arr[j]
                var temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }
        // swap arr[i+1] and arr[high] (or pivot)
        temp = arr[i + 1];
        arr[i + 1] = arr[high];
        arr[high] = temp;

        return i + 1;
    }
}

/**
 * Adds new item into the array
 * @param data: Data to add into the array
 * @returns {*}: Data added into the array
 */
SortedArray.prototype.add = function(data) {
    if(data === null || data === undefined)
        return null;

    if(this.count === this.size)
        this.resize();

    this.array[count] = data;
    this.count++;

    return data;
};

/**
 * Appends the contents of an array to the SortedArray
 * @param data: array to append
 * @returns {boolean}: success|fail
 */
SortedArray.prototype.append = function(data) {
    if(data !== null && data !== undefined){
        for (var i = 0; i < data.length; i++) {
            if(data[i] !== null )
                this.add(data[i]);
        }
        return true;
    }
    return false;
};

/**
 * Sets the data at the given index
 * @param index: index to set
 * @param data: data to set index to
 * @returns {boolean}: success|fail
 */
SortedArray.prototype.set = function(index, data) {
    if(index >= 0 && index < this.size) {
        this.array[index] = data;
        return true;
    }
    return false;
};


/**
 * Gets the data at the arrays given index
 * @param index: Index to get data at
 * @returns {*}: Data at the given index or default value of T if index does not exist
 */
SortedArray.prototype.get = function(index) {
    if(index >= 0 && index < this.size)
        return this.array[index];
    return null;
};

/**
 * Removes the data at arrays given index
 * @param index: Index to remove
 * @returns {*}: Data removed from the array or default T value if index does not exist
 */
SortedArray.prototype.remove = function(index) {
    if (index < 0 || index > this.count)
        return null;

    var tmp = this.array[index];
    this.array[index] = null;
    this.count--;
    return tmp;
};

/**
 * Resets the internal array to default size with no data
 */
SortedArray.prototype.reset = function() {
    this.count = 0;
    this.size = 4;
    this.array = [this.size];
};

/**
 * Clears all data in the array leaving size intact
 */
SortedArray.prototype.clear = function() {
    for(var i = 0; i < this.count; i++){
        this.array[i] = null;
    }
    this.count = 0;
};

/**
 * Gets the current count of the array
 * @returns {SortedArray.count|*}: Number of items in the array
 */
SortedArray.prototype.count = function() {
    return this.count;
};


/**
 * Performs Merge Sort on internal array
 * @returns {[*]}: sorted copy of the internal array
 */
SortedArray.prototype.mergeSort = function(){
    var tmp = [this.count];
    for(var i = 0; i < this.count; i++){
        tmp[i] = this.array[i];
    }
    mergeSortHelper(tmp, 0, (this.count - 1));
    return tmp;
};

/**
 * Performs Bubble sort on internal array
 * @return {[*]}: sorted copy of the internal array
 */
SortedArray.prototype.bubbleSort = function(){
    var tmp = [this.count];
    for(var i = 0; i < this.count; i++){
        tmp[i] = this.array[i];
    }

    for(i = 0; i < this.count - 1; i++)
        for(var j = 0; j < this.count-i-1; j++) {
            if (tmp[j] > tmp[j + 1]){
                // swap temp and arr[i]
                var temp = tmp[j];
                tmp[j] = tmp[j + 1];
                tmp[j + 1] = temp;
            }
        }
    return tmp;
};

/**
 * Performs Quick Sort on the internal array
 * @return {[*]}: sorted copy of the internal array
 */
SortedArray.prototype.quickSort = function(){
    var tmp = [this.count];
    for(var i = 0; i < this.count; i++){
        tmp[i] = this.array[i];
    }
    this.quickSortHelper(tmp, 0, (this.count - 1));

    return tmp;
};

/**
 * Performs Insertion sort on the internal array
 * @return {[*]}: sorted copy of the internal array
 */
SortedArray.prototype.insertionSort = function(){
    var tmp = [this.count];
    for(var i = 0; i < this.count; i++){
        tmp[i] = this.array[i];
    }

    for(var i = 1; i < this.count; ++i) {
        var key = tmp[i];
        var j = i - 1;

        // Move elements of arr[0..i-1], that are greater than key, to one position ahead
        while (j >= 0 && (tmp[j] < key)) {
            tmp[j + 1] = tmp[j];
            j = j - 1;
        }
        tmp[j + 1] = key;
    }
    return tmp;
};


/**
 * Performs Selection Sort on internal array
 * @returns {[*]}: Sorted copy of the internal data
 */
SortedArray.prototype.selectionSort = function(){
    var tmp = [this.count];
    for(var i = 0; i < this.count; i++){
        tmp[i] = this.array[i];
    }
    // One by one move boundary of unsorted sub-array
    for(var i = 0; i < this.count-1; i++) {
        // Find the minimum element in unsorted array
        var min = i;
        for(var j = i + 1; j < count; j++)
            if (tmp[j] < tmp[min])
                min = j;

        // Swap the found minimum element with the first element
        var temp = tmp[min];
        tmp[min] = tmp[i];
        tmp[i] = temp;
    }
    return tmp;
};


