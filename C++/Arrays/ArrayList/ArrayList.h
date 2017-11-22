/*******************************************************
 *  @file ArrayList.h
 *  @author Stephen Hall
 *  @date 11/22/17.
 *  @copyright 2017 Stephen Hall. All rights reserved.
 *  @details ArrayList Header in C++
 ********************************************************/
#ifndef ARRAYLIST_H
#define ARRAYLIST_H

namespace Arrays::ArrayList
{
    /**
     * ArrayList class declaration
     * @tparam T: Generic Type
     */
    template<typename T>
    class ArrayList {
    private:
        /* Private members of the ArrayList class */
        T *array;
        int count;
        int size;

    public:
        /* Public Methods of the ArrayList class */
        ArrayList();

        explicit ArrayList(unsigned int size);

        ~ArrayList();

        T add(T data);

        bool append(T *dataArray, int size);

        T &operator[](int index);

        T remove(T data);

        void reset();

        void clear();

        int length();

    private:
        /**
         * Private methods of the ArrayList class
         */
        void resize();
    };
}
#endif ARRAYLIST_H
