'******************************************************
' *  LinkedQueue.vb
' *  Created by Stephen Hall on 11/01/17.
' *  Copyright (c) 2017 Stephen Hall. All rights reserved.
' *  A Linked Queue implementation in Visual Basic
' *******************************************************

Namespace Queues.LinkedQueue
	''' <summary>
	''' Linked Queue Class
	''' </summary>
	''' <typeparam name="T">Generic Type</typeparam>
	Public Class LinkedQueue(Of T)
		''' <summary>
		''' Node Class
		''' </summary>
		Public Class Node
			''' <summary>
			''' Public accessors for the node class
			''' </summary>
			Public Property Data As T
			Public Property [Next] As Node

			''' <summary>
			''' Node Class Constructor
			''' </summary>
			''' <param name="data1">Data to be held in the node</param>
			Public Sub New(data1 As T)
				Data = data1
				[Next] = Nothing
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
		''' Adds given data onto the queue
		''' </summary>
		''' <param name="data">Data to be added to the queue</param>
		''' <returns>Node added to the queue</returns>
		Public Function Enqueue(data As T) As Node
			Dim node As Node
			If IsEmpty() Then
				node = New Node(data)
				_head = InlineAssignHelper(_tail, node)
				_count += 1
				Return node
			End If
			node = New Node(data)
			node.[Next] = _tail
			_tail = node
			_count += 1
			Return node
		End Function

		''' <summary>
		''' Removes item off the queue
		''' </summary>
		''' <returns>Node popped off of the queue</returns>
		Public Function Dequeue() As Node
			Dim node As Node = _head
			_head = _head.[Next]
			node.[Next] = Nothing
			_count -= 1
			Return node
		End Function

		''' <summary>
		''' Gets the Node onto of the queue
		''' </summary>
		''' <returns>Node on top of the queue</returns>
		Public Function Top() As Node
			Return _head
		End Function

		''' <summary>
		''' Returns a value indicating if the queue is empty
		''' </summary>
		''' <returns>true if empty, false if not</returns>
		Public Function IsEmpty() As Boolean
			Return _count = 0
		End Function

		''' <summary>
		''' Returns a value indicating if the queue is full
		''' </summary>
		''' <returns>False, Linked queue is never full</returns>
		Public Function IisFull() As Boolean
			Return False
		End Function
		Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
			target = value
			Return value
		End Function
	End Class
End Namespace
