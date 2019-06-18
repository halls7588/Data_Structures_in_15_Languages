"""-----------------------------------------------------
-  Circular_Array.py
-  Created by Stephen Hall on 05/14/19.
-  Copyright (c) 2019 Stephen Hall. All rights reserved.
- A Circular Array implementation in Python
-----------------------------------------------------"""


class CircularArray:
    def __init__(self):
        self._array = []
        self._size = 10
        self._zeroIndex = 0
        self._count = 0

    """
    ' Doubles the size of the array
    """
    def resize(self):
        self._size *= 2

    """
    ' Adds an item into the array
    ' @param data: data to be added
    ' @return mixed
    """
    def add(self, data):
        tmp = (self._zeroIndex + self._count) % self._size
        self._array[tmp] = data
        if ((self._count + 1) / self._size) >= 1:
            self.resize()

        self._count += 1
        return self._array[tmp]

    """
    ' Gets the data at passed in index
    ' @param $i: index to retrieve
    ' @return mixed|null: data at index or null
    """
    def dataAt(self, i):
        if (i + self._zeroIndex) % self._size < self._count and self._array[(i + self._zeroIndex) % self._size]:
            return self._array[i + self._zeroIndex % self._size]

        return None

    """
    ' Removes an item from the array
    ' @param $i: index to remove
    ' @return mixed|null: data removed from the array or null
    """
    def remove(self, i):
        tmp = self._array[i + self._zeroIndex % self._size]
        self._array[i + self._zeroIndex % self._size] = self._array[self._zeroIndex]
        self._array[self._zeroIndex] = None
        self._count -=1
        self._zeroIndex = (self._zeroIndex + 1) % self._size
        return tmp

    """
    ' Prints the array
    """
    def printArr(self):
        tmp = ""
        i = 0
        while i < self._size:
            tmp += self._array[(self._zeroIndex + i) % self._size] + "->"
            i += 1

        print(tmp)

