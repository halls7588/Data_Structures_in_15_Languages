/*******************************************************
 * SplayTree.kt
 * Created by Stephen Hall on 11/08/18.
 * Copyright (c) 2018 Stephen Hall. All rights reserved.
 * Splay Tree implementation in Kotlin
 ******************************************************/

/**
 * Splay Tree Class
 * @param <Key> generic type
 * @param <Value> generic type
</Value></Key> */
class SplayTree<Key : Comparable<Key>, Value> {

    private var root: Node? = null   // root of the BST

    /**
     * Node class for splay tree
     */
    inner class Node
    /**
     * Node Constructor
     * @param key: key to the node
     * @param value: value of the node
     */
    (
            /**
             * Data members of the node
             */
             var key: Key, var value: Value?) {
             var left: Node? = null
             var right: Node? = null
    }

    /**
     * Tests to see if a key exist in the tree
     * @param key: key to test
     * @return boolean: yes|no
     */
    operator fun contains(key: Key): Boolean {
        return get(key) != null
    }

    /**
     * Gets the value of the key if it exists
     * @param key: key to test
     * @return Value: value of the key of null
     */
    operator fun get(key: Key): Value? {
        root = splay(root, key)
        return if (key.compareTo(root!!.key) == 0) root!!.value else null
    }

    /**
     * adds or updates an item into the tree
     * @param key: key to insert
     * @param value: value of the key
     */
    fun put(key: Key, value: Value) {
        // splay key to root
        if (root == null) {
            root = Node(key, value)
            return
        }
        root = splay(root, key)
        val tmp = key.compareTo(root!!.key)
        // Insert new node at root
        if (tmp < 0) {
            val node = Node(key, value)
            node.left = root!!.left
            node.right = root
            root!!.left = null
            root = node
        } else if (tmp > 0) {
            val node = Node(key, value)
            node.right = root!!.right
            node.left = root
            root!!.right = null
            root = node
        } else {
            root!!.value = value
        }// Duplicate key. Update value
        // Insert new node at root

    }

    /**
     * Removes a node from the tree
     * @param key: key to remove
     */
    fun remove(key: Key) {
        if (root == null)
            return

        root = splay(root, key)

        val tmp = key.compareTo(root!!.key)

        if (tmp == 0) {
            if (root!!.left == null) {
                root = root!!.right
            } else {
                val node = root!!.right
                root = root!!.left
                splay(root, key)
                root!!.right = node
            }
        }
    }

    /**
     * Rotates the given node to the right
     * @param node: node to rotate
     * @return Node: new root of the rotate
     */
    private fun rotateRight(node: Node): Node {
        val tmp = node.left
        node.left = tmp!!.right
        tmp.right = node
        return tmp
    }

    /**
     * Rotates the node to the left
     * @param node: node to rotate
     * @return Node: new root of the rotate
     */
    private fun rotateLeft(node: Node): Node {
        val tmp = node.right
        node.right = tmp!!.left
        tmp.left = node
        return tmp
    }

    /**
     * Splays the node containing key to the root node given
     * @param node: root to splay to
     * @param key: key to find
     * @return Node: Node found or null
     */
    private fun splay(node: Node?, key: Key): Node? {
        var node: Node? = node ?: return null

        val tmp = key.compareTo(node!!.key)

        if (tmp < 0) {
            // key not in tree, so we're done
            if (node.left == null) {
                return node
            }
            val tmp2 = key.compareTo(node.left!!.key)
            if (tmp2 < 0) {
                node.left!!.left = splay(node.left!!.left, key)
                node = rotateRight(node)
            } else if (tmp2 > 0) {
                node.left!!.right = splay(node.left!!.right, key)
                if (node.left!!.right != null)
                    node.left = rotateLeft(node.left!!)
            }

            return if (node.left == null)
                node
            else
                rotateRight(node)
        } else if (tmp > 0) {
            // key not in tree, so we're done
            if (node.right == null) {
                return node
            }
            val tmp2 = key.compareTo(node.right!!.key)
            if (tmp2 < 0) {
                node.right!!.left = splay(node.right!!.left, key)

                if (node.right!!.left != null)
                    node.right = rotateRight(node.right!!)
            } else if (tmp2 > 0) {
                node.right!!.right = splay(node.right!!.right, key)
                node = rotateLeft(node)
            }
            return if (node.right == null)
                node
            else
                rotateLeft(node)
        } else
            return node
    }

    /**
     * Gets the height of the tree
     * @return int: height of the tree
     */
    fun height(): Int {
        return height(root)
    }

    /**
     * Gets the height of the given node
     * @param node: node to test
     * @return int: height of the node
     */
    private fun height(node: Node?): Int {
        return if (node == null) -1 else Math.max(height(node.left), height(node.right)) + 1
    }

    /**
     * Gets the size of the tree
     * @return int: size of the tree
     */
    fun size(): Int {
        return size(root)
    }

    /**
     * Gets the size of the given node
     * @param node: nose to test
     * @return int: size of the node
     */
    private fun size(node: Node?): Int {
        return if (node == null) 0 else size(node.left) + size(node.right) + 1
    }
}