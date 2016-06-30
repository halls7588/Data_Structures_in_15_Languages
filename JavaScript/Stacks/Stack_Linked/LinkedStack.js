/*******************************************************
*  LinkedStack.js
*  Created by Stephen Hall on 6/29/16.
*  Copyright (c) 2016 Stephen Hall. All rights reserved.
*  A linked Stack implementation in JavaScript
********************************************************/

/** 
 * Node Object declaration
 * 
 * @param  generic data
 *
 * @return none
 * @throws none
 **/
function Node(data) {
    this.data = data;
    this.next = null;
    this.prev = null;
}

/** 
 * Stack Object declaration
 * 
 * @param  none
 *
 * @return none
 * @throws none
 **/
function Stack() {
    this.size = 0;
    this.top = null;

}

/** 
 * Stack object push method
 *      add item to the stack
 * 
 * @param  generic data
 *
 * @return new Node created
 * @throws none
 **/
Stack.prototype.push = function(data) {
    var node = new Node(data);
    
    if(this.top === null){
        this.top = node;
        this.size++;
    }
    else{
        this.top.next = node;
        node.prev = this.top;
        this.top = node;
        this.size++;
    }
    return node;
};

/** 
 * Stack object pop method
 *      remove item from the stack
 * 
 * @param none
 *
 * @return Node removed success
 * @throws none
 **/
Stack.prototype.pop = function() {
    if(this.top !== null){
        var node = this.top.prev;
        delete this.top;
        this.top = node;
        this.size--;
        return true;
    }
    return false;
};

/** 
 * Stack object getSize method
 *     return size of the stack
 * 
 * @param  none
 *
 * @return int Size of stack;
 * @throws none
 **/
Stack.prototype.getSize = function() {
    return this.size;
};

/** 
 * Stack object print method
 *      print the stack top down
 * 
 * @param  none
 *
 * @return none;
 * @throws none
 **/
Stack.prototype.print = function() {
    var node = this.top;
    while(node){
        console.log(node.data);
        node = node.prev;
    }
};

