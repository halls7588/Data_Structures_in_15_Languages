'************************************************************
'*  LinkedList.vb
'*  Created by Stephen Hall on 9/22/17.
'*  Copyright (c) 2017 Stephen Hall. All rights reserved.
'*  A singly linked list implementation in Visual Basic
'************************************************************

Namespace Lists.LinkedList
	''' <summary>
	''' Singly linked list class
	''' </summary>
	''' <typeparam name="T">Generic type</typeparam>
	Public Class LinkedList(Of T As IComparable)
		''' <summary>
		''' Node class for singly linked list
		''' </summary>
		Public Class Node
			''' <summary>
			''' Public aaccessors for the node class
			''' </summary>
			Public Property Data As T
			Public Property [Next] As Node

			''' <summary>
			''' Node Class Constructor
			''' </summary>
			''' <param name="data1">Data to be held in the Node</param>
			Public Sub New(data1 As T)
				Data = data1
				[Next] = Nothing
			End Sub
		End Class

		''' <summary>
		''' Private Members
		''' </summary>
		Private _head As Node
		Private _tail As Node
		Private _count As Integer

		''' <summary>
		''' Linked List Constructor
		''' </summary>
		Public Sub New()
			_head = InlineAssignHelper(_tail, Nothing)
			_count = 0
		End Sub

		''' <summary>
		''' Adds a new node into the list with the given data
		''' </summary>
		''' <param name="data">Data to add into the list</param>
		''' <returns>Node added into the list</returns>
		Public Function Add(data As T) As Node
			' No data to insert into list
			If data IsNot Nothing Then
				Dim node As New Node(data)
				' The Linked list is empty
				If _head Is Nothing Then
					_head = node
					_tail = _head
					_count += 1
					Return node
				End If
				' Add to the end of the list
				_tail.[Next] = node
				_tail = node
				_count += 1
				Return node
			End If
			Return Nothing
		End Function

		''' <summary>
		''' Removes the first node in the list matching the data
		''' </summary>
		''' <param name="data">Data to remove from the lis</param>
		''' <returns>Node removed from the list</returns>
		Public Function Remove(data As T) As Node
			' List is empty or no data to remove
			If _head IsNot Nothing AndAlso data IsNot Nothing Then
				Dim tmp As Node = _head
				' The data to remove what found in the first Node in the list
				If tmp.Data.Equals(data) Then
					_head = _head.[Next]
					_count -= 1
					Return tmp
				End If
				' Try to find the node in the list
				While tmp.[Next] IsNot Nothing
					' Node was found, Remove it from the list
					If tmp.[Next].Data.Equals(data) Then
						Dim node As Node = tmp.[Next]
						tmp.[Next] = tmp.[Next].[Next]
						_count -= 1
						Return node
					End If
					tmp = tmp.[Next]
				End While
			End If
			' The data was not found in the list
			Return Nothing
		End Function

		''' <summary>
		''' Gets the first node that has the given data
		''' </summary>
		''' <param name="data">Data to find in the list</param>
		''' <returns>First node with matching data or null if no node was found</returns>
		Public Function Find(data As T) As Node
			' No list or data to find
			If _head IsNot Nothing OrElse data IsNot Nothing Then
				Dim tmp As Node = _head
				' Try to find the data in the list
				While tmp IsNot Nothing
					' Data was found
					If tmp.Data.Equals(data) Then
						Return tmp
					End If
					tmp = tmp.[Next]
				End While
			End If
			' Data was not found in the list
			Return Nothing
		End Function

		''' <summary>
		''' Gets the node at the given index
		''' </summary>
		''' <param name="index">Index of the Node to get</param>
		''' <returns>Node at given in index</returns>
		Public Function IndexAt(index As Integer) As Node
			'Index was negative or larger then the amount of Nodes in the list
			If index < 0 OrElse index > Size() Then
				Return Nothing
			End If

			Dim tmp As Node = _head
			' Move to index
			For i = 0 To index - 1
				tmp = tmp.[Next]
			Next
			' return the node at the index position
			Return tmp
		End Function

		''' <summary>
		''' Gets the current size of the array
		''' </summary>
		''' <returns>Number of items in the array</returns>
		Public Function Size() As Integer
			Return _count
		End Function
		Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
			target = value
			Return value
		End Function
	End Class
End Namespace
