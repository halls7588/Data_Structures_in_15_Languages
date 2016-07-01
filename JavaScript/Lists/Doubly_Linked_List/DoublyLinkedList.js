/********************************************************
*  DoublyLinkedList.js
*  Created by Stephen Hall on 6/29/16.
*  Copyright (c) 2016 Stephen Hall. All rights reserved.
*  A doubly linked list implementation in JavaScript
*********************************************************/

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
 * List Object declaration
 * 
 * @param  none
 *
 * @return none
 * @throws none
 **/
function List() {
    this.length = 0;
    this.head = null;
    this.tail = null;
}

/** 
 * List object add method
 *      add object to the list
 * 
 * @param  generic data
 *
 * @return new Node created
 * @throws none
 **/
List.prototype.add = function(data) {
    var node = new Node(data);
    
    if (this.head === null) { // If there is an empty list
        this.head = this.tail = node;
        this.length++;
        return node;
    }
    else{ // if List is not empty
        this.tail.next = node;
        node.prev = this.tail;
        this.tail = node;
        this.length++;
        return node;
    }
};

/** 
 * List object search by index method
 *      search for object 
 * 
 * @param  int index
 *
 * @return Node at index
 * @throws none
 **/
List.prototype.searchNodeAt = function(index) {
    var tmpNode = this.head;
    var count = 0;
    
    while(tmpNode != null){
        if(count == index)
            return tmpNode;
        tmpNpde = tmpNode.next;
        count++;
    }

    return null;   
};

/** 
 * List object search by data method
 *      search for object 
 * 
 * @param  generic data
 *
 * @return index of matching node
 * @throws none
 **/
List.prototype.searchNodewith = function(data) {
    var tmpNode = this.head;
    var iondex = 0;
    
    while(tmpNode != null){
        if(tmpNode.data == data)
            return index;
        tmpNpde = tmpNode.next;
        index++;
    }

    return 0;   
};

/** 
 * List object remove method
 *      remove opject from the list
 * 
 * @param  index to be removed
 *
 * @return remove true, false, or null
 * @throws none
 **/
List.prototype.removeByIndex = function(index) {
    
    if(index == null)
        return null;

    if(!(index >= this.length)){

        var tmp = this.head;
        var count = 0;

        if(index == 0){
            tmp = this.head.next;
            delete this.head;
            this.head = tmp;
            this.length--;
            return true;
        }
        else{
            var tmp2 = tmp.next;

            while(tmp2 != null){
                if(count == index - 1){
                    tmp.next = tmp2.next;
                    delete tmp2;
                    this.length--;
                    return true;
                }

                tmp = tmp.next;
                tmp2 = tmp2.next;
                count++;
            }
        }
    }
    return false;
};

/** 
 * List object remove method
 *      remove opject from the list
 * 
 * @param  index to be removed
 *
 * @return remove true, false, or null
 * @throws none
 **/
List.prototype.removeByData = function(data) {

    return this.removeByIndex(this.searchNodewith(data));
};

/** 
 * List object PrintForwards method
 *      prints the list
 * 
 * @param  data to be removed
 *
 * @return remove true, false, or null
 * @throws none
 **/
List.prototype.printForwards = function(){

    var tmp = this.head;
    
    while(tmp != null){
        console.log(tmp.data);
        tmp = tmp.next;
    }
};

/** 
 * List object PrintBackwords method
 *      prints the list
 * 
 * @param  data to be removed
 *
 * @return remove true, false, or null
 * @throws none
 **/
List.prototype.printBackwords = function(){

    var tmp = this.tail;
    
    while(tmp != null){
        console.log(tmp.data);
        tmp = tmp.prev;
    }
};