'******************************************************
' *  SkipList.vb
' *  Created by Stephen Hall on 11/20/17.
' *  Copyright (c) 2017 Stephen Hall. All rights reserved.
' *  Skip List implementation in Visual Basic
' *******************************************************

Namespace Lists.SkipList
	''' <summary>
	''' Skip list class
	''' </summary>
	''' <typeparam name="T">Generic type</typeparam>
	Public Class SkipList(Of T As IComparable)
		''' <summary>
		''' Node for Skip List class
		''' </summary>
		Public Class Node
			''' <summary>
			''' public members of the node class
			''' </summary>
			Public Data As T
			Public NodeList As List(Of Node)

			''' <summary>
			''' Node Constructor
			''' </summary>
			''' <param name="data1">data for the node to hold</param>
			Public Sub New(data1 As T)
				Data = data1
				NodeList = New List(Of Node)()
			End Sub

			''' <summary>
			''' Gets the level of the Node
			''' </summary>
			''' <returns>level of the node</returns>
			Public Function Level() As Integer
				Return NodeList.Count - 1
			End Function
		End Class

		''' <summary>
		''' Private members of the Skip list class
		''' </summary>
		Private _head As Node
		Private _max As Integer
		Private _size As Integer
		Private Const PROBABILITY As Double = 0.5
		Private ReadOnly _rnd As Random

		''' <summary>
		''' Skip List Constructor
		''' </summary>
		Public Sub New()
			_size = 0
			_max = 0
			_rnd = New Random(DateTime.Now.Second)
			_head = New Node(Nothing)
			' a Node with value null marks the beginning
				' null marks the end
			_head.NodeList.Add(Nothing)
		End Sub

		''' <summary>
		''' Adds a node into the Skip List
		''' </summary>
		''' <param name="data">data to add into the list</param>
		''' <returns>success|fail</returns>
		Public Function Add(data As T) As Boolean
			If Contains(data) Then
				Return False
			End If

			_size += 1
			Dim level = 0
			' random number from 0 to max + 1 (inclusive)
			While _rnd.NextDouble() < PROBABILITY
				level += 1
			End While
			While level > _max
				' should only happen once
				_head.NodeList.Add(Nothing)
				_max += 1
			End While

			Dim node As New Node(data)
			Dim current As Node = _head

			Do
				current = FindNext(data, current, level)
				node.NodeList(0) = current.NodeList(level)
				current.NodeList(level) = node
			Loop While Math.Max(Threading.Interlocked.Decrement(level),level + 1) > 0
			Return True
		End Function

		''' <summary>
		''' Finds a node in the list with the same data
		''' </summary>
		''' <param name="data">data to find</param>
		''' <returns>Node found</returns>
		Public Function Find(data As T) As Node
			Return Find(data, _head, _max)
		End Function

		''' <summary>
		''' Returns node with the greatest value
		''' </summary>
		''' <param name="data">data to find</param>
		''' <param name="current">current Node</param>
		''' <param name="level">level to start form</param>
		''' <returns>current node</returns>
		Private Function Find(data As T, current As Node, level As Integer) As Node
			Do
				current = FindNext(data, current, level)
			Loop While Math.Max(Threading.Interlocked.Decrement(level),level + 1) > 0
			Return current
		End Function

		''' <summary>
		''' Returns the node at a given level with highest value less than data
		''' </summary>
		''' <param name="data">data to find</param>
		''' <param name="current">current node</param>
		''' <param name="level">current level</param>
		''' <returns>highest node</returns>
		Private Function FindNext(data As T, current As Node, level As Integer) As Node
			Dim [next] As Node = current.NodeList(level)

			While [next] IsNot Nothing
				Dim value As T = [next].Data
				If LessThan(data, value) Then
					Exit While
				End If
				current = [next]
				[next] = current.NodeList(level)
			End While
			Return current
		End Function

		''' <summary>
		''' gets the size of the list
		''' </summary>
		''' <returns>size of the list</returns>
		Public Function Size() As Integer
			Return _size
		End Function

		''' <summary>
		''' Determines if the object is in the list or not
		''' </summary>
		''' <param name="data">object to test</param>
		''' <returns>true|false</returns>
		Public Function Contains(data As T) As Boolean
			Dim node As Node = Find(data)
			Return node IsNot Nothing AndAlso Not node.Data.Equals(Nothing) AndAlso EqualTo(node.Data, data)
		End Function

		''' <summary>
		''' Determines if a is less than b
		''' </summary>
		''' <param name="a">generic type to test</param>
		''' <param name="b">generic type to test</param>
		''' <returns>true|false</returns>
		Private Shared Function LessThan(a As T, b As T) As Boolean
			Return a.CompareTo(b) < 0
		End Function

		''' <summary>
		''' Determines if a is equal to b
		''' </summary>
		''' <param name="a">generic type to test</param>
		''' <param name="b">generic type to test</param>
		''' <returns>true|false</returns>
		Private Shared Function EqualTo(a As T, b As T) As Boolean
			Return a.CompareTo(b) = 0
		End Function
	End Class
End Namespace