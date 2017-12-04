/*******************************************************
 *  @file LinkedStack.h
 *  @author Stephen Hall
 *  @date 12/04/17.
 *  @copyright 2017 Stephen Hall. All rights reserved.
 *  @details Linked Stack Header in C++
 ********************************************************/
#ifndef LINKEDSTACK_H
#define LINKEDSTACK_H

namespace Stacks::LinkedStack
{
	/**
	 * Linked Stack class declaration
	 * @tparam T: generic type
	 */
	template <typename T>
	class LinkedStack
	{
	  public:
		/**
		 * Public Node struct of the Linked Stack Class
		 */
		struct Node
		{
		  public:
			/* Public members of the Node */
			T data;
			Node* next;
			Node* previous;

			/* Public methods of the Node */
			Node();

			Node(T data);
		};

	  private:
		/* Private members od the Linked Stack */
		Node* head;
		Node* tail;
		int count;

	  public:
		/* Linked Stack public methods */
		LinkedStack();

		~LinkedStack();

		T push(T data);

		T pop();

		T peek();

		bool isFull();

		bool isEmpty();

		int length();

	  private:
		/* Private methods */
		void destroy();
	};
}

#endif
