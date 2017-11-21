'******************************************************
' *  LinkedStack.vb
' *  Created by Stephen Hall on 9/25/17.
' *  Copyright (c) 2017 Stephen Hall. All rights reserved.
' *  A Linked Stack implementation in Visual Basic
' *******************************************************

Namespace Stacks.LinkedStack
	''' <summary>
	''' Linked Stack Class
	''' </summary>
	''' <typeparam name="T"> Generic Type</typeparam>
	Public Class LinkedStack(Of T)
		''' <summary>
		''' Node Class for Linked Stack
		''' </summary>
		Public Class Node
			''' <summary>
			''' public accessors for Node class
			''' </summary>
			Public Property Data As T
			Public Property [Next] As Node

			''' <summary>
			''' Node Constructor
			''' </summary>
			''' <param name="data1">Data for the node to hold</param>
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

		''' <summary>
		''' Linked Stack Constructor
		''' </summary>
		Public Sub New()
			_count = 0
			_head = Nothing
		End Sub

		''' <summary>
		''' Pushes given data onto the stack
		''' </summary>
		''' <param name="data">Data to be added to the stack</param>
		''' <returns>Node added to the stack</returns>
		Public Function Push(data As T) As Node
			Dim node As New Node(data)
			node.[Next] = _head
			_head = node
			_count += 1
			Return Top()
		End Function

		''' <summary>
		''' Pops item off the stack
		''' </summary>
		''' <returns>Node popped off of the stack</returns>
		Public Function Pop() As Node
			Dim node As Node = _head
			_head = _head.[Next]
			node.[Next] = Nothing
			_count -= 1
			Return node
		End Function

		''' <summary>
		''' Gets the Node onto of the stack
		''' </summary>
		''' <returns>Node on top of the stack</returns>
		Public Function Top() As Node
			Return _head
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
		''' <returns>False, Linked stack is never full</returns>
		Public Function IsFull() As Boolean
			Return False
		End Function
	End Class
End Namespace
