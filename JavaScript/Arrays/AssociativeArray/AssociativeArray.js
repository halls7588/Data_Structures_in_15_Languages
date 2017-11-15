/*******************************************************
 *  AssociativeArray.js
 *  Created by Stephen Hall on 11/15/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  Associative Array implementation in JavaScript
 ********************************************************/

/**
 * Helper method: copies Javas hash code except doesn't allow negative values
 * @returns {number}: hash of the string
 */
String.prototype.hashCode = function(){
    var hash = 0;
    if (this.key.length === 0)
        return hash;
    for (var i = 0; i < this.length; i++) {
        var char = this.key.charCodeAt(i);
        hash = ((hash << 5) - hash) + char;
        hash = hash & hash; // Convert to 32bit integer
    }
    return Math.abs(hash);
};

/**
 * AssociativeArray class Constructor
 * @constructor
 */
function AssociativeArray(size) {
    if(size !== undefined && size !== null && size > 0)
        this.size = size;
    else
        this.size = 4;
    this.table = new Node[size];
}

/**
 * Node class for associative array
 * @param key: key to the node
 * @param value: value of the node
 * @param hash: hash of the node
 * @constructor
 */
function Node(key, value, hash) {
    this.key = key;
    this.value = value;
    this.hash = hash;
}

/**
 * Adds or updates Key, Value pair into the Array
 * @param key: Key to associate vale with
 * @param value: value to store
 * @returns {*}: Node added or updated
 */
AssociativeArray.prototype.set = function(key, value) {
    var hash = key.hashCode();
    var bucket = this.getBucket(hash);
    var entry;
    if(this.isEmpty()){
        entry = new Node(key, value, hash);
        this.table[bucket] = entry;
        this.size++;
    }
    else {
        entry = this.table[bucket];
        while (entry.next !== null) {
            if (entry.hashCode() === hash && entry.key === key) {
                entry.value = value;
                return entry;
            }
            entry = entry.next;
        }

        var node = new Node(key, value, hash);
        entry.next = node;
        this.size++;
        entry = node;
    }
    return entry;
};

/**
 * Gets the value of the given key
 * @param key: Key to get value of
 * @returns {*}: value of the key
 */
AssociativeArray.prototype.get = function(key) {
    var hash = key.hashCode();
    var bucket = this.getBucket(hash);

    var entry = this.table[bucket];
    while (entry !== null) {
        /* If hash and key matches, return the value */
        if (entry.hash === hash && entry.key === key) {
            return entry.value;
        }
        entry = entry.next;
    }
    return null;
};

/**
 * Gets the size of the array
 * @returns {AssociativeArray.size|*}: number of elements in the array
 */
AssociativeArray.prototype.size = function() {
    return this.size;
};

/**
 * Checks if the array is empty of not
 * @returns {boolean}: true|false
 */
AssociativeArray.prototype.isEmpty = function() {
    return size === 0;
};


AssociativeArray.prototype.getBucket = function(hash) {
    return (hash % this.table.length);
};
