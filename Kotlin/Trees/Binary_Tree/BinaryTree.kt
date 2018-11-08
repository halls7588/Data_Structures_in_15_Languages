/*******************************************************
 * BinaryTree.kt
 * Created by Stephen Hall on 11/08/18.
 * Copyright (c) 2018 Stephen Hall. All rights reserved.
 * A Binary Tree implementation in Java
 *******************************************************/

import java.util.PriorityQueue
import java.util.Stack

/**
 * Binary Tree Class
 * @param <T> Generic Type
</T> */
class BinaryTree<T : Comparable<T?>> {

    /**
     * Private members
     */
    private var root: Node? = null

    /**
     * Node class for the binary tree
     */
    inner class Node
    /**
     * Node class constructor
     * @param data: data of the node
     */
    (var data: T) {
        /**
         * private members
         */
        var left: Node?
        var right: Node? = null
        var parent: Node? = null

        init {
            right = null
            left = right
        }
    }

    /**
     * Binary tree class constructor
     */
    init {
        root = null
    }

    /**
     * Return the largest node in the tree
     * @return Largest Node in the tree
     */
    val max: Node?
        get() {
            if (root == null)
                return null

            var node = root

            while (node!!.right != null)
                node = node.right

            return node
        }


    /**
     * Returns the smallest node in the tree
     * @return Smallest Node int he tree
     */
    val min: Node?
        get() {
            if (root == null)
                return null

            var node = root

            while (node!!.left != null)
                node = node.left

            return node
        }

    /**
     * Compares the data in given node to the give data for equality
     * @param data data to compare
     * @param node Node to compare to
     * @return 1 if greater then, 0 if equal to, -1 if less then
     */
    private fun compare(data: T, node: Node): Int {
        return node.data.compareTo(data)
    }

    /**
     * Inserts a new node into the tree
     * @param data data to insert into the tree
     * @return Node inserted into the tree
     */
    fun insert(data: T): Node? {
        return if (root == null) {
            root = Node(data)
            root
        }
        else
            insertHelper(data, root)
    }

    /**
     * Recursive helper function to insert a new node into the tree
     * @param data Data to insert into the tree
     * @param node Current node in the tree
     * @return Node inserted into the tree
     */
    private fun insertHelper(data: T, node: Node?): Node? {
        try {
            val cmp = compare(data, node!!)
            when (cmp) {
                1 -> if (node.right == null) {
                    val newNode = Node(data)
                    node.right = newNode
                    newNode.parent = node
                    return newNode
                } else
                    return insertHelper(data, node.right)
                else -> if (node.left == null) {
                    val newNode = Node(data)
                    node.left = newNode
                    newNode.parent = node
                    return newNode
                } else
                    return insertHelper(data, node.left)
            }
        } catch (e: Exception) {
            return null
        }

    }

    /**
     * Removes a Node from the tree
     * @param data Data to remove from the tree
     * @return Node removed from the tree
     */
    fun remove(data: T): Node? {
        return if (root == null) null else removeHelper(data, root)
    }

    /**
     * Recursive helper function to remove a node from the tree
     * @param data Data to remove
     * @param node Current node
     * @return Node removed from the tree
     */
    private fun removeHelper(data: T, node: Node?): Node? {
        var node = node
        try {
            val cmp = compare(data, node!!)
            if (cmp == 1)
                return removeHelper(data, node.right)
            if (cmp == -1)
                return removeHelper(data, node.left)
            if (cmp == 0) {
                //has no children
                var tempNode: Node?

                if (node.left == null && node.right == null) {
                    node = node.parent
                }

                //has one child
                if (node!!.parent != null) {
                    tempNode = node.parent
                } else { // this is the root node
                    if (node.right != null) {
                        tempNode = node.right
                        tempNode!!.parent = null
                        root!!.right = null
                        root = tempNode
                        return node
                    } else {
                        tempNode = node.left
                        tempNode!!.parent = null
                        root!!.left = null
                        root = tempNode
                        return node
                    }
                }
                if (tempNode!!.left === node) {
                    if (node.left != null) {
                        tempNode!!.left = node.left
                        node.left!!.parent = tempNode
                        return node
                    } else if (node.right != null) {
                        tempNode!!.right = node.right
                        node.right!!.parent = tempNode
                        return node
                    }
                }
                if (tempNode!!.right === node) {
                    if (node.left != null) {
                        tempNode!!.left = node.left
                        node.left!!.parent = tempNode
                        return node
                    } else if (node.right != null) {
                        tempNode!!.right = node.right
                        node.right!!.parent = tempNode
                        return node
                    }
                } else if (node.left != null && node.right != null) { // test for 2 children
                    tempNode = node.right // find min value of right child tree to replace the node
                    while (tempNode!!.left != null)
                        tempNode = tempNode.left
                    val temp = node.data
                    node.data = tempNode.data // replace the node with the tempNode
                    tempNode.data = temp
                    tempNode.parent!!.left = null
                    tempNode.parent = null
                    return tempNode
                }
            }
        } catch (e: Exception) {
            return null
        }

        return null
    }

    /**
     * Prints out the tree using Pre-Order Traversal
     * @param node Node to start the Pre-Order Traversal at
     */
    fun preOrederTraversal(node: Node?) {
        if (node != null) {
            println(node.data)
            preOrederTraversal(node.left)
            preOrederTraversal(node.right)
        }
    }

    /**
     * Prints out the Tree using Post Order Traversal
     * @param node Node to start the Post Order Traversal at
     */
    fun postPrderTraversal(node: Node?) {
        if (node != null) {
            postPrderTraversal(node.left)
            postPrderTraversal(node.right)
            println(node.data)
        }
    }

    /**
     * Pints out the tree using in order Traversal
     * @param node Node to start the In Order Traversal at
     */
    fun inOrderedTraversal(node: Node?) {
        if (node != null) {
            inOrderedTraversal(node.left)
            println(node.data)
            inOrderedTraversal(node.right)
        }
    }

    /**
     * Prints out the tree using Depth First Search
     * @param node Node to start the Depth First Search at
     */
    fun depthFirstSearch(node: Node?) {
        var node: Node? = node ?: return

        val stack = Stack<Node>()
        stack.push(node)

        while (!stack.empty()) {
            node = stack.pop()
            println(node)
            if (node!!.right != null)
                stack.push(node.right)
            if (node.left != null)
                stack.push(node.left)
        }
    }

    /**
     * Prints out the tree using breadth first search
     * @param node Node to start Breadth First Search at
     */
    fun breadthFirstSearch(node: Node?) {
        var node: Node? = node ?: return

        val queue = PriorityQueue<Node>()
        queue.add(node)

        while (queue.size > 0) {
            node = queue.remove()
            println(node)
            if (node!!.left != null)
                queue.add(node.left)
            if (node.right != null)
                queue.add(node.right)
        }
    }
}
