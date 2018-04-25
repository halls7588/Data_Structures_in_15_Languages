<?php namespace datastructures\binarytree;
/*******************************************************
 *  BinaryTree.php
 *  Created by Stephen Hall on 11/01/17.
 *  Copyright (c) 2016 Stephen Hall. All rights reserved.
 *  A Binary Tree implementation in PHP
 ********************************************************/

/**
 * Class Node
 */
class Node {
    /**
     * @var $left : left leaf in the tree
     * @var $right : right leaf in the tree
     * @var $parent : parent node in the tree
     * @var $data : data to be help by the node
     */
    public $left;
    public $right;
    public $parent;
    public $data;

    /**
     * Node constructor.
     * @param $data: data to be help in the node
     */
    public function __construct($data) {
        $this->left = $this->right = null;
        $this->data = $data;
    }
}

/**
 * Class BinaryTree
 */
class BinaryTree {

    /**
     * @var $root : root node of the tree
     */
    private $root;

    /**
     * BinaryTree constructor.
     */
    public function __construct() {
        $this->root = null;
    }

    /**
     * Compares the data in given node to the give data for equality
     * @param $data : data to compare
     * @param $node : Node to compare to
     * @return int : 1 if greater then, 0 if equal to, -1 if less then
     */
    private function compare($data, $node) {
        return (strcmp((string)$node->data, (string)$data));
    }

    /**
     * Inserts a new node into the tree
     * @param $data : data to insert into the tree
     * @return Node : Node inserted into the tree
     */
    public function Insert($data) {
        if($this->root == null) {
            return ($this->root = new Node($data));
        }
        return $this->insertHelper($data, $this->root);
    }

    /**
     * Recursive helper function to inset a new node into the tree
     * @param $data : Data to insert into the tree
     * @param $node : Current node in the tree
     * @return Node : Node inserted into the tree or null
     */
    private function insertHelper($data, $node) {
        $cmp = $this->compare($data, $node);

        switch($cmp) {
            case 1:
                if ($node->right == null) {
                    $newNode = new Node($data);
                    $node->right = $newNode;
                    $newNode->parent = $node;
                    return $newNode;
                }
                else
                    return $this->insertHelper($data, $node->right);

            default:
                if ($node->left == null) {
                    $newNode = new Node($data);
                    $node->left = $newNode;
                    $newNode->parent = $node;
                    return $newNode;
                }
                else
                    return $this->insertHelper($data, $node->left);
        }
    }

    /**
     * Removes a Node from the tree
     * @param $data : Data to remove from the tree
     * @return Node : Node removed from the tree
     */
    public function Remove($data) {
        if($this->root != null)
            return null;
        return $this->removeHelper($data, $this->root);
    }

    /**
     * Recursive helper function to remove a node from the tree
     * @param $data : Data to remove
     * @param $node : Current node in the tree
     * @return Node : Node removed from the tree or null
     */
    private function removeHelper($data, $node) {
        $cmp = $this->compare($data, $node);

        if($cmp == 1)
            return $this->removeHelper($data, $node->right);

        if($cmp == -1)
            return $this->removeHelper($data, $node->left);

        if($cmp == 0) {
            //has no children
            if($node->left == null && $node->right == null) {
                $node = $node->parent;
            }
            $tempNode = null;
            //has one child
            if($node->parent != null) {
                $tempNode = $node->parent;
            } else {   // this is the root node
                if($node->right != null) {
                    $tempNode = $node->right;
                    $tempNode->parent = null;
                    $this->root->right = null;
                    $this->root = $tempNode;
                    return $node;
                } else {
                    $tempNode = $node->left;
                    $tempNode->parent = null;
                    $this->root->left = null;
                    $this->root = $tempNode;
                    return $node;
                }
            }
            if($tempNode->left == $node) {
                if($node->left != null) {
                    $tempNode->left = $node->left;
                    $node->left->parent = $tempNode;
                    return $node;
                }
                else if($node->right != null) {
                    $tempNode->right = $node->right;
                    $node->right->parent = $tempNode;
                    return $node;
                }
            }
            if($tempNode->right == $node) {
                if($node->left != null) {
                    $tempNode->left = $node->left;
                    $node->left->parent = $tempNode;
                    return $node;
                }
                else if($node->right != null) {
                    $tempNode->right = $node->right;
                    $node->right->parent = $tempNode;
                    return $node;
                }
            }
            else if ($node->left != null && $node->right != null) {
                // test for 2 children
                $tempNode = $node->right;

                // find min value of right child tree to replace the node
                while($tempNode->left != null)
                    $tempNode = $tempNode->left;

                $temp = $node->data;
                $node->data = $tempNode->data;
                // replace the node with the tempNode
                $tempNode->data = $temp;
                $tempNode->parent->left = null;
                $tempNode->parent = null;
                return $tempNode;
            }
        }
    }

    /**
     * Returns the smallest node in the tree
     * @return Node : Smallest Node in the tree
     */
    public function GetMin() {
        if($this->root == null)
            return null;

        $node = $this->root;

        while($node->left != null)
            $node = $node->left;

        return $node;
    }

    /**
     * Returns the largest node in the tree
     * @return Node|null: Largest Node in the tree
     */
    public function GetMax() {
        if($this->root == null)
            return null;

        $node = $this->root;

        while($node->right != null)
            $node = $node->right;

        return $node;
    }

    /**
     * Prints out the tree from the given node using Pre Order Traversal
     * @param $node : Node to start the Pre Order Traversal at
     */
    public function PreOrderTraversal($node) {
        if($node != null){
            echo $node->data." ";
            $this->PreOrderTraversal($node->left);
            $this->PreOrderTraversal($node->right);
        }
    }

    /**
     * Prints out the Tree from the given node using Post Order Traversal
     * @param $node : Node to start the Post Order Traversal at
     */
    public function PostOrderTraversal($node) {
        if($node != null) {
            $this->PostOrderTraversal($node->left);
            $this->PostOrderTraversal($node->right);
            echo $node->data." ";
        }
    }

    /**
     * Pints out the tree from the given node using in order Traversal
     * @param $node: Node to start the In Order Traversal at
     */
    public function InOrderedTraversal($node) {
        if($node != null) {
            $this->InOrderedTraversal($node->left);
            echo $node->data." ";
            $this->InOrderedTraversal($node->right);
        }
    }

    /**
     * Prints out the tree from the given node using Depth First Search
     * @param $node: Node to start the Depth First Search at
     */
    public function DepthFirstSearch($node){
        if($node == null)
            return;

        $stack = [];
        array_push($stack,$node);

        while (!empty($stack)) {
            $node = array_pop($stack);
            echo $node->data." ";

            if($node->right != null)
                array_push($stack, $node->right);

            if($node->left != null)
                array_push($stack, $node->left);
        }
    }

    /**
     * Prints out the tree from the given node using breadth first search
     * @param $node : Node to start Breadth First Search at
     */
    public function BreadthFirstSearch($node){
        if($node == null)
            return;

        $queue = [];
        array_push($queue,$node);

        while (count($queue) > 0) {
            $node = array_shift($queue);
            echo $node." ";

            if($node->left != null)
                array_push($queue,$node->left);

            if($node->right != null)
                array_push($queue,$node->right);
        }
    }
}