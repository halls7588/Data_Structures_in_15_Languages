'========================================================   
'  CircularArray.vb
'  Created by Stephen Hall on 9/22/17.
'  Copyright (c) 2017 Stephen Hall. All rights reserved.
'  A Circular Array implementation in Visual Basic.Net
'========================================================   

Namespace DataStructures
    ''' <summary>
    ''' Circulay Array class 
    ''' </summary>
    ''' <typeparam name="T">Generis type to be used</typeparam>
    Class CirculayArray(Of T)
        ''' <summary>
        ''' Private members
        ''' </summary>
        Private array As T()
        Private size As Integer
        Private zeroIndex As Integer
        Private count As Integer

        ''' <summary>
        ''' Default Circulay Array class constructor
        ''' </summary>
        Public Sub New()
            size = 10
            array = New T(size - 1) {}
            zeroIndex = 0
            count = 0
        End Sub

        ''' <summary>
        ''' Circulay Array class constructor
        ''' </summary>
        ''' <param name="size">Size to initialize array to</param>
        Public Sub New(size As Integer)
            Me.size = size
            array = New T(size - 1) {}
            zeroIndex = 0
            count = 0
        End Sub

        ''' <summary>
        ''' Adds new item into the array
        ''' </summary>
        ''' <param name="data">Data to add into the array</param>
        ''' <returns>Data added into the array</returns>
        Public Function Add(data As T) As T
            Dim tmp As Integer = (zeroIndex + count) Mod size
            array(tmp) = data
            If ((count + 1) / size) >= 1 Then
                Resize()
            End If
            count += 1
            Return array(tmp)
        End Function

        ''' <summary>
        ''' Gets the data at the arrays given index
        ''' </summary>
        ''' <param name="index">Index to get data at</param>
        ''' <returns>Data at the given index or default value of T if index does not exist</returns>
        Public Function DataAt(index As Integer) As T
            If (index + zeroIndex) Mod size < count AndAlso array((index + zeroIndex) Mod size) IsNot Nothing Then
                Return (array(index + zeroIndex Mod size))
            End If
            Return Nothing
        End Function

        ''' <summary>
        ''' Removes the data at arrays given index
        ''' </summary>
        ''' <param name="index">Index to remove</param>
        ''' <returns>Data removed from the array or default T value if index does not exist</returns>
        Public Function Remove(index As Integer) As T
            If index > size Then
                Return Nothing
            End If

            Dim tmp As T = array((index + zeroIndex Mod size))
            array((index + zeroIndex Mod size)) = array(zeroIndex)
            array(zeroIndex) = Nothing
            count -= 1
            zeroIndex = (zeroIndex + 1) Mod size
            Return tmp
        End Function

        ''' <summary>
        ''' Gets the current count of the array
        ''' </summary>
        ''' <returns>Number of items in the array</returns>
        Public Function ItemCount() As Integer
            Return count
        End Function

        ''' <summary>
        ''' Private method to resize the array if capasity has been reached
        ''' </summary>
        Private Sub Resize()
            size = size * 2
            Dim arr As T() = New T(size - 1) {}
            For i As Integer = 0 To array.Length - 1
                arr(i) = array(i)
            Next
            array = arr
        End Sub
    End Class
End Namespace

