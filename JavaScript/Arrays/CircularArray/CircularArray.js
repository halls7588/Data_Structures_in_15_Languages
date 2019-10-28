/*******************************************************
 *  CircularArray.js
 *  Created by Stephen Hall on 6/29/16.
 *  Copyright (c) 2016 Stephen Hall. All rights reserved.
 *  A Circular Array implementation in JavaScript
 *  Update Date      Name            Description
 *  1      10/28/19  Stephen Hall    Updated to class syntax.
 *                                   Tested and fixed bugs
 ********************************************************/

/**
 * CircularArray class
 */
class CircularArray {
  /**
   * Constructor of the CircularArray class
   * @constructor
   */
  constructor() {
    this.array = [];
    this.size = 10;
    this.zeroIndex = 0;
    this.count = 0;
  }

  /**
   * resize the array
   */
  resize() {
    this.size *= 2;
  }

  /**
   * Adds a new item into the array
   * @param {*} data: data to add
   * @returns {*} data added to the array
   */
  add(data) {
    if ((this.count + 1) / this.size >= 1) {
      this.resize();
    }
    let tmp = (this.zeroIndex + this.count) % this.size;
    this.array[tmp] = data;

    this.count++;
    return this.array[tmp];
  }

  /**
   * Gets the data at passes in index
   * @param {int} i: index to access
   * @returns {*} data at the given index or null
   */
  dataAt(i) {
    if ((i + this.zeroIndex) % this.size < this.count 
    && this.array[(i + this.zeroIndex) % this.size]) {
      return this.array[(i + this.zeroIndex) % this.size];
    }
    return null;
  }

  /**
   * Removes an item from the array using the given index
   * @param {int} i: index to remove
   * @returns {*} data removed from the array
   */
  remove(i) {
    let tmp = this.array[i + (this.zeroIndex % this.size)];
    this.array[i + (this.zeroIndex % this.size)] = this.array[this.zeroIndex];
    this.array[this.zeroIndex] = null;
    this.count--;
    this.zeroIndex = (this.zeroIndex + 1) % this.size;
    return tmp;
  }

  /**
   * prints the circular array in correct order
   */
  print() {
    for (let i = 0; i < this.count; i++) {
      console.log(this.array[(this.zeroIndex + i) % this.size]);
    }
  }
}

//tests

let arr = new CircularArray();
arr.add(5);
arr.print();
console.log(arr.size, arr.count);
console.log("-----------------------");
arr.add("gse");
arr.add(4);
arr.add(42);
arr.add("signbsb");
arr.add("99");
arr.print();
console.log(arr.size, arr.count);
console.log("-----------------------");
arr.remove(1);
arr.remove(1);
arr.remove(1);
arr.print();
console.log("-----------------------");
arr.add("hgse");
arr.add(9999);
arr.add("kkk");
arr.add("78");
arr.add("12");
console.log(arr.size, arr.count);
console.log("-----------------------");
arr.print();
console.log("-----------------------");
console.log(arr.dataAt(7));
