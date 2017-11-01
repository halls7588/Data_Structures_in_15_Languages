'******************************************************
' *  LinkedQueue.vb
' *  Created by Stephen Hall on 11/01/17.
' *  Copyright (c) 2017 Stephen Hall. All rights reserved.
' *  A Linked Queue implementation in Visual Basic
' *******************************************************

Namespace Data_Structures
    ''' <summary>
    ''' Linked Queue Class
    ''' </summary>
    ''' <typeparam name="T">Generic Type</typeparam>
    Class LinkedQueue(Of T)
        ''' <summary>
        ''' Node Class For Linked Queue
        ''' </summary>
        ''' <typeparam name="T">Generic Type</typeparam>
        Public Class Node(Of T)
            ''' <summary>
            ''' Private members of the Node Class
            ''' </summary>
            Private m_data As T
            Private m_next As Node(Of T)

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
            ''' Public property for the Node Next Member
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
            ''' ode Class Constructor
            ''' </summary>
            ''' <param name="data">Data to be held in the node</param>
            Public Sub New(data As T)
                Me.m_data = data
                m_next = Nothing
            End Sub
        End Class

        ''' <summary>
        ''' Private members of the Linked Queue Class
        ''' </summary>
        Private count As Integer
        Private head As Node(Of T)
        Private tail As Node(Of T)

        ''' <summary>
        ''' Linked Queue class constructor
        ''' </summary>
        Public Sub New()
            count = 0
            head = Nothing
        End Sub

        ''' <summary>
        ''' Adds given data onto the Queue
        ''' </summary>
        ''' <param name="data">Data to be added to the queue</param>
        ''' <returns>Node added to the queue</returns>
        Public Function Enqueue(data As T) As Node(Of T)
            If IsEmpty() Then
                Dim node As New Node(Of T)(data)
                head = InlineAssignHelper(tail, node)
                count += 1
                Return node
            Else
                Dim node As New Node(Of T)(data)
                node.[Next] = tail
                tail = node
                count += 1
                Return node
            End If
        End Function

        ''' <summary>
        ''' removes item off the queue
        ''' </summary>
        ''' <returns>Node removed off of the queue</returns>
        Public Function Dequeue() As Node(Of T)
            Dim node As Node(Of T) = head
            head = head.[Next]
            node.[Next] = Nothing
            count -= 1
            Return node
        End Function

        ''' <summary>
        ''' Gets the first Node onto of the queue without removing it
        ''' </summary>
        ''' <returns>Node on top of the queue</returns>
        Public Function Top() As Node(Of T)
            Return head
        End Function

        ''' <summary>
        ''' Returns a value indicating if the queue is empty
        ''' </summary>
        ''' <returns>True if empty, false if not</returns>
        Public Function IsEmpty() As Boolean
            Return (count = 0)
        End Function

        ''' <summary>
        ''' Returns a value indicating if the queue is full
        ''' </summary>
        ''' <returns>False, Linked queue is never full</returns>
        Public Function IsFull() As Boolean
            Return False
        End Function
        Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
            target = value
            Return value
        End Function
    End Class
End Namespace

