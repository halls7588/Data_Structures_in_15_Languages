/*******************************************************
 *  @file DoublyLinkedList.h
 *  @author Stephen Hall
 *  @date 11/30/17.
 *  @copyright 2017 Stephen Hall. All rights reserved.
 *  @details Doubly Linked List Header in C++
 ********************************************************/

#ifndef DOUBLYLINKEDLIST_H
#define DOUBLYLINKEDLIST_H
namespace Lists::DoublyLinkedList
{
	/**
	 * Double Linked List class declaration
	 * @tparam T: Generic type
	 */
	template<typename T>
	class DoublyLinkedList
	{
	  public:
		/**
		 * Node struct for doubly linked list
		 */
		struct Node
		{
		  public:
			/* public members of the Node struct */
			T data;
			Node *next;
			Node *previous;

			/* Public methods of the Node struct */
			Node();

			explicit Node(T data);

		};

	  private:
		/* Private members */
		int count;
		Node *head;
		Node *tail;

	  public:
		/* Public methods */
		DoublyLinkedList();

		~DoublyLinkedList();

		bool add(T data);

		bool remove(T data);

		Node find(T data);

		int size();

	  private:
		/* Private methods */
		void destroy();
	};
};
#endif DOUBLYLINKEDLIST_H
