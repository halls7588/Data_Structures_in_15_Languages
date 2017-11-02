/*******************************************************
 *  LinkedQueue.js
 *  Created by Stephen Hall on 11/02/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  Linked Queue implementation in JavaScript
 ********************************************************/

/**
 * Node constructor
 * @param data: data to be held in the node
 * @constructor
 */
function Node(data) {
    this.data = data;
    this.next = null;
}

/**
 * LinkedQueue Constructor
 * @constructor
 */
function LinkedQueue(){
    this.count = 0;
    this.head = null;
    this.tail = null;
}

/**
 * adds an item onto the queue
 * @param data: data to add onto the queue
 * @return Node: node added into the queue
 */

LinkedQueue.prototype.Enqueue = function(data){
    if(this.isEmpty()){
        var node = new Node(data);
        this.head = node;
        this.tail = this.head;
        this.count++;
        return node;
    }
    else{
        // add to the end of the queue
        var node = new Node(data);
        this.tail.next = node;
        this.tail = node;
        this.count++;
        return node;
    }
};

/**
 * removes the next item in the queue
 * @return Node|null: returns the item removed from the queue or null
 */
LinkedQueue.prototype.Dequeue = function(){
    if(this.isEmpty())
        return null;

    var node = this.head;
    this.head = this.head.next;
    this.count--;
    return node;
};

/**
 * returns a value indicating if the queue is empty or not
 * @return bool: true if the queue is empty
 */
LinkedQueue.prototype.isEmpty = function(){
    return (this.count === 0);
};

/**
 * returns the current size of the queue
 * @return int: size of the queue
 */
LinkedQueue.prototype.size = function(){
    return this.count;
};