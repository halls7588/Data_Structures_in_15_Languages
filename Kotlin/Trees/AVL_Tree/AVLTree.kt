/*******************************************************
 * AvlTree.kt
 * Created by Stephen Hall on 11/08/18.
 * Copyright (c) 2018 Stephen Hall. All rights reserved.
 * AVL Tree implementation in kotlin
 ******************************************************/

class AVLTree<T : Comparable<T>> {

    private var root: Node? = null
    private var count: Int = 0

    /**
     * Determine if the tree is empty.
     * @return True if the tree is empty
     */
    val isEmpty: Boolean
        get() = root == null

    /**
     * Node class for AVL Tree
     */
    inner class Node
    /**
     * Node constructor with left and right leafs
     * @param data: data to hold
     * @param left: left child
     * @param right: right child
     */
    @JvmOverloads constructor(var data: T, var left: Node? = null, var right: Node? = null) {
        var height: Int = 0
    }
    /**
     * Empty Node constructor
     * @param data: data for the node
     */
    /**
     * AVLTree Constructor.
     */
    init {
        root = null
        count = 0
    }

    /**
     * Gets the height of a node
     * @param node: node to test
     * @return int: height of the node
     */
    fun height(node: Node?): Int {
        return node?.height ?: -1
    }

    /**
     * Find the max value among the given numbers.
     * @param a First number
     * @param b Second number
     * @return Maximum value
     */
    fun max(a: Int, b: Int): Int {
        return if (a > b) a else b
    }

    /**
     * Insert an element into the tree.
     * @param data: data to insert into the tree
     * @return boolean: success|fail
     */
    fun insert(data: T): Boolean {
        try {
            root = insert(data, root)
            count++
            return true
        } catch (e: Exception) {
            return false
        }

    }

    /**
     * Private Insert Helper
     * @param data: data to add
     * @param node Root of the tree
     * @return Node: New root of the tree
     * @throws Exception: failure or duplicate value
     */
    @Throws(Exception::class)
    private fun insert(data: T, node: Node?): Node {
        var node = node
        if (node == null)
            node = Node(data)
        else if (data.compareTo(node.data) < 0) {
            node.left = insert(data, node.left)
            if (height(node.left) - height(node.right) == 2) {
                if (data.compareTo(node.left!!.data) < 0) {
                    node = rotateLeft(node)
                } else {
                    node = rotateRightLeft(node)
                }
            }
        } else if (data.compareTo(node.data) > 0) {
            node.right = insert(data, node.right)

            if (height(node.right) - height(node.left) == 2)
                if (data.compareTo(node.right!!.data) > 0) {
                    node = rotateRight(node)
                } else {
                    node = rotateLeftRight(node)
                }
        } else {
            throw Exception("Attempting to insert duplicate value")
        }
        node.height = max(height(node.left), height(node.right)) + 1
        return node
    }

    /**
     * Rotates Node left
     * @param node: node to rotate
     * @return Node: new root node
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
     * Rotate left child Right then rotate left
     * @param node: node to rotate
     * @return Node: new root node
     */
    private fun rotateRightLeft(node: Node): Node {
        node.left = rotateRight(node.left!!)
        return rotateLeft(node)
    }

    /**
     * Rotate Right
     * @param node: node to rotate
     * @return Node: new root node
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
     * Rotate right child left then rotate right
     * @param node: node to rotate
     * @return Node: new root node
     */
    private fun rotateLeftRight(node: Node): Node {
        node.right = rotateLeft(node.right!!)
        return rotateRight(node)
    }

    /**
     * Deletes all nodes from the tree.
     */
    fun makeEmpty() {
        root = null
    }

    /**
     * Find the smallest item in the tree.
     * @return T: smallest item or null if empty.
     */
    fun findMin(): T? {
        return if (isEmpty) null else findMin(root)!!.data
    }

    /**
     * Find the largest item in the tree.
     * @return T: the largest item or null if empty.
     */
    fun findMax(): T? {
        return if (isEmpty) null else findMax(root)!!.data
    }

    /**
     * Find min helper
     * @param node: root to test
     * @return Node: node containing the smallest item.
     */
    private fun findMin(node: Node?): Node? {
        var node: Node? = node ?: return null

        while (node!!.left != null)
            node = node.left

        return node
    }

    /**
     * Find max helper
     * @param node: root to test.
     * @return Node: node containing the largest item.
     */
    private fun findMax(node: Node?): Node? {
        var node: Node? = node ?: return null

        while (node!!.right != null)
            node = node.right

        return node
    }

    /**
     * Remove item from the tree.
     * @param data: item to remove.
     */
    fun remove(data: T) {
        root = remove(data, root)
    }

    /**
     * Remove helper function
     * @param data: data to remove
     * @param node: root to start at
     * @return Node: new root node
     */
    private fun remove(data: T, node: Node?): Node? {
        var node: Node? = node ?: return null

        if (data.compareTo(node!!.data) < 0) {
            node.left = remove(data, node.left)
            val l = if (node.left != null) node.left!!.height else 0

            if (node.right != null && node.right!!.height - l >= 2) {
                val rightHeight = if (node.right!!.right != null) node.right!!.right!!.height else 0
                val leftHeight = if (node.right!!.left != null) node.right!!.left!!.height else 0

                if (rightHeight >= leftHeight)
                    node = rotateLeft(node)
                else
                    node = rotateLeftRight(node)
            }
        } else if (data.compareTo(node.data) > 0) {
            node.right = remove(data, node.right)
            val r = if (node.right != null) node.right!!.height else 0
            if (node.left != null && node.left!!.height - r >= 2) {
                val leftHeight = if (node.left!!.left != null) node.left!!.left!!.height else 0
                val rightHeight = if (node.left!!.right != null) node.left!!.right!!.height else 0
                if (leftHeight >= rightHeight)
                    node = rotateRight(node)
                else
                    node = rotateRightLeft(node)
            }
        } else if (node.left != null) {
            node.data = findMax(node.left)!!.data
            remove(node.data, node.left)

            if (node.right != null && node.right!!.height - node.left!!.height >= 2) {
                val rightHeight = if (node.right!!.right != null) node.right!!.right!!.height else 0
                val leftHeight = if (node.right!!.left != null) node.right!!.left!!.height else 0

                if (rightHeight >= leftHeight)
                    node = rotateLeft(node)
                else
                    node = rotateLeftRight(node)
            }
        } else
            node = node.right

        if (node != null) {
            val leftHeight = if (node.left != null) node.left!!.height else 0
            val rightHeight = if (node.right != null) node.right!!.height else 0
            node.height = max(leftHeight, rightHeight) + 1
        }
        return node
    }

    /**
     * Determines is the data exists in the tree
     * @param data: data to find
     * @return boolean: success|fail
     */
    operator fun contains(data: T): Boolean {
        return contains(data, root)
    }

    /**
     * Contains helper method
     * @param data: data to find
     * @param node: node to test
     * @return boolean: success|fail
     */
    private fun contains(data: T, node: Node?): Boolean {
        if (node == null)
            return false // The node was not found
        else if (data.compareTo(node.data) < 0)
            return contains(data, node.left)
        else if (data.compareTo(node.data) > 0)
            return contains(data, node.right)

        return true
    }
}
