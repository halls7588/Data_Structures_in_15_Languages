<?php
/*******************************************************
 *  CircularArray.php
 *  Created by Stephen Hall on 11/01/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  A Circular Array implementation in PHP
 ********************************************************/


/**
 * Class CircularArray
 */
class CircularArray {
    private $array = [];
    private $size = 0;
    private $zeroIndex = 0;
    private $count = 0;

    /**
     * Doubles the size of the array
     */
    private function resize () {
        $this->size *= 2;
    }


    /**
     * Adds an item into the array
     * @param $data: data to be added
     * @return mixed
     */
    public function add($data) {

        $tmp = ($this->zeroIndex + $this->count) % $this->size;
        $this->array[$tmp] = $data;
        if ((($this->count + 1) / $this->size) >= 1) {
            $this->resize();
        }
        $this->count++;
        return $this->array[$tmp];
    }

    /**
     * Gets the data at passed in index
     * @param $i: index to retrieve
     * @return mixed|null: data at index or null
     */
    public function dataAt($i) {
        if (($i + $this->zeroIndex) % $this->size < $this->count && $this->array[($i + $this->zeroIndex) % $this->size]) {
            return ($this->array[$i + $this->zeroIndex % $this->size]);
        }
        return null;
    }

    /**
     * Removes an item from the array
     * @param $i: index to remove
     * @return mixed|null: data removed from the array or null
     */
    public function remove($i) {
        $tmp = $this->array[$i + $this->zeroIndex % $this->size];
        $this->array[$i + $this->zeroIndex % $this->size] = $this->array[$this->zeroIndex];
        $this->array[$this->zeroIndex] = null;
        $this->count--;
        $this->zeroIndex = ($this->zeroIndex + 1) % $this->size;
        return $tmp;
    }

    /**
     * Prints the array
     */
    public function printArr() {
        $tmp = "";
        for($i = 0; $i < $this->size; $i++){
            $tmp .= $this->array[($this->zeroIndex + $i) % $this->size]."->";
        }
        echo $tmp;
    }
}