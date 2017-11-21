'******************************************************
' *  RedBlackTree.vb
' *  Created by Stephen Hall on 11/21/17.
' *  Copyright (c) 2017 Stephen Hall. All rights reserved.
' *  A Rec Black Tree implementation in Visual Basic
' *******************************************************

Namespace Trees.RedBlackTree
	''' <summary>
	''' RedBlackTree class
	''' </summary>
	''' <typeparam name="T">Generic type</typeparam>
	Public Class RedBlackTree(Of T As IComparable)
		''' <summary>
		''' Node colors
		''' </summary>
		Private Shared BLACK As Integer = 0
		Private Shared RED As Integer = 1

		''' <summary>
		''' Node Class for redblacktree
		''' </summary>
		Public Class Node
			''' <summary>
			''' Public properties of the node class
			''' </summary>
			Public Property Key As T
			Public Property Parent As Node
			Public Property Left As Node
			Public Property Right As Node
			Public Property NumLeft As Integer
			Public Property NumRight As Integer
			Public Property Color As Integer

			''' <summary>
			''' Node Constructor
			''' </summary>
			Public Sub New()
				Color = BLACK
				NumLeft = 0
				NumRight = 0
				Parent = Nothing
				Left = Nothing
				Right = Nothing
			End Sub

			''' <summary>
			''' Constructor which sets key to the argument.
			''' </summary>
			''' <param name="key1">key to set</param>
			Public Sub New(key1 As T)
				Color = BLACK
				NumLeft = 0
				NumRight = 0
				Parent = Nothing
				Left = Nothing
				Right = Nothing
				Key = key1
			End Sub
		End Class

		''' <summary>
		''' private member of the redblack tree.
		''' </summary>
		Private ReadOnly _nil As Node
		Private _root As Node

		''' <summary>
		''' RedBalckTree Constructor
		''' </summary>
		Public Sub New()
			_nil = New Node()
			_root = _nil
			_root.Left = _nil
			_root.Right = _nil
			_root.Parent = _nil
		End Sub

		''' <summary>
		''' Rotates a node left
		''' </summary>
		''' <param name="x">The node which the lefRotate is to be performed on.</param>
		Private Sub RotateLeft(x As Node)
			' Call rotateLeftFixup() which updates the numLeft
			' and numRight values.
			RotateLeftFixup(x)
			' Perform the left rotate as described in the algorithm
			' in the course text.
			Dim y As Node = x.Right
			x.Right = y.Left
			' Check for existence of y.left and make pointer changes
			If Not IsNil(y.Left) Then
				y.Left.Parent = x
			End If
			y.Parent = x.Parent
			' x's parent is null
			If IsNil(x.Parent) Then
				_root = y
			' x is the left child of it's parent
			ElseIf x.Parent.Left Is x Then
				x.Parent.Left = y
			Else
				' x is the right child of it's parent.
				x.Parent.Right = y
			End If
			' Finish of the leftRotate
			y.Left = x
			x.Parent = y
		End Sub

		''' <summary>
		''' Updates the numLeft & numRight values affected by leftRotate.
		''' </summary>
		''' <param name="x">The node which the leftRotate is to be performed on.</param>
		Private Sub RotateLeftFixup(x As Node)
			' Case 1: Only x, x.right and x.right.right always are not nil.
			If IsNil(x.Left) AndAlso IsNil(x.Right.Left) Then
				x.NumLeft = 0
				x.NumRight = 0
				x.Right.NumLeft = 1
			' Case 2: x.right.left also exists in addition to Case 1
			ElseIf IsNil(x.Left) AndAlso Not IsNil(x.Right.Left) Then
				x.NumLeft = 0
				x.NumRight = 1 + x.Right.Left.NumLeft + x.Right.Left.NumRight
				x.Right.NumLeft = 2 + x.Right.Left.NumLeft + x.Right.Left.NumRight
			' Case 3: x.left also exists in addition to Case 1
			ElseIf Not IsNil(x.Left) AndAlso IsNil(x.Right.Left) Then
				x.NumRight = 0
				x.Right.NumLeft = 2 + x.Left.NumLeft + x.Left.NumRight
			Else
				' Case 4: x.left and x.right.left both exist in addtion to Case 1
				x.NumRight = 1 + x.Right.Left.NumLeft + x.Right.Left.NumRight
				x.Right.NumLeft = 3 + x.Left.NumLeft + x.Left.NumRight + x.Right.Left.NumLeft + x.Right.Left.NumRight
			End If
		End Sub

		''' <summary>
		''' Updates the numLeft and numRight values affected by the Rotate.
		''' </summary>
		''' <param name="y"> The node which the rightRotate is to be performed</param>
		Private Sub RotateRight(y As Node)
			' Call rightRotateFixup to adjust numRight and numLeft values
			RotateRightFixup(y)
			' Perform the rotate as described in the course text.
			Dim x As Node = y.Left
			y.Left = x.Right
			' Check for existence of x.right
			If Not IsNil(x.Right) Then
				x.Right.Parent = y
			End If
			x.Parent = y.Parent
			' y.parent is nil
			If IsNil(y.Parent) Then
				_root = x
			' y is a right child of it's parent.
			ElseIf y.Parent.Right Is y Then
				y.Parent.Right = x
			Else
				' y is a left child of it's parent.
				y.Parent.Left = x
			End If

			x.Right = y
			y.Parent = x
		End Sub

		''' <summary>
		''' Updates the numLeft and numRight values affected by the rotate
		''' </summary>
		''' <param name="y">the node around which the righRotate is to be performed.</param>
		Private Sub RotateRightFixup(y As Node)
			' Case 1: Only y, y.left and y.left.left exists.
			If IsNil(y.Right) AndAlso IsNil(y.Left.Right) Then
				y.NumRight = 0
				y.NumLeft = 0
				y.Left.NumRight = 1
			' Case 2: y.left.right also exists in addition to Case 1
			ElseIf IsNil(y.Right) AndAlso Not IsNil(y.Left.Right) Then
				y.NumRight = 0
				y.NumLeft = 1 + y.Left.Right.NumRight + y.Left.Right.NumLeft
				y.Left.NumRight = 2 + y.Left.Right.NumRight + y.Left.Right.NumLeft
			' Case 3: y.right also exists in addition to Case 1
			ElseIf Not IsNil(y.Right) AndAlso IsNil(y.Left.Right) Then
				y.NumLeft = 0
				y.Left.NumRight = 2 + y.Right.NumRight + y.Right.NumLeft
			Else
				' Case 4: y.right & y.left.right exist in addition to Case 1
				y.NumLeft = 1 + y.Left.Right.NumRight + y.Left.Right.NumLeft
				y.Left.NumRight = 3 + y.Right.NumRight + y.Right.NumLeft + y.Left.Right.NumRight + y.Left.Right.NumLeft
			End If
		End Sub

		''' <summary>
		''' Inserts new key into the tree
		''' </summary>
		''' <param name="key">key to insert</param>
		Public Sub Insert(key As T)
			Insert(New Node(key))
		End Sub

		''' <summary>
		''' Inserts z into the appropriate position in the RedBlackTree while updating numLeft and numRight values.
		''' </summary>
		''' <param name="z">the node to be inserted into the Tree rooted at root</param>
		Private Sub Insert(z As Node)
			' Create a reference to root & initialize a node to nil
			Dim y As Node = _nil
			Dim x As Node = _root
			' While we haven't reached a the end of the tree keep
			' trying to figure out where z should go
			While Not IsNil(x)
				y = x
				' if z.key is < than the current key, go left
				If z.Key.CompareTo(x.Key) < 0 Then
					' Update x.numLeft as z is < than x
					x.NumLeft += 1
					x = x.Left
				Else
					' else z.key >= x.key so go right.
					' Update x.numGreater as z is => x
					x.NumRight += 1
					x = x.Right
				End If
			End While
			' y will hold z's parent
			z.Parent = y
			' Depending on the value of y.key, put z as the left or
			' right child of y
			If IsNil(y) Then
				_root = z
			ElseIf z.Key.CompareTo(y.Key) < 0 Then
				y.Left = z
			Else
				y.Right = z
			End If
			' Initialize z's children to nil and z's color to red
			z.Left = _nil
			z.Right = _nil
			z.Color = RED
			' Call insertFixup(z)
			InsertFixup(z)
		End Sub

		''' <summary>
		''' Fixes up the violation of the RedBlackTree properties that may have been caused during insert(z)
		''' </summary>
		''' <param name="z">the node which was inserted and may have caused a violation of the RedBlackTree properties</param>
		Private Sub InsertFixup(z As Node)
			Dim y As Node = _nil
			' While there is a violation of the RedBlackTree properties..
			While z.Parent.Color = RED
				' If z's parent is the the left child of it's parent.
				If z.Parent Is z.Parent.Parent.Left Then
					' Initialize y to z 's cousin
					y = z.Parent.Parent.Right
					' Case 1: if y is red...recolors
					If y.Color = RED Then
						z.Parent.Color = BLACK
						y.Color = BLACK
						z.Parent.Parent.Color = RED
						z = z.Parent.Parent
					' Case 2: if y is black & z is a right child
					ElseIf z Is z.Parent.Right Then
						' leftRotaet around z's parent
						z = z.Parent
						RotateLeft(z)
					Else
						' Case 3: else y is black & z is a left child
						' Recolors and rotate round z's grandpa
						z.Parent.Color = BLACK
						z.Parent.Parent.Color = RED
						RotateRight(z.Parent.Parent)
					End If
				Else
					' If z's parent is the right child of it's parent.
					' Initialize y to z's cousin
					y = z.Parent.Parent.Left
					' Case 1: if y is red...recolors
					If y.Color = RED Then
						z.Parent.Color = BLACK
						y.Color = BLACK
						z.Parent.Parent.Color = RED
						z = z.Parent.Parent
					' Case 2: if y is black and z is a left child
					ElseIf z Is z.Parent.Left Then
						' rightRotate around z's parent
						z = z.Parent
						RotateRight(z)
					Else
						' Case 3: if y  is black and z is a right child
						' Recolors and rotate around z's grandpa
						z.Parent.Color = BLACK
						z.Parent.Parent.Color = RED
						RotateLeft(z.Parent.Parent)
					End If
				End If
			End While
			' Color root black at all times
			_root.Color = BLACK
		End Sub

		''' <summary>
		''' gets the smallest node in the tree
		''' </summary>
		''' <param name="node">Start node to find smallest of</param>
		''' <returns>the node with the smallest key rooted at node</returns>
		Public Function TreeMinimum(node As Node) As Node
			' while there is a smaller key, keep going left
			While Not IsNil(node.Left)
				node = node.Left
			End While
			Return node
		End Function

		''' <summary>
		''' Returns the next largest key from the given node
		''' </summary>
		''' <param name="x">Node whose successor we must find</param>
		''' <returns>node the with the next largest key from x.key</returns>
		Public Function TreeSuccessor(x As Node) As Node
			' if x.left is not nil, call treeMinimum(x.right) and
			' return it's value
			If Not IsNil(x.Left) Then
				Return TreeMinimum(x.Right)
			End If
			Dim y As Node = x.Parent
			' while x is it's parent's right child...
			While Not IsNil(y) AndAlso x Is y.Right
				' Keep moving up in the tree
				x = y
				y = y.Parent
			End While
			' Return successor
			Return y
		End Function

		''' <summary>
		''' Removes a Node from the tree
		''' </summary>
		''' <param name="node">Node which is to be removed from the the tree</param>
		Public Sub Remove(node As Node)
			Dim z As Node = Search(node.Key)
			Dim x As Node = _nil
			Dim y As Node = _nil
			' if either one of z's children is nil, then we must remove z
			If IsNil(z.Left) OrElse IsNil(z.Right) Then
				y = z
			Else
				' else we must remove the successor of z
				y = TreeSuccessor(z)
			End If
			' Let x be the left or right child of y (y can only have one child)
			If Not IsNil(y.Left) Then
				x = y.Left
			Else
				x = y.Right
			End If
			' link x's parent to y's parent
			x.Parent = y.Parent
			' If y's parent is nil, then x is the root
			If IsNil(y.Parent) Then
				_root = x
			' else if y is a left child, set x to be y's left sibling
			ElseIf Not IsNil(y.Parent.Left) AndAlso y.Parent.Left Is y Then
				y.Parent.Left = x
			' else if y is a right child, set x to be y's right sibling
			ElseIf Not IsNil(y.Parent.Right) AndAlso y.Parent.Right Is y Then
				y.Parent.Right = x
			End If
			' if y != z, transfer y's satellite data into z.
			If y IsNot z Then
				z.Key = y.Key
			End If
			' Update the numLeft and numRight numbers which might need
			' updating due to the deletion of z.key.
			FixNodeData(x, y)
			' If y's color is black, it is a violation 
			If y.Color = BLACK Then
				RemoveFixup(x)
			End If
		End Sub

		''' <summary>
		''' Updates the numLeft and numRight numbers which might need updating due to the deletion
		''' </summary>
		''' <param name="x">the RedBlackNode which was actually deleted from the tree</param>
		''' <param name="y">the value of the key that used to be in y</param>
		Private Sub FixNodeData(x As Node, y As Node)
			' Initialize two variables which will help us traverse the tree
			Dim current As Node = _nil
			Dim track As Node = _nil
			' if x is nil, then we will start updating at y.parent
			' Set track to y, y.parent's child
			If IsNil(x) Then
				current = y.Parent
				track = y
			Else
				' if x is not nil, then we start updating at x.parent
				' Set track to x, x.parent's child
				current = x.Parent
				track = x
			End If
			' while we haven't reached the root
			While Not IsNil(current)
				' if the node we deleted has a different key than
				' the current node
				If Not y.Key.Equals(current.Key) Then
					' if the node we deleted is greater than
					' current.key then decrement current.numRight
					If y.Key.CompareTo(current.Key) > 0 Then
						current.NumRight -= 1
					End If
					' if the node we deleted is less than
					' current.key then decrement current.numLeft
					If y.Key.CompareTo(current.Key) < 0 Then
						current.NumLeft -= 1
					End If
				Else
					' if the node we deleted has the same key as the
					' current node we are checking
					' the cases where the current node has any nil
					' children and update appropriately
					If IsNil(current.Left) Then
						current.NumLeft -= 1
					ElseIf IsNil(current.Right) Then
						current.NumRight -= 1
					' the cases where current has two children and
					' we must determine whether track is it's left
					' or right child and update appropriately
					ElseIf track Is current.Right Then
						current.NumRight -= 1
					ElseIf track Is current.Left Then
						current.NumLeft -= 1
					End If
				End If
				' update track and current
				track = current
				current = current.Parent
			End While
		End Sub

		''' <summary>
		''' Restores the Red Black properties that may have been violated during removal
		''' </summary>
		''' <param name="x">the child of the deleted node from remove(RedBlackNode v)</param>
		Private Sub RemoveFixup(x As Node)
			Dim w As Node
			' While we haven't fixed the tree completely...
			While x IsNot _root AndAlso x.Color = BLACK
				' if x is it's parent's left child
				If x Is x.Parent.Left Then
					' set w = x's sibling
					w = x.Parent.Right
					' Case 1, w's color is red.
					If w.Color = RED Then
						w.Color = BLACK
						x.Parent.Color = RED
						RotateLeft(x.Parent)
						w = x.Parent.Right
					End If
					' Case 2, both of w's children are black
					If w.Left.Color = BLACK AndAlso w.Right.Color = BLACK Then
						w.Color = RED
						x = x.Parent
					Else
						' Case 3 / Case 4
						' Case 3, w's right child is black
						If w.Right.Color = BLACK Then
							w.Left.Color = BLACK
							w.Color = RED
							RotateRight(w)
							w = x.Parent.Right
						End If
						' Case 4, w = black, w.right = red
						w.Color = x.Parent.Color
						x.Parent.Color = BLACK
						w.Right.Color = BLACK
						RotateRight(x.Parent)
						x = _root
					End If
				Else
					' if x is it's parent's right child
					' set w to x's sibling
					w = x.Parent.Left
					' Case 1, w's color is red
					If w.Color = RED Then
						w.Color = BLACK
						x.Parent.Color = RED
						RotateRight(x.Parent)
						w = x.Parent.Left
					End If
					' Case 2, both of w's children are black
					If w.Right.Color = BLACK AndAlso w.Left.Color = BLACK Then
						w.Color = RED
						x = x.Parent
					Else
						' Case 3 / Case 4
						' Case 3, w's left child is black
						If w.Left.Color = BLACK Then
							w.Right.Color = BLACK
							w.Color = RED
							RotateRight(w)
							w = x.Parent.Left
						End If
						' Case 4, w = black, and w.left = red
						w.Color = x.Parent.Color
						x.Parent.Color = BLACK
						w.Left.Color = BLACK
						RotateRight(x.Parent)
						x = _root
					End If
				End If
			End While
			' set x to black to ensure there is no violation
			x.Color = BLACK
		End Sub

		''' <summary>
		''' Searches for a node with key k and returns the first such node
		''' </summary>
		''' <param name="key">the key whose node we want to search for</param>
		''' <returns>node with the key if found or null</returns>
		Public Function Search(key As T) As Node
			' Initialize a pointer to the root to traverse the tree
			Dim current As Node = _root
			' While we haven't reached the end of the tree
			While Not IsNil(current)
				' If we have found a node with a key equal to key
				If current.Key.Equals(key) Then
					' return that node and exit search(int)
					Return current
				' go left or right based on value of current and key
				ElseIf current.Key.CompareTo(key) < 0 Then
					current = current.Right
				Else
					' go left or right based on value of current and key
					current = current.Left
				End If
			End While
			' we have not found a node whose key is "key"
			Return Nothing
		End Function

		''' <summary>
		''' Returns the number of nodes in the tree that are greater then the given key
		''' </summary>
		''' <param name="key">Key to test</param>
		''' <returns>the number of elements greater than key</returns>
		Public Function NumGreater(key As T) As Integer
			Return FindNumGreater(_root, key)
		End Function

		''' <summary>
		''' Returns the number of nodes in the tree that are smaller then the given key
		''' </summary>
		''' <param name="key">Key to test</param>
		''' <returns>the number of elements smaller than key</returns>
		Public Function NumSmaller(key As T) As Integer
			Return FindNumSmaller(_root, key)
		End Function

		''' <summary>
		''' Gets the number of nodes greater then the key of the given tree
		''' </summary>
		''' <param name="node">Root or subtree root node to test on</param>
		''' <param name="key">key to compare to</param>
		''' <returns>number of nodes greater then the key</returns>
		Public Function FindNumGreater(node As Node, key As T) As Integer
			' Base Case: if node is nil, return 0
			If IsNil(node) Then
				Return 0
			End If
			' If key is less than node.key
			If key.CompareTo(node.Key) < 0 Then
				Return 1 + node.NumRight + FindNumGreater(node.Left, key)
			End If
			' If key is greater than node.key
			Return FindNumGreater(node.Right, key)
		End Function

		''' <summary>
		''' Returns sorted list of keys greater than key. Size of list will not exceed maxReturned
		''' </summary>
		''' <param name="key">Key to search for</param>
		''' <param name="maxReturned">Maximum number of results to return</param>
		''' <returns>ist of keys greater than key. List may not exceed maxReturned</returns>
		Public Function GetGreaterThan(key As T, maxReturned As Int32) As List(Of T)
			Dim list As New List(Of T)()
			GetGreaterThan(_root, key, list)
			Return list.GetRange(0, Math.Min(maxReturned, list.Count))
		End Function

		''' <summary>
		''' Gets the nodes greater then the key
		''' </summary>
		''' <param name="node">Node to test on key</param>
		''' <param name="key">Key to test</param>
		''' <param name="list">List of nodes greater then the key</param>
		Private Sub GetGreaterThan(node As Node, key As T, list As List(Of T))
			If Not IsNil(node) Then
				If node.Key.CompareTo(key) > 0 Then
					GetGreaterThan(node.Left, key, list)
					list.Add(node.Key)
					GetGreaterThan(node.Right, key, list)
				Else
					GetGreaterThan(node.Right, key, list)
				End If
			End If
		End Sub

		''' <summary>
		''' Takes root or subtree root node and finds number of elements smaller then the given key
		''' </summary>
		''' <param name="node">Root of the tree to start comparison</param>
		''' <param name="key">key to compare</param>
		''' <returns>number of nodes smaller than key.</returns>
		Public Function FindNumSmaller(node As Node, key As T) As Integer
			' Base Case: if node is nil, return 0
			If IsNil(node) Then
				Return 0
			End If
			' If key is less than node.key, look to the left as all
			If key.CompareTo(node.Key) <= 0 Then
				Return FindNumSmaller(node.Left, key)
			End If
			' If key is larger than node.key, all elements to the left of
			Return node.NumLeft + FindNumSmaller(node.Right, key) + 1

		End Function

		''' <summary>
		''' tests if a node is the nil node
		''' </summary>
		''' <param name="node">node to test</param>
		''' <returns>true|false</returns>
		Private Function IsNil(node As Node) As Boolean
			Return node Is _nil
		End Function

		''' <summary>
		''' returns the size of the tree
		''' </summary>
		''' <returns>size of the tree</returns>
		Public Function Size() As Integer
			Return _root.NumLeft + _root.NumRight + 1
		End Function
	End Class
End Namespace