<?php namespace datastructures\arrayedstack;
/*******************************************************
 *  ArrayedStack.php
 *  Created by Stephen Hall on 11/01/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  A Arrayed Stack implementation in PHP
 ********************************************************/

/**
 * Class ArrayedStack
 */
class ArrayedStack
{
    /**
     * @var $array: array to hold the items in the stack
     * @var $count: number of items in the stack
     */
    private $array;
    private $count;

    /**
     * ArrayedStack constructor.
     */
    public function __construct()
    {
        $this->array = [];
        $this->count = 0;
    }

    /**
     * Pushes an item onto the stack
     * @param $data: data to be place on the stack
     * @return mixed: data placed onto the stack
     */
    public function push($data)
    {
        array_push($this->array,$data);
        $this->count++;
        return $this->top();
    }

    /**
     * pops an item off of the stack
     * @return mixed|null: item pop off of the stack or null
     */
    public function pop()
    {
        if($this->isEmpty())
        {
            return null;
        }
        $data = $this->array[$this->count-1];
        $this->count--;
        return $data;
    }

    /**
     * return the size of the stack
     * @return int: current number of item in the stack
     */
    public function size(){
        return $this->count;
    }

    /**
     * returns value indicating if the stack is empty or not
     * @return bool: true if the stack is empty, false otherwise
     */
    public function isEmpty()
    {
        return ($this->size() == 0);
    }

    /**
     * returns the item on the top of the stack
     * @return mixed|null: item on top of the stack or null
     */
    public function top()
    {
        if($this->isEmpty())
        {
            return null;
        }
        return $this->array[$this->count-1];
    }
}