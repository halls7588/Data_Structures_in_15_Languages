/*******************************************************
 *  SortedArray.java
 *  Created by Stephen Hall on 11/09/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  SortedArray implementation in Java
 ********************************************************/
package DataStructures.Java.Arrays.Sorted_Array;

/**
 * SortedArray Class
 * @param <T> Generic type
 */
public class SortedArray<T extends Comparable<T>>{
    /**
     * Private Members
     */
    private T[] array;
    private int count;
    private int size;

    /**
     * SortedArray default constructor
     */
    public SortedArray(){
        this(4);
    }

    /**
     * SortedArray constructor initialized to a specific size
     * @param size Size to initialize the array to
     */
    @SuppressWarnings("unchecked")
	public SortedArray(int size){
        count = 0;
        if(size > 0)
            this.size = size;
        else
            this.size = 4;
        array = (T[]) new Comparable[this.size];
    }

    /**
     * Doubles the size of the internal array
     */
    @SuppressWarnings("unchecked")
	private void resize(){
        size *= 2;
        T[] tmp = (T[]) new Comparable[size];
        for(int i = 0; i < count; i++){
            tmp[i] = array[i];
        }
        array = tmp;
    }

    /**
     * Adds new item into the array
     * @param data Data to add into the array
     * @return Data added into the array
     */
    public T add(T data){
        if(data == null)
            return null;

        if(this.count == size)
            resize();

        array[count] = data;
        count++;

        return data;
    }

