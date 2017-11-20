'******************************************************
'*  CircularArray.cs
'*  Created by Stephen Hall on 9/22/17.
'*  Copyright (c) 2017 Stephen Hall. All rights reserved.
'*  A Circular Array implementation in C#.Net
'*******************************************************

Namespace Arrays.CircularArray
	''' <summary>
	''' Circulay Array class 
	''' </summary>
	''' <typeparam name="T">Generis type to be used</typeparam>
	Public Class CirculayArray(Of T)
		''' <summary>
		''' Private members
		''' </summary>
		Private _array As T()
		Private _size As Integer
		Private _zeroIndex As Integer
		Private _count As Integer

		''' <summary>
		''' Default Circulay Array class constructor
		''' </summary>
		Public Sub New()
			_size = 10
			_array = New T(_size - 1) {}
			_zeroIndex = 0
			_count = 0
		End Sub

		''' <summary>
		''' Circulay Array class constructor
		''' </summary>
		''' <param name="size">Size to initialize array to</param>
		Public Sub New(size As Integer)
			_size = size
			_array = New T(size - 1) {}
			_zeroIndex = 0
			_count = 0
		End Sub

		''' <summary>
		''' Adds new item into the array
		''' </summary>
		''' <param name="data">Data to add into the array</param>
		''' <returns>Data added into the array</returns>
		Public Function Add(data As T) As T
			Dim tmp As Integer = (_zeroIndex + _count) Mod _size
			_array(tmp) = data
			If ((_count + 1) / _size) >= 1 Then
				Resize()
			End If
			_count += 1
			Return _array(tmp)
		End Function

		''' <summary>
		''' Gets the data at the arrays given index
		''' </summary>
		''' <param name="index">Index to get data at</param>
		''' <returns>Data at the given index or default value of T if index does not exist</returns>
		Public Function DataAt(index As Integer) As T
			Return If((index + _zeroIndex) Mod _size < _count AndAlso _array((index + _zeroIndex) Mod _size) IsNot Nothing, _array(index + _zeroIndex Mod _size), Nothing)
		End Function

		''' <summary>
		''' Removes the data at arrays given index
		''' </summary>
		''' <param name="index">Index to remove</param>
		''' <returns>Data removed from the array or default T value if index does not exist</returns>
		Public Function Remove(index As Integer) As T
			If index > _size Then
				Return Nothing
			End If

			Dim tmp As T = _array((index + _zeroIndex Mod _size))
			_array((index + _zeroIndex Mod _size)) = _array(_zeroIndex)
			_array(_zeroIndex) = Nothing
			_count -= 1
			_zeroIndex = (_zeroIndex + 1) Mod _size
			Return tmp
		End Function

		''' <summary>
		''' Gets the current count of the array
		''' </summary>
		''' <returns>Number of items in the array</returns>
		Public Function Count() As Integer
			Return _count
		End Function

		''' <summary>
		''' Private method to resize the array if capacity has been reached
		''' </summary>
		Private Sub Resize()
			_size = _size * 2
			Dim arr As T() = New T(_size - 1) {}
			For i As Integer = 0 To _array.Length - 1
				arr(i) = _array(i)
			Next
			_array = arr
		End Sub
	End Class
End Namespace