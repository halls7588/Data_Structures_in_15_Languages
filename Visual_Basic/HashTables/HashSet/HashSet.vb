'******************************************************
' *  Hashset.cs
' *  Created by Stephen Hall on 11/17/17.
' *  Copyright (c) 2017 Stephen Hall. All rights reserved.
' *  Hashset implementation in C#
' *******************************************************

Namespace HashTables.HashSet
	''' <summary>
	''' HashSet class
	''' </summary>
	''' <typeparam name="TKey">Generic type</typeparam>
	''' <typeparam name="TValue">Generic type</typeparam>
	Public Class Hashset(Of TKey, TValue)
		''' <summary>
		''' Node class
		''' </summary>
		Public Class Node
			''' <summary>
			''' Public properties for the node class private members
			''' </summary>
			Public Property Key As TKey
			Public Property Value As TValue
			Public Property [Next] As Node
			Public Property Hash As Integer

			''' <summary>
			''' Node Constructor
			''' </summary>
			''' <param name="key1">key of the node</param>
			''' <param name="value2">value of the key</param>
			''' <param name="next3">ext node</param>
			''' <param name="hash4">nodes hash</param>
			Public Sub New(key1 As TKey, value2 As TValue, next3 As Node, hash4 As Integer)
				Key = key1
				Value = value2
				[Next] = next3
				Hash = hash4
			End Sub
		End Class

		Private _nodes As Node()

		''' <summary>
		''' HashSet Constructor
		''' </summary>
		''' <param name="size">size of the hash table</param>
		Public Sub New(size As Integer)
			_nodes = New Node(size - 1) {}
		End Sub

		''' <summary>
		''' Gets the hashed index of the give key
		''' </summary>
		''' <param name="key">key to find</param>
		''' <returns>hashed index of the key</returns>
		Private Function GetIndex(key As TKey) As Integer
			Return Math.Abs(key.GetHashCode() Mod _nodes.Length)
		End Function

		''' <summary>
		''' Inserts Key-Value pair into the table or updates new value
		''' </summary>
		''' <param name="key">key to insert</param>
		''' <param name="value">value of the key</param>
		''' <returns>new value in the set</returns>
		Public Function Insert(key As TKey, value As TValue) As TValue
			Dim hash As Integer = GetIndex(key)
			'Node node;
			' check if same key already exists and if so lets update it with the new value
			Dim node As Node = _nodes(hash)
			While node IsNot Nothing
				If (hash = node.Hash) AndAlso key.Equals(node.Key) Then
					Return Nothing
				End If
				node = node.[Next]
			End While

			Dim n As New Node(key, value, _nodes(hash), hash)
			_nodes(hash) = n
			Return value
		End Function

		''' <summary>
		''' Removes the key from the hash table
		''' </summary>
		''' <param name="key">key to remove</param>
		''' <returns>success|fail</returns>
		Public Function Remove(key As TKey) As Boolean
			Dim hash As Integer = GetIndex(key)
			Dim previous As Node = Nothing
			Dim node As Node = _nodes(hash)
			While node IsNot Nothing
				If (hash = node.Hash) AndAlso key.Equals(node.Key) Then
					If previous IsNot Nothing Then
						previous.[Next] = node.[Next]
					Else
						_nodes(hash) = node.[Next]
					End If
					Return True
				End If
				previous = node
				node = node.[Next]
			End While
			Return False
		End Function

		''' <summary>
		''' Gets the value of the given key
		''' </summary>
		''' <param name="key">key to find</param>
		''' <returns>value of the key</returns>
		Public Function [Get](key As TKey) As TValue
			Dim hash As Integer = GetIndex(key)
			Dim node As Node = _nodes(hash)

			While node IsNot Nothing
				If key.Equals(node.Key) Then
					Return node.Value
				End If
				node = node.[Next]
			End While
			Return Nothing
		End Function

		''' <summary>
		''' Resize the Hash table
		''' </summary>
		''' <param name="size">size to make the table</param>
		Public Sub Resize(size As Integer)
			Dim tbl As New Hashset(Of TKey, TValue)(size)
			For Each node As Node In _nodes
				Dim n As Node = node
				While n IsNot Nothing
					tbl.Insert(n.Key, n.Value)
					Remove(node.Key)
					n = n.[Next]
				End While
			Next
			_nodes = tbl._nodes
		End Sub
	End Class
End Namespace