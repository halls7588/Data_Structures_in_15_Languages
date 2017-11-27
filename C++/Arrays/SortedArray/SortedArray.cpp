/*******************************************************
 *  @file SortedArray.cpp
 *  @author Stephen Hall
 *  @date 11/27/17.
 *  @copyright 2017 Stephen Hall. All rights reserved.
 *  @details SortedArray implementation in C++
 ********************************************************/

/* Only needed because I did not include a file where it is defined */
#ifndef NULL
#define NULL   ((void *) 0)
#endif

#include "SortedArray.h"
using namespace Arrays::SortedArray;

/**
 * ArrayList default constructor
 * @tparam T: Generic type
 */
template <typename T>
SortedArray::SortedArray()
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
SortedArray::SortedArray(unsigned int size)
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
 * @return T: Data added into the ArrayList
 */
template <typename T>
T SortedArray::add(T data)
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
bool SortedArray::append(T* dataArray, int size)
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
void SortedArray::clear()
{
	for(int i = 0; i < size; i++)
		array[i] = NULL;
}

/**
 * ArrayList Destructor
 * @tparam T: Generic Type
 */
template <typename T>
SortedArray::~SortedArray()
{
	delete [] array;
	array = NULL;
}

/**
 * Gets the number of elements in the ArrayList
 * @return
 */
int SortedArray::length()
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
T& SortedArray::operator[](unsigned int index)
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
T SortedArray::remove(T data)
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
void SortedArray::reset()
{
	delete [] array;
	array = new T[size = 4];
	for(int i = 0; i < size; i++)
		array[i] = NULL;
	count = 0;
}

/**
 * Resize's the SortedArray internal array
 * @tparam T: Generic Type
 */
template <typename T>
void SortedArray::resize()
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
 * Performs Merge Sort on internal array
 * @tparam T: generic type
 * @return T*: sorted copy of the internal array
 */
template <typename T>
T* SortedArray::mergeSort()
{

	T* tmp = new T[count];
	for(int i = 0; i < count; i++)
		tmp[i] = array[i];
	mergeSortHelper(tmp, 0, (count - 1));
	return tmp;
}

/**
 * Performs Bubble sort on internal array
 * @tparam T: Generic type
 * @return T*: sorted copy of the internal array
 */
template <typename T>
T* SortedArray::bubbleSort()
{
	T* tmp = new T[count];
	for(int i = 0; i < count; i++)
		tmp[i] = array[i];

	for (int i = 0; i < count - 1; i++)
		for (int j = 0; j < count-i-1; j++) {
			if (greaterThan(tmp[j], tmp[j + 1])) {
				// swap temp and arr[i]
				T temp = tmp[j];
				tmp[j] = tmp[j + 1];
				tmp[j + 1] = temp;
			}
		}
	return tmp;
}

/**
 * Performs Quick Sort on the internal array
 * @tparam T: Generic type
 * @return T*: sorted copy of the internal array
 */
template <typename T>
T* SortedArray::quickSort()
{
	T* tmp = new T[count];
	for(int i = 0; i < count; i++)
		tmp[i] = array[i];
	quickSortHelper(tmp, 0, (count - 1));

	return tmp;
}

/**
 * Performs Insertion sort on the internal array
 * @tparam T: Generic type
 * @return T*: sorted copy of the internal array
 */
template <typename T>
T* SortedArray::insertionSort()
{
	T* tmp = new T[count];
	for(int i = 0; i < count; i++)
		tmp[i] = array[i];

	for (int i = 1; i < count; ++i)
	{
		T key = tmp[i];
		int j = i - 1;

		// Move elements of arr[0..i-1], that are greater than key, to one position ahead
		while (j >= 0 && lessThan(tmp[j],key))
		{
			tmp[j + 1] = tmp[j];
			j = j - 1;
		}
		tmp[j + 1] = key;
	}
	return tmp;
}

/**
 * Performs Selection Sort on internal array
 * @tparam T: Generic type
 * @return T*: Sorted copy of the internal data
 */
template <typename T>
T* SortedArray::selectionSort()
{
	T* tmp = new T[count];
	for(int i = 0; i < count; i++)
		tmp[i] = array[i];

	// One by one move boundary of unsorted sub-array
	for (int i = 0; i < count-1; i++) {
		// Find the minimum element in unsorted array
		int min = i;
		for (int j = i + 1; j < count; j++)
			if (lessThan(tmp[j],tmp[min]))
				min = j;

		// Swap the found minimum element with the first
		// element
		T temp = tmp[min];
		tmp[min] = tmp[i];
		tmp[i] = temp;
	}
	return tmp;
}

