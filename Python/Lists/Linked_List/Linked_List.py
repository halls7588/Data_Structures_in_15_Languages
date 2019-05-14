"""----------------------------------------------------
' Linked_List.py
' Created by Stephen Hall on 05/14/19.
' Copyright (c) 2019 Stephen Hall. All rights reserved.
' Linked List implementation in Python
 ---------------------------------------------------"""


class Node:
    """
    ' Node constructor.
    ' @ param data: data to be added to the list
    """
    def __init__(self, data=None):
        self.next = None
        self.data = data


class LinkedList:
    """
    ' Class Constructor
    """
    def __init__(self):
        self.count = 0
        self.head = None
        self.tail = None

    """
    ' Adds a new node into the list with the given data
    ' @ param data: data to be added to the list
    ' @ return Node: node added to the list
    """
    def add(self, data):
        new_node = Node(data)
        'If the list is empty'
        if self.head is None:
            self.head = self.tail = new_node
            self.count += 1
        else:
            'Add to the end of the list'
            self.tail.next = new_node
            self.tail = new_node
            self.count += 1
        return new_node

    """
    ' Gets the current size of the list
    ' @ return int: current size of the list
    """
    def size(self):
        return self.count

    """
    ' Removes the first node in the list matching the data
    ' @ param data: data to remove from the list
    ' @ return Node | None: node removed from the list or None
    """
    def remove(self, data):
        if (None == self.head) or (None == data):
            return None
        tmp = self.head
        'The data to remove what found in the first Node in the list'
        if tmp.data == data:
            self.head = self.head.next
            self.count -= 1
            return tmp

        'Try to find the node in the list'
        while tmp.next is not None:
            'Node was found, Remove it from the list'
            if tmp.next.data == data:
                node = tmp.next
                tmp.next = tmp.next.next
                self.count -= 1
                return node

        'The data was not found in the list'
        return None

    """
    ' Finds the first node that has the given data
    ' @param key: key to find in the list
    ' @return Node|None: node found or None
    """
    def find(self, key):
        current = self.head
        while current and current.data != key:
            if current.next is None:
                return None
            else:
                current = current.next
        return current

    """
    ' Prints the list
    """
    def print(self):
        items = []
        current = self.head
        while current is not None:
            items.append(current.data)
            current = current.next
        str = ''
        for item in items:
            str += item
            str += '->'

        print(str)
