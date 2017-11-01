'******************************************************
' *  ArrayedQueue.vb
' *  Created by Stephen Hall on 11/01/17.
' *  Copyright (c) 2017 Stephen Hall. All rights reserved.
' *  A Arrayed Queue implementation in Visual Basic
' *******************************************************


Namespace Data_Structures
    Class ArrayedQueue(Of T)
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
        ''' Arrayed queue Constructor
        ''' </summary>
        ''' <param name="size">Size to initialize the queue to</param>
        Public Sub New(size As Integer)
            array = New T((InlineAssignHelper(Me.size, size)) - 1) {}
            count = 0
        End Sub

        ''' <summary>
        ''' Add given data onto the queue if space is available
        ''' </summary>
        ''' <param name="data">Data to be added to the queue</param>
        ''' <returns>item added to the queue</returns>
        Public Function Enqueue(data As T) As T
            If Not IsFull() Then
                array(count) = data
                count += 1
                Return Top()
            End If
            Return Nothing
        End Function

        ''' <summary>
        ''' Removed the first item off the queue
        ''' </summary>
        ''' <returns>item removed off of the queue</returns>
        Public Function Dequeue() As T
            If IsEmpty() Then
                Return Nothing
            End If

            Dim data As T = array(0)
            Dim tmp As T() = New T((Me.size) - 1) {}
            For i As Integer = 1 To size - 2
                tmp(i - 1) = array(i)
            Next
            array = tmp
            count -= 1
            Return data
        End Function

        ''' <summary>
        ''' Gets the item onto of the queue without removing it
        ''' </summary>
        ''' <returns>Node on top of the queue</returns>
        Public Function Top() As T
            If IsEmpty() Then
                Return Nothing
            End If
            Return array(0)
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