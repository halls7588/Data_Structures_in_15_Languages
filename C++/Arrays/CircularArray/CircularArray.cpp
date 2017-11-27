/*******************************************************
 *  @file CircularArray.cpp
 *  @author Stephen Hall
 *  @date 11/27/17.
 *  @copyright 2017 Stephen Hall. All rights reserved.
 *  @details Circular Array implementation in C++
 ********************************************************/

/* Only needed because I did not include a file where it is defined */
#ifndef NULL
#define NULL   ((void *) 0)
#endif

#include <iostream>
#include "CircularArray.h"
using namespace Arrays::CircularArray;

/**
 * Circular Array default constructor
 * @tparam T: generic type
 */
template <typename T>
CircularArray::CircularArray()
{
	count = zeroIndex = 0;
	array = new T[size = 4];
}

/**
 * Circular array constructor initialized to given size
 * @tparam T: Generic Type
 * @param size: size to initialize the array to
 */
template <typename T>
CircularArray::CircularArray(unsigned int size)
{
	if(size == 0)
		size = 4;

	count = zeroIndex = 0;
	array = new T[this->size = size];
}

/**
 * Circular Array destructor
 */
CircularArray::~CircularArray()
{
	destroy();
}

/**
 * Adds the given data into in array
 * @tparam T: generic type
 * @param data: Data to add into the array
 * @return T: copy of the data added into the array
 */
template <typename T>
T CircularArray::add(T data)
{
	/* resize if capacity has been reached */
	if(count >= size - 1)
		resize();

	/* get the next available index and add the data into it */
	int tmp = (zeroIndex + count) % size;
	array[tmp] = data;
	count++;
	return array[tmp];
}

/**
 * Overloaded operator to act like an array
 * @tparam T: generic type
 * @param index: index to access
 * @return T&: data at the given index
 */
template <typename T>
T& CircularArray::operator[](unsigned int index)
{
	/* Get a reference of the data at the given index */
	return (index < size) ? array[(index + zeroIndex % size)] : NULL;
}

/**
 * Removes the data at the given index
 * @tparam T: Generic type
 * @param index: Index to remove
 * @return T: data removed from the array
 */
template <typename T>
T CircularArray::remove(int index)
{
	/* can not remove something out of bounds */
	if (index > size)
		return NULL;

	/* Get a copy of the data and replace with the data at the zeroIndex */
	T tmp = array[(index + zeroIndex % size)];
	array[(index + zeroIndex % size)] = array[zeroIndex];
	array[zeroIndex] = NULL;
	count--;
	zeroIndex = (zeroIndex + 1) % size;
	/* return the copy of the data */
	return tmp;
}

/**
 * Returns the number of elements in the array
 * @return int: number of elements in the array
 */
int CircularArray::length()
{
	return count;
}

/**
 * Prints the array out to the console
 */
void CircularArray::print()
{
	for(int i = 0; i < count; i++)
		std::cout << (array[(zeroIndex + i % size)]) << "\n";

}

/**
 * Doubles the size of teh internal array
 * @tparam T: generic type
 */
template <typename T>
void CircularArray::resize()
{
	/* Create a larger array and copy the data over */
	T* arr = new T[size * 2];
	for(int i = 0; i < count; i++)
		arr[i] = array[(zeroIndex + i) % size];

	delete [] array;
	size *= 2;
	zeroIndex = 0;
	array = arr;
	arr = NULL;
}

/**
 * Frees the memory allocated by the Circular Array Class
 */
void CircularArray::destroy()
{
	delete [] array;
	array = NULL;
	count = size = zeroIndex = 0;
}