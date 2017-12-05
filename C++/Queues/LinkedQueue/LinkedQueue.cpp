/*******************************************************
 *  @file LinkedQueue.cpp
 *  @author Stephen Hall
 *  @date 12/05/17.
 *  @copyright 2017 Stephen Hall. All rights reserved.
 *  @details Linked Queue implementation in C++
 ********************************************************/

using namespace Queues::LinkedQueue;
using LinkedQueue::Node;
#include "LinkedQueue.h"

#ifndef NULL
#define NULL   ((void *) 0)
#endif

/**
 * Node default constructor
 */
Node::Node()
{
	data = NULL;
	next = previous = NULL;
}

/**
 * Node Constructor
 * @tparam T: Generic type
 * @param data: Data of the node
 */
template <typename T>
Node::Node(T data)
{
	this->data = data;
	next = previous = NULL;
}

/**
 * Linked Queue constructor
 */
LinkedQueue::LinkedQueue()
{
	count = 0;
	head = tail = NULL;
}

/**
 * Linked Queue destructor
 */
LinkedQueue::~LinkedQueue()
{
	destroy();
}

/**
 * Added an item onto the queue;
 * @tparam T: Generic type
 * @return T: data added onto the queue;
 */
template <typename T>
T LinkedQueue::engueue(T data)
{
	if(data == NULL)
		return data;

	Node* node = new Node(data);

	if(isEmpty())
		head = tail = node;
	else
	{
		tail->next = node;
		node->previous = tail;
		tail = node;
	}
	count++;
	return data;
}

/**
 * Removes the first element of the queue
 * @tparam T: Generic type
 * @return T: data removed from the queue
 */
template <typename T>
T LinkedQueue::dequeue()
{
	if(isEmpty())
		return NULL;

	Node* node = head;
	head = head->next;
	head->previous = NULL;
	node->next = NULL;
	T data = node->data;
	delete node;
	count--;
	return data;
}

/**
 * Gets the first element of the queue without removing it
 * @tparam T: Generic Type
 * @return T: Data at the front of the queue
 */
template <typename T>
T LinkedQueue::top()
{
	return isEmpty() ? NULL : head->data;
}

/**
 * Determines if the queue is full
 * @return false: linked queue is never full
 */
bool LinkedQueue::isFull()
{
	return false;
}

/**
 * Determines if the queue is empty
 * @return bool: true|false
 */
bool LinkedQueue::isEmpty()
{
	return count == 0;
}

/**
 * Gets the count of elements in the queue
 * @return int: number of elements in the queue
 */
int LinkedQueue::length()
{
	return count;
}

/**
 * Frees the memory of the queue
 * @tparam T: Generic type
 */
template <typename T>
void LinkedQueue::destroy()
{
	Node* tmp = head;
	while(tmp != NULL)
	{
		head = head->next;
		delete tmp;
		tmp = head;
	}
	head = tail = NULL;
	count = 0;
}
