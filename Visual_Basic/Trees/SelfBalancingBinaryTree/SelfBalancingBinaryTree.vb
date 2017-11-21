'*************************************************************
' *  SelfBalancingBinaryTree.vb
' *  Created by Stephen Hall on 11/21/17.
' *  Copyright (c) 2017 Stephen Hall. All rights reserved.
' *  Self Balancing Binary Tree implementation in Visual Basic
' *************************************************************

Namespace Trees.SelfBalancingBinaryTree
	''' <summary>
	''' Self Balancing Binary Tree Class
	''' </summary>
	''' <typeparam name="T">Generic Type</typeparam>
	Public Class SelfBalancingBinaryTree(Of T As IComparable)
		''' <summary>
		''' Node class for SelfBalancingBinaryTree
		''' </summary>
		Public Class Node
			''' <summary>
			''' public members of the node class
			''' </summary>
			Public Property Left As Node
			Public Property Right As Node
			Public Property Data As T
			Public Property Height As Integer

			''' <summary>
			''' Node Constructor
			''' </summary>
			Public Sub New()
				Left = Nothing
				Right = Nothing
				Data = Nothing
				Height = 0
			End Sub

			''' <summary>
			''' Node Constructor
			''' </summary>
			''' <param name="data1">Data for the node to hold</param>
			Public Sub New(data1 As T)
				Left = Nothing
				Right = Nothing
				Data = data1
				Height = 0
			End Sub
		End Class

		''' <summary>
		''' Private members
		''' </summary>
		Private _root As Node
		Private _count As Integer

		''' <summary>
		''' SelfBalancingBinaryTree Constructor
		''' </summary>
		Public Sub New()
			_root = Nothing
			_count = 0
		End Sub

		''' <summary>
		''' Insert data into the tree
		''' </summary>
		''' <param name="data">data to insert</param>
		Public Sub Insert(data As T)
			_root = Insert(data, _root)
		End Sub

		''' <summary>
		''' Inserts data into the tree
		''' </summary>
		''' <param name="data">data to insert</param>
		''' <param name="node">node to start at</param>
		''' <returns>new root</returns>
		Private Function Insert(data As T, node As Node) As Node
			If node Is Nothing Then
				node = New Node(data)
			ElseIf LessThan(data, node.Data) Then
				node.Left = Insert(data, node.Left)
				If Height(node.Left) - Height(node.Right) = 2 Then
					node = If(LessThan(data, node.Left.Data), RotateLeft(node), DoubleLeft(node))
				End If
			ElseIf GreaterThan(data, node.Data) Then
				node.Right = Insert(data, node.Right)
				If Height(node.Right) - Height(node.Left) = 2 Then
					node = If(GreaterThan(data, node.Right.Data), RotateRight(node), DoubleRight(node))
				End If
			End If
			node.Height = Max(Height(node.Left), Height(node.Right)) + 1
			_count += 1
			Return node
		End Function

		''' <summary>
		''' Rotates the given root node left
		''' </summary>
		''' <param name="node">node to rotate</param>
		''' <returns>new root</returns>
		Private Function RotateLeft(node As Node) As Node
			Dim tmp As Node = node.Left
			node.Left = tmp.Right
			tmp.Right = node
			node.Height = Max(Height(node.Left), Height(node.Right)) + 1
			tmp.Height = Max(Height(tmp.Left), node.Height) + 1
			Return tmp
		End Function

		''' <summary>
		''' Rotates the given root node right
		''' </summary>
		''' <param name="node">node to rotate</param>
		''' <returns>new root</returns>
		Private Function RotateRight(node As Node) As Node
			Dim tmp As Node = node.Right
			node.Right = tmp.Left
			tmp.Left = node
			node.Height = Max(Height(node.Left), Height(node.Right)) + 1
			tmp.Height = Max(Height(tmp.Right), node.Height) + 1
			Return tmp
		End Function

		''' <summary>
		''' Rotates the left child right than the root node left
		''' </summary>
		''' <param name="node">root node to rotate</param>
		''' <returns>new root</returns>
		Private Function DoubleLeft(node As Node) As Node
			node.Left = RotateRight(node.Left)
			Return RotateLeft(node)
		End Function

		''' <summary>
		''' rotates the right child left than the root right
		''' </summary>
		''' <param name="node">node to rotate</param>
		''' <returns>new root</returns>
		Private Function DoubleRight(node As Node) As Node
			node.Right = RotateLeft(node.Right)
			Return RotateRight(node)
		End Function

		''' <summary>
		''' Gets the size of the tree
		''' </summary>
		''' <returns>number of node in the tree</returns>
		Public Function Size() As Integer
			Return _count
		End Function

		''' <summary>
		''' Gets the height of the node
		''' </summary>
		''' <param name="node">node to test</param>
		''' <returns>height of the node</returns>
		Private Function Height(node As Node) As Integer
			Return If(node Is Nothing, -1, node.Height)
		End Function

		''' <summary>
		''' Function to get the max of left/right node
		''' </summary>
		''' <param name="left">left element to test</param>
		''' <param name="right">right element to test</param>
		''' <returns>the max of given left and right</returns>
		Private Shared Function Max(left As Integer, right As Integer) As Integer
			Return If(left > right, left, right)
		End Function

		''' <summary>
		''' check if tree is empty
		''' </summary>
		''' <returns>rue|false</returns>
		Public Function IsEmpty() As Boolean
			Return _root Is Nothing
		End Function

		''' <summary>
		''' Clears the data from the tree
		''' </summary>
		Public Sub Clear()
			_root = Nothing
		End Sub

		''' <summary>
		''' Determines if a is less than b
		''' </summary>
		''' <param name="a">generic type to test</param>
		''' <param name="b">generic type to test</param>
		''' <returns>true|false</returns>
		Private Shared Function LessThan(a As T, b As T) As Boolean
			Return a.CompareTo(b) < 0
		End Function

		''' <summary>
		''' Determines if a is greater than b
		''' </summary>
		''' <param name="a">generic type to test</param>
		''' <param name="b">generic type to test</param>
		''' <returns>true|false</returns>
		Private Shared Function GreaterThan(a As T, b As T) As Boolean
			Return a.CompareTo(b) > 0
		End Function
	End Class
End Namespace