'******************************************************
' *  HeapSort.vb
' *  Created by Stephen Hall on 11/20/17.
' *  Copyright (c) 2017 Stephen Hall. All rights reserved.
' *  HeapSort implementation in Visual Basic
' *******************************************************

Namespace Heaps.HeapSort
	''' <summary>
	''' HeapSort Class
	''' </summary>
	''' <typeparam name="T">Generic Type</typeparam>
	Public NotInheritable Class HeapSort(Of T As IComparable)
		Private Sub New()
		End Sub

		''' <summary>
		''' orts the given array
		''' </summary>
		''' <param name="heap">array to sort</param>
		Public Shared Sub Sort(heap As T())
			For i As Integer = heap.Length / 2 - 1 To 0 Step -1
				Heapify(heap, heap.Length, i)
			Next
			For i As Integer = heap.Length - 1 To 0 Step -1
				Dim temp As T = heap(0)
				heap(0) = heap(i)
				heap(i) = temp
				Heapify(heap, i, 0)
			Next
		End Sub

		''' <summary>
		''' Builds a heap out of the array
		''' </summary>
		''' <param name="arr">array to heapify</param>
		''' <param name="length">length allowed</param>
		''' <param name="index">starting index </param>
		Private Shared Sub Heapify(arr As T(), length As Integer, index As Integer)
			Dim left As Integer = 2 * index + 1
			Dim right As Integer = 2 * index + 2
			Dim largest As Integer = index

			If left < length AndAlso arr(left).CompareTo(arr(largest)) > 0 Then
				largest = left
			End If

			If right < length AndAlso arr(right).CompareTo(arr(largest)) > 0 Then
				largest = right
			End If

			If largest <> index Then
				Dim swap As T = arr(index)
				arr(index) = arr(largest)
				arr(largest) = swap
				Heapify(arr, length, largest)
			End If
		End Sub
	End Class
End Namespace