'******************************************************
' *  AssociativeArray.vb
' *  Created by Stephen Hall on 11/20/17.
' *  Copyright (c) 2017 Stephen Hall. All rights reserved.
' *  Associative Array implementation in vb
' *******************************************************

Namespace Arrays.AssociativeArray
	''' <summary>
	''' Associative Array class declaration
	''' </summary>
	''' <typeparam name="TKey"></typeparam>
	''' <typeparam name="TValue"></typeparam>
	Public Class AssociativeArray(Of TKey, TValue)

		''' <summary>
		''' Node class declaration
		''' </summary>
		Public Class Node
            ''' <summary>
            ''' Public properties of the node class
            ''' </summary>
			Public Property Key As TKey
			Public Property Value As TValue
			Public Property [Next] As Node
			Public Property Hash As Integer

			''' <summary>
			''' Node class Constructor
			''' </summary>
			''' <param name="key1">key of the node</param>
			''' <param name="value2">value for node to hold</param>
			''' <param name="hash3">hash index for index</param>
			Public Sub New(key1 As TKey, value2 As TValue, hash3 As Integer)
				Key = key1
				Value = value2
				Hash = hash3
			End Sub

		End Class

		Private ReadOnly _table As Node()
		Private _size As Integer

		''' <summary>
		''' AssociativeArray class Constructor
		''' </summary>
		Public Sub New()
			_table = New Node(9) {}
			_size = 0
		End Sub

		''' <summary>
		'''  AssociativeArray class Constructo
		''' </summary>
		''' <param name="size">Size to initialize array to</param>
		Public Sub New(size As Integer)
			_table = New Node(size - 1) {}
			_size = 0
		End Sub

		''' <summary>
		''' Adds or updates Key, Value pair into the Array
		''' </summary>
		''' <param name="key">Key to associate vale with</param>
		''' <param name="value">value to store</param>
		''' <returns>Node added or updated</returns>
		Public Function [Set](key As TKey, value As TValue) As Node
			' Find the hash of the key and bucket it belongs to 
			Dim hash As Integer = key.GetHashCode()
			Dim bucket As Integer = GetBucket(hash)
			Dim entry As Node
			If IsEmpty() Then
				entry = New Node(key, value, hash)
				_table(bucket) = entry
				_size += 1
			Else
				entry = _table(bucket)
				While entry.[Next] IsNot Nothing
					If entry.GetHashCode() = hash AndAlso entry.Key.Equals(key) Then
						entry.Value = value
						Return entry
					End If
					entry = entry.[Next]
				End While

				Dim node As New Node(key, value, hash)
				entry.[Next] = node
				_size += 1
				entry = node
			End If
			Return entry
		End Function

		''' <summary>
		''' Gets the value of the given key
		''' </summary>
		''' <param name="key">Key to get value of</param>
		''' <returns>value of the key</returns>
		Public Function [Get](key As TKey) As TValue
			Dim hash As Integer = key.GetHashCode()
			Dim bucket As Integer = GetBucket(hash)

			Dim entry As Node = _table(bucket)
			While entry IsNot Nothing
				' If hash and key matches, return the value 

				If (entry.Hash = hash) AndAlso entry.Key.Equals(key) Then
					Return entry.Value
				End If
				entry = entry.[Next]
			End While
			Return Nothing
		End Function

		''' <summary>
		''' Gets the size of the array
		''' </summary>
		''' <returns>number of elements in the array</returns>
		Public Function Size() As Integer
			Return _size
		End Function

		''' <summary>
		''' Checks if the array is empty of not
		''' </summary>
		''' <returns>true|false</returns>
		Public Function IsEmpty() As Boolean
			Return _size = 0
		End Function

		''' <summary>
		''' Gets the bucket container for the internal array
		''' </summary>
		''' <param name="hash">hash to find bucket of</param>
		''' <returns>bucket index of the array</returns>
		Private Function GetBucket(hash As Integer) As Integer
			Return hash Mod _table.Length
		End Function
	End Class
End Namespace