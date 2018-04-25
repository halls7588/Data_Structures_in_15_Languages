<?php namespace datastructures\doublylinkedlist;
/*******************************************************
 *  DoublyLinkedList.php
 *  Created by Stephen Hall on 11/01/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  A Linked List implementation in PHP
 ********************************************************/

/**
 * Class Node
 */
class Node
{
    /**
     * @var $data : data to be help by the node
     * @var $next : next node in the list
     * @var $previous : previous node in the list
     */
    public $data;
    public $next;
    public $previous;

    /**
     * Node constructor.
     * @param $data : data to add to the list
     */
    function __construct($data)
    {
        $this->data = $data;
        $this->next = null;
        $this->previous = null;
    }
}


/**
 * Class DoublyLinkedList
 */
class DoublyLinkedList
{

    /**
     * Private Members
     */
    private $head;
    private $tail;
    private $count;

    /**
     * Linked List Constructor
     */
    public function __construct()
    {
        $this->head = $this->tail = null;
        $this->count = 0;
    }

    /**
     * Adds a new node into the list with the given data
     * @param $data: Data to add into the list
     * @return Node|null: Node added into the list or null
     */
    public function Add($data)
    {

        // No data to insert into list
        if ($data == null)
            return null;

        $node = new Node($data);

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
        $node->previous = $this->tail;
        $this->tail = $node;
        $this->count++;
        return $node;
    }

    /**
     * Removes the first node in the list matching the data
     * @param $data: Data to remove from the list
     * @return Node|null: Node removed from the list or null
     */
    public function Remove($data)
    {

        // List is empty or no data to remove
        if ($this->head == null || $data == null)
            return null;

        $tmp = $this->head;
        // The data to remove what found in the first Node in the list
        if($tmp->data == $data)
        {
            $this->head = $this->head->next;
            $this->count--;
            return $tmp;
        }

        // Try to find the node in the list
        while ($tmp->next != null)
        {
            // Node was found, Remove it from the list
            if ($tmp->next->data == $data)
            {
                if($tmp->next == $this->tail)
                {
                    $this->tail = $tmp;
                    $tmp = $tmp->next;
                    $this->tail->next = null;
                    $this->count--;
                    return $tmp;
                }
                else
                {
                    $node = $tmp->next;
                    $tmp->next = $tmp->next->next;
                    $tmp->next->next->previous = $tmp;
                    $node->next = $node->previous = null;
                    $this->count--;
                    return $node;
                }
            }
        }
        // The data was not found in the list
        return null;
    }

    /**
     * Gets the first node that has the given data
     * @param data Data to find in the list
     * @return Node First node with matching data or null if no node was found
     */
    public function Find($data)
    {
        // No list or data to find
        if ($this->head == null || $data == null)
            return null;

        $tmp = $this->head;
        // Try to find the data in the list
        while($tmp != null)
        {
            // Data was found
            if ($tmp->data == $data)
                return $tmp;

            $tmp = $tmp->next;
        }
        // Data was not found in the list
        return null;
    }

    /**
     * Gets the node at the given index
     * @param index Index of the Node to get
     * @return Node at passed in index
     */
    public function IndexAt($index)
    {
        //Index was negative or larger then the amount of Nodes in the list
        if ($index < 0 || $index > $this->Size())
            return null;

        $tmp = $this->head;

        // Move to index
        for ($i = 0; $i < $index; $i++)
        {
            $tmp = $tmp->next;
        }
        // return the node at the index position
        return $tmp;
    }

    /**
     * Gets the current count of the array
     * @return Number of items in the array
     */
    public function Size()
    {
        return $this->count;
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
