<?php
/*******************************************************
 *  ArrayedSet.php
 *  Created by Stephen Hall on 4/19/18.
 *  Copyright (c) 2018 Stephen Hall. All rights reserved.
 *  Arrayed Set implementation in php
 ********************************************************/

/**
 * ArrayedSet Class
 * @param <T> Generic type
 */
public class ArrayedSet {

    /**
     * Private Members
     */
    private $array = [];
    private $count = 0;
    private $size = 0;

     /**
     * ArrayedSet constructor initialized to a specific size
     * @param $size : Size to initialize the array to
     */
	public function __construct($size){
        $this->count = 0;
        if($size > 0)
            $this->size = $size;
        else
            $this->size = 4;
        $this->array = [$this->size];
    }

    /**
     * Doubles the size of the internal array
     */
	private function resize(){
        $this->size *= 2;
        $tmp = [$this->size];
        for($i = 0; $i < $this->count; $i++){
            $tmp[$i] = $this->array[$i];
        };
        $this->array = $tmp;
    }

    /**
     * Adds new item into the array
     * @param $data : Data to add into the array
     * @return mixed : data added into the array
     */
    public function add($data){
        if(!isset($data))
            return null;

        if($this->contains($data))
            return null;

        if($this->count == $this->size)
            $this->resize();

        $this->array[$this->count] = $data;
        $this->count++;
        return $data;
    }

    /**
     * Appends the contents of an array to the ArrayedSet
     * @param $data : array to append
     * @return boolean : success|fail
     */
    public function append($data){
        if(isset($data)){
            foreach ($data as $aData) {
                if (isset($aData))
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
        if(!$this->contains($data)){
            if($index >= 0 && $index < $this->size) {
                $this->array[$index] = $data;
                return true;
            }
        }
        return false;
    }

    /**
     * Gets the data at the arrays given index
     * @param $index : Index to get data at
     * @return mixed : data at the given index or null
     */
    public function get($index){
        if($index >= 0 && $index < $this->size)
            return $this->array[$index];
        return null;
    }

    /**
     * Removes the data at arrays given index
     * @param $index : Index to remove
     * @return mixed : data removed from the array or null
     */
    public function remove($index){
        if ($index <= 0 || $index > $this->count || $this->count == 0)
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
     * Tests to see if the data exist in the list
     * @param $data : data to find
     * @return bool : success|fail
     */
    private function contains($data){
        for($i = 0; $i < $this->size; $i++){
            if($this->array[$i] == $data)
            return true;
        }
        return false;
    }

    /**
     * Gets the current count of the array
     * @return Number of items in the array
     */
    public function count(){
        return $this->count;
    }

}