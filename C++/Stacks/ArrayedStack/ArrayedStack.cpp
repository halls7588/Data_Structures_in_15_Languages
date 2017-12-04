/*******************************************************
 *  @file ArrayedStack.cpp
 *  @author Stephen Hall
 *  @date 12/04/17.
 *  @copyright 2017 Stephen Hall. All rights reserved.
 *  @details Arrayed Stack implementation in C++
 ********************************************************/

#ifndef NULL
#define NULL   ((void *) 0)
#endif

using namespace::Stacks::ArrayedStack;
#include "ArrayedStack.h"

/**
 * Arrayed Stack Constructor to default size
 * @tparam T: Generic Type
 */
template<typename T>
ArrayedStack::ArrayedStack()
{
	array = new T[size = 10];
	count = 0;
}

/**
 * Arrayed stack constructor to a specific size
 * @tparam T: generic type
 * @param size: Size to initialize the stack to.
 */
template<typename T>
ArrayedStack::ArrayedStack(unsigned int size)
{
	array = new T[this->size = size];
	count = 0;
}

/**
 * Arrayed Stack Destructor
 */
ArrayedStack::~ArrayedStack()
{
	destroy();
}

/**
 * Pushes and item onto the stack
 * @tparam T: Generic type
 * @param data: Item to add to the stack
 * @return T: Item added onto the stack
 */
template<typename T>
T ArrayedStack::push(T data)
{
	if(!isFull())
	{
		array[count] = data;
		count++;
		return data;
	}
	return NULL;
}

/**
 * Pops an item off of the stack
 * @tparam T: Generic type
 * @return T: Item popped off of the stack
 */
template<typename T>
T ArrayedStack::pop()
{
	if(!isEmpty())
	{
		T data = array[count -1];
		count--;
		return count;
	}
	return NULL;
}

/**
 * Determines if teh stack is empty or not
 * @return bool: if the stack is empty
 */
bool ArrayedStack::isEmpty()
{
	return count == 0;
}

/**
 * Determines if the stack is full or not
 * @return bool: if the stack is full or nut
 */
bool ArrayedStack::isFull()
{
	return size == count;
}

/**
 * Gets teh number of elements in the stack
 * @return int: number of elements in the stack
 */
int ArrayedStack::length()
{
	return count;
}

/**
 * Destroys the Arrayed Stack
 */
void ArrayedStack::destroy()
{
	delete [] array;
	array = NULL;
	size = count = 0;
}