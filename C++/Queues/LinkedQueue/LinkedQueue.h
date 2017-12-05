/*******************************************************
 *  @file LinkedQueue.h
 *  @author Stephen Hall
 *  @date 12/05/17.
 *  @copyright 2017 Stephen Hall. All rights reserved.
 *  @details Linked Queue Header in C++
 ********************************************************/

#ifndef LINKEDQUEUE_H
#define LINKEDQUEUE_H

namespace Queues::LinkedQueue
{
	/**
	 * Linked Queue class declaration
	 * @tparam T: Generic type
	 */
	template <typename T>
	class LinkedQueue
	{
	  public:
		/**
		 * Node for Linked Queue
		 */
		struct Node
		{
		  public:
			/* Public members of the Node */
			Node* next;
			Node* previous;
			T data;

			/* Public methods of the Node */
			Node();

			explicit Node(T data);
		};

	  private:
		/* Private members */
		Node* head;
		Node* tail;
		int count;

	  public:
		/* Public methods */
		LinkedQueue();

		~LinkedQueue();

		T engueue(T data);

		T dequeue();

		T top();

		bool isFull();

		bool isEmpty();

		int length();

	  private:
		/* Private methods */
		void destroy();

	};
};

#endif
