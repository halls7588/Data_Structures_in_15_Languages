<?php namespace datastructures\arrayedqueue;
/*******************************************************
 *  ArrayedQueue.php
 *  Created by Stephen Hall on 11/01/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  A Arrayed Queue implementation in PHP
 ********************************************************/

/**
 * Class ArrayedQueue
 */
class ArrayedQueue
{
    /**
     * @var int: the curent size of the queue
     * @var array: array to hold the items in the queue
     */
    private $count;
    private $array;

    /**
     * ArrayedQueue constructor.
     */
    public function __construct()
    {
        $this->count = 0;
        $this->array = [];
    }

    /**
     * adds an item onto the queue
     * @param $data: data to add onto the queue
     * @return $data: data added into the queue
     */
    public function enqueue($data)
    {
        array_push($this->array, $data);
        $this->count++;
        return $this->array[$this->count-1];
    }

    /**
     * removes the next item in the queue
     * @return Mixed|null: returns the item removed from the queue or null
     */
    public function dequeue()
    {
        if($this->isEmpty())
            return null;

        $data = array_shift($this->array);
        $this->count--;
        return $data;
    }

    /**
     * returns a value indicating if the queue is empty or not
     * @return bool: true if the queue is empty
     */
    public function isEmpty()
    {
        return ($this->size() == 0);
    }

    /**
     * returns the current size of the queue
     * @return int: size of the queue
     */
    public function size()
    {
        return $this->count;
    }
}