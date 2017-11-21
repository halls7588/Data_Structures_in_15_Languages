'******************************************************
' *  AvlTree.cs
' *  Created by Stephen Hall on 11/20/17.
' *  Copyright (c) 2017 Stephen Hall. All rights reserved.
' *  AVL Tree implementation in C#
' *******************************************************

Namespace Trees.AVLTree
	''' <summary>
	''' AVL Tree Class
	''' </summary>
	''' <typeparam name="T">Generic Type</typeparam>
	Public Class AvlTree(Of T As IComparable)
		''' <summary>
		''' Node class for AVL Tree
		''' </summary>
		Public Class Node
			''' <summary>
			''' public accessors of the node class
			''' </summary>
			Public Property Data As T
            Public Property Left As Node
			Public Property Right As Node
			Public Property Height As Integer

			''' <summary>
			''' Default Node constructor
			''' </summary>
			''' <param name="data1">data for the node</param>
			Public Sub New(data1 As T)
				Data = data1
				Left = InlineAssignHelper(Right, Nothing)
			End Sub

			''' <summary>
			''' Node constructor with left and right leafs
			''' </summary>
			''' <param name="data1">data to hold</param>
			''' <param name="left2">left child</param>
			''' <param name="right3">right child</param>
			Public Sub New(data1 As T, left2 As Node, right3 As Node)
				Data = data1
				Left = left2
				Right = right3
			End Sub
			Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
				target = value
				Return value
			End Function
		End Class

		''' <summary>
		''' Private members of the AVL Tree class
		''' </summary>
		Private _root As Node
		Private _count As Integer

		''' <summary>
		''' AVLTree Constructor.
		''' </summary>
		Public Sub New()
			_root = Nothing
			_count = 0
		End Sub

		''' <summary>
		'''  Gets the height of a node
		''' </summary>
		''' <param name="node">node to test</param>
		''' <returns>height of the node</returns>
		Public Function Height(node As Node) As Integer
			Return If(node Is Nothing, -1, node.Height)
		End Function

		''' <summary>
		''' Find the max value among the given numbers.
		''' </summary>
		''' <param name="a">First number</param>
		''' <param name="b">Second number</param>
		''' <returns>Maximum value</returns>
		Public Function Max(a As Integer, b As Integer) As Integer
			Return If(a > b, a, b)
		End Function

		''' <summary>
		''' Insert an element into the tree.
		''' </summary>
		''' <param name="data">data to insert into the tree</param>
		''' <returns>success|fail</returns>
		Public Function Insert(data As T) As Boolean
			Try
				_root = Insert(data, _root)
				_count += 1
				Return True
			Catch generatedExceptionName As Exception
				Return False
			End Try
		End Function

		''' <summary>
		''' Private Insert Helper
		''' </summary>
		''' <param name="data">data to add</param>
		''' <param name="node">Root of the tree</param>
		''' <returns>New root of the tree</returns>
		''' <exception cref="Exception">failure or duplicate value</exception>
		Private Function Insert(data As T, node As Node) As Node
			If node Is Nothing Then
				node = New Node(data)
			ElseIf data.CompareTo(node.Data) < 0 Then
				node.Left = Insert(data, node.Left)
				If Height(node.Left) - Height(node.Right) = 2 Then
					If data.CompareTo(node.Left.Data) < 0 Then
						node = RotateLeft(node)
					Else
						node = RotateRightLeft(node)
					End If
				End If
			ElseIf data.CompareTo(node.Data) > 0 Then
				node.Right = Insert(data, node.Right)

				If Height(node.Right) - Height(node.Left) = 2 Then
					If data.CompareTo(node.Right.Data) > 0 Then
						node = RotateRight(node)
					Else
						node = RotateLeftRight(node)
					End If
				End If
			Else
				Throw New Exception("Attempting to insert duplicate value")
			End If
			node.Height = Max(Height(node.Left), Height(node.Right)) + 1
			Return node
		End Function

		''' <summary>
		''' Rotates Node left
		''' </summary>
		''' <param name="node">node to rotate</param>
		''' <returns>new root node</returns>
		Private Function RotateLeft(node As Node) As Node
			Dim tmp As Node = node.Left
			node.Left = tmp.Right
			tmp.Right = node
			node.Height = Max(Height(node.Left), Height(node.Right)) + 1
			tmp.Height = Max(Height(tmp.Left), node.Height) + 1
			Return tmp
		End Function

		''' <summary>
		''' Rotate left child Right then rotate left
		''' </summary>
		''' <param name="node">node to rotate</param>
		''' <returns>new root node</returns>
		Private Function RotateRightLeft(node As Node) As Node
			node.Left = RotateRight(node.Left)
			Return RotateLeft(node)
		End Function

		''' <summary>
		''' Rotate Right
		''' </summary>
		''' <param name="node">node to rotate</param>
		''' <returns>new root node</returns>
		Private Function RotateRight(node As Node) As Node
			Dim tmp As Node = node.Right
			node.Right = tmp.Left
			tmp.Left = node
			node.Height = Max(Height(node.Left), Height(node.Right)) + 1
			tmp.Height = Max(Height(tmp.Right), node.Height) + 1
			Return tmp
		End Function

		''' <summary>
		''' Rotate right child left then rotate right
		''' </summary>
		''' <param name="node">node to rotate</param>
		''' <returns>new root node</returns>
		Private Function RotateLeftRight(node As Node) As Node
			node.Right = RotateLeft(node.Right)
			Return RotateRight(node)
		End Function

		''' <summary>
		''' Deletes all nodes from the tree.
		''' </summary>
		Public Sub MakeEmpty()
			_root = Nothing
		End Sub

		''' <summary>
		''' Determine if the tree is empty.
		''' </summary>
		''' <returns>True if the tree is empty</returns>
		Public Function IsEmpty() As Boolean
			Return _root Is Nothing
		End Function

		''' <summary>
		''' Find the smallest item in the tree.
		''' </summary>
		''' <returns>smallest item or null if empty.</returns>
		Public Function FindMin() As T
			Return If((IsEmpty()), Nothing, FindMin(_root).Data)
		End Function

		''' <summary>
		''' Find the largest item in the tree.
		''' </summary>
		''' <returns>the largest item or null if empty.</returns>
		Public Function FindMax() As T
			Return If((IsEmpty()), Nothing, FindMax(_root).Data)
		End Function

		''' <summary>
		''' Find min helper
		''' </summary>
		''' <param name="node">root to test</param>
		''' <returns>node containing the smallest item.</returns>
		Private Function FindMin(node As Node) As Node
			If node Is Nothing Then
				Return Nothing
			End If

			While node.Left IsNot Nothing
				node = node.Left
			End While

			Return node
		End Function

		''' <summary>
		''' Find max helper
		''' </summary>
		''' <param name="node">root to test.</param>
		''' <returns>node containing the largest item.</returns>
		Private Function FindMax(node As Node) As Node
			If node Is Nothing Then
				Return Nothing
			End If

			While node.Right IsNot Nothing
				node = node.Right
			End While

			Return node
		End Function

		''' <summary>
		''' Removes an item from the tree.
		''' </summary>
		''' <param name="data">item to remove.</param>
		Public Sub Remove(data As T)
			_root = Remove(data, _root)
		End Sub

		''' <summary>
		''' Recursive Remove helper function
		''' </summary>
		''' <param name="data">data to remove</param>
		''' <param name="node">root to start at</param>
		''' <returns>new root node</returns>
		Private Function Remove(data As T, node As Node) As Node
			If node Is Nothing Then
				Return Nothing
			End If

			If data.CompareTo(node.Data) < 0 Then
				node.Left = Remove(data, node.Left)
				Dim l As Integer = If(node.Left IsNot Nothing, node.Left.Height, 0)

				If (node.Right IsNot Nothing) AndAlso (node.Right.Height - l >= 2) Then
					Dim rightHeight As Integer = If(node.Right.Right IsNot Nothing, node.Right.Right.Height, 0)
					Dim leftHeight As Integer = If(node.Right.Left IsNot Nothing, node.Right.Left.Height, 0)

					If rightHeight >= leftHeight Then
						node = RotateLeft(node)
					Else
						node = RotateLeftRight(node)
					End If
				End If
			ElseIf data.CompareTo(node.Data) > 0 Then
				node.Right = Remove(data, node.Right)
				Dim r As Integer = If(node.Right IsNot Nothing, node.Right.Height, 0)
				If (node.Left IsNot Nothing) AndAlso (node.Left.Height - r >= 2) Then
					Dim leftHeight As Integer = If(node.Left.Left IsNot Nothing, node.Left.Left.Height, 0)
					Dim rightHeight As Integer = If(node.Left.Right IsNot Nothing, node.Left.Right.Height, 0)
					If leftHeight >= rightHeight Then
						node = RotateRight(node)
					Else
						node = RotateRightLeft(node)
					End If
				End If
			ElseIf node.Left IsNot Nothing Then
				node.Data = FindMax(node.Left).Data
				Remove(node.Data, node.Left)

				If (node.Right IsNot Nothing) AndAlso (node.Right.Height - node.Left.Height >= 2) Then
					Dim rightHeight As Integer = If(node.Right.Right IsNot Nothing, node.Right.Right.Height, 0)
					Dim leftHeight As Integer = If(node.Right.Left IsNot Nothing, node.Right.Left.Height, 0)

					If rightHeight >= leftHeight Then
						node = RotateLeft(node)
					Else
						node = RotateLeftRight(node)
					End If
				End If
			Else
				node = node.Right
			End If

			If node IsNot Nothing Then
				Dim leftHeight As Integer = If(node.Left IsNot Nothing, node.Left.Height, 0)
				Dim rightHeight As Integer = If(node.Right IsNot Nothing, node.Right.Height, 0)
				node.Height = Max(leftHeight, rightHeight) + 1
			End If
			Return node
		End Function

		''' <summary>
		''' Determines is the data exists in the tree
		''' </summary>
		''' <param name="data">data to find</param>
		''' <returns>success|fail</returns>
		Public Function Contains(data As T) As Boolean
			Return Contains(data, _root)
		End Function

		''' <summary>
		''' Recursive Contains helper method
		''' </summary>
		''' <param name="data">data to find</param>
		''' <param name="node">node to test</param>
		''' <returns>success|fail</returns>
		Private Function Contains(data As T, node As Node) As Boolean
			If node Is Nothing Then
				Return False
			End If
			' The node was not found
			If data.CompareTo(node.Data) < 0 Then
				Return Contains(data, node.Left)
			End If
			If data.CompareTo(node.Data) > 0 Then
				Return Contains(data, node.Right)
			End If

			Return True
		End Function
	End Class
End Namespace