/*******************************************************
 *  AssociativeArray.js
 *  Created by Stephen Hall on 11/15/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  Associative Array implementation in JavaScript
 *  Update Date      Name            Description
 *  1      10/28/19  Stephen Hall    Updated to class syntax
 ********************************************************/

/**
 * AssociativeArray class
 */
class AssociativeArray {
  /**
   * AssociativeArray class Constructor
   * @param {int} size
   * @constructor
   */
  constructor(size) {
    /**
     * Node class for associative array
     * @param key: key to the node
     * @param value: value of the node
     * @param hash: hash of the node
     * @constructor
     */
    this.Node = class {
      constructor(key, value, hash) {
        this.key = key;
        this.value = value;
        this.hash = hash;
      }
    };

    if (size !== undefined && size !== null && size > 0) this.size = size;
    else this.size = 4;
    this.table = [];
    this.count = 0;
  }

  /**
   * Helper method: copies Javas hash code except doesn't allow negative values
   * @param {*} str: string to be hashed
   * @returns {int}: hash of the string
   */
  _hashCode(str) {
    var hash = 0;
    if (str.length === 0) return hash;
    for (var i = 0; i < str.length; i++) {
      var char = str.charCodeAt(i);
      hash = (hash << 5) - hash + char;
      hash = hash & hash; // Convert to 32bit integer
    }
    return Math.abs(hash);
  }

  /**
   * Adds or updates Key, Value pair into the Array
   * @param {*}: Key to associate vale with
   * @param {*}: value to store
   * @returns {Node}: Node added or updated
   */
  set(key, value) {
    var hash = this._hashCode(key.toString());
    var bucket = this._getBucket(hash);
    var entry;
    if (this.isEmpty()) {
      entry = new this.Node(key, value, hash);
      this.table[bucket] = entry;
      this.count++;
    } else {
      entry = this.table[bucket];
      if (!entry) {
        var node = new this.Node(key, value, hash);
        this.table[bucket] = node;
        this.count++;
        entry = node;
      } else {
        while (entry.next !== null) {
          if (this._hashCode(entry) === hash && entry.key === key) {
            entry.value = value;
            return entry;
          }
          entry = entry.next;
        }

        var node = new this.Node(key, value, hash);
        entry.next = node;
        this.count++;
        entry = node;
      }
    }
    return entry;
  }

  /**
   *  Gets the value of the given key
   * @param {*} key
   * @returns {*}: value of the key
   */
  get(key) {
    var hash = this._hashCode(key.toString());
    var bucket = this._getBucket(hash);

    var entry = this.table[bucket];
    while (entry !== null) {
      /* If hash and key matches, return the value */
      if (entry.hash === hash && entry.key === key) {
        return entry.value;
      }
      entry = entry.next;
    }
    return null;
  }

  /**
   * Gets the size of the array
   * @returns {int}
   */
  size() {
    return this.size;
  }

  /**
   * Checks if the array is empty of not
   * @returns {boolean}: true|false
   */
  isEmpty() {
    return this.count === 0;
  }

  /**
   * Gets the bucket index of the hash
   * @param {int} hash
   * @returns {int}
   */
  _getBucket(hash) {
    return hash % this.size;
  }
}

// Tests

let arr = new AssociativeArray(5);
arr.set("taco", "bell");
console.log(arr.get("taco"));
arr.set("king", "kong");
console.log(arr.get("king"));
arr.set("life", 42);
console.log(arr.get("life"));
arr.set(24, 72);
console.log(arr.get(24));
