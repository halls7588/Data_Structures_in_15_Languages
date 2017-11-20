'******************************************************
' *  ArrayedHeap.vb
' *  Created by Stephen Hall on 11/20/17.
' *  Copyright (c) 2017 Stephen Hall. All rights reserved.
' *  Arrayed Heap implementation in Visual basic
' *******************************************************

Namespace Heaps.ArrayedHeap

	''' <summary>
	''' ArrayedHeap class
	''' </summary>
	''' <typeparam name="T">Generic type</typeparam>
	Public Class ArrayedHeap(Of T As IComparable)
		''' <summary>
		''' private members to be used by the heap
		''' </summary>
		Private _array As T()
		Private _size As Integer

		''' <summary>
		''' ArrayedHeap Constructor.
		''' </summary>
		Public Sub New()
			_array = New T(9) {}
			_size = 0
		End Sub

		''' <summary>
		''' Adds an item into the heap
		''' </summary>
		''' <param name="value">value added to the heap</param>
		Public Sub Add(value As T)
			' grow array if needed
			If _size >= _array.Length - 1 Then
				_array = Resize()
			End If
			' place element into heap at bottom
			_array(Math.Max(Threading.Interlocked.Increment(_size),_size - 1)) = value
			BubbleUp()
		End Sub

		''' <summary>
		''' checks if the heap is empty
		''' </summary>
		''' <returns>true|false</returns>
		Public Function IsEmpty() As Boolean
			Return _size = 0
		End Function

		''' <summary>returns the first item on the heap
		''' 
		''' </summary>
		''' <returns>first element of the heap</returns>
		Public Function Peek() As T
			Return If(IsEmpty(), Nothing, _array(1))
		End Function

		''' <summary>
		''' Removes and returns the minimum element in the heap.
		''' </summary>
		''' <returns>element removed</returns>
		Public Function Remove() As T
			Dim result As T = Peek()
			' get rid of the last element
			_array(1) = _array(_size)
			_array(_size) = Nothing
			_size -= 1
			BubbleDown()
			Return result
		End Function

		''' <summary>
		''' Gets the size of the heap
		''' </summary>
		''' <returns>number of elements in the heap</returns>
		Public Function Size() As Integer
			Return _size
		End Function

		''' <summary>
		''' Bubbles down the root element to the correct placement
		''' </summary>
		Private Sub BubbleDown()
			Dim index As Integer = 1

			While HasLeftChild(index)
				Dim smallerChild As Integer = LeftIndex(index)
				If HasRightChild(index) AndAlso _array(LeftIndex(index)).CompareTo(_array(RightIndex(index))) > 0 Then
					smallerChild = RightIndex(index)
				End If
				If _array(index).CompareTo(_array(smallerChild)) > 0 Then
					Swap(index, smallerChild)
				Else
					Exit While
				End If
				index = smallerChild
			End While
		End Sub

		''' <summary>
		''' Performs bubble up to place new element in correct position
		''' </summary>
		Private Sub BubbleUp()
			Dim index As Integer = _size
			While HasParent(index) AndAlso Parent(index).CompareTo(_array(index)) > 0
				Swap(index, ParentIndex(index))
				index = ParentIndex(index)
			End While
		End Sub

		''' <summary>
		''' Determines if the given index has a parent
		''' </summary>
		''' <param name="i">index to test</param>
		''' <returns>true|false</returns>
		Private Shared Function HasParent(i As Integer) As Boolean
			Return i > 1
		End Function

		''' <summary>
		''' Gets the index of the left child
		''' </summary>
		''' <param name="i">index to test</param>
		''' <returns>left child index</returns>
		Private Shared Function LeftIndex(i As Integer) As Integer
			Return i * 2
		End Function

		''' <summary>
		''' Gets the index of the right child of given index
		''' </summary>
		''' <param name="i">index to test</param>
		''' <returns>index of right child</returns>
		Private Shared Function RightIndex(i As Integer) As Integer
			Return i * 2 + 1
		End Function

		''' <summary>
		''' Determines if element has a left child or
		''' </summary>
		''' <param name="i">index to test</param>
		''' <returns>true|false</returns>
		Private Function HasLeftChild(i As Integer) As Boolean
			Return LeftIndex(i) <= _size
		End Function

		''' <summary>
		''' Determines if element has a right child or
		''' </summary>
		''' <param name="i">index to test</param>
		''' <returns>true|false</returns>
		Private Function HasRightChild(i As Integer) As Boolean
			Return RightIndex(i) <= _size
		End Function

		''' <summary>
		'''  gets the data of the parent
		''' </summary>
		''' <param name="i">child index</param>
		''' <returns>data of the parent index</returns>
		Private Function Parent(i As Integer) As T
			Return _array(ParentIndex(i))
		End Function

		'*
'         * Gets the parent index of i
'         * @param i: index to get parent of
'         * @return int: parent index
'         

		Private Shared Function ParentIndex(i As Integer) As Integer
			Return CType((i / 2), Integer)
		End Function

		''' <summary>
		''' Doubles the size of the internal array
		''' </summary>
		''' <returns>new resized array with copy of the data</returns>
		Private Function Resize() As T()
			Dim arr As T() = New T(_array.Length * 2 - 1) {}
			Array.Copy(_array, arr, _array.Length * 2)
			Return InlineAssignHelper(_array, arr)
		End Function

		''' <summary>
		''' Swaps the data at index a with b
		''' </summary>
		''' <param name="a">first index to swap</param>
		''' <param name="b">second index to swap</param>
		Private Sub Swap(a As Integer, b As Integer)
			Dim tmp As T = _array(a)
			_array(a) = _array(b)
			_array(b) = tmp
		End Sub
		Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
			target = value
			Return value
		End Function
	End Class
End Namespace