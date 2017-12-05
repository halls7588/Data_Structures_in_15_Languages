/*******************************************************
 *  @file Deque.h
 *  @author Stephen Hall
 *  @date 12/05/17.
 *  @copyright 2017 Stephen Hall. All rights reserved.
 *  @details Deque Header in C++
 ********************************************************/

#ifndef DEQUE_H
#define DEQUE_H

namespace Queues::Deque
{
	/**
	 * Deque class declaration
	 * @tparam T: Generic type
	 */
	template <typename T>
	class Deque
	{
	  public:
		/**
		 * Node for Deque class
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
		Deque();

		~Deque();

		T engueue_front(T data);

		T dequeue_front();

		T engueue_back(T data);

		T dequeue_back();

		T front();

		T back();

		bool isFull();

		bool isEmpty();

		int length();

	  private:
		/* Private methods */
		void destroy();
	};
};

#endif DEQUE_H
