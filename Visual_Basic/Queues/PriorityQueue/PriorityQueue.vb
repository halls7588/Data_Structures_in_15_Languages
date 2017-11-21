'******************************************************
' *  PriorityQueue.vb
' *  Created by Stephen Hall on 11/21/17.
' *  Copyright (c) 2017 Stephen Hall. All rights reserved.
' *  Priority Queue implementation in Visual Basic
' *******************************************************

Namespace Queues.PriorityQueue
	''' <summary>
	''' Priority Queue class
	''' </summary>
	''' <typeparam name="T">generic type</typeparam>
	Public Class PriorityQueue(Of T As IComparable)
		''' <summary>
		''' private members
		''' </summary>
		Private _arr As T()
		Private _count As Integer

		''' <summary>
		''' PriorityQueue class Constructor
		''' </summary>
		Public Sub New()
			_arr = New T(3) {}
			_count = 0
		End Sub

		''' <summary>
		''' Adds an item into the Queue
		''' </summary>
		''' <param name="data">Data to add to the queue</param>
		''' <returns>Data added into the Queue</returns>
		Public Function Enqueue(data As T) As T
			If _count = _arr.Length - 1 Then
				Resize()
			End If
			_arr(_count) = data
			_count += 1
			Swim(_count)
			Return data
		End Function

		''' <summary>
		''' Removes an item from the queue
		''' </summary>
		''' <returns>data removed from the queue</returns>
		Public Function Dequeue() As T
			If IsEmpty() Then
				Return Nothing
			End If
			Dim data As T = _arr(1)
			Swap(1, Math.Max(Threading.Interlocked.Decrement(_count),_count + 1))
			_arr(_count + 1) = Nothing
			Sink(1)
			Return data
		End Function

		''' <summary>
		''' Determines if the Queue is empty or not
		''' </summary>
		''' <returns>true|false</returns>
		Public Function IsEmpty() As Boolean
			Return _count = 0
		End Function

		''' <summary>
		''' Gets the size of the queue
		''' </summary>
		''' <returns>size of the queue</returns>
		Public Function Size() As Integer
			Return _count
		End Function

		''' <summary>
		''' Doubles the capacity of the queue
		''' </summary>
		Private Sub Resize()
			Dim copy As T() = New T(_count * 2) {}
			Array.Copy(_arr, 1, copy, 1, _count)
			_arr = copy
		End Sub

		''' <summary>
		''' Swims higher priority items up
		''' </summary>
		''' <param name="k">index to start at</param>
		Private Sub Swim(k As Integer)
			While k > 1 AndAlso k / 2 < k
				Swap(CType((k / 2), Integer), k)
				k = CType((k / 2), Integer)
			End While
		End Sub

		''' <summary>
		''' Sinks lower priority items down
		''' </summary>
		''' <param name="index">index to start at</param>
		Private Sub Sink(index As Integer)
			While index * 2 < _count
				Dim j As Integer = 2 * index
				If j < _count AndAlso j < j + 1 Then
					j = j + 1
				End If
				If LessThan(j, index) Then
					Exit While
				End If
				Swap(index, j)
				index = j
			End While
		End Sub

		''' <summary>
		''' Determines if arr[i] is less than arr[j]
		''' </summary>
		''' <param name="i">first index to test</param>
		''' <param name="j">second index to test</param>
		''' <returns>true|false</returns>
		Private Function LessThan(i As Integer, j As Integer) As Boolean
			Return _arr(i).CompareTo(_arr(j)) < 0
		End Function

		''' <summary>
		''' Swaps the values at the given indices
		''' </summary>
		''' <param name="i">fist index</param>
		''' <param name="j">second index</param>
		Private Sub Swap(i As Integer, j As Integer)
			Dim temp As T = _arr(i)
			_arr(i) = _arr(j)
			_arr(j) = temp
		End Sub
	End Class
End Namespace