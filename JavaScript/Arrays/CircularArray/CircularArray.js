/*******************************************************
 *  CircularArray.js
 *  Created by Stephen Hall on 6/29/16.
 *  Copyright (c) 2016 Stephen Hall. All rights reserved.
 *  A Circular Array implementation in JavaScript
 ********************************************************/

/**
 * CircularArray Object declaration
 *
 * @param none
 *
 * @return none
 * @throws none
 **/
function CircularArray() {
    this.array = [];
    this.size = 0;
    this.zeroIndex = 0;
    this.count = 0;
    this.resize = function () {
        this.size *= 2;
    };
}

/**
 * Add function prototype declaration
 *      Adds a new item into the array
 *
 * @param  data: data to add to the array
 *
 * @return item added to the array
 * @throws none
 **/
CircularArray.prototype.add = function (data) {

    var tmp = (this.zeroIndex + this.count) % this.size;
    this.array[tmp] = data;
    if (((this.count + 1) / this.size) >= 1) {
        this.resize();
    }
    this.count++;
    return this.array[tmp];
};

/**
 * DataAt function prototype declaration
 *      Gets the data at passes in index
 *
 * @param  i: index of the array
 *
 * @return Item at index
 * @throws none
 **/
CircularArray.prototype.dataAt = function (i) {
    if ((i + this.zeroIndex) % this.size < this.count && this.array[(i + this.zeroIndex) % this.size]) {
        return (this.array[i + this.zeroIndex % this.size]);
    }
    return null;
};

/**
 * Remove function prototype declaration
 *      Removes an item from the array
 *
 * @param  i: index to remove
 *
 * @return item removed from the array
 * @throws none
 **/
CircularArray.prototype.remove = function (i) {
    var tmp = this.array[i + this.zeroIndex % this.size];
    this.array[i + this.zeroIndex % this.size] = this.array[this.zeroIndex];
    this.array[this.zeroIndex] = null;
    this.count--;
    this.zeroIndex = (this.zeroIndex + 1) % this.size;
    return tmp;
};

/**
 * Print function prototype declaration
 *      Prints out the array
 *
 * @param  array to print
 *
 * @return none
 * @throws none
 **/
CircularArray.prototype.print = function (array) {
    this.array.map(function (data) {
        if (data !== null) {
            console.log(data);
        }
    });
};