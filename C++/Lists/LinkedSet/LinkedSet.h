/*******************************************************
 *  @file LinkedSet.h
 *  @author Stephen Hall
 *  @date 12/04/17.
 *  @copyright 2017 Stephen Hall. All rights reserved.
 *  @details Linked Set Header in C++
 ********************************************************/

#ifndef LINKEDSET_H
#define LINKEDSET_H

namespace Lists::LinkedSet
{
	/**
	 * Linked Set class declaration
	 * @tparam T: Generic type
	 */
	template <typename T>
	class LinkedSet
	{
	  public:
		struct Node
		{
		  public:
			/* Public members of the Node struct */
			T data;
			Node* next;
			Node* prevoius;

			/* Public methods of the Node struct */
			Node();

			Node(T data);
		};

	  private:
		/* Private members */
		Node* head;
		Node* tail;
		int count;

	  public:
		/* Public methods */
		LinkedSet();

		~LinkedSet();

		T add(T data);

		T remove(T data);

		Node* find(T data);

		int size();

	  private:
		/* Private methods */
		bool contains(T data);

		void destroy();

	};
};

#endif
