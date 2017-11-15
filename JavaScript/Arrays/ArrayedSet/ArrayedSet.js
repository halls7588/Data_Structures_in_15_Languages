/*******************************************************
 *  ArrayedSet.js
 *  Created by Stephen Hall on 11/15/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  Arrayed Set implementation in JavaScript
 ********************************************************/

/**
 * Arrayed Set object
 * @param size: size to initialize to
 * @constructor
 */
function ArrayedSet(size) {
    if(size !== undefined && size !== null && size > 0)
        this.size = size;
    else
        this.size = 4;

    this.array = [this.size];
    this.count = 0;

    function resize() {
        this.size *= 2;
    }
}

/**
 * Adds data into the array
 * @param data: data to add
 * @returns {*}; data added or null if already exists
 */
ArrayedSet.prototype.add = function(data) {
    if(data === null || data === undefined)
        return null;

    if(this.contains(data))
        return null;

    if(this.count === this.size)
        resize();

    this.array[this.count] = data;
    this.count++;
    return data;
};

/**
 * Appends the contents of an array to the ArrayedSet
 * @param data: array to append
 * @returns {boolean} success|fail
 */
ArrayedSet.prototype.append = function(data){
    if(data !== null){
        for (var i = 0; i < data.length; i++) {
            if(data[i] !== null )
                this.add(data[i]);
        }
        return true;
    }
    return false;
};

/**
 * Sets the data at the given index
 * @param index: index to set
 * @param data: data to set index to
 * @returns {boolean}: success|fail
 */
ArrayedSet.prototype.set = function(index, data) {
    if(!this.contains(data)){
        if(index >= 0 && index < this.size) {
            this.array[index] = data;
            return true;
        }
    }
    return false;
};

/**
 * Gets the data at the arrays given index
 * @param index: Index to get data at
 * @returns {*}: Data at the given index or default value of T if index does not exist
 */
ArrayedSet.prototype.get = function(index) {
    if(index >= 0 && index < this.size)
        return this.array[index];
    return null;
};

/**
 * Removes the data at arrays given index
 * @param index: Index to remove
 * @returns {*}: Data removed from the array or null
 */
ArrayedSet.prototype.remove = function(index) {
    if (index === undefined || index === null || index < 0 || index > this.count)
        return null;

    var tmp = this.array[index];
    this.array[index] = null;
    this.count--;
    return tmp;
};

/**
 * Resets the internal array to default size with no data
 */
ArrayedSet.prototype.reset = function() {
    this.count = 0;
    this.size = 4;
    this.array = [this.size];
};

/**
 * Clears all data in the array leaving size intact
 */
ArrayedSet.prototype.clear = function() {
    for(var i = 0; i < this.count; i++){
        this.array[i] = null;
    }
    this.count = 0;
};

/**
 * Tests to see if the data exist in the list
 * @param data: data to find
 * @returns {boolean}: true|false
 */
ArrayedSet.prototype.contains = function(data) {
    if(data !== undefined && data !== null)
    for(var i = 0; i < this.size; i++){
        if(this.array[i] === data)
            return true;
    }
    return false;
};

/**
 * Gets the current count of the array
 * @returns {ArrayedSet.count|*}: umber of items in the array
 */
ArrayedSet.prototype.count = function() {
    return this.count;
};