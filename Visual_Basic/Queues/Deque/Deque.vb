'******************************************************
' *  Deque.vb
' *  Created by Stephen Hall on 11/21/17.
' *  Copyright (c) 2017 Stephen Hall. All rights reserved.
' *  A Deque implementation in Visual Basic
' *******************************************************

Namespace Queues.Deque
	''' <summary>
	''' Linked Queue Class
	''' </summary>
	''' <typeparam name="T">Generic Type</typeparam>
	Public Class Deque(Of T)
		''' <summary>
		''' Node Class for Linked Queue
		''' </summary>
		Public Class Node
			''' <summary>
			''' Public properties for the node class 
			''' </summary>
			Public Property Data As T
			Public Property [Next] As Node
			Public Property Previous As Node

			''' <summary>
			''' Node Class Constructor
			''' </summary>
			''' <param name="data1">Data to be held in the node</param>
			Public Sub New(data1 As T)
				Data = data1
				[Next] = Nothing
				Previous = Nothing
			End Sub
		End Class

		''' <summary>
		''' Private Members
		''' </summary>
		Private _count As Integer
		Private _head As Node
		Private _tail As Node

		''' <summary>
		''' Linked Queue Constructor
		''' </summary>
		Public Sub New()
			_count = 0
			_head = InlineAssignHelper(_tail, Nothing)
		End Sub

		''' <summary>
		''' Adds given data onto the front of the deque
		''' </summary>
		''' <param name="data">Data to be added to the deque</param>
		''' <returns>Node added to the deque</returns>
		Public Function EnqueueFront(data As T) As Node
			Dim node As Node
			If IsEmpty() Then
				node = New Node(data)
				_head = InlineAssignHelper(_tail, node)
				_count += 1
				Return node
			End If
			node = New Node(data)
			node.[Next] = _head
			_head.Previous = node
			_head = node
			_count += 1
			Return node
		End Function

		''' <summary>
		''' Adds given data onto the back of the deque
		''' </summary>
		''' <param name="data">Data to be added to the deque</param>
		''' <returns>Node added to the deque</returns>
		Public Function EnqueueBack(data As T) As Node
			Dim node As Node
			If IsEmpty() Then
				node = New Node(data)
				_head = InlineAssignHelper(_tail, node)
				_count += 1
				Return node
			End If
			node = New Node(data)
			node.[Next] = _tail
			_tail.Previous = node
			_tail = node
			_count += 1
			Return node
		End Function

		''' <summary>
		''' Removes item off the front of the deque
		''' </summary>
		''' <returns>Node popped off of the deque</returns>
		Public Function DequeueFront() As Node
			If IsEmpty() Then
				Return Nothing
			End If
			Dim node As Node = _head
			_head = _head.[Next]
			_head.Previous = Nothing
			node.[Next] = Nothing
			_count -= 1
			Return node
		End Function

		''' <summary>
		''' Removes item off the back of the deque
		''' </summary>
		''' <returns>Node removes from the back of the deque</returns>
		Public Function DequeueBack() As Node
			If IsEmpty() Then
				Return Nothing
			End If
			Dim node As Node = _tail
			_tail = _tail.Previous
			_tail.[Next] = Nothing
			node.Previous = Nothing
			_count -= 1
			Return node
		End Function

		''' <summary>
		''' Gets the Node at the front of the deque
		''' </summary>
		''' <returns>Node on top of the deque</returns>
		Public Function PeekFront() As Node
			Return _head
		End Function

		''' <summary>
		''' Gets the Node at the back of the deque
		''' </summary>
		''' <returns>Node on bottom of the deque</returns>
		Public Function PeekBack() As Node
			Return _tail
		End Function

		''' <summary>
		''' Returns a value indicating if the deque is empty
		''' </summary>
		''' <returns>rue if empty, false if not</returns>
		Public Function IsEmpty() As Boolean
			Return _count = 0
		End Function

		''' <summary>
		''' Returns a value indicating if the deque is full
		''' </summary>
		''' <returns>False, deque is never full</returns>
		Public Function IsFull() As Boolean
			Return False
		End Function
		Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
			target = value
			Return value
		End Function
	End Class
End Namespace