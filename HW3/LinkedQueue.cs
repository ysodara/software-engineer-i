using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3
{
    class LinkedQueue<T> :  QueueUnderflowException, IQueueInterface<T>
    {
        private Node<T> front;
        private Node<T> rear;

       
        public T Push(T element)
        {
            if (element == null)
            {
                //Console.WriteLine("Element is NUll");
                throw new NullReferenceException();
                
            }
            if (IsEmpty())
            {
                Node<T> tmp = new Node<T>(element, null);
                rear = front = tmp;
            } 
            else
            {
                Node<T> tmp = new Node<T>(element, null);
                rear.next = tmp;
                rear = tmp;
            }
            return element;
        }

        public T Pop()
        {
            T tmp = default;
            if (IsEmpty() )
            {
                throw new QueueUnderflowException("The queue was empty when pop was invoked.");
            }
            else if (front == rear)
            {
                tmp = front.data;
                front = null;
                rear  = null;
            }
            else
            {
                tmp = front.data;
                front = front.next;
            }

            return tmp;
        }

        public T Peek()
        {
            if (IsEmpty())
            {
                throw new QueueUnderflowException("The queue was empty when pop was invoked.");
            }
            return front.data;
        }


        public bool IsEmpty()
        {
            if (front == null && rear == null)
            {
                return true;
            } 
            else
            {
                return false;
            }
        }
        
    }
}
