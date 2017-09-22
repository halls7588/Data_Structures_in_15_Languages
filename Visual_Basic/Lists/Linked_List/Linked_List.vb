
Imports System.Collections.Generic
Imports System.Text

Namespace DataStructures
    ''' <summary>
    ''' Node class for singlely linked list
    ''' </summary>
    ''' <typeparam name="T">Generic type</typeparam>
    Class Node(Of T)
        Public Property Data() As T
            Get
                Return m_Data
            End Get
            Set
                m_Data = Value
            End Set
        End Property
        Private m_Data As T
        Public Property [Next]() As Node(Of T)
            Get
                Return m_Next
            End Get
            Set
                m_Next = Value
            End Set
        End Property
        Private m_Next As Node(Of T)

        Public Sub New(data As T)
            Me.Data = data
            Me.[Next] = Nothing
        End Sub
    End Class
    ''' <summary>
    ''' Singlely linked list class 
    ''' </summary>
    ''' <typeparam name="T">Generic type</typeparam>
    Class LinkedList(Of T)
        ''' <summary>
        ''' Internal varriables
        ''' </summary>
        Private count As Integer
        Private head As Node(Of T)
        Private tail As Node(Of T)

        ''' <summary>
        ''' Class constructor
        ''' </summary>
        Public Sub New()
            count = 0
            head = Nothing
            tail = Nothing
        End Sub

        ''' <summary>
        ''' Adds a new node into the list with the given data
        ''' </summary>
        ''' <param name="data">Data to add into the list</param>
        ''' <returns>Node added into the list</returns>
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
            tail = node
            count += 1
            Return node
        End Function

        ''' <summary>
        ''' Removes the first node in the list matching the data
        ''' </summary>
        ''' <param name="data">Data to remove from the list</param>
        ''' <returns>Node removed from the list</returns>
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
                    Dim node As Node(Of T) = tmp.[Next]
                    tmp.[Next] = tmp.[Next].[Next]
                    count -= 1
                    Return node
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
            'Index was negitive or larger then the amount of Nodes in the list
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
        ''' Gets the first index matching the given data or -1 if data does not exist in the list
        ''' </summary>
        ''' <param name="data">Data to find in the list</param>
        ''' <returns>First index that has data or -1</returns>
        Public Function IndexOf(data As T) As Integer
            Dim index As Integer = 0
            Dim tmp As Node(Of T) = head
            ' Try to find the data in the list
            While tmp IsNot Nothing
                ' Data was found, return current index
                If tmp.Data.Equals(data) Then
                    Return index
                End If
                index += 1
                tmp = tmp.[Next]
            End While
            ' Data was not found in the list
            Return -1
        End Function

        ''' <summary>
        ''' Returns the current size of the list
        ''' </summary>
        ''' <returns>Size of the list</returns>
        Public Function Size() As Integer
            Return count
        End Function
    End Class
End Namespace

