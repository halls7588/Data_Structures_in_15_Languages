/*******************************************************
 *  @file ArrayList.cpp
 *  @author Stephen Hall
 *  @date 11/22/17.
 *  @copyright 2017 Stephen Hall. All rights reserved.
 *  @details ArrayList implementation in C++
 ********************************************************/

/* Only needed because I did not include a file where it is defined */
#ifndef NULL
#define NULL   ((void *) 0)
#endif

#include "ArrayList.h"

/**
 * ArrayList default constructor
 * @tparam T: Generic type
 */
template <typename T>
ArrayList::ArrayList()
{
    array = new T[size = 4];
    for(int i = 0; i < size; i++)
        array[i] = NULL;
    count = 0;
}

/**
 * ArrayList Constructor initialized to given size
 * @tparam T: Generic Type
 * @param size: Size to initialize the array list to
 */
template <typename T>
ArrayList::ArrayList(unsigned int size)
{
    array =  new T[this->size = size > 0 ? size : 4];
    for(int i = 0; i < size; i++)
        array[i] = NULL;
    count = 0;
}

/**
 * Adds data into the ArrayList
 * @tparam T: Generic Type
 * @param data: data to add into the ArrayList
 * @return T: Data sdded into the ArrayList
 */
template <typename T>
T ArrayList::add(T data)
{
    if(data == NULL)
        return NULL;

    if(count == size)
        resize();

    array[count] = data;
    count++;

    return data;
}

/**
 * Appends an Array of T onto the ArrayList
 * @tparam T: Generic Type
 * @param dataArray: array to append
 * @param size: size of the array
 * @return bool: success|fail
 */
template <typename T>
bool ArrayList::append(T* dataArray, int size)
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
void ArrayList::clear()
{
    for(int i = 0; i < size; i++)
        array[i] = NULL;
}

/**
 * ArrayList Destructor
 * @tparam T: Generic Type
 */
template <typename T>
ArrayList::~ArrayList()
{
    delete [] array;
    array = NULL;
}

/**
 * Gets the number of elements in the ArrayList
 * @return
 */
int ArrayList::length()
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
T& ArrayList::operator[](int index)
{
    return (index < size) ? array[index] : NULL;
}

/**
 * Removes the first instance of the given data from the ArrayList
 * @tparam T: generic type
 * @param data: Data to remove
 * @return T: data removed from the ArrayList
 */
template <typename T>
T ArrayList::remove(T data)
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
void ArrayList::reset()
{
    delete [] array;
    array = new T[size = 4];
    for(int i = 0; i < size; i++)
        array[i] = NULL;
    count = 0;
}

/**
 * Resize's the ArrayLists internal array
 * @tparam T: Generic Type
 */
template <typename T>
void ArrayList::resize()
{
    size *= 2;
    T* tmp =  new T[size];
    for(int i = 0; i < length(); i++)
        tmp[i] = array[i];

    delete [] array;
    array = tmp;
    array = tmp;
}