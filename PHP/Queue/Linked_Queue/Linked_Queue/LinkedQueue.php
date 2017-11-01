<?php
/*******************************************************
 *  LinkedQueue.php
 *  Created by Stephen Hall on 11/01/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  A Linked Queue implementation in PHP
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
 * Class LinkedQueue
 */
class LinkedQueue
{
    /**
     * @var int: number of items in the queue
     * @var Node: node at the front of the queue
     * @var Node: node at the end of the queue
     */
    private $count;
    private $head;
    private $tail;

    /**
     * LinkedQueue constructor.
     */
    public function __construct()
    {
        $this->count = 0;
        $this->head = null;
        $this->tail = null;
    }

    /**
     * adds an item onto the queue
     * @param $data: data to add onto the queue
     * @return Node: node added into the queue
     */
    public function enqueue($data)
    {
        if($this->isEmpty())
        {
            // queue is empty
            $node = new Node($data);
            $this->head = $node;
            $this->tail = $this->head;
            $this->count++;
            return $node;
        }
        else
        {
            // add to the end of the queue
            $node = new Node($data);
            $this->tail->next = $node;
            $this->tail = $node;
            $this->count++;
            return $node;
        }
    }

    /**
     * removes the next item in the queue
     * @return Node|null: returns the item removed from the queue or null
     */
    public function dequeue()
    {
        if($this->isEmpty())
            return null;
        $node = $this->head;
        $this->head = $this->head->next;
        $this->count--;
        return $node;
    }

    /**
     * returns a vlaue indicating if the queue is empty or not
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