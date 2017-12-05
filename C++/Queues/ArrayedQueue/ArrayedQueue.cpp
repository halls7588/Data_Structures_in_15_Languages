/*******************************************************
 *  @file ArrayedQueue.cpp
 *  @author Stephen Hall
 *  @date 12/05/17.
 *  @copyright 2017 Stephen Hall. All rights reserved.
 *  @details Arrayed Queue implementation in C++
 ********************************************************/

using namespace Queues::ArrayedQueue;
#include "ArrayedQueue.h"

#ifndef NULL
#define NULL   ((void *) 0)
#endif

/**
 * Arrayed Queue default constructor
 * @tparam T: Generic type
 */
template<typename T>
ArrayedQueue::ArrayedQueue()
{
	array = new T[size = 10];
	count = 0;
}

/**
 * Arrayed Queue constructor initialized to size
 * @tparam T: generic type
 * @param size: Max length of the queue
 */
template<typename T>
ArrayedQueue::ArrayedQueue(unsigned int size)
{
	array = new T[this->size = size];
	count = 0;
}

/**
 * Arrayed Queue destructor
 */
ArrayedQueue::~ArrayedQueue()
{
	destroy();
}

/**
 * Adds data onto the queue if queue is not full
 * @tparam T: Generic type
 * @param data: Data to add to the queue
 * @return T: data added to the queue
 */
template<typename T>
T ArrayedQueue::enqueue(T data)
{
	if(isFull() || data == NULL)
		return NULL;

	array[count] = data;
	count++;
	return data;
}

/**
 * Removes the first item from the queue
 * @tparam T: Generic type
 * @return T: Item removed from the queue
 */
template<typename T>
T ArrayedQueue::dequeue()
{
	if(isEmpty())
		return NULL;

	T data = array[0];
	for(int i = 0; i < count - 1; i++)
	{
		array[i] = array[i + 1];
	}
	count--;
	return data;
}

/**
 * Gets the element at the front of the queue
 * @tparam T: Generic Type
 * @return T: Element at the front od the queue
 */
template<typename T>
T ArrayedQueue::top()
{
	return isEmpty() ? NULL : array[0];
}

/**
 * Determines if the queue is empty
 * @tparam T: Generic Type
 * @return bool: true|false
 */
template<typename T>
bool ArrayedQueue::isEmpty()
{
	return count == 0;
}

/**
 * Determines if the queue is full
 * @tparam T: generic type
 * @return bool: true|false
 */
template<typename T>
bool ArrayedQueue::isFull()
{
	return count == size;
}

/**
 * Gets the number od elements in the queue
 * @return int: length of the queue
 */
int ArrayedQueue::length()
{
	return count;
}

/**
 * Frees the memory of the
 * @tparam T
 */
template<typename T>
void ArrayedQueue::destroy()
{
	delete [] array;
	array = NULL;
	size = count = 0;
}