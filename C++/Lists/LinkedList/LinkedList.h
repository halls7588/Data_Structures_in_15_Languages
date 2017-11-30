/*******************************************************
 *  @file LinkedList.h
 *  @author Stephen Hall
 *  @date 11/30/17.
 *  @copyright 2017 Stephen Hall. All rights reserved.
 *  @details Linked List Header in C++
 ********************************************************/

#ifndef LINKEDLIST_H
#define LINKEDLIST_H

namespace Lists::LinkedList
{

/**
 * Linked List Class declaration
 * @tparam T
 */
	template<typename T>
	class LinkedList
	{
	  public:
		/**
		 * Node struct declaration for the Linked List class
		 */
		struct Node
		{
		  public:
			/* Public Member of the Node */
			Node* next;
			T data;

			/* Node constructors */
			Node();

			explicit Node(T data);
		};

	  private:
		/* Private Members */
		int count;
		Node* head;
		Node* tail;

	  public:
		/* Public Methods */
		LinkedList();

		~LinkedList();

		bool add(T data);

		bool remove(T data);

		Node find(T data);

		int size();

	  private:
		/* Private methods */
		void destroy();
	};
}
#endif
