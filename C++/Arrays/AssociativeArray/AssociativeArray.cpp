/*******************************************************
 *  @file AssociativeArray.cpp
 *  @author Stephen Hall
 *  @date 11/27/17.
 *  @copyright 2017 Stephen Hall. All rights reserved.
 *  @details AssociativeArray implementation in C++
 ********************************************************/

#include <iostream>
#include "AssociativeArray.h"
using namespace Arrays::AssociativeArray;
using namespace std;

/**
 * Associative Array Node constructor
 */
AssociativeArray::Node::Node()
{
	key = NULL;
	value = NULL;
	hash = 0;
	next = NULL;
}

/**
 * Associative Array Node constructor
 * @tparam Key: Generic Type
 * @tparam Value: Generic Type
 * @param key: key of the node
 * @param value: value of the key
 * @param hash: bucket index of the node
 */
template<typename Key, typename Value>
AssociativeArray::Node::Node(Key key, Value value, int hashCode)
{
	this->key = key;
	this->hash = hashCode;
	this->value = value;
	this->next = NULL;
}

/**
 * Associative Array class constructor
 * @tparam Key: Generic type
 * @param hashFunction: Function pointer to user defined hash function for type Key
 */
template<typename Key>
AssociativeArray::AssociativeArray(unsigned int (*hashFunction)(Key))
{
	table = new Node*[size = 10];
	this->hash = hashFunction;
}

/**
 * Associative Array class constructor
 * @tparam Key: Generic type
 * @param size: Size to initialize the array to
 * @param hashFunction: function pointer to user defined hash function for type Key
 */
template<typename Key>
AssociativeArray::AssociativeArray(unsigned int size, unsigned int (*hashFunction)(Key))
{
	table = new Node*[this->size = size];
	this->hash = hashFunction;
}

/**
 * Associative Array class destructor
 */
AssociativeArray::~AssociativeArray()
{
	destroy();
}

/**
 * Overloaded operator to act as an array
 * @tparam Key: Generic type
 * @tparam Value: Generic Type
 * @param key: Key to find
 * @return Value: Value of the key
 */
template<typename Key, typename Value>
Value& AssociativeArray::operator[](Key key)
{
	unsigned int hash = this->hash(key) % size;
	Node* tmp = table[hash];
	while(tmp != NULL)
	{
		if(tmp->key == key)
			return tmp->value;
	}
	return NULL;
}

/**
 * Adds or updates Key, Value pair into the Array
 * @tparam Key: Generic Type
 * @tparam Value: Generic Type
 * @param key: Key to add into the array
 * @param value: Value of the key
 * @return Node*: New node added into the array
 */
template<typename Key, typename Value>
Node* AssociativeArray::set(Key key, Value value)
{
	int hash = hashCode(key);
	int bucket = getBucket(hash);
	Node* entry;
	if(isEmpty()){
		entry = new Node(key, value, hash);
		table[bucket] = entry;
		count++;
	}
	else {
		entry = table[bucket];
		while (entry->next != NULL) {
			/* Key exists, update value */
			if (hashCode(entry->key) == hash && equalTo(entry->key, key)) {
				entry->value = value;
				return entry;
			}
			entry = entry->next;
		}
		/* Add new key value pair into the array */
		Node* node = new Node(key, value, hash);
		entry->next = node;
		count++;
		entry = node;
	}
	return entry;
}

/**
 * Keys the value of the given key
 * @tparam Key: Generic Type
 * @tparam Value: Generic Type
 * @param key: Key to find
 * @return Value: value of the key
 */
template<typename Key, typename Value>
Value AssociativeArray::get(Key key)
{
	int hash = hashCode(key);
	int bucket = getBucket(hash);

	Node* entry = table[bucket];
	while (entry != NULL) {
		/* If hash and key matches, return the value */
		if ((entry->hash == hash) && equalTo(entry->key, key)) {
			return entry->value;
		}
		entry = entry->next;
	}
	return NULL;
}

/**
 * returns the number of elements in the array
 * @return int: number of elements in the array
 */
int AssociativeArray::length()
{
	return count;
}

/**
 * determins if the array is empty
 * @return bool: true|false
 */
bool AssociativeArray::isEmpty()
{
	return size == 0;
}

/**
 * Gets the bucket index of the given hash
 * @param hash: Hash to index
 * @return int: bucket index of the hash
 */
int AssociativeArray::getBucket(int hash)
{

	return hash % size;
}

/**
 * Determines if a is equal to b
 * @tparam Key: Generic type
 * @param a: key to test
 * @param b: second key to test
 * @return bool: true|false
 */
template<typename Key>
bool AssociativeArray::equalTo(Key a, Key b)
{
	return a == b;
}

/**
 * Frees the memory dynamically allocated
 */
void AssociativeArray::destroy()
{
	Node* tmp;
	Node* tmp2;
	for(int i = 0; i < size; i++)
	{
		tmp = table[i];
		tmp2 = tmp;
		while(tmp2 != NULL)
		{
			tmp2 = tmp2->next;
			delete tmp;
			tmp = tmp2;
		}
	}
	delete [] table;
	table = NULL;
}

/**
 * Calls the function pointer for hash code give at time of creation
 * @tparam Key: Generic Type
 * @param key: Key to hash
 * @return unsigned int: Hash of the key
 */
template<typename Key>
unsigned int AssociativeArray::hashCode(Key key)
{
	return this->hash(key);
}
