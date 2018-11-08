/*******************************************************
 * SelfBalancingBinaryTree.kt
 * Created by Stephen Hall on 11/08/18.
 * Copyright (c) 2018 Stephen Hall. All rights reserved.
 * SelfBalancingBinaryTree implementation in Kotlin
 *******************************************************/

/**
 * Self Balancing Binary Tree Class
 * @param <T> Generic Type</T>
 */
class SelfBalancingBinaryTree<T : Comparable<T>> {

    private var root: Node? = null
    private var count: Int = 0
    /**
     * check if tree is empty
     * @return boolean: true|false
     */
    val isEmpty: Boolean
        get() = root == null

    /**
     * Node class for SelfBalancingBinaryTree
     */
    inner class Node {
        var left: Node? = null
        var right: Node? = null
        var data: T? = null
        var height: Int = 0

        /**
         * Node Constructor
         */
        constructor() {
            left = null
            right = null
            data = null
            height = 0
        }

        /**
         * Node Constructor
         */
        constructor(n: T) {
            left = null
            right = null
            data = n
            height = 0
        }
    }

    /**
     * SelfBalancingBinaryTree Constructor
     */
    init {
        root = null
        count = 0
    }

    /**
     * Insert data into the tree
     * @param data: data to insert
     */
    fun insert(data: T) {
        root = insert(data, root)
    }

    /**
     * Inserts data into the tree
     * @param data: data to insert
     * @param node: node to start at
     * @return Node: new root
     */
    private fun insert(data: T, node: Node?): Node {
        var node = node
        if (node == null)
            node = Node(data)
        else if (lessThan(data, node.data)) {
            node.left = insert(data, node.left)
            if (height(node.left) - height(node.right) == 2)
                if (lessThan(data, node.left!!.data))
                    node = rotateLeft(node)
                else
                    node = doubleLeft(node)
        } else if (greaterThan(data, node.data)) {
            node.right = insert(data, node.right)
            if (height(node.right) - height(node.left) == 2)
                if (greaterThan(data, node.right!!.data))
                    node = rotateRight(node)
                else
                    node = doubleRight(node)
        }
        node.height = max(height(node.left), height(node.right)) + 1
        count++
        return node
    }

    /**
     * rotates the given root node left
     * @param node: node to rotate
     * @return Node: new root
     */
    private fun rotateLeft(node: Node): Node {
        val tmp = node.left
        node.left = tmp!!.right
        tmp.right = node
        node.height = max(height(node.left), height(node.right)) + 1
        tmp.height = max(height(tmp.left), node.height) + 1
        return tmp
    }

    /**
     * Rotates the given root node right
     * @param node: node to rotate
     * @return Node: new root
     */
    private fun rotateRight(node: Node): Node {
        val tmp = node.right
        node.right = tmp!!.left
        tmp.left = node
        node.height = max(height(node.left), height(node.right)) + 1
        tmp.height = max(height(tmp.right), node.height) + 1
        return tmp
    }

    /**
     * Rotates the left child right than the root node left
     * @param node: root node to rotate
     * @return Node: new root
     */
    private fun doubleLeft(node: Node): Node {
        node.left = rotateRight(node.left!!)
        return rotateLeft(node)
    }

    /**
     * rotates the right child left than the root right
     * @param node: node to rotate
     * @return Node: new root
     */
    private fun doubleRight(node: Node): Node {
        node.right = rotateLeft(node.right!!)
        return rotateRight(node)
    }

    /**
     * Gets the size of the tree
     * @return int: number of node in the tree
     */
    fun size(): Int {
        return count
    }

    /**
     * Gets the height of the node
     * @param node: node to test
     * @return int: height of the node
     */
    private fun height(node: Node?): Int {
        return node?.height ?: -1
    }

    /**
     * Function to max of left/right node
     * @param left:
     * @param right:
     * @return int: max
     */
    private fun max(left: Int, right: Int): Int {
        return if (left > right) left else right
    }

    /**
     * Clears the data from the tree
     */
    fun clear() {
        root = null
    }

    /**
     * Determines if a is less than b
     * @param a: generic type to test
     * @param b: generic type to test
     * @return boolean: true|false
     */
    private fun lessThan(a: T, b: T?): Boolean {
        return a.compareTo(b!!) < 0
    }

    /**
     * Determines if a is greater than b
     * @param a: generic type to test
     * @param b: generic type to test
     * @return boolean: true|false
     */
    private fun greaterThan(a: T, b: T?): Boolean {
        return a.compareTo(b!!) > 0
    }

    //TODO: add search and remove
}