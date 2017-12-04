/*******************************************************
 *  @file LinkedSet.cpp
 *  @author Stephen Hall
 *  @date 12/04/17.
 *  @copyright 2017 Stephen Hall. All rights reserved.
 *  @details Linked Set implementation in C++
 ********************************************************/

#ifndef NULL
#define NULL   ((void *) 0)
#endif

using namespace Lists::LinkedSet;
using LinkedSet::Node;
#include "LinkedSet.h"

/**
 * Node struct default constructor
 */
Node::Node()
{
	next = prevoius = NULL;
	data = NULL;
}

/**
 * Node constructor
 * @tparam T: generic type
 * @param data: Data of the Node
 */
template<typename T>
Node::Node(T data)
{
	next = prevoius = NULL;
	this->data = data;
}

/**
 * Linked set constructor
 */
LinkedSet::LinkedSet()
{
	head = tail = NULL;
	count = 0;
}

/**
 * Linked set destructor
 */
LinkedSet::~LinkedSet()
{
	destroy();
}

/**
 * Adds the given data into the list
 * @tparam T: Generic type
 * @param data: data to add into the list
 * @return T: data added into the list
 */
template<typename T>
T LinkedSet::add(T data)
{
	if(contains(data))
		return NULL;

	Node* node = new Node(data);
	if(count == 0)
	{
		head = tail = node;
	}
	else
	{
		tail->next = node;
		node->prevoius = tail;
		tail = node;
	}
	count++;
	return data;
}

/**
 * Removes the given data from the list if it exists
 * @tparam T: Generic type
 * @param data: Data to remove
 * @return T: data removed from the list of NULL
 */
template<typename T>
T LinkedSet::remove(T data)
{
	Node* tmp = find(data);
	if(tmp != NULL)
	{
		T item = tmp->data;
		if(tmp == head)
		{
			if(head->next)
				head->next->prevoius = NULL;
			head = head->next;
		}
		else if(tmp == tail)
		{
			if(tail != head)
			{
				tail = tail->prevoius;
				tail->next = NULL;
			}
		}
		else
		{
			tmp->prevoius->next = tmp->next;
			tmp->next->prevoius = tmp->prevoius;
			tmp->prevoius = tmp->next = NULL;
		}
		delete tmp;
		count--;
		return item;
	}
	return NULL;
}

/**
 * Finds the given data in the list if it exists
 * @tparam T: Generic type
 * @param data: Data to find
 * @return Node*: Node that contains the given data or NULL
 */
template<typename T>
Node* LinkedSet::find(T data)
{
	Node* tmp = head;

	while(tmp != NULL)
	{
		if(tmp->data == data)
			break;
		tmp = tmp->next;
	}
	return tmp;
}

/**
 * Gets the number of elements in the list
 * @return int: number of elements in the list
 */
int LinkedSet::size()
{
	return count;
}

/**
 * Determines if the Linked Set contains the given data
 * @tparam T: Generic type
 * @param data: Data to  find
 * @return bool: true|false
 */
template<typename T>
bool LinkedSet::contains(T data)
{
	return find(data) == NULL;
}

/**
 * Destroys the Linked set
 */
void LinkedSet::destroy()
{
	Node* tmp = head;

	while(tmp != NULL)
	{
		head = head->next;
		tmp->next = tmp->prevoius = NULL;
		delete tmp;
		tmp = head;
	}
	head = tail = NULL;
	count = 0;
}