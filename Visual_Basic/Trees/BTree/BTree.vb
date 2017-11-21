'******************************************************
' *  BTree.vb
' *  Created by Stephen Hall on 11/21/17.
' *  Copyright (c) 2017 Stephen Hall. All rights reserved.
' *  B-Tree implementation in Visual Basic
' *******************************************************

Namespace Trees.BTree
	''' <summary>
	''' Btree class
	''' </summary>
	''' <typeparam name="TKey">Generic type</typeparam>
	''' <typeparam name="TValue">Generic type</typeparam>
	Public Class BTree(Of TKey As IComparable, TValue As IComparable)
		''' <summary>
		''' Node class
		''' </summary>
		Public Class Node
			''' <summary>
			''' public members of the node class
			''' </summary>
			Public Size As Integer
			Public Children As Entry()

			''' <summary>
			''' Node Constructor
			''' </summary>
			''' <param name="size1">number of children of the node</param>
			Public Sub New(size1 As Integer)
				Children = New Entry(Max - 1) {}
				Size = size1
			End Sub
		End Class

		''' <summary>
		''' Helper class for BTree Node class
		''' </summary>
		Public Class Entry
			''' <summary>
			''' public member of the enrty class
			''' </summary>
			Public Key As TKey
			Public Val As TValue
			Public [Next] As Node

			''' <summary>
			''' Entry class Constructor
			''' </summary>
			''' <param name="key1">key to hold</param>
			''' <param name="val2">value of the key</param>
			''' <param name="next3">next node in the tree</param>
			Public Sub New(key1 As TKey, val2 As TValue, next3 As Node)
				Key = key1
				Val = val2
				[Next] = next3
			End Sub
		End Class


		''' <summary>
		''' private member of the BTree class
		''' </summary>
		Private Const Max As Integer = 4
		' max children per B-tree node = M-1
		Private _root As Node
		Private _height As Integer
		Private _pairs As Integer
		' number of key-value pairs in the B-tree
		''' <summary>
		''' B-tree constructor
		''' </summary>
		Public Sub New()
			_root = New Node(0)
		End Sub

		''' <summary>
		''' Determines if the tree is empty
		''' </summary>
		''' <returns>true|false</returns>
		Public Function IsEmpty() As Boolean
			Return Size() = 0
		End Function

		''' <summary>
		''' Returns the number number of pars in the tree
		''' </summary>
		''' <returns>the number of key-value pairs in the tree</returns>
		Public Function Size() As Integer
			Return _pairs
		End Function

		''' <summary>
		''' Gets the height of the tree
		''' </summary>
		''' <returns>height of the tree</returns>
		Public Function Height() As Integer
			Return _height
		End Function

		''' <summary>
		''' Gets the value of the given key
		''' </summary>
		''' <param name="key">key to find value of</param>
		''' <returns>value of the given key or null</returns>
		Public Function [Get](key As TKey) As TValue
			Return If(key.CompareTo(Nothing) = 0, Nothing, Search(_root, key, _height))
		End Function

		''' <summary>
		''' Searches the tree for the key
		''' </summary>
		''' <param name="node">node to start at</param>
		''' <param name="key">key to find</param>
		''' <param name="ht">height of the tree</param>
		''' <returns>Value of the key or null</returns>
		Private Shared Function Search(node As Node, key As TKey, ht As Integer) As TValue
			Dim children As Entry() = node.Children
			' external node
			If ht = 0 Then
				For j As Integer = 0 To node.Size - 1
					If EqualTo(key, children(j).Key) Then
						Return children(j).Val
					End If
				Next
			Else
				' internal node
				For j As Integer = 0 To node.Size - 1
					If j + 1 = node.Size OrElse LessThan(key, children(j + 1).Key) Then
						Return Search(children(j).[Next], key, ht - 1)
					End If
				Next
			End If
			Return Nothing
		End Function

		''' <summary>
		''' Adds a node into the tree or replaces value
		''' </summary>
		''' <param name="key">key of the node</param>
		''' <param name="val">value of the key</param>
		Public Sub Put(key As TKey, val As TValue)
			If key.CompareTo(Nothing) = 0 Then
				Return
			End If

			Dim node As Node = Insert(_root, key, val, _height)
			_pairs += 1

			If node Is Nothing Then
				Return
			End If
			' need to split root
			Dim tmp As New Node(2)
			tmp.Children(0) = New Entry(_root.Children(0).Key, Nothing, _root)
			tmp.Children(1) = New Entry(node.Children(0).Key, Nothing, node)
			_root = tmp
			_height += 1
		End Sub

		''' <summary>
		''' Inserts a node into the tree and updates the tree
		''' </summary>
		''' <param name="node">root to start at</param>
		''' <param name="key">key of the new node</param>
		''' <param name="val">value of the key</param>
		''' <param name="ht">current height</param>
		''' <returns>node added into the tree</returns>
		Private Function Insert(node As Node, key As TKey, val As TValue, ht As Integer) As Node
			Dim j As Integer
			Dim entry As New Entry(key, val, Nothing)
			' external node
			If ht = 0 Then
				For j = 0 To node.Size - 1
					If LessThan(key, node.Children(j).Key) Then
						Exit For
					End If
				Next
			Else
				' internal node
				For j = 0 To node.Size - 1
					If j + 1 = node.Size OrElse LessThan(key, node.Children(j + 1).Key) Then
						Dim tmp As Node = Insert(node.Children(Math.Max(Threading.Interlocked.Increment(j),j - 1)).[Next], key, val, (ht - 1))

						If tmp Is Nothing Then
							Return Nothing
						End If

						entry.Key = tmp.Children(0).Key
						entry.[Next] = tmp
						Exit For
					End If
				Next
			End If

			Array.Copy(node.Children, j, node.Children, j + 1, node.Size - j)

			node.Children(j) = entry
			node.Size += 1

			If node.Size < Max Then
				Return Nothing
			End If
			Return Split(node)
		End Function

		''' <summary>
		''' Splits the given node in half
		''' </summary>
		''' <param name="node">node to split</param>
		''' <returns>split node</returns>
		Private Shared Function Split(node As Node) As Node
			Dim tmp As New Node(CType((Max / 2), Integer))
			node.Size = CType((Max / 2), Integer)
			Array.Copy(node.Children, 2, tmp.Children, 0, CType((Max / 2), Integer))
			Return tmp
		End Function

		''' <summary>
		''' Determines if a is less than b
		''' </summary>
		''' <param name="a">generic type to test</param>
		''' <param name="b">generic type to test</param>
		''' <returns>true|false</returns>
		Private Shared Function LessThan(a As TKey, b As TKey) As Boolean
			Return a.CompareTo(b) < 0
		End Function

		''' <summary>
		''' Determines if a is equal to b
		''' </summary>
		''' <param name="a">generic type to test</param>
		''' <param name="b">generic type to test</param>
		''' <returns>true|false</returns>
		Private Shared Function EqualTo(a As TKey, b As TKey) As Boolean
			Return a.CompareTo(b) = 0
		End Function
	End Class
End Namespace