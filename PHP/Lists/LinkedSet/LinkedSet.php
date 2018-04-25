<?php namespace datastructures\linkedset;
/*******************************************************
 *  Linkedset.php
 *  Created by Stephen Hall on 04/24/18.
 *  Copyright (c) 2018 Stephen Hall. All rights reserved.
 *  Linked Set implementation in PHP
 ********************************************************/

/**
 * Node class for singly linked set
 */
class Node{
    /**
    * Public Members
    */
    public $data = null;
    public $next = null;

    /**
    * Node Class Constructor
    * @param $data : Data to be held in the Node
    */
    public function __construct($data){
        $this->data = $data;
        $this->next = null;
    }
}

/**
 * Linked set class
 */
class LinkedSet {

    /**
     * Private Members
     */
    private $head;
    private $tail;
    private $count;

    /**
     * Linked Set Constructor
     */
    public function __construct(){
        $this->head = $this->tail = null;
        $this->count = 0;
    }

    /**
     * Adds a new node into the list with the given data
     * @param $data : Data to add into the list<
     * @return Node added into the list
     */
    public function add($data){
        // No data to insert into list
        if ($data == null || ($this->find($data) != null))
            return null;

        $node = new Node($data);
        // The Linked list is empty
        if ($this->head == null) {
            $this->head = $node;
            $this->tail = $this->head;
            $this->count++;
        } else {
            // Add to the end of the list
            $this->tail->next = $node;
            $this->tail = $node;
            $this->count++;
        }
        return $node;
    }

    /**
     * Removes the first node in the list matching the data
     * @param $data : Data to remove from the list
     * @return Node removed from the list
     */
    public function remove($data){
        // List is empty or no data to remove
        if ($this->head == null || $data == null)
            return null;

        $tmp = $this->head;
        // The data to remove what found in the first Node in the list
        if($this->equalTo($tmp->data, $data)) {
            $this->head = $this->head->next;
            $this->count--;
            return $tmp;
        }
        // Try to find the node in the list
        while ($tmp->next != null) {
            // Node was found, Remove it from the list
            if ($this->equalTo($tmp->next->data, $data)){
                $this->node = $tmp->next;
                $tmp->next = $tmp->next->next;
                $this->count--;
                return $tmp;
            }
            $tmp = $tmp->next;
        }
        // The data was not found in the list
        return null;
    }

    /**
     * Gets the first node that has the given data
     * @param $data : Data to find in the list
     * @return Node First node with matching data or null if no node was found
     */
    public function find($data){
        // No list or data to find
        if ($this->head == null || $data == null)
            return null;

        $tmp = $this->head;
        // Try to find the data in the list
        while($tmp != null) {
            // Data was found
            if ($this->equalTo($tmp->data, $data))
                return $tmp;
            $tmp = $tmp->next;
        }
        // Data was not found in the list
        return null;
    }

    /**
     * Gets the node at the given index
     * @param $index : Index of the Node to get
     * @return Node at passed in index
     */
    public function indexAt($index){
        //Index was negative or larger then the amount of Nodes in the list
        if ($index < 0 || $index > $this->size())
            return null;

        $tmp = $this->head;
        // Move to index
        for ($i = 0; $i < $index; $i++)
            $tmp = $tmp->next;
        // return the node at the index position
        return $tmp;
    }

    /**
     * Gets the current count of the array
     * @return Number of items in the array
     */
    public function size(){
        return $this->count;
    }

    /**
     * Determines if a is equal to b
     * @param $a: generic type to test
     * @param $b: generic type to test
     * @return boolean: true|false
     */
    private function equalTo($a, $b) {
        return $a === $b;
    }
}