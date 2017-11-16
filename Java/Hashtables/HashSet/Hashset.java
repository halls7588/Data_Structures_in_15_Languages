/*******************************************************
 *  Hashset.java
 *  Created by Stephen Hall on 11/14/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  Hashset implementation in Java
 ********************************************************/
package Hashtables.HashSet;


/**
 * HashSet class
 * @param <Key>: Generic type
 * @param <Value>: Generic type
 */
public class Hashset<Key, Value> {
    /**
     * Node Class
     */
    public class Node {
        private Key key;
        private Value value;
        private Node next;
        private int hash;

        /**
         * Node Constructor
         * @param key: key of the node
         * @param value: value of the key
         * @param next: next node
         * @param hash: nodes hash
         */
        public Node(Key key, Value value, Node next, int hash){
            this.key = key;
            this.value = value;
            this.next = next;
            this.hash = hash;
        }
    }

    private Node[] nodes;

    /**
     * HashSet Constructor
     * @param size: size of the hash table
     */
    @SuppressWarnings("unchecked")
	public Hashset(int size){
        nodes = new Hashset.Node[size];
    }

    /**
     * Gets the hashed index of the give key
     * @param key: key to find
     * @return int: hashed index of the key
     */
    private int getIndex(Key key){
        int hash = key.hashCode() % nodes.length;
        if(hash < 0){
            hash += nodes.length;
        }
        return hash;
    }

    /**
     * Inserts Key-Value pair into the table or updates new value
     * @param key: key to insert
     * @param value: value of the key
     * @return Value: new value in the set
     */
    public Value insert(Key key, Value value){
        int hash = getIndex(key);
        // check if same key already exists and if so lets update it with the new value
        for(Node node = nodes[hash]; node != null; node = node.next)
            if ((hash == node.hash) && key.equals(node.key))
                return null;

        Node node = new Node(key, value, nodes[hash], hash);
        nodes[hash] = node;
        return value;
    }

    /**
     * Removes the key from the hash table
     * @param key: key to remove
     * @return boolean: success|fail
     */
    public boolean remove(Key key){
        int hash = getIndex(key);
        Node previous = null;
        for(Node node = nodes[hash]; node != null; node = node.next){
            if((hash == node.hash) && key.equals(node.key)){
                if(previous != null)
                    previous.next = node.next;
                else
                    nodes[hash] = node.next;
                return true;
            }
            previous = node;
        }
        return false;
    }

    /**
     * Gets the value of the given key
     * @param key: key to find
     * @return Value: value of the key
     */
    public Value get(Key key){
        int hash = getIndex(key);
        Node node = nodes[hash];

        while (node != null) {
            if(key.equals(node.key))
                return node.value;
            node = node.next;
        }
        return null;
    }

    /**
     * Resize the Hash table
     * @param size: size to make the table
     */
    public void resize(int size){
        Hashset<Key, Value> tbl = new Hashset<Key, Value>(size);
        for(Node node : nodes){
            while (node != null) {
                tbl.insert(node.key, node.value);
                remove(node.key);
                node = node.next;
            }
        }
        nodes = tbl.nodes;
    }
}