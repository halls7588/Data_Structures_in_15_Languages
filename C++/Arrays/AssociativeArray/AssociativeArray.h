/*******************************************************
 *  @file AssociativeArray.h
 *  @author Stephen Hall
 *  @date 11/27/17.
 *  @copyright 2017 Stephen Hall. All rights reserved.
 *  @details Associative Array Header in C++
 ********************************************************/

#ifndef ASSOCIATIVEARRAY_H
#define ASSOCIATIVEARRAY_H

namespace Arrays::AssociativeArray
{
    /**
     * Associative Array class declaration
     * @tparam Key
     * @tparam Value
     */
	template<typename Key, typename Value>
	class AssociativeArray
	{
	  public:
		/* Public helper class */
		struct Node
		{
		  public:
			/* Public members */
			Key key;
			Value value;
			Node* next;
			int hash;

			/* public methods */
			Node(Key key, Value value, int hashCode);

			Node();
		};

	  private:
		/* Private members */
		Node **table;
		unsigned int size;
		unsigned int count;
		unsigned int (*hash)(Key);


	  public:
		/* Public Methods */
		explicit AssociativeArray(unsigned int (*hashFunction)(Key));

		explicit AssociativeArray(unsigned int size, unsigned int (*hashFunction)(Key));

		~AssociativeArray();

		Value &operator[](Key key);

		Node* set(Key key, Value value);

		Value get(Key);

		int length();

		bool isEmpty();

	  private:
		/* Private methods */
		int getBucket(int hash);

		bool equalTo(Key a, Key b);

		void destroy();

		unsigned int hashCode(Key key);
	};
}

#endif