'******************************************************
' *  SortedArray.vb
' *  Created by Stephen Hall on 11/20/17.
' *  Copyright (c) 2017 Stephen Hall. All rights reserved.
' *  SortedArray implementation in Visual Basic
' *******************************************************

Namespace Arrays.SortedArray
	''' <summary>
	''' Sorted Array Class
	''' </summary>
	''' <typeparam name="T">Generic Type</typeparam>
	Public Class SortedArray(Of T As IComparable)
		''' <summary>
		''' Private members
		''' </summary>
		Private _array As T()
		Private _count As Integer
		Private _size As Integer

		''' <summary>
		''' SortedArray default Constructor
		''' </summary>
		Public Sub New()
			_count = 0
			_array = New T(InlineAssignHelper(_size, 4) - 1) {}
		End Sub

		''' <summary>
		''' SortedArray constructor initialized to a specific size
		''' </summary>
		''' <param name="size">Size to initialize the array to</param>
		Public Sub New(size As Integer)
			_count = 0
			_size = If((size > 0), size, 4)
			_array = New T(_size - 1) {}
		End Sub

		''' <summary>
		'''  Doubles the size of the internal array
		''' </summary>
		Private Sub Resize()
			_size *= 2
			Dim tmp As T() = New T(_size - 1) {}
			Array.Copy(_array, tmp, _count)
			_array = tmp
		End Sub

		''' <summary>
		''' Adds new item into the array
		''' </summary>
		''' <param name="data">Data to add into the array</param>
		''' <returns>Data added into the array</returns>
		Public Function Add(data As T) As T
			If data Is Nothing Then
				Return Nothing
			End If

			If _count = _size Then
				Resize()
			End If

			_array(_count) = data
			_count += 1

			Return data
		End Function

		''' <summary>
		''' Appends the contents of an array to the SortedArray
		''' </summary>
		''' <param name="data"> array to append</param>
		''' <returns>success|fail</returns>
		Public Function Append(data As T()) As Boolean
			If data Is Nothing Then
				Return False
			End If
			For Each aData As T In data
				If aData IsNot Nothing Then
					Add(aData)
				End If
			Next
			Return True
		End Function

		''' <summary>
		''' Sets the data at the given index
		''' </summary>
		''' <param name="index">index to set</param>
		''' <param name="data">data to set index to</param>
		''' <returns>success|fail</returns>
		Public Function [Set](index As Integer, data As T) As Boolean
			If index >= 0 AndAlso index < _size Then
				_array(index) = data
				Return True
			End If
			Return False
		End Function

		''' <summary>
		''' Gets the data at the arrays given index
		''' </summary>
		''' <param name="index">Index to get data at</param>
		''' <returns>Data at the given index or default value of T if index does not exist</returns>
		Public Function [Get](index As Integer) As T
			Return If(index >= 0 AndAlso index < _size, _array(index), Nothing)
		End Function

		''' <summary>
		''' Removes the data at arrays given index
		''' </summary>
		''' <param name="index">Index to remove</param>
		''' <returns>Data removed from the array or default T value if index does not exist</returns>
		Public Function Remove(index As Integer) As T
			If index < 0 OrElse index > _count Then
				Return Nothing
			End If

			Dim tmp As T = _array(index)
			_array(index) = Nothing
			_count -= 1
			Return tmp
		End Function

		''' <summary>
		''' Resets the internal array to default size with no data
		''' </summary>
		Public Sub Reset()
			_count = 0
			_size = 4
			_array = New T(_size - 1) {}
		End Sub

		''' <summary>
		''' Clears all data in the array leaving size intact
		''' </summary>
		Public Sub Clear()
			_array = New T(_size - 1) {}
			_count = 0
		End Sub

		''' <summary>
		''' Gets the current count of the array
		''' </summary>
		''' <returns>Number of items in the array</returns>
		Public Function Count() As Integer
			Return _count
		End Function

		''' <summary>
		''' Private helper method for Merger Sort. Merges two sub-arrays of arr[].
		''' </summary>
		''' <param name="arr">array to be sorted</param>
		''' <param name="l1">index of first sub array</param>
		''' <param name="m">merge point</param>
		''' <param name="r2">index of second sub array</param>
		Private Sub Merge(arr As T(), l1 As Integer, m As Integer, r2 As Integer)
			Dim i As Integer, j As Integer, k As Integer
			Dim n1 As Integer = m - l1 + 1
			Dim n2 As Integer = r2 - m

			' create temp arrays
			Dim l3 As T() = New T(n1 - 1) {}
			Dim r4 As T() = New T(n2 - 1) {}

			' Copy data to temp arrays L[] and R[]
			For i = 0 To n1 - 1
				l3(i) = arr(l1 + i)
			Next
			For j = 0 To n2 - 1
				r4(j) = arr(m + 1 + j)
			Next

			' Merge the temp arrays back into arr[l..r]
			i = 0
			' Initial index of first sub-array
			j = 0
			' Initial index of second sub-array
			k = l1
			' Initial index of merged sub-array
			While i < n1 AndAlso j < n2
				If LessThan(l3(i), r4(j)) OrElse EqualTo(l3(i), r4(j)) Then
					arr(k) = l3(i)
					i += 1
				Else
					arr(k) = r4(j)
					j += 1
				End If
				k += 1
			End While

			' Copy the remaining elements of L[], if there are any
			While i < n1
				arr(k) = l3(i)
				i += 1
				k += 1
			End While

			' Copy the remaining elements of R[], if there are any
			While j < n2
				arr(k) = r4(j)
				j += 1
				k += 1
			End While
		End Sub

		''' <summary>
		''' Recursive helper method for merge sort
		''' </summary>
		''' <param name="arr">sub array to be sorted</param>
		''' <param name="l">left index</param>
		''' <param name="r">right index</param>
		Private Sub MergeSortHelper(arr As T(), l As Integer, r As Integer)
			If l < r Then
				' Same as (l+r)/2, but avoids overflow for
				' large l and h
				Dim m As Integer = CType((l + (r - l) / 2), Integer)

				' Sort first and second halves
				MergeSortHelper(arr, l, m)
				MergeSortHelper(arr, (m + 1), r)

				Merge(arr, l, m, r)
			End If
		End Sub

		''' <summary>
		''' Performs Merge Sort on internal array
		''' </summary>
		''' <returns>sorted copy of the internal array</returns>
		Public Function MergeSort() As T()
			Dim tmp As T() = New T(_count - 1) {}
			Array.Copy(_array, tmp, _count)
			MergeSortHelper(tmp, 0, (_count - 1))
			Return tmp
		End Function

		''' <summary>
		''' Performs Bubble sort on internal array
		''' </summary>
		''' <returns>sorted copy of the internal array</returns>
		Public Function BubbleSort() As T()
			Dim tmp As T() = New T(_count - 1) {}
			Array.Copy(_array, tmp, _count)

			For i As Integer = 0 To _count - 2
				For j As Integer = 0 To _count - i - 2
					If GreaterThan(tmp(j), tmp(j + 1)) Then
						' swap temp and arr[i]
						Dim temp As T = tmp(j)
						tmp(j) = tmp(j + 1)
						tmp(j + 1) = temp
					End If
				Next
			Next
			Return tmp
		End Function

		''' <summary>
		''' Helper method for Quick Sort. Swaps data in the partition
		''' </summary>
		''' <param name="arr">array to be sorted</param>
		''' <param name="low">low index</param>
		''' <param name="high">high index</param>
		''' <returns>pivot index</returns>
		Private Function Partition(arr As T(), low As Integer, high As Integer) As Integer
			Dim pivot As T = arr(high)
			Dim temp As T
			Dim i As Integer = (low - 1)
			' index of smaller element
			For j As Integer = low To high - 1
				' If current element is smaller than or
				' equal to pivot
				If LessThan(arr(j), pivot) OrElse EqualTo(arr(j), pivot) Then
					i += 1
					' swap arr[i] and arr[j]
					temp = arr(i)
					arr(i) = arr(j)
					arr(j) = temp
				End If
			Next

			' swap arr[i+1] and arr[high] (or pivot)
			temp = arr(i + 1)
			arr(i + 1) = arr(high)
			arr(high) = temp

			Return i + 1
		End Function

		''' <summary>
		''' Helper method for recursive Quick Sort
		''' </summary>
		''' <param name="arr">array to be sorted</param>
		''' <param name="low">low index</param>
		''' <param name="high">high index</param>
		Private Sub QuickSortHelper(arr As T(), low As Integer, high As Integer)
			If low < high Then
				' pivot is partitioning index, arr[pivot] is now at right place
				Dim pivot As Integer = Partition(arr, low, high)

				' Recursively sort elements before and after pivot
				QuickSortHelper(arr, low, (pivot - 1))
				QuickSortHelper(arr, (pivot + 1), high)
			End If
		End Sub

		''' <summary>
		''' Performs Quick Sort on the internal array
		''' </summary>
		''' <returns>sorted copy of the internal array</returns>
		Public Function QuickSort() As T()
			Dim tmp As T() = New T(_count - 1) {}
			Array.Copy(_array, tmp, _count)
			QuickSortHelper(tmp, 0, (_count - 1))

			Return tmp
		End Function

		''' <summary>
		''' Performs Insertion sort on the internal array
		''' </summary>
		''' <returns>sorted copy of the internal array</returns>
		Public Function InsertionSort() As T()
			Dim tmp As T() = New T(_count - 1) {}
			Array.Copy(_array, 0, tmp, 0, _count)

			For i As Integer = 1 To _count - 1
				Dim key As T = tmp(i)
				Dim j As Integer = i - 1

				' Move elements of arr[0..i-1], that are greater than key, to one position ahead
				While j >= 0 AndAlso LessThan(tmp(j), key)
					tmp(j + 1) = tmp(j)
					j = j - 1
				End While
				tmp(j + 1) = key
			Next
			Return tmp
		End Function

		''' <summary>
		'''  Performs Selection Sort on internal array
		''' </summary>
		''' <returns>Sorted copy of the internal data</returns>
		Public Function SelectionSort() As T()
			Dim tmp As T() = New T(_count - 1) {}
			Array.Copy(_array, tmp, _count)
			' One by one move boundary of unsorted sub-array
			For i As Integer = 0 To _count - 2
				' Find the minimum element in unsorted array
				Dim min As Integer = i
				For j As Integer = i + 1 To _count - 1
					If LessThan(tmp(j), tmp(min)) Then
						min = j
					End If
				Next

				' Swap the found minimum element with the first
				' element
				Dim temp As T = tmp(min)
				tmp(min) = tmp(i)
				tmp(i) = temp
			Next
			Return tmp
		End Function

		''' <summary>
		''' Determines if a is less than b
		''' </summary>
		''' <param name="a">generic type to test</param>
		''' <param name="b">generic type to test</param>
		''' <returns>true|false</returns>
		Private Function LessThan(a As T, b As T) As Boolean
			Return a.CompareTo(b) < 0
		End Function

		''' <summary>
		''' Determines if a is equal to b
		''' </summary>
		''' <param name="a">generic type to test</param>
		''' <param name="b">generic type to test</param>
		''' <returns>true|false</returns>
		Private Function EqualTo(a As T, b As T) As Boolean
			Return a.CompareTo(b) = 0
		End Function

		''' <summary>
		''' Determines if a is greater than b
		''' </summary>
		''' <param name="a">generic type to test</param>
		''' <param name="b">generic type to test</param>
		''' <returns>true|false</returns>
		Private Function GreaterThan(a As T, b As T) As Boolean
			Return a.CompareTo(b) > 0
		End Function
		Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
			target = value
			Return value
		End Function
	End Class
End Namespace