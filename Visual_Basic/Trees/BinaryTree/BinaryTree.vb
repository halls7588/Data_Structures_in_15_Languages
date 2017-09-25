' *******************************************************
' BinaryTree.vb
' Created by Stephen Hall on 9/25/17.
' Copyright (c) 2016 Stephen Hall. All rights reserved.
' A Binary Tree implementation in Visual Basic
' *******************************************************

Namespace DataStructures
    ''' <summary>
    ''' Binary Tree Class
    ''' </summary>
    ''' <typeparam name="T">Generic Type</typeparam>
    Public Class BinaryTree(Of T)
        ''' <summary>
        ''' Node class for the binary tree
        ''' </summary>
        ''' <typeparam name="T">Generic Type</typeparam>
        Public Class Node(Of T)
            ''' <summary>
            ''' Private members
            ''' </summary>
            Private m_left As Node(Of T)
            Private m_right As Node(Of T)
            Private m_parent As Node(Of T)
            Private m_data As T

            ''' <summary>
            ''' Public Property for the Node data Member
            ''' </summary>
            Public Property Data() As T
                Get
                    Return m_data
                End Get
                Set
                    m_data = Value
                End Set
            End Property

            ''' <summary>
            ''' Public Property for the Node left Member
            ''' </summary>
            Public Property Left() As Node(Of T)
                Get
                    Return m_left
                End Get
                Set
                    m_left = Value
                End Set
            End Property

            ''' <summary>
            ''' Public Property for the Node right Member
            ''' </summary>
            Public Property Right() As Node(Of T)
                Get
                    Return m_right
                End Get
                Set
                    m_right = Value
                End Set
            End Property

            ''' <summary>
            ''' Public Property for the Node parent Member
            ''' </summary>
            Public Property Parent() As Node(Of T)
                Get
                    Return m_parent
                End Get
                Set
                    m_parent = Value
                End Set
            End Property

            ''' <summary>
            ''' Node class constructor             
            ''' </summary>
            ''' <param name="data">Data to be held in the Node</param>
            Public Sub New(data As T)
                m_left = InlineAssignHelper(m_right, Nothing)
                Me.m_data = data
            End Sub
            Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
                target = value
                Return value
            End Function
        End Class

        ''' <summary>
        ''' Private Binary Tree members
        ''' </summary>
        Private root As Node(Of T)

        ''' <summary>
        ''' Binary tree class constructor
        ''' </summary>
        Public Sub New()
            root = Nothing
        End Sub

        ''' <summary>
        ''' Compares the data in given node to the give data for equality
        ''' </summary>
        ''' <param name="data">data to compare</param>
        ''' <param name="node">Node to compare to</param>
        ''' <returns>1 if greater then, 0 if equal to, -1 if less then</returns>
        Private Function compare(data As T, node As Node(Of T)) As Integer
            Return DirectCast(node, IComparable).CompareTo(data)
        End Function

        ''' <summary>
        ''' Inserts a new node into the tree
        ''' </summary>
        ''' <param name="data">data to insert into the tree</param>
        ''' <returns>Node inserted into the tree</returns>
        Public Function Insert(data As T) As Node(Of T)
            If root Is Nothing Then
                Return (InlineAssignHelper(root, New Node(Of T)(data)))
            End If
            Return Me.insertHelper(data, root)
        End Function

        ''' <summary>
        ''' Recursive helper function to inset a new node into the tree
        ''' </summary>
        ''' <param name="data">Data to insert into the tree</param>
        ''' <param name="node">urrent node in the tree</param>
        ''' <returns>Node inserted into the tree</returns>
        Private Function insertHelper(data As T, node As Node(Of T)) As Node(Of T)
            Try
                Dim cmp As Integer = Me.compare(data, node)
                Select Case cmp
                    Case 1
                        If node.Right Is Nothing Then
                            Dim newNode As New Node(Of T)(data)
                            node.Right = newNode
                            newNode.Parent = node
                            Return newNode
                        Else
                            Return Me.insertHelper(data, node.Right)
                        End If
                    Case Else

                        If node.Left Is Nothing Then
                            Dim newNode As New Node(Of T)(data)
                            node.Left = newNode
                            newNode.Parent = node
                            Return newNode
                        Else
                            Return Me.insertHelper(data, node.Left)
                        End If
                End Select
            Catch e As Exception
                Return Nothing
            End Try
        End Function

        ''' <summary>
        ''' Removes a Node from the tree
        ''' </summary>
        ''' <param name="data">Data to remove from the tree</param>
        ''' <returns>Node removed from the tree</returns>
        Public Function Remove(data As T) As Node(Of T)
            If root IsNot Nothing Then
                Return Nothing
            End If
            Return Me.removeHelper(data, root)
        End Function

        ''' <summary>
        ''' Recursive helper function to remove a node from the tree
        ''' </summary>
        ''' <param name="data">Data to remove</param>
        ''' <param name="node">urrent node</param>
        ''' <returns>Node removed from the tree</returns>
        Private Function removeHelper(data As T, node As Node(Of T)) As Node(Of T)
            Try
                Dim cmp As Integer = Me.compare(data, node)
                If cmp = 1 Then
                    Return Me.removeHelper(data, node.Right)
                End If
                If cmp = -1 Then
                    Return Me.removeHelper(data, node.Left)
                End If
                If cmp = 0 Then
                    'has no children
                    Dim tempNode As Node(Of T)

                    If node.Left Is Nothing AndAlso node.Right Is Nothing Then
                        node = node.Parent
                    End If

                    'has one child
                    If node.Parent IsNot Nothing Then
                        tempNode = node.Parent
                    Else
                        ' this is the root node
                        If node.Right IsNot Nothing Then
                            tempNode = node.Right
                            tempNode.Parent = Nothing
                            Me.root.Right = Nothing
                            Me.root = tempNode
                            Return node
                        Else
                            tempNode = node.Left
                            tempNode.Parent = Nothing
                            Me.root.Left = Nothing
                            Me.root = tempNode
                            Return node
                        End If
                    End If
                    If tempNode.Left Is node Then
                        If node.Left IsNot Nothing Then
                            tempNode.Left = node.Left
                            node.Left.Parent = tempNode
                            Return node
                        ElseIf node.Right IsNot Nothing Then
                            tempNode.Right = node.Right
                            node.Right.Parent = tempNode
                            Return node
                        End If
                    End If
                    If tempNode.Right Is node Then
                        If node.Left IsNot Nothing Then
                            tempNode.Left = node.Left
                            node.Left.Parent = tempNode
                            Return node
                        ElseIf node.Right IsNot Nothing Then
                            tempNode.Right = node.Right
                            node.Right.Parent = tempNode
                            Return node
                        End If
                    ElseIf node.Left IsNot Nothing AndAlso node.Right IsNot Nothing Then
                        ' test for 2 children
                        tempNode = node.Right
                        ' find min value of right child tree to replace the node
                        While tempNode.Left IsNot Nothing
                            tempNode = tempNode.Left
                        End While
                        Dim temp As T = node.Data
                        node.Data = tempNode.Data
                        ' replace the node with the tempNode
                        tempNode.Data = temp
                        tempNode.Parent.Left = Nothing
                        tempNode.Parent = Nothing
                        Return tempNode
                    End If
                End If
            Catch e As Exception
            End Try
            Return Nothing
        End Function

        ''' <summary>
        ''' Returns the smallest node in the tree
        ''' </summary>
        ''' <returns>Smallest Node int he tree</returns>
        Public Function GetMin() As Node(Of T)
            If root Is Nothing Then
                Return Nothing
            End If

            Dim node As Node(Of T) = Me.root

            While node.Left IsNot Nothing
                node = node.Left
            End While

            Return node
        End Function

        ''' <summary>
        ''' Return the largest node in the tree
        ''' </summary>
        ''' <returns>argest Node in the tree</returns>
        Public Function GetMax() As Node(Of T)
            If root Is Nothing Then
                Return Nothing
            End If

            Dim node As Node(Of T) = Me.root

            While node.Right IsNot Nothing
                node = node.Right
            End While

            Return node
        End Function

        ''' <summary>
        ''' Prints out the tree using Pre Order Traversal
        ''' </summary>
        ''' <param name="node">Node to start the Pre Order Traversal at</param>
        Public Sub PreOrederTraversal(node As Node(Of T))
            If node IsNot Nothing Then
                Console.WriteLine(node.Data)
                PreOrederTraversal(node.Left)
                PreOrederTraversal(node.Right)
            End If
        End Sub

        ''' <summary>
        ''' Prints out the Tree using Post Order Traversal
        ''' </summary>
        ''' <param name="node">Node to start the Post Order Traversal at</param>
        Public Sub PostPrderTraversal(node As Node(Of T))
            If node IsNot Nothing Then
                PostPrderTraversal(node.Left)
                PostPrderTraversal(node.Right)
                Console.WriteLine(node.Data)
            End If
        End Sub

        ''' <summary>
        ''' Pints out the tree using in order Traversal
        ''' </summary>
        ''' <param name="node">Node to start the In Order Traversal at</param>
        Public Sub InOrderedTraversal(node As Node(Of T))
            If node IsNot Nothing Then
                InOrderedTraversal(node.Left)
                Console.WriteLine(node.Data)
                InOrderedTraversal(node.Right)
            End If
        End Sub

        ''' <summary>
        ''' Prints out the tree using Depth First Search
        ''' </summary>
        ''' <param name="node">Node to start the Depth First Search at</param>
        Public Sub DepthFirstSearch(node As Node(Of T))
            If node Is Nothing Then
                Return
            End If

            Dim stack As New Stack(Of Node(Of T))()
            stack.Push(node)

            While stack.Count > 0
                Console.WriteLine(InlineAssignHelper(node, stack.Pop()))
                If node.Right IsNot Nothing Then
                    stack.Push(node.Right)
                End If
                If node.Left IsNot Nothing Then
                    stack.Push(node.Left)
                End If
            End While
        End Sub

        ''' <summary>
        ''' Prints out the tree using breadth first search
        ''' </summary>
        ''' <param name="node">Node to start Breadth First Search at</param>
        Public Sub BreadthFirstSearch(node As Node(Of T))
            If node Is Nothing Then
                Return
            End If

            Dim queue As New Queue(Of Node(Of T))()
            queue.Enqueue(node)

            While queue.Count > 0
                Console.WriteLine(InlineAssignHelper(node, queue.Dequeue()))
                If node.Left IsNot Nothing Then
                    queue.Enqueue(node.Left)
                End If
                If node.Right IsNot Nothing Then
                    queue.Enqueue(node.Right)
                End If
            End While
        End Sub
        Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
            target = value
            Return value
        End Function
    End Class

End Namespace
