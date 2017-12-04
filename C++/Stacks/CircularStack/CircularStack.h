/*******************************************************
 *  @file CircularStack.h
 *  @author Stephen Hall
 *  @date 12/04/17.
 *  @copyright 2017 Stephen Hall. All rights reserved.
 *  @details Circular Stack Header in C++
 ********************************************************/

#ifndef CIRCULARSTACK_H
#define CIRCULARSTACK_H

namespace Stacks::CircularStack
{
	/**
	 * Circular Stack class declaration
	 * @tparam T: Generic Type
	 */
	template <typename T>
	class CircularStack
	{
	  private:
		/* Private members */
		int size;
		int count;
		int zeroIndex;
		T* array;

	  public:
		/* Public methods */
		CircularStack();

		explicit CircularStack(unsigned int size);

		~CircularStack();

		T push(T data);

		T pop();

		T peek();

		bool isEmpty();

		bool isFull();

		int length();

	  private:
		/* Private methods */
		void destroy();
	};
};
#endif
