'******************************************************
' *  SplayTree.vb
' *  Created by Stephen Hall on 11/21/17.
' *  Copyright (c) 2017 Stephen Hall. All rights reserved.
' *  Splay Tree implementation in Visual Basic
' *******************************************************

Namespace Trees.SplayTree
	''' <summary>
	''' Splay Tree Class
	''' </summary>
	''' <typeparam name="TKey">generic type</typeparam>
	''' <typeparam name="TValue">generic type</typeparam>
	Public Class SplayTree(Of TKey As IComparable, TValue)
		''' <summary>
		''' Node class for splay tree
		''' </summary>
		Public Class Node
			''' <summary>
			''' public properties for the node class
			''' </summary>
			Public Property Key As TKey
			Public Property Value As TValue
			Public Property Left As Node
			Public Property Right As Node

			''' <summary>
			''' Node Constructor
			''' </summary>
			''' <param name="key1">key to the node</param>
			''' <param name="value2">value of the node</param>
			Public Sub New(key1 As TKey, value2 As TValue)
				Key = key1
				Value = value2
			End Sub
		End Class

		''' <summary>
		''' private members
		''' </summary>
		Private _root As Node

		''' <summary>
		''' Tests to see if a key exist in the tree
		''' </summary>
		''' <param name="key">key to test</param>
		''' <returns>yes|no</returns>
		Public Function Contains(key As TKey) As Boolean
			Return [Get](key) IsNot Nothing
		End Function

		''' <summary>
		''' Gets the value of the key if it exists
		''' </summary>
		''' <param name="key">Key to test</param>
		''' <returns>Value of the Key</returns>
		Public Function [Get](key As TKey) As TValue
			_root = Splay(_root, key)
			Return If(key.CompareTo(_root.Key) = 0, _root.Value, Nothing)
		End Function

		''' <summary>
		''' adds or updates an item into the tree
		''' </summary>
		''' <param name="key">key to insert</param>
		''' <param name="value">value of the key</param>
		Public Sub Put(key As TKey, value As TValue)
			' splay key to root
			If _root Is Nothing Then
				_root = New Node(key, value)
				Return
			End If
			_root = Splay(_root, key)
			Dim tmp As Integer = key.CompareTo(_root.Key)
			' Insert new node at root
			If tmp < 0 Then
				Dim node As New Node(key, value)
				node.Left = _root.Left
				node.Right = _root
				_root.Left = Nothing
				_root = node
			' Insert new node at root
			ElseIf tmp > 0 Then
				Dim node As New Node(key, value)
				node.Right = _root.Right
				node.Left = _root
				_root.Right = Nothing
				_root = node
			Else
				' Duplicate key. Update value
				_root.Value = value
			End If

		End Sub

		''' <summary>
		''' Removes a node from the tree
		''' </summary>
		''' <param name="key">key to remove</param>
		Public Sub Remove(key As TKey)
			If _root Is Nothing Then
				Return
			End If

			_root = Splay(_root, key)

			Dim tmp As Integer = key.CompareTo(_root.Key)

			If tmp = 0 Then
				If _root.Left Is Nothing Then
					_root = _root.Right
				Else
					Dim node As Node = _root.Right
					_root = _root.Left
					Splay(_root, key)
					_root.Right = node
				End If
			End If
		End Sub

		''' <summary>
		''' Rotates the given node to the right
		''' </summary>
		''' <param name="node">node to rotate</param>
		''' <returns>new root of the rotate</returns>
		Private Shared Function RotateRight(node As Node) As Node
			Dim tmp As Node = node.Left
			node.Left = tmp.Right
			tmp.Right = node
			Return tmp
		End Function

		''' <summary>
		''' Rotates the node to the left
		''' </summary>
		''' <param name="node">node to rotate</param>
		''' <returns>new root of the rotate</returns>
		Private Shared Function RotateLeft(node As Node) As Node
			Dim tmp As Node = node.Right
			node.Right = tmp.Left
			tmp.Left = node
			Return tmp
		End Function

		''' <summary>
		''' Splays the node containing key to the root node given
		''' </summary>
		''' <param name="node">root to splay to</param>
		''' <param name="key">key to find</param>
		''' <returns>Node found or null</returns>
		Private Shared Function Splay(node As Node, key As TKey) As Node
			If node Is Nothing Then
				Return Nothing
			End If

			Dim tmp As Integer = key.CompareTo(node.Key)

			If tmp < 0 Then
				' key not in tree, so we're done
				If node.Left Is Nothing Then
					Return node
				End If
				Dim tmp2 As Integer = key.CompareTo(node.Left.Key)
				If tmp2 < 0 Then
					node.Left.Left = Splay(node.Left.Left, key)
					node = RotateRight(node)
				ElseIf tmp2 > 0 Then
					node.Left.Right = Splay(node.Left.Right, key)
					If node.Left.Right IsNot Nothing Then
						node.Left = RotateLeft(node.Left)
					End If
				End If

				Return If(node.Left Is Nothing, node, RotateRight(node))
			End If
			If tmp > 0 Then
				' key not in tree, so we're done
				If node.Right Is Nothing Then
					Return node
				End If
				Dim tmp2 As Integer = key.CompareTo(node.Right.Key)
				If tmp2 < 0 Then
					node.Right.Left = Splay(node.Right.Left, key)

					If node.Right.Left IsNot Nothing Then
						node.Right = RotateRight(node.Right)
					End If
				ElseIf tmp2 > 0 Then
					node.Right.Right = Splay(node.Right.Right, key)
					node = RotateLeft(node)
				End If
				Return If(node.Right Is Nothing, node, RotateLeft(node))
			End If

			Return node
		End Function

		''' <summary>
		''' Gets the height of the tree
		''' </summary>
		''' <returns>height of the tree</returns>
		Public Function Height() As Integer
			Return Height(_root)
		End Function

		''' <summary>
		''' Gets the height of the given node
		''' </summary>
		''' <param name="node">node to test</param>
		''' <returns>height of the node</returns>
		Private Shared Function Height(node As Node) As Integer
			Return If((node Is Nothing), -1, Math.Max(Height(node.Left), Height(node.Right)) + 1)
		End Function

		''' <summary>
		''' Gets the size of the tree
		''' </summary>
		''' <returns>size of the tree</returns>
		Public Function Size() As Integer
			Return Size(_root)
		End Function

		''' <summary>
		''' Gets the size of the given node
		''' </summary>
		''' <param name="node">noDe to test</param>
		''' <returns>size of the node</returns>
		Private Shared Function Size(node As Node) As Integer
			Return If((node Is Nothing), 0, Size(node.Left) + Size(node.Right) + 1)
		End Function
	End Class
End Namespace