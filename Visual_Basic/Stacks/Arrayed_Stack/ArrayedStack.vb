' ********************************************************
' ArrayedStack.vb
' Created by Stephen Hall on 9/25/17.
' Copyright (c) 2017 Stephen Hall. All rights reserved.
' A Arrayed Stack implementation in Visual Basic
' *******************************************************

Namespace DataStructures
    ''' <summary>
    ''' Arrayed Stack Class
    ''' </summary>
    ''' <typeparam name="T">Generic Type</typeparam>
    Public Class ArrayedStack(Of T)
        ''' <summary>
        ''' Private members
        ''' </summary>
        Private array As T()
        Private count As Integer
        Private size As Integer

        ''' <summary>
        ''' Default Constructor
        ''' </summary>
        Public Sub New()
            array = New T((InlineAssignHelper(size, 10)) - 1) {}
            count = 0
        End Sub

        ''' <summary>
        ''' Arrayed Stack Constructor
        ''' </summary>
        ''' <param name="size">Size to initialize the stack to</param>
        Public Sub New(size As Integer)
            array = New T((InlineAssignHelper(Me.size, size)) - 1) {}
            count = 0
        End Sub

        ''' <summary>
        ''' Pushes given data onto the stack if space is available
        ''' </summary>
        ''' <param name="data">Data to be added to the stack</param>
        ''' <returns>Node added to the stack</returns>
        Public Function Push(data As T) As T
            If Not IsFull() Then
                array(count) = data
                count += 1
                Return Top()
            End If
            Return Nothing
        End Function

        ''' <summary>
        ''' Pops the first item off the stack
        ''' </summary>
        ''' <returns>Node popped off of the stack</returns>
        Public Function Pop() As T
            Dim data As T = array(count - 1)
            count -= 1
            Return data
        End Function

        ''' <summary>
        ''' Gets the Node onto of the stack without removing it
        ''' </summary>
        ''' <returns>Node on top of the stack</returns>
        Public Function Top() As T
            Return array(count - 1)
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
        ''' <returns>True if full, false if not</returns>
        Public Function IsFull() As Boolean
            Return (count = size)
        End Function
        Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
            target = value
            Return value
        End Function
    End Class

End Namespace