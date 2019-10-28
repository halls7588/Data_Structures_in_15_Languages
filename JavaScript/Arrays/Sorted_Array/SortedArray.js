/*******************************************************
 *  SortedArray.js
 *  Created by Stephen Hall on 11/15/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  SortedArray implementation in JavaScript
 *  Update Date      Name            Description
 *  1      10/28/19  Stephen Hall    Updated to class syntax.
 *                                   Tested and fixed bugs
 ********************************************************/

/**
 * SortedArray Class
 */
class SortedArray {
    /**
     * SortedArray Class Constructor
     * @param size: size to initialize the array to
     * @constructor
     */
  constructor(size) {
    if (size !== undefined && size !== null && size > 0)
      this.array = [(this.size = size)];
    else this.array = [(this.size = 4)];
    this.count = 0;
  }

  /**
   * Doubles the size of the internal array
   */
  resize() {
    this.size *= 2;
    var tmp = [size];
    for (var i = 0; i < this.count; i++) {
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
  mergeSortHelper(arr, l, r) {
    if (l < r) {
      // Same as (l+r)/2, but avoids overflow for
      // large l and h
      var m = l + (r - l) / 2;

      // Sort first and second halves
      this.mergeSortHelper(arr, l, m);
      this.mergeSortHelper(arr, m + 1, r);

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
  merge(left, right) {
    let resultArray = [],
      leftIndex = 0,
      rightIndex = 0;

    // We will concatenate values into the resultArray in order
    while (leftIndex < left.length && rightIndex < right.length) {
      if (left[leftIndex] < right[rightIndex]) {
        resultArray.push(left[leftIndex]);
        leftIndex++; // move left array cursor
      } else {
        resultArray.push(right[rightIndex]);
        rightIndex++; // move right array cursor
      }
    }

    // We need to concat here because there will be one element remaining
    // from either left OR the right
    return resultArray
      .concat(left.slice(leftIndex))
      .concat(right.slice(rightIndex));
  }

  /**
   * Helper method for recursive Quick Sort
   * @param arr: array to be sorted
   * @param low: low index
   * @param high: high index
   */
  quickSortHelper(arr, low, high) {
    if (low < high) {
      // pivot is partitioning index, arr[pivot] is now at right place
      var pivot = this.partition(arr, low, high);

      // Recursively sort elements before and after pivot
      this.quickSortHelper(arr, low, pivot - 1);
      this.quickSortHelper(arr, pivot + 1, high);
    }
  }

  /**
   * Helper method for Quick Sort. Swaps data in the partition
   * @param arr: array to be sorted
   * @param low: low index
   * @param high: high index
   * @return int: pivot index
   */
  partition(arr, low, high) {
    var pivot = arr[high];
    var i = low - 1; // index of smaller element
    for (var j = low; j < high; j++) {
      // If current element is smaller than or
      // equal to pivot
      if (arr[j] <= pivot) {
        i++;
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

  /**
   * Adds new item into the array
   * @param data: Data to add into the array
   * @returns {*}: Data added into the array
   */
  add(data) {
    if (data === null || data === undefined) 
        return null;

    if (this.count === this.size) 
        this.resize();

    this.array[this.count] = data;
    this.count++;

    return data;
  }

  /**
   * Appends the contents of an array to the SortedArray
   * @param data: array to append
   * @returns {boolean}: success|fail
   */
  append(data) {
    if (data !== null && data !== undefined) {
      for (var i = 0; i < data.length; i++) {
        if (data[i] !== null) this.add(data[i]);
      }
      return true;
    }
    return false;
  }

  /**
   * Sets the data at the given index
   * @param index: index to set
   * @param data: data to set index to
   * @returns {boolean}: success|fail
   */
  set(index, data) {
    if (index >= 0 && index < this.size) {
      this.array[index] = data;
      return true;
    }
    return false;
  }

  /**
   * Gets the data at the arrays given index
   * @param index: Index to get data at
   * @returns {*}: Data at the given index or default value of T if index does not exist
   */
  get(index) {
    if (index >= 0 && index < this.size) return this.array[index];
    return null;
  }

  /**
   * Removes the data at arrays given index
   * @param index: Index to remove
   * @returns {*}: Data removed from the array or default T value if index does not exist
   */
  remove(index) {
    if (index < 0 || index > this.count) return null;

    var tmp = this.array[index];
    this.array[index] = null;
    this.count--;
    return tmp;
  }

  /**
   * Resets the internal array to default size with no data
   */
  reset() {
    this.count = 0;
    this.size = 4;
    this.array = [this.size];
  }

  /**
   * Clears all data in the array leaving size intact
   */
  clear() {
    for (var i = 0; i < this.count; i++) {
      this.array[i] = null;
    }
    this.count = 0;
  }

  /**
   * Gets the current count of the array
   * @returns {SortedArray.count|*}: Number of items in the array
   */
  count() {
    return this.count;
  }

  /**
   * Performs Merge Sort on internal array
   * @returns {[*]}: sorted copy of the internal array
   */
  mergeSort(arr) {

    if (arr.length <= 1) {
      return arr;
    }

    const middle = Math.floor(arr.length / 2);
    const left = arr.slice(0, middle);
    const right = arr.slice(middle);

    return this.merge(this.mergeSort(left), this.mergeSort(right));
  }

  /**
   * Performs Bubble sort on internal array
   * @return {[*]}: sorted copy of the internal array
   */
  bubbleSort() {
    var tmp = [this.count];
    for (var i = 0; i < this.count; i++) {
      tmp[i] = this.array[i];
    }

    for (i = 0; i < this.count - 1; i++)
      for (var j = 0; j < this.count - i - 1; j++) {
        if (tmp[j] > tmp[j + 1]) {
          // swap temp and arr[i]
          var temp = tmp[j];
          tmp[j] = tmp[j + 1];
          tmp[j + 1] = temp;
        }
      }
    return tmp;
  }

  /**
   * Performs Quick Sort on the internal array
   * @return {[*]}: sorted copy of the internal array
   */
  quickSort() {
    var tmp = [this.count];
    for (var i = 0; i < this.count; i++) {
      tmp[i] = this.array[i];
    }
    this.quickSortHelper(tmp, 0, this.count - 1);

    return tmp;
  }

  /**
   * Performs Insertion sort on the internal array
   * @return {[*]}: sorted copy of the internal array
   */
  insertionSort() {
    var tmp = [this.count];
    for (var i = 0; i < this.count; i++) {
      tmp[i] = this.array[i];
    }

    for (var i = 0; i < tmp.length; i++) {
      let value = tmp[i];
      for (var j = i - 1; j > -1 && tmp[j] > value; j--) {
        tmp[j + 1] = tmp[j];
      }
      tmp[j + 1] = value;
    }

    return tmp;
  }

  /**
   * Performs Selection Sort on internal array
   * @returns {[*]}: Sorted copy of the internal data
   */
  selectionSort() {
    var tmp = [this.count];
    for (var i = 0; i < this.count; i++) {
      tmp[i] = this.array[i];
    }
    // One by one move boundary of unsorted sub-array
    for (var i = 0; i < this.count - 1; i++) {
      // Find the minimum element in unsorted array
      var min = i;
      for (var j = i + 1; j < this.count; j++) if (tmp[j] < tmp[min]) min = j;

      // Swap the found minimum element with the first element
      var temp = tmp[min];
      tmp[min] = tmp[i];
      tmp[i] = temp;
    }
    return tmp;
  }

  /**
   * prints the array
   */
  print() {
    this.array.map(function(data) {
      if (data !== null) {
        console.log(data);
      }
    });
  }
}

let arr = new SortedArray(10);
arr.append([9, 45, 6, 78, 42, 7, 1]);
arr.print();
console.log("---------bubble------------");
console.log(arr.bubbleSort());
console.log("---------merge-------------");
console.log(arr.mergeSort(arr.array));
console.log("---------insertion---------");
console.log(arr.insertionSort());
console.log("---------quick-------------");
console.log(arr.quickSort());
console.log("---------selection---------");
console.log(arr.selectionSort());
