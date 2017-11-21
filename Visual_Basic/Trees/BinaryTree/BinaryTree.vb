'******************************************************
' *  BinaryTree.vb
' *  Created by Stephen Hall on 9/25/17.
' *  Copyright (c) 2016 Stephen Hall. All rights reserved.
' *  A Binary Tree implementation in Visual Basic
' *******************************************************

Namespace Trees.BinaryTree
	''' <summary>
	''' Binary Tree Class
	''' </summary>
	''' <typeparam name="T">Generic Type</typeparam>
	Public Class BinaryTree(Of T As IComparable)
		''' <summary>
		''' Node class for the binary tree
		''' </summary>
		Public Class Node
			''' <summary>
			''' Public Properties for  the Node class
			''' </summary>
			Public Property Data As T
			Public Property Left As Node
            Public Property Right As Node
			Public Property Parent As Node

			''' <summary>
			''' Node class constructor             
			''' </summary>
			''' <param name="data1">Data to be held in the Node</param>
			Public Sub New(data1 As T)
				Left = InlineAssignHelper(Right, Nothing)
				Data = data1
			End Sub
			Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
				target = value
				Return value
			End Function
		End Class

		''' <summary>
		''' Private Binary Tree members
		''' </summary>
		Private _root As Node

		''' <summary>
		''' Binary tree class constructor
		''' </summary>
		Public Sub New()
			_root = Nothing
		End Sub

		''' <summary>
		''' Compares the data in given node to the give data for equality
		''' </summary>
		''' <param name="data">data to compare</param>
		''' <param name="node">Node to compare to</param>
		''' <returns>1 if greater then, 0 if equal to, -1 if less then</returns>
		Private Shared Function Compare(data As T, node As Node) As Integer
			Return node.Data.CompareTo(data)
		End Function

		''' <summary>
		''' Inserts a new node into the tree
		''' </summary>
		''' <param name="data">data to insert into the tree</param>
		''' <returns>Node inserted into the tree</returns>
		Public Function Insert(data As T) As Node
			Return If(_root Is Nothing, InlineAssignHelper(_root, New Node(data)), InsertHelper(data, _root))
		End Function

		''' <summary>
		''' Recursive helper function to inset a new node into the tree
		''' </summary>
		''' <param name="data">Data to insert into the tree</param>
		''' <param name="node">urrent node in the tree</param>
		''' <returns>Node inserted into the tree</returns>
		Private Shared Function InsertHelper(data As T, node As Node) As Node
			Try
				Dim cmp As Integer = Compare(data, node)
				Select Case cmp
					Case 1
						If node.Right Is Nothing Then
							Dim newNode As New Node(data)
							node.Right = newNode
							newNode.Parent = node
							Return newNode
						Else
							Return InsertHelper(data, node.Right)
						End If
					Case Else

						If node.Left Is Nothing Then
							Dim newNode As New Node(data)
							node.Left = newNode
							newNode.Parent = node
							Return newNode
						Else
							Return InsertHelper(data, node.Left)
						End If
				End Select
			Catch generatedExceptionName As Exception
				Return Nothing
			End Try
		End Function

		''' <summary>
		''' Removes a Node from the tree
		''' </summary>
		''' <param name="data">Data to remove from the tree</param>
		''' <returns>Node removed from the tree</returns>
		Public Function Remove(data As T) As Node
			Return If(_root IsNot Nothing, Nothing, RemoveHelper(data, _root))
		End Function

		''' <summary>
		''' Recursive helper function to remove a node from the tree
		''' </summary>
		''' <param name="data">Data to remove</param>
		''' <param name="node">urrent node</param>
		''' <returns>Node removed from the tree</returns>
		Private Function RemoveHelper(data As T, node As Node) As Node
			Try
				Dim cmp As Integer = Compare(data, node)
				If cmp = 1 Then
					Return RemoveHelper(data, node.Right)
				End If
				If cmp = -1 Then
					Return RemoveHelper(data, node.Left)
				End If
				If cmp = 0 Then
					'has no children
					Dim tempNode As Node

					If node.Left Is Nothing AndAlso node.Right Is Nothing Then
						node = node.Parent
					End If

					'has one child
					If node.Parent IsNot Nothing Then
						tempNode = node.Parent
					Else
						' this is the root node
						If node.Right IsNot Nothing Then
							tempNode = node.Right
							tempNode.Parent = Nothing
							_root.Right = Nothing
							_root = tempNode
							Return node
						End If
						tempNode = node.Left
						If tempNode IsNot Nothing Then
							tempNode.Parent = Nothing
							_root.Left = Nothing
							_root = tempNode
						End If
						Return node
					End If
					If tempNode.Left Is node Then
						If node.Left IsNot Nothing Then
							tempNode.Left = node.Left
							node.Left.Parent = tempNode
							Return node
						End If
						If node.Right IsNot Nothing Then
							tempNode.Right = node.Right
							node.Right.Parent = tempNode
							Return node
						End If
					End If
					If tempNode.Right Is node Then
						If node.Left IsNot Nothing Then
							tempNode.Left = node.Left
							node.Left.Parent = tempNode
							Return node
						End If
						If node.Right IsNot Nothing Then
							tempNode.Right = node.Right
							node.Right.Parent = tempNode
							Return node
						End If
					ElseIf node.Left IsNot Nothing AndAlso node.Right IsNot Nothing Then
						' test for 2 children
						tempNode = node.Right
						' find min value of right child tree to replace the node
						While tempNode.Left IsNot Nothing
							tempNode = tempNode.Left
						End While
						Dim temp As T = node.Data
						node.Data = tempNode.Data
						' replace the node with the tempNode
						tempNode.Data = temp
						tempNode.Parent.Left = Nothing
						tempNode.Parent = Nothing
						Return tempNode
					End If
				End If
			Catch generatedExceptionName As Exception
				Return Nothing
			End Try
			Return Nothing
		End Function

		''' <summary>
		''' Returns the smallest node in the tree
		''' </summary>
		''' <returns>Smallest Node int he tree</returns>
		Public Function GetMin() As Node
			If _root Is Nothing Then
				Return Nothing
			End If

			Dim node As Node = _root

			While node.Left IsNot Nothing
				node = node.Left
			End While

			Return node
		End Function

		''' <summary>
		''' Return the largest node in the tree
		''' </summary>
		''' <returns>argest Node in the tree</returns>
		Public Function GetMax() As Node
			If _root Is Nothing Then
				Return Nothing
			End If

			Dim node As Node = _root

			While node.Right IsNot Nothing
				node = node.Right
			End While

			Return node
		End Function

		''' <summary>
		''' Prints out the tree using Pre Order Traversal
		''' </summary>
		''' <param name="node">Node to start the Pre Order Traversal at</param>
		Public Sub PreOrederTraversal(node As Node)
			If node IsNot Nothing Then
				Console.WriteLine(node.Data)
				PreOrederTraversal(node.Left)
				PreOrederTraversal(node.Right)
			End If
		End Sub

		''' <summary>
		''' Prints out the Tree using Post Order Traversal
		''' </summary>
		''' <param name="node">Node to start the Post Order Traversal at</param>
		Public Sub PostPrderTraversal(node As Node)
			If node IsNot Nothing Then
				PostPrderTraversal(node.Left)
				PostPrderTraversal(node.Right)
				Console.WriteLine(node.Data)
			End If
		End Sub

		''' <summary>
		''' Pints out the tree using in order Traversal
		''' </summary>
		''' <param name="node">Node to start the In Order Traversal at</param>
		Public Sub InOrderedTraversal(node As Node)
			If node IsNot Nothing Then
				InOrderedTraversal(node.Left)
				Console.WriteLine(node.Data)
				InOrderedTraversal(node.Right)
			End If
		End Sub

		''' <summary>
		''' Prints out the tree using Depth First Search
		''' </summary>
		''' <param name="node">Node to start the Depth First Search at</param>
		Public Sub DepthFirstSearch(node As Node)
			If node Is Nothing Then
				Return
			End If

			Dim stack As New Stack(Of Node)()
			stack.Push(node)

			While stack.Count > 0
				Console.WriteLine(InlineAssignHelper(node, stack.Pop()))
				If node.Right IsNot Nothing Then
					stack.Push(node.Right)
				End If
				If node.Left IsNot Nothing Then
					stack.Push(node.Left)
				End If
			End While
		End Sub

		''' <summary>
		''' Prints out the tree using breadth first search
		''' </summary>
		''' <param name="node">Node to start Breadth First Search at</param>
		Public Sub BreadthFirstSearch(node As Node)
			If node Is Nothing Then
				Return
			End If

			Dim queue As New Queue(Of Node)()
			queue.Enqueue(node)

			While queue.Count > 0
				Console.WriteLine(InlineAssignHelper(node, queue.Dequeue()))
				If node.Left IsNot Nothing Then
					queue.Enqueue(node.Left)
				End If
				If node.Right IsNot Nothing Then
					queue.Enqueue(node.Right)
				End If
			End While
		End Sub
		Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
			target = value
			Return value
		End Function
	End Class
End Namespace
