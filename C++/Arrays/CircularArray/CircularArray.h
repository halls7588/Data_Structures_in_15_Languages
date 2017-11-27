/*******************************************************
 *  @file CircularArray.h
 *  @author Stephen Hall
 *  @date 11/27/17.
 *  @copyright 2017 Stephen Hall. All rights reserved.
 *  @details Circular Array Header in C++
 ********************************************************/

#ifndef CIRCULARARRAY_H
#define CIRCULARARRAY_H

namespace Arrays::CircularArray
{
/**
 * Circular Array Class declaration
 * @tparam T: Generic Type
 */
	template<typename T>
	class CircularArray
	{
	  private:
		/* Private members */
		T *array;
		int count;
		int size;
		int zeroIndex;

	  public:
		/* Public methods */
		CircularArray();

		explicit CircularArray(unsigned int size);

		~CircularArray();

		T add(T data);

		T &operator[](unsigned int index);

		T remove(int index);

		int length();

		void print();

	  private:
		/* Private methods */
		void resize();

		void destroy();
	};
}

#endif