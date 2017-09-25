' *******************************************************
' LinkedStack.vb
' Created by Stephen Hall on 9/25/17.
' Copyright (c) 2017 Stephen Hall. All rights reserved.
' A Linked Stack implementation in Visual Basic
' *******************************************************

Namespace DataStructures
    ''' <summary>
    ''' Linked Stack Class
    ''' </summary>
    ''' <typeparam name="T">Generic Type</typeparam>
    Public Class LinkedStack(Of T)
        ''' <summary>
        ''' Node Class For Linked Stack
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
        ''' Private members of the Linked Stack Class
        ''' </summary>
        Private count As Integer
        Private head As Node(Of T)

        ''' <summary>
        ''' Linked Stack class constructor
        ''' </summary>
        Public Sub New()
            count = 0
            head = Nothing
        End Sub

        ''' <summary>
        ''' Pushes given data onto the stack
        ''' </summary>
        ''' <param name="data">Data to be added to the stack</param>
        ''' <returns>Node added to the stack</returns>
        Public Function Push(data As T) As Node(Of T)
            Dim node As New Node(Of T)(data)
            node.[Next] = head
            head = node
            count += 1
            Return Top()
        End Function

        ''' <summary>
        ''' Pops item off the stack
        ''' </summary>
        ''' <returns>Node popped off of the stack</returns>
        Public Function Pop() As Node(Of T)
            Dim node As Node(Of T) = head
            head = head.[Next]
            node.[Next] = Nothing
            count -= 1
            Return node
        End Function

        ''' <summary>
        ''' Gets the first Node onto of the stack without removing it
        ''' </summary>
        ''' <returns>Node on top of the stack</returns>
        Public Function Top() As Node(Of T)
            Return head
        End Function

        ''' <summary>
        ''' Returns a value indicating if the stack is empty
        ''' </summary>
        ''' <returns>True if empty, false if not</returns>
        Public Function IsEmpty() As Boolean
            Return (count = 0)
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
