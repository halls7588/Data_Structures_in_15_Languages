'******************************************************
' *  ArrayedQueue.vb
' *  Created by Stephen Hall on 11/01/17.
' *  Copyright (c) 2017 Stephen Hall. All rights reserved.
' *  A Arrayed Queue implementation in Visula Basic
' *******************************************************

Namespace Queues.ArrayedQueue
	''' <summary>
	''' Arrayed Queue Class
	''' </summary>
	''' <typeparam name="T">Generic type</typeparam>
	Public Class ArrayedQueue(Of T As IComparable)
		''' <summary>
        ''' Private Members  
        ''' </summary>
		Private _array As T()
		Private _count As Integer
		Private _size As Integer

		''' <summary>
		''' Default Constructor
		''' </summary>
		Public Sub New()
			_array = New T(InlineAssignHelper(_size, 10) - 1) {}
			_count = 0
		End Sub

		''' <summary>
		''' Arrayed Queue Constructor
		''' </summary>
		''' <param name="size">Size to initialize the queue to</param>
		Public Sub New(size As Integer)
			_array = New T(InlineAssignHelper(_size, size) - 1) {}
			_count = 0
		End Sub

        ''' <summary>
        ''' Pushes given data onto the queue if space is available
        ''' </summary>
        ''' <param name="data">Data to be added to the queue</param>
        ''' <returns>Item added to the queue</returns>
		Public Function Enqueue(data As T) As T
			If Not IsFull() Then
				_array(_count) = data
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
			Dim data As T = _array(0)
			Dim tmp As T() = New T(_size - 1) {}
			Array.Copy(_array, 1, tmp, 0, _size - 1 - 1)
			_array = tmp
			_count -= 1
			Return data
		End Function

		''' <summary>
		''' Gets the top item of the queue
		''' </summary>
		''' <returns>item on top of the queue</returns>
		Public Function Top() As T
			Return If((IsEmpty()), Nothing, _array(0))
		End Function

		''' <summary>
		''' Returns a value indicating if the queue is empty
		''' </summary>
		''' <returns>true if empty, false if not</returns>
		Public Function IsEmpty() As Boolean
			Return (_count = 0)
		End Function

		''' <summary>
		''' Returns a value indicating if the queue is full
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
