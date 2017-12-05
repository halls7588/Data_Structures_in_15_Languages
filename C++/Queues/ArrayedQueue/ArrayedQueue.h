/*******************************************************
 *  @file ArrayedQueue.h
 *  @author Stephen Hall
 *  @date 12/05/17.
 *  @copyright 2017 Stephen Hall. All rights reserved.
 *  @details Arrayed Queue Header in C++
 ********************************************************/

#ifndef ARRAYEDQUEUE_H
#define ARRAYEDQUEUE_H

namespace Queues::ArrayedQueue
{
	/**
	 * Arrayed Queue class declaration
	 * @tparam T: Generic type
	 */
	template <typename T>
	class ArrayedQueue
	{
	  private:
		/* Private members */
		T* array;
		int count;
		int size;

	  public:
		/* Public methods */
		ArrayedQueue();

		explicit ArrayedQueue(unsigned int size);

		~ArrayedQueue();

		T enqueue(T data);

		T dequeue();

		T top();

		bool isEmpty();

		bool isFull();

		int length();

	  private:
		/* Private methods */
		void destroy();
	};
};

#endif