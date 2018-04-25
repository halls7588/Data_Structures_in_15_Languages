<?php namespace datastructures\sortedarrray;
/*******************************************************
 *  SortedArray.php
 *  Created by Stephen Hall on 04/25/18.
 *  Copyright (c) 2018 Stephen Hall. All rights reserved.
 *  SortedArray implementation in PHP
 ********************************************************/

/**
 * SortedArray Class
 */
class SortedArray {
    /**
     * Private Members
     */
    private $array = [];
    private $count;
    private $size;

    /**
     * SortedArray constructor initialized to a specific size
     * @param $size: Size to initialize the array to
     */
	public function __construct($size){
        $this->count = 0;
        ($size > 0) ? $this->size = $size : $this->size = 4;
        $this->array = [$this->size];
    }

    /**
     * Doubles the size of the internal array
     */
	private function resize(){
        $this->size *= 2;
        $tmp = [$this->size];
        for($i = 0; $i < $this->count; $i++)
            $tmp[$i] = $this->array[$i];
        $this->array = $tmp;
    }

    /**
     * Adds new item into the array
     * @param $data : Data to add into the array
     * @return mixed : Data added into the array
     */
    public function add($data){
        if($data == null)
            return null;

        if($this->count == $this->size)
            $this->resize();

        $this->array[$this->count] = $data;
        $this->count++;

        return $data;
    }

    /**
     * Appends the contents of an array to the SortedArray
     * @param $data : array to append
     * @return boolean : success|fail
     */
    public function append(array $data){
        if($data != null){
            foreach ($data as $aData) {
                if ($aData != null)
                    $this->add($aData);
            }
            return true;
        }
        return false;
    }

    /**
     * Sets the data at the given index
     * @param $index : index to set
     * @param $data : data to set index to
     * @return boolean : success|fail
     */
    public function set($index, $data){
        if($index >= 0 && $index < $this->size && is_int($index)) {
            $this->array[$index] = $data;
            return true;
        }
        return false;
    }

    /**
     * Gets the data at the arrays given index
     * @param $index : Index to get data at
     * @return mixed : Data at the given index or null if index does not exist
     */
    public function get($index){
        if(is_int($index) && $index >= 0 && $index < $this->size)
            return $this->array[$index];
        return null;
    }

    /**
     * Removes the data at arrays given index
     * @param $index : Index to remove
     * @return mixed : Data removed from the array or null if index does not exist
     */
    public function remove($index){
        if (is_int($index) && $index < 0 || $index > $this->count)
            return null;

        $tmp = $this->array[$index];
        $this->array[$index] = null;
        $this->count--;
        return $tmp;
    }

    /**
     * Resets the internal array to default size with no data
     */
    public function reset(){
        $this->count = 0;
        $this->size = 4;
        $this->array = [$this->size];
    }

    /**
     * Clears all data in the array leaving size intact
     */
    public function clear(){
        for($i = 0; $i < $this->count; $i++){
            $this->array[$i] = null;
        }
        $this->count = 0;
    }


    /**
     * Gets the current count of the array
     * @return Number of items in the array
     */
    public function count(){
        return $this->count;
    }

    /**
     * Private helper method for Merger Sort. Merges two sub-arrays of arr[].
     * @param &$arr: array to be sorted
     * @param $l: index of first sub array
     * @param $m: merge point
     * @param $r: index of second sub array
     */
    private function merge(array &$arr, $l, $m, $r) {
        $n1 = $m - $l + 1;
        $n2 =  $r - $m;

        // create temp arrays
        $L = [$n1];
        $R = [$n2];

        // Copy data to temp arrays L[] and R[]
        for ($i = 0; $i < $n1; $i++)
            $L[$i] = $arr[$l + $i];
        for ($j = 0; $j < $n2; $j++)
            $R[$j] = $arr[$m + 1+ $j];

        // Merge the temp arrays back into arr[l..r]
        $i = 0; // Initial index of first sub-array
        $j = 0; // Initial index of second sub-array
        $k = $l; // Initial index of merged sub-array
        while ($i < $n1 && $j < $n2) {
            if ($this->lessThan($L[$i], $R[$j]) ||$this-> equalTo($L[$i], $R[$j])) {
                $arr[$k] = $L[$i];
                $i++;
            }
            else {
                $arr[$k] = $R[$j];
                $j++;
            }
            $k++;
        }

        // Copy the remaining elements of L[], if there are any
        while ($i < $n1){
            $arr[$k] = $L[$i];
            $i++;
            $k++;
        }

        // Copy the remaining elements of R[], if there are any
        while ($j < $n2){
            $arr[$k] = $R[$j];
            $j++;
            $k++;
        }
    }

    /**
     * Recursive helper method for merge sort
     * @param &$arr : sub array to be sorted
     * @param $l : left index
     * @param $r : right index
     */
    private function mergeSortHelper(array &$arr, $l, $r) {
        if ($l < $r){
            // Same as (l+r)/2, but avoids overflow for
            // large l and h
            $m = $l+($r-$l)/2;

            // Sort first and second halves
            $this->mergeSortHelper($arr, $l, $m);
            $this->mergeSortHelper($arr, ($m + 1), $r);

            $this->merge($arr, $l, $m, $r);
        }
    }

