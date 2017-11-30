/*******************************************************
 *  @file LinkedList.cpp
 *  @author Stephen Hall
 *  @date 11/30/17.
 *  @copyright 2017 Stephen Hall. All rights reserved.
 *  @details Linked List implementation in C++
 ********************************************************/

#ifndef NULL
#define NULL   ((void *) 0)
#endif

#include "LinkedList.h"
using namespace Lists::LinkedList;
using LinkedList::Node;

/**
 * Linked List constructor
 */
LinkedList::LinkedList()
{
	count = 0;
	head = tail = NULL;
}

/**
 * Destroys the list
 */
LinkedList::~LinkedList()
{
	destroy();
}

/**
 * Frees the memory of the list
 */
void  LinkedList::destroy()
{
	Node* tmp = head;
	while(head != NULL)
	{
		head = head->next;
		delete tmp;
		tmp = head;
	}
}

/**
 * Adds the given data into the list
 * @tparam T: Generic type
 * @param data: Data to add into the list
 * @return bool: success|fail
 */
template<typename T>
bool LinkedList::add(T data)
{
	Node* node = new Node(data);
	if(count == 0)
	{
		head = tail = node;
	}
	else
	{
		tail->next = node;
		tail = node;
	}
	count++;
	return node != NULL;
}

/**
 * Removes the first instance of the given data from the list
 * @tparam T: Generic Type
 * @param data: data to remove from the list
 * @return bool: success|fail
 */
template<typename T>
bool LinkedList::remove(T data)
{
	Node* tmp = head;
	if(data != NULL)
	{
		if (tmp->data != data)
		{
			while (tmp->next != NULL)
			{
				if (tmp->next->data == data)
					break;

				tmp = tmp->next;
			}
			if (tmp->next->data == data)
			{
				Node *tmp2 = tmp->next;
				tmp->next = tmp->next->next;
				delete tmp2;
				count--;
				return true;
			}
			return false;
		}
		else
		{
			head = head->next;
			delete tmp;
			count--;
			return true;
		}
	}
	return false;
}

/**
 * Finds a node in the list if it exists
 * @tparam T: Generic Type
 * @param data: Data to find
 * @return Node: Node found in the list or NULL
 */
template<typename T>
Node LinkedList::find(T data)
{
	if(data == NULL)
		return NULL;

	Node* tmp = head;
	while(tmp != NULL)
	{
		if(tmp->data == data)
			break;
		tmp = tmp->next;
	}

	return tmp == NULL ? NULL : *tmp;
}

/**
 * Returns the number of elements in the list
 * @return int: number of elements in the list
 */
int LinkedList::size()
{
	return count;
}

/**
 * Node struct default constructor
 */
Node::Node()
{
	data = NULL;
	next = NULL;
}

/**
 * Node struct Constructor
 * @tparam T: generic Type
 * @param data: data fro the node to hold
 */
template<typename T>
Node::Node(T data)
{
	this->data = data;
	next = NULL;
}

