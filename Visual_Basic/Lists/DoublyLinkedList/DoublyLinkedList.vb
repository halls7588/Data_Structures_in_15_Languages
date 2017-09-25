' *******************************************************
' DoublyLinkedList.cs
' Created by Stephen Hall on 9/25/17.
' Copyright (c) 2017 Stephen Hall. All rights reserved.
' A Linked List implementation in C#
' *******************************************************

Namespace DataStructures
    ''' <summary>
    ''' Doubly linked List Class
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    Public Class DoublyLinkedList(Of T)
        ''' <summary>
        ''' Node class for Doubly linked list
        ''' </summary>
        ''' <typeparam name="T"Ggeneric type</typeparam>
        Public Class Node(Of T)
            ''' <summary>
            ''' Private Member of the Node class
            ''' </summary>
            Private m_data As T
            Private m_next As Node(Of T)
            Private m_previous As Node(Of T)

            ''' <summary>
            ''' Public Property for private data member
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
            ''' Public property for private next member
            ''' </summary>
            Public Property [Next]() As Node(Of T)
                Get
                    Return m_next
                End Get
                Set
                    m_next = Value
                End Set
            End Property
            ''' <summary>
            ''' Public Property for private previous member
            ''' </summary>
            Public Property Previous() As Node(Of T)
                Get
                    Return m_previous
                End Get
                Set
                    m_previous = Value
                End Set
            End Property

            ''' <summary>
            ''' Node class constructor
            ''' </summary>
            ''' <param name="data">Generic type</param>
            Public Sub New(data As T)
                Me.m_data = data
                m_next = InlineAssignHelper(m_previous, Nothing)
            End Sub
            Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
                target = value
                Return value
            End Function

        End Class

        ''' <summary>
        ''' Private data members of the Doubly Linked List class
        ''' </summary>
        Private head As Node(Of T)
        Private tail As Node(Of T)
        Private count As Integer

        ''' <summary>
        ''' Doubly Linked List class constructor
        ''' </summary>
        Public Sub New()
            head = InlineAssignHelper(tail, Nothing)
            count = 0
        End Sub

        ''' <summary>
        ''' Adds a new node into the list with the given data
        ''' </summary>
        ''' <param name="data">Data to add into the list</param>
        ''' <returns>ode added into the list</returns>
        Public Function Add(data As T) As Node(Of T)
            ' No data to insert into list
            If data Is Nothing Then
                Return Nothing
            End If

            Dim node As New Node(Of T)(data)

            ' The Linked list is empty
            If head Is Nothing Then
                head = node
                tail = head
                count += 1
                Return node
            End If

            ' Add to the end of the list
            tail.[Next] = node
            node.Previous = tail
            tail = node
            count += 1
            Return node
        End Function

        ''' <summary>
        ''' Removes the first node in the list matching the data
        ''' </summary>
        ''' <param name="data">Data to remove from the list</param>
        ''' <returns> Node removed from the list</returns>
        Public Function Remove(data As T) As Node(Of T)

            ' List is empty or no data to remove
            If head Is Nothing OrElse data Is Nothing Then
                Return Nothing
            End If

            Dim tmp As Node(Of T) = head
            ' The data to remove what found in the first Node in the list
            If tmp.Data.Equals(data) Then
                head = head.[Next]
                count -= 1
                Return tmp
            End If

            ' Try to find the node in the list
            While tmp.[Next] IsNot Nothing
                ' Node was found, Remove it from the list
                If tmp.[Next].Data.Equals(data) Then
                    If tmp.[Next] Is tail Then
                        tail = tmp
                        tmp = tmp.[Next]
                        tail.[Next] = Nothing
                        count -= 1
                        Return tmp
                    Else
                        Dim node As Node(Of T) = tmp.[Next]
                        tmp.[Next] = tmp.[Next].[Next]
                        tmp.[Next].[Next].Previous = tmp
                        node.[Next] = InlineAssignHelper(node.Previous, Nothing)
                        count -= 1
                        Return node
                    End If
                End If
            End While
            ' The data was not found in the list
            Return Nothing
        End Function

        ''' <summary>
        ''' Gets the first node that has the given data
        ''' </summary>
        ''' <param name="data">Data to find in the list</param>
        ''' <returns>First node with matching data or null if no node was found</returns>
        Public Function Find(data As T) As Node(Of T)
            ' No list or data to find
            If head Is Nothing OrElse data Is Nothing Then
                Return Nothing
            End If

            Dim tmp As Node(Of T) = head
            ' Try to find the data in the list
            While tmp IsNot Nothing
                ' Data was found
                If tmp.Data.Equals(data) Then
                    Return tmp
                End If

                tmp = tmp.[Next]
            End While
            ' Data was not found in the list
            Return Nothing
        End Function

        ''' <summary>
        ''' Gets the node at the given index
        ''' </summary>
        ''' <param name="index">Index of the Node to get</param>
        ''' <returns>Node at passed in index</returns>
        Public Function IndexAt(index As Integer) As Node(Of T)
            'Index was negative or larger then the amount of Nodes in the list
            If index < 0 OrElse index > Size() Then
                Return Nothing
            End If

            Dim tmp As Node(Of T) = head

            ' Move to index
            For i As Integer = 0 To index - 1
                tmp = tmp.[Next]
            Next
            ' return the node at the index position
            Return tmp
        End Function

        ''' <summary>
        ''' Gets the current count of the array
        ''' </summary>
        ''' <returns>Number of items in the array</returns>
        Public Function Size() As Integer
            Return count
        End Function
        Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
            target = value
            Return value
        End Function
    End Class
End Namespace
