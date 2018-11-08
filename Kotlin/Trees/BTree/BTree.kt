/*******************************************************
 * BTree.java
 * Created by Stephen Hall on 11/08/18.
 * Copyright (c) 2018 Stephen Hall. All rights reserved.
 * B-Tree implementation in Kotlin
 *******************************************************/


/**
 * Btree class
 * @param <Key> Generic type
 * @param <Value> Generic type
</Value></Key> */
class BTree<Key : Comparable<Key>, Value> {
    private var root: Node? = null
    private var height: Int = 0
    // number of key-value pairs in the B-tree
    private var pairs: Int = 0

    /**
     * Determines if the tree is empty
     * @return boolean: true|false
     */
    val isEmpty: Boolean
        get() = size() == 0

    // helper B-tree node data type

    /**
     * Node class
     */
    inner class Node
    /**
     * Node Constructor
     * @param size: number of children of the node
     */
    constructor(// number of children
            var size: Int) {
        // array of children
        var children: Array<Entry?>

        init {
            children = arrayOfNulls<Entry?>(Max)
        }
    }

    /**
     * Helper class for BTree Node class
     */
    inner class Entry
    /**
     * Helper class Constructor
     * @param key: key to hold
     * @param val: value of the key
     * @param next: next node in the tree
     */
    (var key: Key, var `val`: Value?, var next: Node?)

    /**
     * B-tree constructor
     */
    init {
        root = Node(0)
    }

    /**
     * Returns the number number of pars in the tree
     * @return int: the number of key-value pairs in the tree
     */
    fun size(): Int {
        return pairs
    }

    /**
     * Gets the height of the tree
     * @return int: height of the tree
     */
    fun height(): Int {
        return height
    }

    /**
     * Gets the value of the given key
     * @param key: key to find value of
     * @return Value: value of the given key or null
     */
    operator fun get(key: Key?): Value? {
        return if (key == null) null else search(root!!, key, height)
    }

    /**
     * Searches the tree for the key
     * @param node: node to start at
     * @param key: key to find
     * @param ht: height of the tree
     * @return Value: Value of the key or null
     */
    private fun search(node: Node?, key: Key, ht: Int): Value? {
        val children = node!!.children
        // external node
        if (ht == 0) {
            for (j in 0 until node.size) {
                if (equalTo(key, children[j]!!.key))
                    return children[j]!!.`val`
            }
        } else {
            for (j in 0 until node.size) {
                if (j + 1 == node.size || lessThan(key, children[j + 1]!!.key))
                    return search(children[j]!!.next, key, ht - 1)
            }
        }// internal node
        return null
    }


    /**
     * Adds a node into the tree or replaces value
     * @param key: key of the node
     * @param val: value of the key
     */
    fun put(key: Key?, `val`: Value) {
        if (key == null)
            return
        val node = insert(root, key, `val`, height)
        pairs++
        if (node == null)
            return
        // need to split root
        val tmp = Node(2)
        tmp.children[0] = Entry(root!!.children[0]!!.key, null, root)
        tmp.children[1] = Entry(node.children[0]!!.key, null, node)
        root = tmp
        height++
    }

    /**
     * Inserts a node into the tree and updates the tree
     * @param node: root to start at
     * @param key: key of the new node
     * @param val: value of the key
     * @param ht: current height
     * @return Node: node added into the tree
     */
    private fun insert(node: Node?, key: Key, `val`: Value, ht: Int): Node? {
        var j: Int
        val entry = Entry(key, `val`, null)
        // external node
        if (ht == 0) {
            j = 0
            while (j < node!!.size) {
                if (lessThan(key, node.children[j]!!.key))
                    break
                j++
            }
        } else {
            j = 0
            while (j < node!!.size) {
                if (j + 1 == node.size || lessThan(key, node.children[j + 1]!!.key)) {
                    val tmp = insert(node.children[j++]!!.next, key, `val`, ht - 1) ?: return null

                    entry.key = tmp.children[0]!!.key
                    entry.next = tmp
                    break
                }
                j++
            }
        }// internal node

        System.arraycopy(node.children, j, node.children, j + 1, node.size - j)

        node.children[j] = entry
        node.size++

        return if (node.size < Max)
            null
        else
            split(node)
    }

    /**
     * Splits the given node in half
     * @param node: node to split
     * @return Node: split node
     */
    private fun split(node: Node): Node {
        val tmp = Node(Max / 2)
        node.size = Max / 2
        System.arraycopy(node.children, 2, tmp.children, 0, Max / 2)
        return tmp
    }

    /**
     * Determines if a is less than b
     * @param a: generic type to test
     * @param b: generic type to test
     * @return boolean: true|false
     */
    private fun lessThan(a: Key, b: Key): Boolean {
        return a.compareTo(b) < 0
    }

    /**
     * Determines if a is equal to b
     * @param a: generic type to test
     * @param b: generic type to test
     * @return boolean: true|false
     */
    private fun equalTo(a: Key, b: Key): Boolean {
        return a.compareTo(b) == 0
    }

    companion object {
        // max children per B-tree node = M-1
        private val Max = 4
    }
}