<?php namespace datastructures\skiplist;
/*******************************************************
 *  SkipList.php
 *  Created by Stephen Hall on 04/25/18.
 *  Copyright (c) 2018 Stephen Hall. All rights reserved.
 *  Skip List implementation in PHP
 ********************************************************/

/**
 * Node for Skip List class
 */
class Node {
    public $data;
    public $nodeList;

    /**
     * Node Constructor
     * @param $data : data for the node to hold
     */
    public function __construct($data) {
        $this->data = $data;
        $this->nodeList = [];
    }

    /**
     * Gets the level of the Node
     * @return int: level of the node
     */
    public function Level() {
       return sizeof($this->nodeList) - 1;
    }
}


/**
 * Skip list class
 */
class SkipList {
    private $head;
    private $max;
    private $size;
    const PROBABILITY = 0.5;



    /**
     * Skip List Constructor
     */
    public function __construct() {
        $this->size = 0;
        $this->max = 0;
        $this->head = new Node(null); // a Node with value null marks the beginning
        array_push($this->head->nodeList,null); // null marks the end
    }

    /**
     * Adds a node into the Skip List
     * @param $data : data to add into the list
     * @return boolean : success|fail
     */
    public function add($data) {
        if($this->contains($data))
            return false;

        $this->size++;
        $level = 0;
        // random number from 0 to max + 1 (inclusive)
        while ($this->random() < self::PROBABILITY)
            $level++;
        while($level > $this->max) {
            // should only happen once
            array_push($this->head->nodeList,null);
            $this->max++;
        }

        $node = new Node($data);
        $current = $this->head;

        do {
            $current = $this->findNext($data, $current, $level);
            array_push($node->nodeList, $current->nodeList[$level]);
            $current->nodeList[$level] = $node;
        } while (($level--) > 0);
        return true;
    }

    /**
     * @return float : random 0-1
     */
    function random()
    {
        return (float)rand() / (float)getrandmax();
    }

    /**
     * Finds a node in the list with the same data
     * @param $data : data to find
     * @return Node : Node found or null
     */
    private function find($data) {
        return $this->finder($data, $this->head, $this->max);
    }

    /**
     * Returns node with the greatest value
     * @param $data : data to find
     * @param $current : current Node
     * @param $level : level to start form
     * @return Node : current node
     */
    private function finder($data, Node $current, $level) {
        do {
           $current = $this->findNext($data, $current, $level);
        } while(($level--) > 0);
        return $current;
    }

    /**
     * Returns the node at a given level with highest value less than data
     * @param $data : data to find
     * @param $current : current node
     * @param $level : current level
     * @return Node : highest node
     */
    private function findNext($data, Node $current, $level) {
        $next = $current->nodeList[$level];

        while($next != null) {
            $value = $next->data;
            if($this->lessThan($data, $value))
                break;
            $current = $next;
            $next = $current->nodeList[$level];
        }
        return $current;
    }

    /**
     * gets the size of the list
     * @return int: size of the list
     */
    public function size() {
        return $this->size;
    }

    /**
     * Determines if the object is in the list or not
     * @param $data : object to test
     * @return boolean : true|false
     */
	public function contains($data) {
        $node = $this->find($data);
        return (($node != null) && ($node->data != null) && $this->equalTo($node->data, $data));
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
}