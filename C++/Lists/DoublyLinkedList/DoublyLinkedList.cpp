/*******************************************************
 *  @file DoublyLinkedList.cpp
 *  @author Stephen Hall
 *  @date 11/30/17.
 *  @copyright 2017 Stephen Hall. All rights reserved.
 *  @details Doubly Linked List implementation in C++
 ********************************************************/

#ifndef NULL
#define NULL   ((void *) 0)
#endif

using namespace Lists::DoublyLinkedList;
using DoublyLinkedList::Node;
#include "DoublyLinkedList.h"

/**
 * Node struct default constructor
 */
Node::Node()
{
	data = NULL;
	next = previous = NULL;
}

/**
 * Node struct constructor
 * @tparam T: Generic type
 * @param data: Data for the node to hold
 */
template<typename T>
Node::Node(T data)
{
	this->data = data;
	next = previous = NULL;
}

/**
 * Doubly Linked List constructor
 */
DoublyLinkedList::DoublyLinkedList()
{
	count = 0;
	head = tail = NULL;
}

/**
 * Doubly Linked List destructor
 */
DoublyLinkedList::~DoublyLinkedList()
{
	destroy();
}

/**
 * Adds the given data into the list
 * @tparam T: Generic Type
 * @param data: data to add into the list
 * @return bool success|fail
 */
template<typename T>
bool DoublyLinkedList::add(T data)
{
	if(data == NULL)
		return false;

	Node* node = new Node(data);
	count++;
	// First node in the list
	if(head == NULL)
	{
		head = tail = node;
		return true;
	}
	// Not the first node in the list
	tail->next = node;
	node->previous = tail;
	tail = node;

	return node == NULL;
}

/**
 * Removes the first instance of the given data
 * @tparam T: Generic Type
 * @param data: Data to remove from the list
 * @return bool: success|fail
 */
template<typename T>
bool DoublyLinkedList::remove(T data)
{
	if(data == NULL)
		return false;

	Node* tmp = head;
	while(tmp != NULL)
	{
		if(tmp->data == data)
		{
			count--;
			// if tmp is the head
			if(tmp->previous == NULL)
			{
				tmp->next->previous = NULL;
				tmp->next = NULL;
				delete tmp;
				return true;
			}
			// if tmp is the tail
			if(tmp->next == NULL)
			{
				tmp->previous->next = NULL;
				tail = tmp->previous;
				tmp->previous = NULL;
				delete tmp;
				return true;
			}
			// if tmp is a middle node
			tmp->previous->next = tmp->next;
			tmp->next->previous = tmp->previous;
			tmp->next = tmp->previous = NULL;
			delete tmp;
			return true;
		}
		tmp = tmp->next;
	}
	return false;
}

/**
 * Finds the given data in the list
 * @tparam T: Generic type
 * @param data: Data to find
 * @return Node: Node found in the list or NULL
 */
template<typename T>
Node DoublyLinkedList::find(T data)
{
	Node* tmp = tail;
	while(tmp != NULL)
	{
		if(tmp->data == data)
			break;
		tmp = tmp->previous;
	}
	return tmp == NULL ? NULL : *tmp;
}

/**
 * Gets the number of elements in the list
 * @return int: Number of elements in the list
 */
int DoublyLinkedList::size()
{
	return count;
}

/**
 * Frees the memory in the list
 */
void DoublyLinkedList::destroy()
{
	Node* tmp = head;
	while(tmp != NULL)
	{
		head = head->next;
		tmp->next = NULL;
		tmp->previous = NULL;
		delete tmp;
		tmp = head;
	}
	head = tail = NULL;
}