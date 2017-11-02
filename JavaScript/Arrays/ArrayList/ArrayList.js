/*******************************************************
 *  ArrayList.js
 *  Created by Stephen Hall on 11/02/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  ArrayList implementation in JavaScript
 ********************************************************/

/**
 * Creates an instance of ArrayList
 * @param size: optional variable to set the size of the list
 * @constructor
 */
function ArrayList(size){
    if(size === undefined) {
        this.arr = [2];
        this.maxSize = 2;
        this.count = 0;
    }else {
        this.arr = [size];
        this.maxSize = size;
        this.count = 0;
    }

    /**
     * private method
     * doubles the size of the internal array
     */
    this.resize = function(){
        maxSize *= 2;
        var tmp = [maxSize];
        for(var i = 0; i < this.count; i++){
            tmp[i] = this.arr[i];
        }
        this.arr = tmp;
    };

    /**
     * private method
     * checks if x is an integer number
     * @param x: variable to check
     * @returns {boolean}
     */
    this.isInteger = function(x) {
        if(x === undefined || x === null)
            return false;

        return x % 1 === 0;
    };
}

/**
 * Adds the given data to the list
 * @param data: data to add to the ArrayList
 * @returns Mixed|null
 */
ArrayList.prototype.add = function(data){
    if(data === undefined || data === null)
        return null;

    if(this.count === maxSize)
        this.resize();

    this.arr[this.count] = data;
    this.count++;

    return data;
};

/**
 * Appends the given data to the ArrayList
 * @param array
 * @returns {boolean}
 */
ArrayList.prototype.append = function(array){
    if(array !== undefined && array !== null){
        if(!Array.isArray(array)) {
            for (var d in array) {
                this.add(d);
            }
            return true;
        }
        else {
            this.add(array);
            return true;
        }
    }
    return false;
};

/**
 * Removes the first instance of the given data from the list
 * @param data: data to remove from the list
 * @returns Mixed|null: data removed from the list or null
 */
ArrayList.prototype.remove = function(data){
    if(data === undefined || data === null)
        return null;

    for(var i = 0; i < this.count; i++){
        if(this.arr[i] === data) {
            var d = this.arr[i];
            this.arr[i] = null;
            return d;
        }
    }

    return null;
};

/**
 * removes what ever data is at the given index
 * @param index: index to remove
 * @returns Mixed|null: data removed from the list or null
 */
ArrayList.prototype.removeAt = function(index){
    if(index !== undefined && index !== null && this.isInteger(index) && index < this.maxSize){
        var data = this.arr[index];
        this.arr[index] = null;
        return data;
    }
    return null;
};

/**
 * resets the list to its default state
 */
ArrayList.prototype.reset = function(){
    this.arr = [2];
    this.maxSize = 2;
    this.count = 0;
};

/**
 * gets the data at the given index
 * @param index: index to retrieve
 * @returns Mixed|null: data retrieved or null
 */
ArrayList.prototype.get = function(index){
    if(index !== undefined && index !== null && this.isInteger(index) && index < this.maxSize)
        return this.arr[index];
    return null;
};

/**
 * sets the data at the given index with the given data
 * @param index: index to set
 * @param data: data to be stored at the given index
 * @returns {boolean} success|fail
 */
ArrayList.prototype.set = function(index, data){
    if(index !== undefined && index !== null && this.isInteger(index) && index < this.maxSize) {
        this.arr[index] = data;
        return true;
    }
    return false;

};

/**
 * clears all the data in the list leaving it at its current size
 */
ArrayList.prototype.clear = function(){
    for(var i = 0; i < this.count; i++){
        this.arr[i] = null;
    }
};

/**
 * gets the current number of elements in the list
 * @returns {number}
 */
ArrayList.prototype.size = function(){
    return this.count;
};
