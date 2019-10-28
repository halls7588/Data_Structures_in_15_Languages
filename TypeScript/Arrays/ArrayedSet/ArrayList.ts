/*******************************************************
 *  ArrayList.ts
 *  Created by Stephen Hall on 10/28/19.
 *  Copyright (c) 2019 Stephen Hall. All rights reserved.
 *  ArrayList implementation in TypeScript
 ********************************************************/

/**
 * ArrayList Class
 */
class ArrayList {
  arr: any[];
  maxSize: number;
  count: number;
  /**
   * Initialize the ArrayList
   * @param {int} size: optional variable to set the size of the list
   */
  constructor(size: number) {
    if (size === undefined || !this._isInteger(size)) {
      this.arr = [2];
      this.maxSize = 2;
      this.count = 0;
    } else {
      this.arr = [size];
      this.maxSize = size;
      this.count = 0;
    }
  }

  /**
   * private method
   * doubles the size of the internal array
   */
  _resize() {
    this.maxSize *= 2;
    var tmp = [this.maxSize];
    for (var i = 0; i < this.count; i++) {
      tmp[i] = this.arr[i];
    }
    this.arr = tmp;
  }

  /**
   * checks if x is an integer number
   * @param {*} x
   * @returns {boolean}
   */
  _isInteger(x: any): boolean {
    if (x === undefined || x === null)
     return false;

    return x % 1 === 0;
  }

  /**
   * Adds the given data to the list
   * @param {*} data: data to add to the ArrayList
   * @returns {*} data inserted or null
   */
  add(data: any) {
    if (data === undefined || data === null)
     return null;

    if (this.count === this.maxSize)
     this._resize();

    this.arr[this.count] = data;
    this.count++;

    return data;
  }

  /**
   * Appends the given array data to the ArrayList
   * @param {[]} array: array to be added into the list
   * @returns {boolean}
   */
  append(array: any[]): boolean {
    if (array !== undefined && array !== null) {
      if (Array.isArray(array)) {
        for (var d in array) {
          this.add(array[d]);
        }
        return true;
      } else {
        this.add(array);
        return true;
      }
    }
    return false;
  }

  /**
   * Removes the first instance of the given data from the list
   * @param {*} data: data to remove from the list
   * @returns {*} data removed from the list or null
   */
  remove(data: any): any {
    if (data === undefined || data === null) return null;

    var index = this.arr.indexOf(data);
    var data = null;

    if (index > -1) {
      data = this.arr[index];
      this.arr.splice(index, 1);
      this.count--;
      this.maxSize--;
    }
    return data;
  }

  /**
   * removes what ever data is at the given index
   * @param {int} index: index to remove
   * @returns {*} data removed from the list or null
   */
  removeAt(index: number): any {
    if (
      index !== undefined &&
      index !== null &&
      this._isInteger(index) &&
      index < this.maxSize
    ) {
      var data = this.arr[index];
      this.arr.splice(index, 1);
      this.count--;
      this.maxSize--;
      return data;
    }
    return null;
  }

  /**
   * resets the list to its default state
   */
  reset() {
    this.arr = [2];
    this.maxSize = 2;
    this.count = 0;
  }

  /**
   * gets the data at the given index
   * @param {int} index: index to retrieve
   * @returns {*} data retrieved or null
   */
  get(index: number): any {
    if (
      index !== undefined &&
      index !== null &&
      this._isInteger(index) &&
      index < this.maxSize
    )
      return this.arr[index];
    return null;
  }

  /**
   * sets the data at the given index with the given data
   * @param {int} index: index to set
   * @param {*} data: data to be stored at the given index
   * @returns {boolean} success|fail
   */
  set(index: number, data: any): boolean {
    if (
      index !== undefined &&
      index !== null &&
      this._isInteger(index) &&
      index < this.maxSize
    ) {
      this.arr[index] = data;
      return true;
    }
    return false;
  }

  /**
   * clears all the data in the list leaving it at its current size
   */
  clear() {
    for (var i = 0; i < this.count; i++) {
      this.arr[i] = [];
    }
    this.count = 0;
  }

  /**
   * gets the current number of elements in the list
   * @returns {int}
   */
  size() {
    return this.count;
  }

  /**
   * Prints the contents of the ArrayList to the console.
   */
  print() {
    console.log(this.arr.toString());
  }
}

// Tests

let arr = new ArrayList(5);
console.log(arr.count, arr.maxSize);
arr.append([1, 3, 5, 7, 9, 11]);
arr.print();
console.log(arr.count, arr.maxSize);
console.log(arr.removeAt(2));
console.log(arr.count, arr.maxSize);
console.log(arr.remove(9));
console.log(arr.count, arr.maxSize);
arr.print();
arr.clear();
console.log(arr.count, arr.maxSize);
arr.print();
