/*******************************************************
 *  @file LinkedStack.cpp
 *  @author Stephen Hall
 *  @date 12/04/17.
 *  @copyright 2017 Stephen Hall. All rights reserved.
 *  @details Linked Stack implementation in C++
 ********************************************************/

#ifndef NULL
#define NULL   ((void *) 0)
#endif

using namespace::Stacks::LinkedStack;
using LinkedStack::Node;
#include "LinkedStack.h"

/**
 * Node default constructor
 */
Node::Node()
{
	next = previous = NULL;
	data = NULL;
}

/**
 * Node constructor
 * @tparam T: Generic type
 * @param data: Data of the node
 */
template<typename T>
Node::Node(T data)
{
	next = previous = NULL;
	this->data = data;
}

/**
 * Linked stack constructor
 */
LinkedStack::LinkedStack()
{
	count = 0;
	head = tail = NULL;
}

/**
 * Linked Stack destructor
 */
LinkedStack::~LinkedStack()
{
	destroy();
}

/**
 * Pushes an item onto the stack
 * @tparam T: generic type
 * @param data: data to add to the stack
 * @return T: data added onto the stack
 */
template<typename T>
T LinkedStack::push(T data)
{
	if(data != NULL)
	{
		Node *node = new Node(data);
		if (isEmpty())
		{
			head = tail = node;
		}
		else
		{
			tail->next = node;
			node->previous = tail;
			tail = node;
		}
		count++;
	}
	return data;
}

/**
 * Pops an item off of the stack
 * @tparam T: Generic type
 * @return T: item on top of the stack
 */
template<typename T>
T LinkedStack::pop()
{
	if(isEmpty())
		return NULL;

	Node* tmp = tail;
	tail = tail->previous;
	tail->next = NULL;
	tmp->previous = NULL;
	T data = tail->data;
	delete tmp;
	count--;
	return data;
}

/**
 * Peeks at the data on the top of the stack;
 * @tparam T: Generic Type
 * @return T: data at the top of the stack
 */
template<typename T>
T LinkedStack::peek()
{
	return tail == NULL ? NULL : tail->data;
}

/**
 * Determines if the stack is full
 * @return bool: false, Linked stack is never full
 */
bool LinkedStack::isFull()
{
	return false;
}

/**
 * Determines if the stack is empty
 * @return bool: true|false
 */
bool LinkedStack::isEmpty()
{
	return count == 0;
}

/**
 * Gets the number of elements in the stack;
 * @return int: size of the stack
 */
int LinkedStack::length()
{
	return count;
}

/**
 * Destroys the stack
 */
void LinkedStack::destroy()
{
	Node *tmp;
	while(count > 0)
	{
		tmp = head;
		head = head->next;
		head->previous = NULL;
		tmp->next = NULL;
		delete tmp;
		count--;
	}
}
