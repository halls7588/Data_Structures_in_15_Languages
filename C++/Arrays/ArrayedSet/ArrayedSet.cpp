/*******************************************************
 *  @file ArrayedSet.cpp
 *  @author Stephen Hall
 *  @date 11/22/17.
 *  @copyright 2017 Stephen Hall. All rights reserved.
 *  @details ArrayedSet implementation in C++
 ********************************************************/

/* Only needed because I did not include a file where it is defined */
#ifndef NULL
#define NULL   ((void *) 0)
#endif

#include "ArrayedSet.h"
using namespace Arrays::ArrayedSet;

/**
 * ArrayedSet default constructor
 * @tparam T: Generic type
 */
template <typename T>
ArrayedSet::ArrayedSet()
{
	array = new T[size = 4];
	for(int i = 0; i < size; i++)
		array[i] = NULL;
	count = 0;
}

/**
 * ArrayedSet Constructor initialized to given size
 * @tparam T: Generic Type
 * @param size: Size to initialize the array list to
 */
template <typename T>
ArrayedSet::ArrayedSet(unsigned int size)
{
	array =  new T[this->size = size > 0 ? size : 4];
	for(int i = 0; i < size; i++)
		array[i] = NULL;
	count = 0;
}

/**
 * Adds data into the ArrayedSet
 * @tparam T: Generic Type
 * @param data: data to add into the ArrayedSet
 * @return T: Data added into the ArrayedSet
 */
template <typename T>
T ArrayedSet::add(T data)
{
	if(data == NULL || contains(data))
		return NULL;

	if(count == size)
		resize();

	array[count] = data;
	count++;

	return data;
}

/**
 * Appends an Array of T onto the ArrayedSet
 * @tparam T: Generic Type
 * @param dataArray: array to append
 * @param size: size of the array
 * @return bool: success|fail
 */
template <typename T>
bool ArrayedSet::append(T* dataArray, int size)
{
	if(dataArray != NULL){
		for(int i = 0; i < size; i++)
		{
			if (dataArray[i] != NULL)
				add(dataArray[i]);
		}
		return true;
	}
	return false;
}

/**
 * Clears all the elements in the Array List but keeps the size intact
 * @tparam T: Generic Type
 */
template <typename T>
void ArrayedSet::clear()
{
	for(int i = 0; i < size; i++)
		array[i] = NULL;
}

/**
 * ArrayedSet Destructor
 * @tparam T: Generic Type
 */
template <typename T>
ArrayedSet::~ArrayedSet()
{
	delete [] array;
	array = NULL;
}

/**
 * Gets the number of elements in the ArrayedSet
 * @return
 */
int ArrayedSet::length()
{
	return count;
}

/**
 * Overloaded [] operator for array indexing
 * @tparam T: Generic type
 * @param index: index to access
 * @return T: data at the given index or null
 */
template <typename T>
T& ArrayedSet::operator[](int index)
{
	return (index < size) ? array[index] : NULL;
}

/**
 * Removes the first instance of the given data from the ArrayedSet
 * @tparam T: generic type
 * @param data: Data to remove
 * @return T: data removed from the ArrayedSet
 */
template <typename T>
T ArrayedSet::remove(T data)
{
	for(int i = 0; i < count; i++)
	{
		if(array[i] == data)
		{
			T tmp = array[i];
			array[i] = NULL;
			return tmp;
		}
	}
	return NULL;
}

/**
 * Resets the array to its default state
 * @tparam T: Generic Type
 */
template <typename T>
void ArrayedSet::reset()
{
	delete [] array;
	array = new T[size = 4];
	for(int i = 0; i < size; i++)
		array[i] = NULL;
	count = 0;
}

/**
 * Resize's the ArrayedSets internal array
 * @tparam T: Generic Type
 */
template <typename T>
void ArrayedSet::resize()
{
	size *= 2;
	T* tmp =  new T[size];
	for(int i = 0; i < length(); i++)
		tmp[i] = array[i];

	delete [] array;
	array = tmp;
	array = tmp;
}

/**
 * Determines if the data is contained in the ArrayedSet
 * @tparam T: Generic Type
 * @param data: data to test for
 * @return bool: yes|no
 */
template <typename T>
bool ArrayedSet::contains(T data)
{
	if(data != NULL)
		for(int i = 0; i < count; i++)
			if(array[i] == data)
				return true;
	return false;
}