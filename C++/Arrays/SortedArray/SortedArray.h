/*******************************************************
 *  @file SortedArray.h
 *  @author Stephen Hall
 *  @date 11/27/17.
 *  @copyright 2017 Stephen Hall. All rights reserved.
 *  @details SortedArray Header in C++
 ********************************************************/
#ifndef SORTEDARRAY_H
#define SORTEDARRAY_H

namespace Arrays::SortedArray
{
	/**
	 * ArrayList class declaration
	 * @tparam T: Generic Type
	 */
	template <typename T>
	class SortedArray {
	  private:
		/* Private members of the ArrayList class */
		T *array;
		int count;
		int size;

	  public:
		/* Public Methods of the ArrayList class */
		SortedArray();

		explicit SortedArray(unsigned int size);

		~SortedArray();

		T add(T data);

		bool append(T *dataArray, int size);

		T& operator[](unsigned int index);

		T remove(T data);

		void reset();

		void clear();

		int length();

		T* mergeSort();

		T* bubbleSort();

		T* quickSort();

		T* insertionSort();

		T* selectionSort();

	  private:
		/**
		 * Private methods of the ArrayList class
		 */
		void resize();

		void merge(T* arr, int l, int m, int r);

		void mergeSortHelper(T arr[], int l, int r);

		int partition(T* arr, int low, int high);

		void quickSortHelper(T arr[], int low, int high);

		bool lessThan(T a, T b);

		bool equalTo(T a, T b);

		bool greaterThan(T a, T b);

	};
}
#endif
