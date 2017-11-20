'******************************************************
' *  ArrayedSet.vb
' *  Created by Stephen Hall on 11/20/17.
' *  Copyright (c) 2017 Stephen Hall. All rights reserved.
' *  Arrayed Set implementation in Visual Basic
' *******************************************************

Namespace Arrays.ArrayedSet
	''' <summary>
	''' ArrayedSet Class
	''' </summary>
	''' <typeparam name="T">Generic Type</typeparam>
	Public Class ArrayedSet(Of T)
		Private _array As T()
		Private _size As Integer

		''' <summary>
		''' Gets the current count of the ArrayList
		''' </summary>
		Public Property Count As Integer

		''' <summary>
		''' Default constructor
		''' </summary>
		Public Sub New()
			Count = 0
			_array = New T(InlineAssignHelper(_size, 4) - 1) {}
		End Sub

		''' <summary>
		''' Constructor Initialized to given size
		''' </summary>
		''' <param name="size">Size to initialize array to</param>
		Public Sub New(size As Integer)
			Count = 0
			_size = If(size > 0, size, 4)
			_array = New T(_size - 1) {}
		End Sub
		''' <summary>
		''' Doubles the size of the internal array
		''' </summary>
		Private Sub Resize()
			_size *= 2
			Dim tmp As T() = New T(_size - 1) {}
			Array.Copy(_array, tmp, Count)
			_array = tmp
		End Sub

		''' <summary>
		''' Adds new item into the array
		''' </summary>
		''' <param name="data">Data to add into the arrayo</param>
		''' <returns>Data added into the array</returns>
		Public Function Add(data As T) As T
			If data Is Nothing OrElse Contains(data) Then
				Return Nothing
			End If

			If Count = _size Then
				Resize()
			End If

			_array(Count) = data
			Count += 1
			Return data
		End Function

		''' <summary>
		''' Appends the contents of an array to the ArrayedSet
		''' </summary>
		''' <param name="data">array to append</param>
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
			If Not Contains(data) AndAlso (index >= 0 AndAlso index < _size) Then
				_array(index) = data
				Return True
			End If
			Return False
		End Function

		''' <summary>
		''' Gets the data at the arrays given index
		''' </summary>
		''' <param name="index">Index to get data at</param>
		''' <returns>Data at the given index or default(T)</returns>
		Public Function [Get](index As Integer) As T
			Return If(index >= 0 AndAlso index < _size, _array(index), Nothing)
		End Function

		''' <summary>
		''' Removes the data at arrays given index
		''' </summary>
		''' <param name="index">Index to remove</param>
		''' <returns> Data removed from the array or default(T)</returns>
		Public Function Remove(index As Integer) As T
			If index < 0 OrElse index > Count Then
				Return Nothing
			End If

			Dim tmp As T = _array(index)
			_array(index) = Nothing
			Count -= 1
			Return tmp
		End Function

		''' <summary>
		''' Resets the internal array to default size with no data
		''' </summary>
		Public Sub Reset()
			Count = 0
			_size = 4
			_array = New T(_size - 1) {}
		End Sub

		''' <summary>
		''' Clears all data in the array leaving size intact
		''' </summary>
		Public Sub Clear()
			For i As Integer = 0 To Count - 1
				_array(i) = Nothing
			Next
			Count = 0
		End Sub

		''' <summary>
		''' Tests to see if the data exist in the list
		''' </summary>
		''' <param name="data">data to find</param>
		''' <returns>success|fail</returns>
		Private Function Contains(data As T) As Boolean
			Return _array.Contains(data)
		End Function
		Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
			target = value
			Return value
		End Function
	End Class
End Namespace