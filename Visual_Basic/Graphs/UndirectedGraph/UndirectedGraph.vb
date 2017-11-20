'******************************************************
' *  UndirectedGraph.cs
' *  Created by Stephen Hall on 11/20/17.
' *  Copyright (c) 2017 Stephen Hall. All rights reserved.
' *  Undirected Graph implementation in C#
' *******************************************************

Namespace Graphs.UndirectedGraph
	''' <summary>
	''' Undirected Graph class
	''' </summary>
	Public Class UndirectedGraph
		''' <summary>
		''' private members
		''' </summary>
		Private _edges As Integer
		Private ReadOnly _map As New Dictionary(Of Int32, Dictionary(Of Int32, [Double]))()

		''' <summary>
		''' Gets the size of teh graph
		''' </summary>
		''' <returns>size of the graph</returns>
		Public Function Size() As Integer
			Return _map.Count
		End Function

		''' <summary>
		''' gets the number of edges
		''' </summary>
		''' <returns>number of edges</returns>
		Public Function GetNumberOfEdges() As Integer
			Return _edges
		End Function

		''' <summary>
		'''  adds a node into the graph
		''' </summary>
		''' <param name="nodeId">id to add</param>
		''' <returns>success|fail</returns>
		Public Function AddNode(nodeId As Integer) As Boolean
			If _map.ContainsKey(nodeId) Then
				Return False
			End If

			_map.Add(nodeId, New Dictionary(Of Int32, [Double])())
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
		''' test to see if the node exisits
		''' </summary>
		''' <param name="nodeId">id to find</param>
		''' <returns>true|false</returns>
		Public Function HasNode(nodeId As Integer) As Boolean
			Return _map.ContainsKey(nodeId)
		End Function

		''' <summary>
		''' clears a node from the graph
		''' </summary>
		''' <param name="nodeId">id to clear</param>
		''' <returns> success|fail</returns>
		Public Function ClearNode(nodeId As Integer) As Boolean
			If Not HasNode(nodeId) Then
				Return False
			End If

			Dim neighbors As Dictionary(Of Int32, [Double]) = _map(nodeId)
			If neighbors.Count = 0 Then
				Return False
			End If

			For Each neighborId As Int32 In neighbors.Keys
				_map(neighborId).Remove(nodeId)
			Next

			_edges -= neighbors.Count
			neighbors.Clear()
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
				_map.Remove(nodeId)
				Return True
			End If
			Return False
		End Function

		''' <summary>
		''' helper function to add an edge to the graph
		''' </summary>
		''' <param name="tailNodeId">tail node</param>
		''' <param name="headNodeId">head node</param>
		''' <param name="weight">weight of the edge</param>
		''' <returns>success|fail</returns>
		Public Function AddEdge(tailNodeId As Integer, headNodeId As Integer, weight As Double) As Boolean
			If tailNodeId <> headNodeId Then
				AddNode(tailNodeId)
				AddNode(headNodeId)

				If Not _map(tailNodeId).ContainsKey(headNodeId) Then
					_map(tailNodeId).Add(headNodeId, weight)
					_map(headNodeId).Add(tailNodeId, weight)
					_edges += 1
					Return True
				End If
				Dim oldWeight As Double = _map(tailNodeId)(headNodeId)
				_map(tailNodeId)(headNodeId) = weight
				_map(headNodeId)(tailNodeId) = weight
				Return oldWeight <> weight
			End If
			' Undirected graph are not allowed to contain self-loops.
			Return False
		End Function

		''' <summary>
		''' tests to see if an edge exisit
		''' </summary>
		''' <param name="tailNodeId">tail node</param>
		''' <param name="headNodeId">head node</param>
		''' <returns>true|false</returns>
		Public Function HasEdge(tailNodeId As Integer, headNodeId As Integer) As Boolean
			Return _map.ContainsKey(tailNodeId) AndAlso _map(tailNodeId).ContainsKey(headNodeId)
		End Function

		''' <summary>
		''' gets the weight of an edge
		''' </summary>
		''' <param name="tailNodeId">tail node</param>
		''' <param name="headNodeId">head node</param>
		''' <returns>weight of the edge</returns>
		Public Function GetEdgeWeight(tailNodeId As Integer, headNodeId As Integer) As Double
			Return If(Not HasEdge(tailNodeId, headNodeId), [Double].NaN, _map(tailNodeId)(headNodeId))
		End Function

		''' <summary>
		''' removes an edge from the graph
		''' </summary>
		''' <param name="tailNodeId">tail node</param>
		''' <param name="headNodeId">head node</param>
		''' <returns>success|fail</returns>
		Public Function RemoveEdge(tailNodeId As Integer, headNodeId As Integer) As Boolean
			If Not (_map.ContainsKey(tailNodeId) OrElse (Not _map(tailNodeId).ContainsKey(headNodeId))) Then
				Return False
			End If

			_map(tailNodeId).Remove(headNodeId)
			_map(headNodeId).Remove(tailNodeId)
			_edges -= 1
			Return True
		End Function

		''' <summary>
		''' gets the children of the given node
		''' </summary>
		''' <param name="nodeId">node to test</param>
		''' <returns>set of child nodes</returns>
		Public Function GetChildrenOf(nodeId As Integer) As HashSet(Of Int32)
			Return If(_map.ContainsKey(nodeId), New HashSet(Of Int32)(_map(nodeId).Keys), New HashSet(Of Int32)())
		End Function

		''' <summary>
		''' gets the parents of a node
		''' </summary>
		''' <param name="nodeId">node to test</param>
		''' <returns>set of parents</returns>
		Public Function GetParentsOf(nodeId As Integer) As HashSet(Of Int32)
			Return If(_map.ContainsKey(nodeId), New HashSet(Of Int32)(_map(nodeId).Keys), New HashSet(Of Int32)())
		End Function

		''' <summary>
		'''  Returns all nodes of a graph
		''' </summary>
		''' <returns>set of the nodes in the graph</returns>
		Public Function GetAllNodes() As HashSet(Of Int32)
			Return New HashSet(Of Int32)(_map.Keys)
		End Function

		''' <summary>
		''' Clears the graph of all nodes and edges
		''' </summary>
		Public Sub Clear()
			_map.Clear()
			_edges = 0
		End Sub
	End Class
End Namespace