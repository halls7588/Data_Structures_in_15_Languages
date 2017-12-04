/*******************************************************
 *  @file CircularStack.cpp
 *  @author Stephen Hall
 *  @date 12/04/17.
 *  @copyright 2017 Stephen Hall. All rights reserved.
 *  @details Circular Stack implementation in C++
 ********************************************************/

#ifndef NULL
#define NULL   ((void *) 0)
#endif

using namespace Stacks::CircularStack;
#include "CircularStack.h"

/**
 * Circular Stack default constructor
 * @tparam T: Generic type
 */
template <typename T>
CircularStack::CircularStack()
{
	array = new T[this->size = 10];
	zeroIndex = count = 0;
}

/**
 * Constructor for Circular stack
 * @tparam T: Generic type
 * @param size: Size to initialize the stack to
 */
template <typename T>
CircularStack::CircularStack(unsigned int size)
{
	array = new T[this->size = size];
	zeroIndex = count = 0;
}

/**
 * Destructor of the Circular stack
 */
CircularStack::~CircularStack()
{
	destroy();
}

/**
 * Pushes an item onto the stack
 * @tparam T: Generic type
 * @param data: Data to add to the stack
 * @return T: data added onto the stack
 */
template <typename T>
T CircularStack::push(T data)
{
	if(isFull() || data == NULL)
		return NULL;

	array[(zeroIndex + count) % size] = data;
	count++;
	return data;
}

/**
 * Removes the item on the top of the stack
 * @tparam T: Generic Type
 * @return T: Item removed from the stack
 */
template <typename T>
T CircularStack::pop()
{
	if(isEmpty())
		return NULL;

	T data = array[(zeroIndex + count - 1) % size];
	array[(count + zeroIndex % size)] = array[zeroIndex];
	array[zeroIndex] = NULL;
	count--;
	zeroIndex = (zeroIndex + 1) % size;
	return data;
}

/**
 * Peeks at the item at the top of the stack
 * @tparam T: generic type
 * @return T: item on top of the stack
 */
template <typename T>
T CircularStack::peek()
{
	return isEmpty() ? NULL : array[(zeroIndex + count - 1) % size];
}

/**
 * Determines if the stack is empty of not
 * @return bool: true|false
 */
bool CircularStack::isEmpty()
{
	return count == 0;
}

/**
 * Determines is the stack is full or not
 * @return bool: true|false
 */
bool CircularStack::isFull()
{
	return size == count;
}

/**
 * gets the number of items on the stack
 * @return int: number of items on the stack
 */
int CircularStack::length()
{
	return count;
}

/**
 * Destroys the Circular Stack
 * @tparam T
 */
template <typename T>
void CircularStack::destroy()
{
	delete [] array;
	array = NULL;
	count = size = 0;
}