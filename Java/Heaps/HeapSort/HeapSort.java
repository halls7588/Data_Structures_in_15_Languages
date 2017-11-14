/*******************************************************
 *  HeapSort.java
 *  Created by Stephen Hall on 11/14/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  HeapSort implementation in Java
 ********************************************************/

/**
 * HeapSort Class
 * @param <T> Generic Type
 */
public class HeapSort<T extends Comparable<T>>{
	
    /**
     * Sorts the given array
     * @param heap; array to sort
     */
    public void sort(T heap[]){
        for (int i =  heap.length / 2 - 1; i >= 0; i--){
            heapify(heap, heap.length, i);
        }
        for (int i = heap.length - 1; i >= 0; i--){
            T temp = heap[0];
            heap[0] = heap[i];
            heap[i] = temp;
            heapify(heap, i, 0);
        }
    }

    /**
     * Builds a heap maating conditions out of the array
     * @param arr: array to heapify
     * @param length: length allowed
     * @param index: starting index 
     */
    private void heapify(T arr[], int length, int index){
        int left = 2 * index + 1; 
        int right = 2 * index + 2; 
        int largest = index;

        if (left < length && arr[left].compareTo(arr[largest]) > 0)
            largest = left;
        
        if (right < length && arr[right].compareTo(arr[largest]) > 0)
            largest = right;
        
        if (largest != index){
            T swap = arr[index];
            arr[index] = arr[largest];
            arr[largest] = swap;
            heapify(arr, length, largest);
        }
    }
}
