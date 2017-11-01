<?php
/*******************************************************
 *  LinkedList.php
 *  Created by Stephen Hall on 10/31/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  A Linked List implementation in php
 ********************************************************/

/**
 * Class Node
 */
class Node
{
    /**
     * @var data: data to be help by the node
     * @var next: next node in the list
     */
    public $data;
    public $next;

    /**
     * Node constructor.
     * @param $data: data to be added to the list
     */
    function __construct($data)
    {
        $this->data = $data;
        $this->next = NULL;
    }
}

/**
 * Class LinkList
 */
class LinkList
{
    /**
     * @var head: first node in the list
     * @var tail: last node in the list
     * @var count: count of nodes in the list
     */
    private $head;
    private $tail;
    private $count;

    /**
     * LinkList constructor.
     */
    function __construct()
    {
        $this->head = NULL;
        $this->tail = NULL;
        $this->count = 0;
    }

    /**
     * Gets the current size of the list
     * @return int: current size of the list
     */
    public function size()
    {
        return $this->count;
    }

    /**
     * Adds a new node into the list with the given data
     * @param $data: data to be added to the list
     * @return Node|null: node added to the list or null
     */
    public function add($data)
    {
        // No data to insert into list
        if ($data == null)
            return null;

        $node = new Node(data);
        // The Linked list is empty
        if ($this->head == null)
        {
            $this->head = $node;
            $this->tail = $this->head;
            $this->count++;
            return $node;
        }

        // Add to the end of the list
        $this->tail->next = $node;
        $this->tail = $node;
        $this->count++;
        return $node;
    }

    /**
     * Removes the first node in the list matching the data
     * @param $data: data to remove from the list
     * @return Node|null: node removed from the list or null
     */
    public function remove($data)
    {
        // List is empty or no data to remove
        if ($this->head == null || $data == null)
        return null;

        $tmp = $this->head;
            // The data to remove what found in the first Node in the list
        if($tmp->data == data)
        {
            $this->head = $this->head->next;
            $this->count--;
            return $tmp;
        }
        // Try to find the node in the list
        while ($tmp->next != null)
        {
            // Node was found, Remove it from the list
            if ($tmp->next->data == data)
            {
                $node = $tmp->next;
                $tmp->next = $tmp->next->next;
                $this->count--;
                return $node;
            }
        }
        // The data was not found in the list
        return null;
    }

    /**
     * Finds the first node that has the given data
     * @param $key: key to find in the list
     * @return Node|null: node found or null
     */
    public function find($key)
    {
        $current = $this->head;
        while($current->data != $key)
        {
            if($current->next == NULL)
                return null;
            else
                $current = $current->next;
        }
        return $current;
    }

    /**
     * Prints the list
     */
    public function printList()
    {
        $items = [];
        $current = $this->head;
        while($current != null) 
        {
            array_push($items, $current->data);
            $current = $current->next;
        }

        $str = '';
        foreach($items as $item)
        {
            $str .= $item . '->';
        }

        echo $str;
    }
}
