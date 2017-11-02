/*******************************************************
 *  ArrayedQueue.js
 *  Created by Stephen Hall on 11/02/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  Arrayed Queue implementation in JavaScript
 ********************************************************/

/*
* Arrayed queue Class
* @param none
*/
function ArrayedQueue() {
    this.arr = new Array(10);
    this.count = 0;
    this.size = 10;
}

/**
 * Pushes given data onto the queue if space is available
 * @param data Data to be added to the queue
 * @return Node added to the queue
 */
Arrayedqueue.prototype.Enqueue = function (data){
    if(!this.IsFull()) {
        this.array[count] = data;
        this.count++;
        return this.Top();
    }
    return null;
}

/**
 * Pops item off the queue
 * @return Node popped off of the queue
 */
Arrayedqueue.prototype.Dequque = function(){
    if(this.IsEmpty())
        return null;
    var data = this.array.shift();
    this.count--;
    return data;
}

/**
 * Gets the Node onto of the queue
 * @return Node on top of the queue
 */
Arrayedqueue.prototype.Top = function(){
    if(this.IsEmpty())
        return null;
    return this.array[0];
}

/**
 * Returns a value indicating if the queue is empty
 * @return true if empty, false if not
 */
Arrayedqueue.prototype.IsEmpty = function(){
    return (this.count === 0);
}

/**
 * Returns a value indicating if the queue is full
 * @return True if full, false if not
 */
Arrayedqueue.prototype.IsFull = function(){
    return (this.count === size);
}
