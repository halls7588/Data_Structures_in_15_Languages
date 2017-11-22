/*******************************************************
 *  @file ArrayedSet.h
 *  @author Stephen Hall
 *  @date 11/22/17.
 *  @copyright 2017 Stephen Hall. All rights reserved.
 *  @details ArrayedSet Header in C++
 ********************************************************/

#ifndef ARRAYEDSET_H
#define ARRAYEDSET_H

namespace  Arrays::ArrayedSet
{
	/**
	 * ArrayedSet Class declaration
	 * @tparam T: Generic Type
	 */
	template <typename T>
	class ArrayedSet
	{
	  private:
		/* Private members of the ArrayedSet class */
		T *array;
		int count;
		int size;

	  public:
		/* Public Methods of the ArrayedSet class */
		ArrayedSet();

		explicit ArrayedSet(unsigned int size);

		~ArrayedSet();

		T add(T data);

		bool append(T *dataArray, int size);

		T &operator[](int index);

		T remove(T data);

		void reset();

		void clear();

		int length();

		bool contains(T data);

	  private:
		/**
		 * Private methods of the ArrayedSet class
		 */
		void resize();

	};
}

#endif ARRAYEDSET_H
