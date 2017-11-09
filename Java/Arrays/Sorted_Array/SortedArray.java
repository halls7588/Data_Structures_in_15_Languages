/*******************************************************
 *  SortedArray.java
 *  Created by Stephen Hall on 11/09/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  SortedArray implementation in Java
 ********************************************************/

package DataStructures.Arrays;

/**
 * ArrayList Class
 * @param <T> Generic type
 */
public class SortedArray<T> {

    /**
     * Private Members
     */
    private T[] array;
    private int count;
    private int size;

    /**
     * SortedArray default constructor
     */
    public ArrayList(){
        count = 0;
        size = 4;
        array = (T[]) new Object[size];
    }

    /**
     * SortedArray constructor initialized to a specific size
     * @param size Size to initialize the array to
     */
    public ArrayList(int size){
        count = 0;
        if(size > 0)
            this.size = size;
        else
            this.size = 4;
        array = (T[]) new Object[this.size];
    }

    /**
     * Doubles the size of the internal array
     */
    private void Resize(){
        size *= 2;
        T[] tmp = new Object[this.size];
        for(int i = 0; i < this.count; i++){
            tmp[i] = this.arr[i];
        }
        this.arr = tmp;
    }

    /**
     * Adds new item into the array
     * @param data Data to add into the array
     * @return Data added into the array
     */
    public T Add(T data){
        if(data == null)
            return null;

        if(this.count == size)
            this.resize();

        this.arr[this.count] = data;
        this.count++;

        return data;
    }

    /**
     * Appends the contents of an array to the SortedArray
     * @param data: array to append
     * @return boolean: success|fail
     */
    public boolean Append(T[] data){
        if(data != null){
            for (int i = 0; i < data.length; i++) {
                if(data[i] != null )
                    this.add(data[i]);
            }
            return true;
        }
        return false;
    }

    /**
     * Sets the data at the given index
     * @param index: index to set
     * @param data: data to set index to
     * @return boolean: success|fail
     */
    public boolean Set(int index, T data){
        if(index >= 0 && index < this.size) {
            this.arr[index] = data;
            return true;
        }
        return false;
    }

    /**
     * Gets the data at the arrays given index
     * @param index Index to get data at
     * @return Data at the given index or default value of T if index does not exist
     */
    public T Get(int index){
        if(index >= 0 && index < this.size)
            return this.arr[index];
        return null;
    }

    /**
     * Removes the data at arrays given index
     * @param index Index to remove
     * @return Data removed from the array or default T value if index does not exist
     */
    public T Remove(int index)
    {
        if (index < 0 || index > count)
            return null;

        T tmp = array[index];
        array[index] = null;
        count--;
        return tmp;
    }

    /**
     * Resets the internal array to default size with no data
     */
    public void Reset(){
        this.count = 0;
        this.size = 4;
        this.array = (T[]) new Object[this.size];
    }

    /**
     * Clears all data in the array leaving size intact
     */
    public void Clear(){
        for(int i = 0; i < this.count; i++){
            this.arr[i] = null;
        }
        this.count = 0;
    }


    /**
     * Gets the current count of the array
     * @return Number of items in the array
     */
    public int Count(){
        return count;
    }

