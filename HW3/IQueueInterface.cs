﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3
{
    public interface IQueueInterface<T> 
    {
        /**
         * Add an element to the rear of the queue
         * 
         * @return the element that was enqueued
         */
        T Push(T element);

        /**
     * Remove and return the front element.
     * 
     * @throws Thrown if the queue is empty
     */
        T Pop();

        /**
     * Return but don't remove the front element.
     * 
     * @throws Thrown if the queue is empty
     */
        T Peek();


        /**
     * Test if the queue is empty
     * 
     * @return true if the queue is empty; otherwise false
     */
        bool IsEmpty();


 






    }


}
