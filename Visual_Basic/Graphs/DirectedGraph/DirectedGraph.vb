'******************************************************
' *  DirectedGraph.vb
' *  Created by Stephen Hall on 11/20/17.
' *  Copyright (c) 2017 Stephen Hall. All rights reserved.
' *  Directed Graph implementation in Visual Basic
' *******************************************************

Namespace Graphs.DirectedGraph
	''' <summary>
	''' Directed Graph Class
	''' </summary>
	Public Class DirectedGraph
		Private _edges As Integer
		Private ReadOnly _parentMap As New Dictionary(Of Int32, Dictionary(Of Int32, [Double]))()
		Private ReadOnly _childMap As New Dictionary(Of Int32, Dictionary(Of Int32, [Double]))()

		''' <summary>
		''' Gets the size of the graph
		''' </summary>
		''' <returns>size of the graph</returns>
		Public Function Size() As Integer
			Return _parentMap.Count
		End Function

		''' <summary>
		''' gets the number of edges
		''' </summary>
		''' <returns>number of edges</returns>
		Public Function GetNumberOfEdges() As Integer
			Return _edges
		End Function

		''' <summary>
		''' adds a node into the graph
		''' </summary>
		''' <param name="nodeId">id to add</param>
		''' <returns>success|fail</returns>
		Public Function AddNode(nodeId As Integer) As Boolean
			If Not _parentMap.ContainsKey(nodeId) Then
				_parentMap.Add(nodeId, New Dictionary(Of Int32, [Double])())
				_childMap.Add(nodeId, New Dictionary(Of Int32, [Double])())
				Return True
			End If
			Return False
		End Function

		''' <summary>
		''' test to see if the node exisits
		''' </summary>
		''' <param name="nodeId">id to find</param>
		''' <returns>true|false</returns>
		Public Function HasNode(nodeId As Integer) As Boolean
			Return _parentMap.ContainsKey(nodeId)
		End Function

		''' <summary>
		''' clears a node from the graph
		''' </summary>
		''' <param name="nodeId">id to clear</param>
		''' <returns>success|fail</returns>
		Public Function ClearNode(nodeId As Integer) As Boolean
			If Not HasNode(nodeId) Then
				Return False
			End If

			Dim parents As Dictionary(Of Int32, [Double]) = _parentMap(nodeId)
			Dim children As Dictionary(Of Int32, [Double]) = _childMap(nodeId)

			If parents.Count = 0 AndAlso children.Count = 0 Then
				Return False
			End If

			For Each childId As Int32 In children.Keys
				_parentMap(childId).Remove(nodeId)
			Next

			For Each parentId As Int32 In parents.Keys
				_childMap(parentId).Remove(nodeId)
			Next

			_edges -= parents.Count
			_edges -= children.Count
			parents.Clear()
			children.Clear()
			Return True
		End Function

		''' <summary>
		''' removes a node from the graph
		''' </summary>
		''' <param name="nodeId">id to remove</param>
		''' <returns>success|fail</returns>
		Public Function RemoveNode(nodeId As Integer) As Boolean
			If HasNode(nodeId) Then
				ClearNode(nodeId)
				_parentMap.Remove(nodeId)
				_childMap.Remove(nodeId)
				Return True
			End If
			Return False
		End Function

		''' <summary>
		''' helper function to add an edge to the graph
		''' </summary>
		''' <param name="tailNodeId"> tail node</param>
		''' <param name="headNodeId">head node</param>
		''' <param name="weight">weight of the edge</param>
		''' <returns>success|fail</returns>
		Private Function AddEdge(tailNodeId As Integer, headNodeId As Integer, weight As Double) As Boolean
			AddNode(tailNodeId)
			AddNode(headNodeId)

			If Not _childMap(tailNodeId).ContainsKey(headNodeId) Then
				Dim oldWeight As Double = _childMap(tailNodeId)(headNodeId)
				_childMap(tailNodeId).Add(headNodeId, weight)
				_parentMap(headNodeId).Add(tailNodeId, weight)
				Return oldWeight <> weight
			End If
			_childMap(tailNodeId)(headNodeId) = weight
			_parentMap(headNodeId)(tailNodeId) = weight
			_edges += 1
			Return True
		End Function

		''' <summary>
		''' Adds an edge to the graph
		''' </summary>
		''' <param name="tailNodeId">tail node</param>
		''' <param name="headNodeId">head node</param>
		''' <returns>success|fail</returns>
		Public Function AddEdge(tailNodeId As Integer, headNodeId As Integer) As Boolean
			Return AddEdge(tailNodeId, headNodeId, 1.0)
		End Function

		''' <summary>
		''' tests to see if an edge exist
		''' </summary>
		''' <param name="tailNodeId">tail node</param>
		''' <param name="headNodeId">head node</param>
		''' <returns>true|false</returns>
		Public Function HasEdge(tailNodeId As Integer, headNodeId As Integer) As Boolean
			Return _childMap.ContainsKey(tailNodeId) AndAlso _childMap(tailNodeId).ContainsKey(headNodeId)
		End Function

		''' <summary>
		''' gets the weight of an edge
		''' </summary>
		''' <param name="tailNodeId">tail node</param>
		''' <param name="headNodeId">head node</param>
		''' <returns>weight of the edge</returns>
		Public Function GetEdgeWeight(tailNodeId As Integer, headNodeId As Integer) As Double
			Return If(Not HasEdge(tailNodeId, headNodeId), [Double].NaN, _childMap(tailNodeId)(headNodeId))
		End Function

		''' <summary>
		''' removes an edge from the graph
		''' </summary>
		''' <param name="tailNodeId">tail node</param>
		''' <param name="headNodeId">head node</param>
		''' <returns>success|fail</returns>
		Public Function RemoveEdge(tailNodeId As Integer, headNodeId As Integer) As Boolean
			If _childMap.ContainsKey(tailNodeId) Then
				If _childMap(tailNodeId).ContainsKey(headNodeId) Then
					_childMap(tailNodeId).Remove(headNodeId)
					_parentMap(headNodeId).Remove(tailNodeId)
					_edges -= 1
					Return True
				End If
			End If
			Return False
		End Function

		''' <summary>
		''' gets the children of the given node
		''' </summary>
		''' <param name="nodeId">node to test</param>
		''' <returns>set of child nodes</returns>
		Public Function GetChildrenOf(nodeId As Integer) As HashSet(Of Int32)
			Return If(_childMap.ContainsKey(nodeId), New HashSet(Of Int32)(_childMap(nodeId).Keys), New HashSet(Of Int32)())
		End Function

		''' <summary>
		''' gets the parents of a node
		''' </summary>
		''' <param name="nodeId"> node to test</param>
		''' <returns>set of parents</returns>
		Public Function GetParentsOf(nodeId As Integer) As HashSet(Of Int32)
			Return If(_parentMap.ContainsKey(nodeId), New HashSet(Of Int32)(_parentMap(nodeId).Keys), New HashSet(Of Int32)())
		End Function

		''' <summary>
		''' all nodes of a graph
		''' </summary>
		''' <returns>set of the nodes in the graph</returns>
		Public Function GetAllNodes() As HashSet(Of Int32)
			Return New HashSet(Of Int32)(_childMap.Keys)
		End Function

		''' <summary>
		''' Clears the graph of all nodes and edges
		''' </summary>
		Public Sub Clear()
			_childMap.Clear()
			_parentMap.Clear()
			_edges = 0
		End Sub
	End Class
End Namespace