/**
 * Private helper method for Merger Sort. Merges two sub-arrays of arr[].
 * @tparam T: Generic type
 * @param arr: array to be sorted
 * @param l: index of first sub array
 * @param m: merge point
 * @param r: index of second sub array
 */
template <typename T>
void SortedArray::merge(T* arr, int l, int m, int r)
{
	int i, j, k;
	int n1 = m - l + 1;
	int n2 =  r - m;

	// create temp arrays
	T* L = new T[n1];
	T* R = new T[n2];

	// Copy data to temp arrays L[] and R[]
	for (i = 0; i < n1; i++)
		L[i] = arr[l + i];
	for (j = 0; j < n2; j++)
		R[j] = arr[m + 1+ j];

	// Merge the temp arrays back into arr[l..r]
	i = 0; // Initial index of first sub-array
	j = 0; // Initial index of second sub-array
	k = l; // Initial index of merged sub-array
	while (i < n1 && j < n2) {
		if (lessThan(L[i], R[j]) || equalTo(L[i], R[j])) {
			arr[k] = L[i];
			i++;
		}
		else {
			arr[k] = R[j];
			j++;
		}
		k++;
	}

	// Copy the remaining elements of L[], if there are any
	while (i < n1){
		arr[k] = L[i];
		i++;
		k++;
	}

	// Copy the remaining elements of R[], if there are any
	while (j < n2){
		arr[k] = R[j];
		j++;
		k++;
	}
}

/**
 * Recursive helper method for merge sort
 * @tparam T: Generic type
 * @param arr: sub array to be sorted
 * @param l: left index
 * @param r: right index
 */
template <typename T>
void SortedArray::mergeSortHelper(T* arr, int l, int r)
{
	if (l < r){
		// Same as (l+r)/2, but avoids overflow for
		// large l and h
		int m = l+(r-l)/2;

		// Sort first and second halves
		mergeSortHelper(arr, l, m);
		mergeSortHelper(arr, (m + 1), r);

		merge(arr, l, m, r);
	}
}

/**
 * Helper method for Quick Sort. Swaps data in the partition
 * @tparam T: Generic type
 * @param arr: array to be sorted
 * @param low: low index
 * @param high: high index
 * @return int: pivot index
 */
template <typename T>
int SortedArray::partition(T* arr, int low, int high)
{
	T pivot = arr[high];
	int i = (low - 1); // index of smaller element
	for (int j = low; j < high; j++) {
		// If current element is smaller than or
		// equal to pivot
		if (lessThan(arr[j], pivot) || equalTo(arr[j], pivot)) {
			i++;
			// swap arr[i] and arr[j]
			T temp = arr[i];
			arr[i] = arr[j];
			arr[j] = temp;
		}
	}

	// swap arr[i+1] and arr[high] (or pivot)
	T temp = arr[i + 1];
	arr[i + 1] = arr[high];
	arr[high] = temp;

	return i + 1;
}

/**
 * Helper method for recursive Quick Sort
 * @tparam T: Generic Type
 * @param arr: array to be sorted
 * @param low: low index
 * @param high: high index
 */
template <typename T>
void SortedArray::quickSortHelper(T* arr, int low, int high)
{
	if (low < high) {
		// pivot is partitioning index, arr[pivot] is now at right place
		int pivot = partition(arr, low, high);

		// Recursively sort elements before and after pivot
		quickSortHelper(arr, low, (pivot - 1));
		quickSortHelper(arr, (pivot + 1), high);
	}
}

/**
 * Determines if a is less than b
 * @tparam T: Generic type
 * @param a: generic type to test
 * @param b: generic type to test
 * @return boolean: true|false
 */
template <typename T>
bool SortedArray::lessThan(T a, T b)
{
	return a < b;
}

/**
 * Determines if a is equal to b
 * @tparam T: Generic type
 * @param a: generic type to test
 * @param b: generic type to test
 * @return boolean: true|false
 */
template <typename T>
bool SortedArray::equalTo(T a, T b)
{
	return a == b;
}

/**
 * Determines if a is greater  than b
 * @tparam T: Generic type
 * @param a: generic type to test
 * @param b: generic type to test
 * @return boolean: true|false
 */
template <typename T>
bool SortedArray::greaterThan(T a, T b)
{
	return a > b;
}