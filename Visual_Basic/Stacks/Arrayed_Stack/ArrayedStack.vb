'******************************************************
' *  ArrayedStack.vb
' *  Created by Stephen Hall on 9/25/17.
' *  Copyright (c) 2017 Stephen Hall. All rights reserved.
' *  A Arrayed Stack implementation in Visual Basic
' *******************************************************

Namespace Stacks.ArrayedStack
	''' <summary>
	'''  Arrayed Stack Class
	''' </summary>
	''' <typeparam name="T"> Generic type</typeparam>
	Public Class ArrayedStack(Of T)
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
		''' Arrayed Stack Constructor
		''' </summary>
		''' <param name="size">Size to initialize the stack to</param>
		Public Sub New(size As Integer)
			_array = New T(InlineAssignHelper(_size, size) - 1) {}
			_count = 0
		End Sub

		''' <summary>
		''' Pushes given data onto the stack if space is available
		''' </summary>
		''' <param name="data">Data to be added to the stack</param>
		''' <returns>Item added to the stack</returns>
		Public Function Push(data As T) As T
			If Not IsFull() Then
				_array(_count) = data
				_count += 1
				Return Top()
			End If
			Return Nothing
		End Function

		''' <summary>
		''' Pops item off the stack
		''' </summary>
		''' <returns>Item popped off of the stack</returns>
		Public Function Pop() As T
			Dim data As T = _array(_count - 1)
			_count -= 1
			Return data
		End Function

		''' <summary>
		''' Gets the Item onto of the stack
		''' </summary>
		''' <returns>Item on top of the stack</returns>
		Public Function Top() As T
			Return _array(_count - 1)
		End Function

		''' <summary>
		''' Returns a value indicating if the stack is empty
		''' </summary>
		''' <returns>true if empty, false if not</returns>
		Public Function IsEmpty() As Boolean
			Return _count = 0
		End Function

		''' <summary>
		''' Returns a value indicating if the stack is full
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
