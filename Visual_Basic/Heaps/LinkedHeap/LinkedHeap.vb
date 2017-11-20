'******************************************************
' *  ArrayedHeap.cs
' *  Created by Stephen Hall on 11/20/17.
' *  Copyright (c) 2017 Stephen Hall. All rights reserved.
' *  Arrayed Heap implementation in C#
' *******************************************************

Namespace Heaps.LinkedHeap
	''' <summary>
	''' Linked Heap class
	''' </summary>
	''' <typeparam name="T">Generic type</typeparam>
	Public Class LinkedHeap(Of T As IComparable)
		''' <summary>
		''' Node Class
		''' </summary>
		Public Class Node
			''' <summary>
			''' Public properties for the node class
			''' </summary>
			Public Property Data As T
			Public Property Left As Node
			Public Property Right As Node
			Public Property Npl As Integer

			''' <summary>
			''' Node Constructor
			''' </summary>
			''' <param name="data1">data for node to hold</param>
			Public Sub New(data1 As T)
				Data = data1
				Left = Nothing
				Right = Nothing
				Npl = 0
			End Sub

			''' <summary>
			''' ode Constructor
			''' </summary>
			''' <param name="data1">data for node to hold</param>
			''' <param name="left2">left child</param>
			''' <param name="right3">right child</param>
			Public Sub New(data1 As T, left2 As Node, right3 As Node)
				Data = data1
				Left = left2
				Right = right3
				Npl = 0
			End Sub

		End Class
		''' <summary>
		''' Private members
		''' </summary>
		Private _root As Node

		''' <summary>
		''' Gets the root of the heap
		''' </summary>
		''' <returns>root node of the heap</returns>
		Public Function Root() As Node
			Return _root
		End Function

		''' <summary>
		''' Constructor for linked heap.
		''' </summary>
		Public Sub New()
			_root = Nothing
		End Sub

		''' <summary>
		''' Merges two heaps together
		''' </summary>
		''' <param name="heap">heap to merge with</param>
		Public Sub Merge(heap As LinkedHeap(Of T))
			If Not Equals(heap) Then
				_root = Merge(_root, heap.Root())
				heap._root = Nothing
			End If
		End Sub

		''' <summary>
		''' Merges two roots together
		''' </summary>
		''' <param name="n1">first root</param>
		''' <param name="n2"> second root</param>
		''' <returns>merged roots</returns>
		Private Function Merge(n1 As Node, n2 As Node) As Node
			If n1 Is Nothing Then
				Return n2
			End If
			If n2 Is Nothing Then
				Return n1
			End If
			Return If((n1.Data.CompareTo(n2.Data) < 0), MergeHelper(n1, n2), MergeHelper(n2, n1))
		End Function

		''' <summary>
		''' Helper method to Merge()
		''' </summary>
		''' <param name="n1">first root</param>
		''' <param name="n2">second root</param>
		''' <returns>merged roots</returns>
		Private Function MergeHelper(n1 As Node, n2 As Node) As Node
			If n1.Left Is Nothing Then
				n1.Left = n2
			Else
				n1.Right = Merge(n1.Right, n2)
				If n1.Left.Npl < n1.Right.Npl Then
					SwapChildren(n1)
				End If
				n1.Npl = n1.Right.Npl + 1
			End If
			Return n1
		End Function

		''' <summary>
		''' Swaps the children of the given node
		''' </summary>
		''' <param name="node">node with two children to swap</param>
		Private Sub SwapChildren(node As Node)
			Dim tmp As Node = node.Left
			node.Left = node.Right
			node.Right = tmp
		End Sub

		''' <summary>
		''' Insert into the priority queue, maintaining heap order.
		''' </summary>
		''' <param name="data">the item to insert.</param>
		Public Sub Insert(data As T)
			_root = Merge(New Node(data), _root)
		End Sub

		''' <summary>
		''' Find the smallest item in the priority queue.
		''' </summary>
		''' <returns>smallest item or default(T).</returns>
		Public Function FindMin() As T
			Return If(IsEmpty(), Nothing, _root.Data)
		End Function

		''' <summary>
		'''  Remove the smallest item from the heap
		''' </summary>
		''' <returns>item removed or default(T)</returns>
		Public Function DeleteMin() As T
			If Not IsEmpty() Then
				Dim minItem As T = _root.Data
				_root = Merge(_root.Left, _root.Right)
				Return minItem
			End If
			Return Nothing
		End Function

		''' <summary>
		''' Test if heap is empty.
		''' </summary>
		''' <returns> true|false.</returns>
		Public Function IsEmpty() As Boolean
			Return _root Is Nothing
		End Function

		''' <summary>
		''' Test if the heap is full.
		''' </summary>
		''' <returns>false: Is never full</returns>
		Public Function IsFull() As Boolean
			Return False
		End Function

		''' <summary>
		''' Clears the data in the heap
		''' </summary>
		Public Sub MakeEmpty()
			_root = Nothing
		End Sub
	End Class
End Namespace