    /**
     * Appends the contents of an array to the SortedArray
     * @param data: array to append
     * @return boolean: success|fail
     */
    public boolean append(T[] data){
        if(data != null){
            for (int i = 0; i < data.length; i++) {
                if(data[i] != null )
                    add(data[i]);
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
    public boolean set(int index, T data){
        if(index >= 0 && index < size) {
            array[index] = data;
            return true;
        }
        return false;
    }

    /**
     * Gets the data at the arrays given index
     * @param index Index to get data at
     * @return Data at the given index or default value of T if index does not exist
     */
    public T get(int index){
        if(index >= 0 && index < size)
            return array[index];
        return null;
    }

    /**
     * Removes the data at arrays given index
     * @param index Index to remove
     * @return Data removed from the array or default T value if index does not exist
     */
    public T remove(int index){
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
    @SuppressWarnings("unchecked")
	public void reset(){
        count = 0;
        size = 4;
        array = (T[]) new Comparable[this.size];
    }

    /**
     * Clears all data in the array leaving size intact
     */
    public void clear(){
        for(int i = 0; i < count; i++){
            array[i] = null;
        }
        this.count = 0;
    }


    /**
     * Gets the current count of the array
     * @return Number of items in the array
     */
    public int count(){
        return count;
    }

    /**
     * Private helper method for Merger Sort. Merges two sub-arrays of arr[].
     * @param arr: array to be sorted
     * @param l: index of first sub array
     * @param m: merge point
     * @param r: index of second sub array
     */
    @SuppressWarnings("unchecked")
	private void merge(T arr[], int l, int m, int r) {
        int i, j, k;
        int n1 = m - l + 1;
        int n2 =  r - m;

        // create temp arrays
        T[] L = (T[]) new Comparable[n1];
        T[] R = (T[]) new Comparable[n2];

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
            if (lessThan(L[i], R[j]) || equalTo(L[i], R[j])) {
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
            mergeSortHelper(arr, (m + 1), r);

            merge(arr, l, m, r);
        }
    }

    /**
     * Performs Merge Sort on internal array
     * @return T[]: sorted copy of the internal array
     */
    @SuppressWarnings("unchecked")
	public T[] mergeSort(){
        T[] tmp = (T[]) new Comparable[count];
        for(int i = 0; i < count; i++){
            tmp[i] = array[i];
        }
        mergeSortHelper(tmp, 0, (count - 1));
        return tmp;
    }

    /**
     * Performs Bubble sort on internal array
     * @return T[]: sorted copy of the internal array
     */
    @SuppressWarnings("unchecked")
	public T[] bubbleSort(){
        T[] tmp = (T[]) new Comparable[count];
        for(int i = 0; i < count; i++){
            tmp[i] = array[i];
        }

        for (int i = 0; i < count - 1; i++)
            for (int j = 0; j < count-i-1; j++) {
                if (greaterThan(tmp[j], tmp[j + 1])) {
                    // swap temp and arr[i]
                    T temp = tmp[j];
                    tmp[j] = tmp[j + 1];
                    tmp[j + 1] = temp;
                }
            }
        return tmp;
    }

    /**
     * Helper method for Quick Sort. Swaps data in the partition
     * @param arr: array to be sorted
     * @param low: low index
     * @param high: high index
     * @return int: pivot index
     */
    private int partition(T arr[], int low, int high) {
        T pivot = arr[high];
        int i = (low - 1); // index of smaller element
        for (int j = low; j < high; j++) {
            // If current element is smaller than or
            // equal to pivot
            if (lessThan(arr[j], pivot) || equalTo(arr[j], pivot)) {
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
     * Helper method for recursive Quick Sort
     * @param arr: array to be sorted
     * @param low: low index
     * @param high: high index
     */
    private void quickSortHelper(T arr[], int low, int high) {
        if (low < high) {
            // pivot is partitioning index, arr[pivot] is now at right place
            int pivot = partition(arr, low, high);

            // Recursively sort elements before and after pivot
            quickSortHelper(arr, low, (pivot - 1));
            quickSortHelper(arr, (pivot + 1), high);
        }
    }

    /**
     * Performs Quick Sort on the internal array
     * @return T[]: sorted copy of the internal array
     */
    @SuppressWarnings("unchecked")
	public T[] quickSort(){
        T[] tmp = (T[]) new Comparable[count];
        for(int i = 0; i < count; i++){
            tmp[i] = array[i];
        }
        quickSortHelper(tmp, 0, (count - 1));

        return tmp;
    }

    /**
     * Performs Insertion sort on the internal array
     * @return T[]: sorted copy of the internal array
     */
    @SuppressWarnings("unchecked")
	public T[] insertionSort(){
        T[] tmp = (T[]) new Comparable[count];
        for(int i = 0; i < count; i++){
            tmp[i] = array[i];
        }

        for (int i=1; i < count; ++i) {
            T key = tmp[i];
            int j = i - 1;

            // Move elements of arr[0..i-1], that are greater than key, to one position ahead
            while (j >= 0 && lessThan(tmp[j],key)) {
                tmp[j + 1] = tmp[j];
                j = j - 1;
            }
            tmp[j + 1] = key;
        }
        return tmp;
    }

    /**
     * Performs Selection Sort on internal array
     * @return T[]: Sorted copy of the internal data
     */
    @SuppressWarnings("unchecked")
	public T[] selectionSort(){
        T[] tmp = (T[]) new Comparable[count];
        for(int i = 0; i < count; i++){
            tmp[i] = array[i];
        }
        // One by one move boundary of unsorted sub-array
        for (int i = 0; i < count-1; i++) {
            // Find the minimum element in unsorted array
            int min = i;
            for (int j = i + 1; j < count; j++)
                if (lessThan(tmp[j],tmp[min]))
                    min = j;

            // Swap the found minimum element with the first
            // element
            T temp = tmp[min];
            tmp[min] = tmp[i];
            tmp[i] = temp;
        }
        return tmp;
    }

    /**
     * Determines if a is less than b
     * @param a: generic type to test
     * @param b: generic type to test
     * @return boolean: true|false
     */
    private boolean lessThan(T a, T b) {
        return a.compareTo(b) < 0;
    }

    /**
     * Determines if a is equal to b
     * @param a: generic type to test
     * @param b: generic type to test
     * @return boolean: true|false
     */
    private boolean equalTo(T a, T b) {
        return a.compareTo(b) == 0;
    }

    /**
     * Determines if a is greater than b
     * @param a: generic type to test
     * @param b: generic type to test
     * @return boolean: true|false
     */
    private boolean greaterThan(T a, T b) {
        return a.compareTo(b) > 0;
    }
}