    /**
     * Private helper method for Merger Sort. Merges two subarrays of arr[].
     * @param arr: array to be sorted
     * @param l: index of first sub array
     * @param m: merge point
     * @param r: index of second sub array
     */
    private void merge(T arr[], int l, int m, int r) {
        int i, j, k;
        int n1 = m - l + 1;
        int n2 =  r - m;

        // create temp arrays
        T L[n1], R[n2];

        // Copy data to temp arrays L[] and R[]
        for (i = 0; i < n1; i++)
            L[i] = arr[l + i];
        for (j = 0; j < n2; j++)
            R[j] = arr[m + 1+ j];

        // Merge the temp arrays back into arr[l..r]
        i = 0; // Initial index of first subarray
        j = 0; // Initial index of second subarray
        k = l; // Initial index of merged subarray
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
     * Recursive helper method for merge sort
     * @param arr: sub array to be sorted
     * @param l: left index
     * @param r: right index
     */
    private void mergeSortHelper(T arr[], int l, int r) {
        if (l < r){
            // Same as (l+r)/2, but avoids overflow for
            // large l and h
            int m = l+(r-l)/2;

            // Sort first and second halves
            mergeSortHelper(arr, l, m);
            mergeSortHelper(arr, m+1, r);

            merge(arr, l, m, r);
        }
    }

    /**
     * Preforms Merge Sort on internal array
     * @return T[]: sorted copy of the internal array
     */
    public T[] MergeSort(){
        T[] tmp = = (T[]) new Object[count];
        for(int i = 0; i < count; i++){
            tmp[i] = arr[i];
        }
        mergeSortHelper(tmp, 0, count -1);
        return tmp;
    }

    /**
     * Preforms Bubble sort on internal array
     * @return T[]: sorted copy of the internal array
     */
    public T[] BubbleSort(){
        T[] tmp = = (T[]) new Object[count];
        for(int i = 0; i < count; i++){
            tmp[i] = arr[i];
        }

        for (int i = 0; i < count - 1; i++)
            for (int j = 0; j < count-i-1; j++) {
                if (tmp[j] > tmp[j + 1]) {
                    // swap temp and arr[i]
                    T temp = tmp[j];
                    tmp[j] = tmp[j + 1];
                    tmp[j + 1] = temp;
                }
            }
        return tmp;
    }

    /**
     * Helper method for Quicksort. Swaps data in the partition
     * @param arr: array to be sorted
     * @param low: low index
     * @param high: high index
     * @return int: pivot index
     */
    private int partition(T arr[], int low, int high) {
        int pivot = arr[high];
        int i = (low - 1); // index of smaller element
        for (int j = low; j < high; j++) {
            // If current element is smaller than or
            // equal to pivot
            if (arr[j] <= pivot) {
                i++;

                // swap arr[i] and arr[j]
                T temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }

        // swap arr[i+1] and arr[high] (or pivot)
        T temp = arr[i + 1];
        arr[i + 1] = arr[high];
        arr[high] = temp;

        return i + 1;
    }

    /**
     * Helper method for recursive quicksort
     * @param arr: array to be sorted
     * @param low: low index
     * @param high: high index
     */
    private void quickSortHelper(T arr[], int low, int high) {
        if (low < high) {
            // pivot is partitioning index, arr[pivot] is now at right place
            int pivot = partition(arr, low, high);

            // Recursively sort elements before and after pivot
            quickSortHelper(arr, low, pivot-1);
            quickSortHelper(arr, pivot+1, high);
        }
    }

    /**
     * Preforms Quick Sort on the internal array
     * @return T[]: sorted copy of the internal array
     */
    public T[] QuickSort(){
        T[] tmp = = (T[]) new Object[count];
        for(int i = 0; i < count; i++){
            tmp[i] = arr[i];
        }
        quickSortHelper(tmp, 0 count - 1);

        return tmp;
    }

    /**
     * Preforsms Insertion sort on the internal array
     * @return T[]: sorted copy of the internal array
     */
    public T[] InsertionSort(){
        T[] tmp = = (T[]) new Object[count];
        for(int i = 0; i < count; i++){
            tmp[i] = arr[i];
        }

        for (int i=1; i < count; ++i) {
            T key = tmp[i];
            int j = i - 1;

            // Move elements of arr[0..i-1], that are greater than key, to one position ahead
            while (j >= 0 && tmp[j] > key) {
                tmp[j + 1] = tmp[j];
                j = j - 1;
            }
            tmp[j + 1] = key;
        }

        return tmp;
    }

    /**
     * Preforms Selection Sort on internal array
     * @return T[]: Sorted copy of the internal data
     */
    public T[] SelectionSort(){
        T[] tmp = = (T[]) new Object[count];
        for(int i = 0; i < count; i++){
            tmp[i] = arr[i];
        }

        // One by one move boundary of unsorted subarray
        for (int i = 0; i < count-1; i++) {
            // Find the minimum element in unsorted array
            int min = i;
            for (int j = i + 1; j < count; j++)
                if (tmp[j] < tmp[min])
                    min = j;

            // Swap the found minimum element with the first
            // element
            T temp = tmp[min];
            tmp[min] = tmp[i];
            tmp[i] = temp;
        }
        return tmp;
    }
}
