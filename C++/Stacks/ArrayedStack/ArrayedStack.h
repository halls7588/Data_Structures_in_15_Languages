/*******************************************************
 *  @file ArrayedStack.h
 *  @author Stephen Hall
 *  @date 12/04/17.
 *  @copyright 2017 Stephen Hall. All rights reserved.
 *  @details Arrayed Stack Header in C++
 ********************************************************/

#ifndef ARRAYEDSTACK_H
#define ARRAYEDSTACK_H
namespace Stacks::ArrayedStack
{
	/**
	 * Arrayed Stack class declaration.
	 * @tparam T: Generic type
	 */
	template<typename T>
	class ArrayedStack
	{
	  private:
		/* Private members */
		T *array;
		int count;
		int size;

	  public:
		/* Public methods */
		ArrayedStack();

		explicit ArrayedStack(unsigned int size);

		~ArrayedStack();

		T push(T data);

		T pop();

		bool isEmpty();

		bool isFull();

		int length();

	  private:
		/* Private methods */
		void destroy();
	};
}
#endif
