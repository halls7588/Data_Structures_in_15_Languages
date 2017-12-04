/*******************************************************
 *  @file SkipList.cpp
 *  @author Stephen Hall
 *  @date 12/04/17.
 *  @copyright 2017 Stephen Hall. All rights reserved.
 *  @details Skip List implementation in C++
 ********************************************************/

using namespace Lists::SkipList;
using SkipList::Node;
#include "SkipList.h"

/**
 * Node Default constructor
 * @tparam T: Generic type
 */
template<typename T>
Node::Node()
{
	data = NULL;
	nodeList = new std::vector<T>();
}

/**
 * Node constructor with value
 * @tparam T: Generic Type
 * @param data: Data of the node
 */
template<typename T>
Node::Node(T data)
{
	this->data = data;
	nodeList = new std::vector<T>();
}

/**
 * Node Destructor
 */
Node::~Node()
{
	for(int i = 0; i < nodeList->size(); ++i)
		delete nodeList[i];
	nodeList->clear();
	delete nodeList;
}

/**
 * Gets the node level
 * @return int: Level of the node
 */
int Node::level()
{
	return nodeList->size() - 1;
}

/**
 * Skip List constructor
 */
SkipList::SkipList()
{
	size = 0;
	max = 0;
	head = new Node(NULL);
	// a Node with value null marks the beginning
	head->nodeList->push_back(NULL);
}

/**
 * Skip List destructor
 */
SkipList::~SkipList()
{
	destroy();
}

/**
 * Adds an item into the List
 * @tparam T: Generic type
 * @param data: Data to add into the list
 * @return bool: success|fail
 */
template<typename T>
bool SkipList::add(T data)
{
	if(contains(data))
		return false;

	size++;
	int level = 0;
	// random number from 0 to max + 1 (inclusive)
	while (rand() < PROBABILITY)
		level++;

	while(level > max)
	{
		// should only happen once
		head->nodeList->push_back(NULL);
		max++;
	}

	Node* node = new Node(data);
	Node* current = head;

	do
	{
		current = findNext(data, current, level);
		node->nodeList->push_back(0, current->nodeList[level]);
		current->nodeList->at(level) = node;
	} while ((level--) > 0);
	return true;
}

/**
 * Finds a node in the list with the same data
 * @tparam T: Generic type
 * @param data: data to find
 * @return Node: Node found or NULL
 */
template<typename T>
Node* SkipList::find(T data)
{
	return find(data, head, max);
}

/**
 * Gets the size of the List
 * @return int: size of the list
 */
int SkipList::length()
{
	return size;
}

/**
 * Determines if the object is in the list or not
 * @tparam T: Generic type
 * @param data: object to test
 * @return boolean: true|false
 */
template<typename T>
bool SkipList::contains(T data)
{
	Node* node = find(data);
	return ((node != NULL) && (node->data != NULL) && (node->data == data));
}

/**
 * Destroys the skip list
 */
void SkipList::destroy()
{
	delete head;
}

/**
 * Returns the node at a given level with highest value less than data
 * @tparam T: Generic Type
 * @param data: data to find
 * @param current: current node
 * @param level: current level
 * @return Node: highest node
 */
template<typename T>
Node* SkipList::findNext(T data, Node* current, int level)
{
	Node* next = current->nodeList->at(level);

	while(next != NULL)
	{
		T value = next->data;
		if(data < value)
			break;
		current = next;
		next = current->nodeList->at(level);
	}
	return current;
}

/**
 * Returns node with the greatest value
 * @tparam T: Generic type
 * @param data: data to find
 * @param current: current Node
 * @param level: level to start form
 * @return Node: current node
 */
template<typename T>
Node* SkipList::find(T data, Node* current, int level)
{
	do
	{
		current = findNext(data, current, level);
	} while((level--) > 0);
	return current;
}
