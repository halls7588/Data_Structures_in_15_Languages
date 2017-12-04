/*******************************************************
 *  @file SkipList.h
 *  @author Stephen Hall
 *  @date 12/04/17.
 *  @copyright 2017 Stephen Hall. All rights reserved.
 *  @details Skip List Header in C++
 ********************************************************/

#ifndef SKIPLIST_H
#define SKIPLIST_H

#include <vector>

namespace Lists::SkipList
{
	/**
	 * Skip List class declaration
	 * @tparam T
	 */
	template<typename T>
	class SkipList
	{
	  public:
		/**
		 * Node struct for Skip List declaration
		 */
		struct Node
		{
		  public:
			/* Public members of the node */
			T data;
			std::vector<T>* nodeList;

			/* Public methods of the Node */
			Node();

			explicit Node(T data);

			~Node();

			int level();
		};

	  private:
		/* Private members */
		Node* head;
		int max;
		int size;
		static const double PROBABILITY = 0.5;

	  public:
		/* Public methods */
		SkipList();

		~SkipList();

		bool add(T data);

		Node* find(T data);

		int length();

		bool contains(T data);

	  private:
		/* Private methods */
		void destroy();

		Node* findNext(T data, Node* current, int level);

		Node* find(T data, Node* current, int level);
	};
}
#endif
