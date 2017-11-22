//
// Created by stephenh on 11/22/2017.
//

#ifndef ARRAYLIST_H
#define ARRAYLIST_H

template <typename T>
class ArrayList {
private:
    T* array;
    int count;
    int size;

public:
    ArrayList();
    ArrayList(unsigned int size);
    ~ArrayList();
    T add(T data);
    bool append(T* dataArray, int size);
    T& operator[](int index);
    T remove(T data);
    void reset();
    void clear();
    int length();

private:
    void resize();
};


#endif ARRAYLIST_H
