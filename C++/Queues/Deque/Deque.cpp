/*******************************************************
 *  @file Deque.cpp
 *  @author Stephen Hall
 *  @date 12/05/17.
 *  @copyright 2017 Stephen Hall. All rights reserved.
 *  @details Deque implementation in C++
 ********************************************************/

using namespace Queues::Deque;
using Deque::Node;
#include "Deque.h"

#ifndef NULL
#define NULL   ((void *) 0)
#endif

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
 * @param data: Data of the Node
 */
template <typename T>
Node::Node(T data)
{
	this->data = data;
	next = previous = NULL;
}

/**
 * Deque constructor
 */
Deque::Deque()
{
	head = tail = NULL;
	count = 0;
}

/**
 * Deque destructor
 */
Deque::~Deque()
{
	destroy();
}

/**
 * Adds an element to the front of the Deque
 * @tparam T: Generic type
 * @param data: Data to add
 * @return T: data added onto the Deque
 */
template <typename T>
T Deque::engueue_front(T data)
{
	if(data == NULL)
		return data;

	Node* node = new Node(data);

	if(isEmpty())
		head = tail = node;
	else
	{
		node->next = head;
		head->previous = node;
		head = node;
	}
	count++;
	return data;
}

/**
 * Removes an element from the front of the Deque
 * @tparam T: Generic type
 * @return T: data removed from the Deque
 */
template <typename T>
T Deque::dequeue_front()
{
	if(isEmpty())
		return NULL;

	Node* node = head;
	head = head->next;
	head ? head->previous = NULL : NULL;
	node->next = NULL;
	T data = node->data;
	delete node;
	count--;
	return data;
}

/**
 * Adds and element onto the back of the Deque
 * @tparam T: Generic type
 * @return T: Data added onto the Deque
 */
template <typename T>
T Deque::engueue_back(T data)
{
	if(data == NULL)
		return data;

	Node* node = new Node(data);

	if(isEmpty())
		head = tail = node;
	else
	{
		node->previous = tail;
		tail->next = node;
		tail = node;
	}
	count++;
	return data;
}

/**
 * Removes an element from the back of the Deque
 * @tparam T: Generic type
 * @return T: data removed from the Deque
 */
template <typename T>
T Deque::dequeue_back()
{
	if(isEmpty())
		return NULL;

	Node* node = tail;
	tail = tail->previous;
	tail ? tail->next = NULL : NULL;
	node->previous = NULL;
	T data = node->data;
	delete node;
	count--;
	return data;
}

/**
 * Gets the element at the front of the Deque
 * @tparam T: Generic Type
 * @return T: Data at the front of the Deque
 */
template <typename T>
T Deque::front()
{
	return head ? head->data : NULL;
}

/**
 * gets the element at teh back of the Deque
 * @tparam T: Generic type
 * @return T: Data at the back of the Deque
 */
template <typename T>
T Deque::back()
{
	return tail ? tail->data : NULL;
}

/**
 * Determines if the Deque if full
 * @return false: Deque is never full
 */
bool Deque::isFull()
{
	return false;
}

/**
 * Determines if the deque is empty
 * @return bool: true|false
 */
bool Deque::isEmpty()
{
	return count == 0;
}

/**
 * Gets the number of elements in the Deque
 * @return int: number of elements in the Deque
 */
int Deque::length()
{
	return count;
}

/**
 * Frees the memors of the Deque
 */
void Deque::destroy()
{
	Node* node = head;
	while(node)
	{
		head = head->next;
		delete node;
		node = head;
	}
	head = tail = node = NULL;
	count = 0;
}