    /**
     * Performs Merge Sort on internal array
     * @return array : sorted copy of the internal array
     */
	public function mergeSort(){
    $tmp = [$this->count];
        for($z = 0; $z < $this->count; $z++)
            $tmp[$z] = $this->array[$z];
        $this->mergeSortHelper($tmp, 0, ($this->count - 1));
        return $tmp;
    }

    /**
     * Performs Bubble sort on internal array
     * @return array : sorted copy of the internal array
     */
	public function bubbleSort(){
        $tmp = [$this->count];
        for($z = 0; $z < $this->count; $z++)
            $tmp[$z] = $this->array[$z];

        for ($i = 0; $i < $this->count - 1; $i++)
            for ($j = 0; $j < $this->count-$i-1; $j++) {
        if ($this->greaterThan($tmp[$j], $tmp[$j + 1])) {
            // swap temp and arr[i]
            $temp = $tmp[$j];
            $tmp[$j] = $tmp[$j + 1];
            $tmp[$j + 1] = $temp;
        }
    }
        return $tmp;
    }

    /**
     * Helper method for Quick Sort. Swaps data in the partition
     * @param &$arr : array to be sorted
     * @param $low : low index
     * @param $high : high index
     * @return int: pivot index
     */
    private function partition(array &$arr, $low, $high) {
        $pivot = $arr[$high];
        $i = ($low - 1); // index of smaller element
        for ($j = $low; $j < $high; $j++) {
            // If current element is smaller than or
            // equal to pivot
            if ($this->lessThan($arr[$j], $pivot) || $this->equalTo($arr[$j], $pivot)) {
                $i++;
                // swap arr[i] and arr[j]
                $temp = $arr[$i];
                $arr[$i] = $arr[$j];
                $arr[$j] = $temp;
            }
        }

        // swap arr[i+1] and arr[high] (or pivot)
        $temp = $arr[$i + 1];
        $arr[$i + 1] = $arr[$high];
        $arr[$high] = $temp;

        return $i + 1;
    }

    /**
     * Helper method for recursive Quick Sort
     * @param &$arr : array to be sorted
     * @param $low : low index
     * @param $high : high index
     */
    private function quickSortHelper(array &$arr, $low, $high) {
        if ($low < $high) {
            // pivot is partitioning index, arr[pivot] is now at right place
            $pivot = $this->partition($arr, $low, $high);

            // Recursively sort elements before and after pivot
            $this->quickSortHelper($arr, $low, ($pivot - 1));
            $this->quickSortHelper($arr, ($pivot + 1), $high);
        }
    }

    /**
     * Performs Quick Sort on the internal array
     * @return array : sorted copy of the internal array
     */
	public function quickSort(){
        $tmp = [$this->count];
        for($z = 0; $z < $this->count; $z++)
            $tmp[$z] = $this->array[$z];

        $this->quickSortHelper($tmp, 0, ($this->count - 1));

        return $tmp;
    }

    /**
     * Performs Insertion sort on the internal array
     * @return array : sorted copy of the internal array
     */
	public function insertionSort(){
        $tmp = [$this->count];

        for($z = 0; $z < $this->count; $z++)
            $tmp[$z] = $this->array[$z];

        for ($i=1; $i < $this->count; ++$i) {
            $key = $tmp[$i];
            $j = $i - 1;

            // Move elements of arr[0..i-1], that are greater than key, to one position ahead
            while ($j >= 0 && $this->lessThan($tmp[$j],$key)) {
                $tmp[$j + 1] = $tmp[$j];
                $j = $j - 1;
            }
            $tmp[$j + 1] = $key;
        }
        return $tmp;
    }

    /**
     * Performs Selection Sort on internal array
     * @return array : Sorted copy of the internal data
     */
    public function selectionSort(){
        $tmp = [$this->count];

        for($q = 0; $q < $this->count; $q++){
            $tmp[$q] = $this->array[$q];
        }

        // One by one move boundary of unsorted sub-array
        for ($i = 0; $i < $this->count-1; $i++) {
        // Find the minimum element in unsorted array
            $min = $i;
            for ($j = $i + 1; $j < $this->count; $j++)
                if ($this->lessThan($tmp[$j],$tmp[$min]))
                    $min = $j;

            // Swap the found minimum element with the first
            // element
            $temp = $tmp[$min];
            $tmp[$min] = $tmp[$i];
            $tmp[$i] = $temp;
        }
        return $tmp;
    }

    /**
     * Determines if a is less than b
     * @param $a : generic type to test
     * @param $b : generic type to test
     * @return boolean : true|false
     */
    private function lessThan($a, $b) {
        return $a < $b;
    }

    /**
     * Determines if a is equal to b
     * @param $a : generic type to test
     * @param $b : generic type to test
     * @return boolean : true|false
     */
    private function equalTo($a, $b) {
        return $a === $b;
    }

    /**
     * Determines if a is greater than b
     * @param $a : generic type to test
     * @param $b : generic type to test
     * @return boolean : true|false
     */
    private function greaterThan($a, $b) {
        return $a > $b;
    }
}