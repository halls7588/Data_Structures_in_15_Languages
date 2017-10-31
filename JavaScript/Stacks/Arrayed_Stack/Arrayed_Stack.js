/*******************************************************
 *  Arrayed_Stack.js
 *  Created by Stephen Hall on 10/31/17.
 *  Copyright (c) 2017 Stephen Hall. All rights reserved.
 *  A Arrayed Stack implementation in JavaScript
 ********************************************************/

/*
* Arrayed Stack Class
* @param none
*/
function ArrayedStack() {
    this.arr = new array(10);
    this.count = 0;
    this.size = 10;
}

/**
 * Pushes given data onto the stack if space is available
 * @param data Data to be added to the stack
 * @return Node added to the stack
 */
ArrayedStack.prototype.Push = function (data){
    if(!this.IsFull()) {
        this.array[count] = data;
        this.count++;
        return this.Top();
    }
    return null;
}

/**
 * Pops item off the stack
 * @return Node popped off of the stack
 */
ArrayedStack.prototype.Pop = function(){
    var data = this.array[count-1];
    this.count--;
    return data;
}

/**
 * Gets the Node onto of the stack
 * @return Node on top of the stack
 */
ArrayedStack.prototype.Top = function(){
    return this.array[count-1];
}

/**
 * Returns a value indicating if the stack is empty
 * @return true if empty, false if not
 */
ArrayedStack.prototype.IsEmpty = function(){
    return (this.count == 0);
}

/**
 * Returns a value indicating if the stack is full
 * @return True if full, false if not
 */
ArrayedStack.prototype.IsFull = function(){
    return (this.count == size);
}
