'******************************************************
' *  CircularQueue.vb
' *  Created by Stephen Hall on 11/21/17.
' *  Copyright (c) 2017 Stephen Hall. All rights reserved.
' *  Circular Queue implementation in Visual Basic
' *******************************************************

Namespace Queues.CircularQueue
	''' <summary>
	''' Circular Queue Class
	''' </summary>
	''' <typeparam name="T">Generic type</typeparam>
	Public Class CircularQueue(Of T)
		''' <summary>
		''' Private Members
		''' </summary>
		Private _array As T()
		Private _count As Integer
		Private _size As Integer
		Private _zeroIndex As Integer

		''' <summary>
		''' Default Constructor
		''' </summary>
		Public Sub New()
			_array = New T(InlineAssignHelper(_size, 10) - 1) {}
			_count = InlineAssignHelper(_zeroIndex, 0)
		End Sub

		''' <summary>
		''' Circular Queue Constructor
		''' </summary>
		''' <param name="size">Size to initialize the queue to</param>
		Public Sub New(size As Integer)
			_array = New T(InlineAssignHelper(_size, size) - 1) {}
			_count = InlineAssignHelper(_zeroIndex, 0)
		End Sub

		''' <summary>
		''' Pushes given data onto the queue if space is available
		''' </summary>
		''' <param name="data">Data to be added to the queue</param>
		''' <returns>Node added to the queue</returns>
		Public Function Enqueue(data As T) As T
			If Not IsFull() Then
				_array((_zeroIndex + _count) Mod _size) = data
				_count += 1
				Return Top()
			End If
			Return Nothing
		End Function

		''' <summary>
		''' Removes item from the queue
		''' </summary>
		''' <returns>item removed from the queue</returns>
		Public Function Dequeue() As T
			If IsEmpty() Then
				Return Nothing
			End If

			Dim tmp As T = _array(_zeroIndex)
			_array(_zeroIndex) = Nothing
			_count -= 1
			_zeroIndex = (_zeroIndex + 1) Mod _size
			Return tmp
		End Function

		''' <summary>
		''' Gets the top item of the queue
		''' </summary>
		''' <returns>item on top of the queue</returns>
		Public Function Top() As T
			Return If(IsEmpty(), Nothing, _array(_zeroIndex))
		End Function

		''' <summary>
		''' Returns a value indicating if the queue is empty
		''' </summary>
		''' <returns>true if empty, false if not</returns>
		Public Function IsEmpty() As Boolean
			Return _count = 0
		End Function

		''' <summary>
		'''  * Returns a value indicating if the queue is full
		''' </summary>
		''' <returns>True if full, false if not</returns>
		Public Function IsFull() As Boolean
			Return _count = _size
		End Function
		Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
			target = value
			Return value
		End Function
	End Class
End Namespace