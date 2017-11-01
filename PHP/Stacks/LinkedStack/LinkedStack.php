<?php
/*******************************************************
 *  LinkedStack.php
 *  Created by Stephen Hall on 11/01/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  A Linked Stack implementation in PHP
 ********************************************************/

class Node
{
    /**
     * @var $data: data to be help by the node
     * @var $next: next node in the list
     */
    public $data;
    public $next;

    /**
     * Node constructor.
     * @param $data: data for the node to hold
     */
    public function __construct($data)
    {
        $this->data = $data;
        $this->next = null;
    }
}

/**
 * Class LinkedStack
 */
class LinkedStack
{
    /**
     * @var $count: number of items in the stack
     * @var $head: item at the top of the stack
     */
    private $count;
    private $head;

    /**
     * LinkedStack constructor.
     */
    public function __construct()
    {
        $this->count = 0;
        $this->head = null;
    }

    /**
     * Pushes an item onto the stack
     * @param $data: data to be place on the stack
     * @return mixed: data placed onto the stack
     */
    public function push($data)
    {
        $node = new Node($data);
        $node->next = $this->head;
        $this->head = $node;
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
            return null;

        $node = $this->head;
        $this->head = $this->head->next;
        $node->next = null;
        $this->count--;
        return $node;
    }

    /**
     * returns the item on the top of the stack
     * @return mixed|null: item on top of the stack or null
     */
    public function top()
    {
        return $this->head;
    }

    /**
     * returns value indicating if the stack is empty or not
     * @return bool: true if the stack is empty, false otherwise
     */
    public function isEmpty(){
        return ($this->count == 0);
    }

    /**
     * return the size of the stack
     * @return int: current number of item in the stack
     */
    public function size()
    {
        return $this->count;
    }

}