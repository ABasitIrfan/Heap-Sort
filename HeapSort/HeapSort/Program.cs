using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeapSort
{
    class Program
    {
        static void Main(string[] args)
        {
           
            int L = 0;
            for (  ;  ; )
            {
                Console.WriteLine("Enter Lenght Of Your Array: ");
                L = Convert.ToInt32(Console.ReadLine());   //Array Lenght By User Input

                if (L<=0)
                {
                    Console.WriteLine("Wrong Input (Press Enter To Input Again)");
                    Console.ReadLine();
                    Console.Clear();
                }
                else
                {
                    break;
                }
            }
            //Initializing an Array
            int[] arrray = new int[L];

            string User = "";
            Console.WriteLine("Enter 'u' For User Input Or Enter 'r' For Random Input");
            User = Console.ReadLine();
            User = User.ToLower();
            if (User == "u")
            {
                for (int i = 0; i < arrray.Length; i++)
                {
                    Console.WriteLine("Enter Value For Index {0}", i);
                    arrray[i] = Convert.ToInt32(Console.ReadLine());   //User Input
                }
            }
            else if(User=="r")
            {
                Random r = new Random();
                for (int i = 0; i < arrray.Length; i++)
                {
                    arrray[i] = r.Next(0 , 100);   //User Input
                }
            }
            else
            {
                Console.WriteLine("Wrong Input");
            }
            Console.WriteLine("\nArray Before Sorting:");
            for (int i = 0; i < arrray.Length; i++)
            {
                Console.WriteLine(arrray[i]);
            }
            int[] arrray1 = new int[arrray.Length]; //Output Array(Finalizing Position Of the Elements or Sorted)
            int[] tempArry = new int[0];//Temporary Array For Remaining Elements(Unsorted)
            int HeapSize = arrray.Length;

            arrray = BuildHeap(arrray);//Calling Function To Make Heap

            arrray = Exchanging(arrray, tempArry, arrray1, HeapSize); //Exchanging Elements

            Console.WriteLine("\nArray After Sorting:");
            for (int i = 0; i < arrray1.Length; i++)
            {
                Console.WriteLine(arrray1[i]); //Printing Sorted Array
            }
        }
        public static int[] BuildHeap(int[] arrray) //Function To Make Max Heap
        {
            if (arrray.Length == 1)
            {
                return arrray;
            }
            else if (arrray.Length == 2)
            {
                if (arrray[1] > arrray[0])
                {
                    int temp = arrray[0];
                    arrray[0] = arrray[1];
                    arrray[1] = temp;
                }
            }
            else
            {
                int p = 1, left = 0, right = 0, TempP = 0, TempL = 0, TempR = 0; //p=Parent, Left=Left Child, Right= Right Child, Temp=Save Previous Values for Recursion Help
                bool heap = true, reach = false, reach1 = true; //Heap= Heapify or not ,ReachIs Controlling (Odd-Even) Size Of Heap

                for (int i = (arrray.Length - 2) / 2; i >= 0; i--)  //Building Heap
                {
                    if ((p * 2) < arrray.Length || (arrray.Length % 2 == 0 && reach1 == true))
                    {
                        left = p * 2;
                        if (arrray.Length % 2 == 0 && reach1 == true && i == 0)
                        {
                            reach = true;
                            reach1 = false;
                        }
                    }

                    if (((p * 2) + 1) < arrray.Length + 1)
                        right = (p * 2) + 1;
                    p++;
                }
                p--;
                TempL = left;
                TempR = right;
                TempP = p;

                arrray = Heapify(arrray, heap, reach1, reach, p, TempP, right, TempR, left, TempL); //Calling Function To Heapify
            }
            return arrray;
        }
        public static int[] Exchanging(int[] arrray, int[] tempArry, int[] arrray1, int HeapSize) //Swaping And Deletion (Function to Exchange 1st And Last Node)
        {
            arrray = BuildHeap(arrray);
            int temp = arrray[0];
            arrray[0] = arrray[arrray.Length - 1];
            arrray[arrray.Length - 1] = temp;
            arrray1[HeapSize - 1] = arrray[HeapSize - 1];
            HeapSize--;
            tempArry = new int[HeapSize];
            for (int i = 0; i < HeapSize; i++)
            {
                tempArry[i] = arrray[i];
            }
            arrray = tempArry;
            if ((HeapSize - 1) != -1)
            {
                Exchanging(arrray, tempArry, arrray1, HeapSize);
            }
            return arrray;
        }

        //Function To Heapify
        public static int[] Heapify(int[] arrray, bool heap, bool reach1, bool reach, int p, int TempP, int right, int TempR, int left, int TempL)
        {

            if (heap == false)
            {
                p = TempP;
                right = TempR;
                left = TempL;
                if (arrray.Length % 2 == 0)
                {
                    reach = true;
                    reach1 = false;
                }
            }

            int j = (arrray.Length - 2) / 2;
            for (int i = (arrray.Length - 2) / 2; i >= 0; i--)
            {
                int large = 0; //
                ///Checking Right Child Is Largest Or Left (Comparing The Nodes To make Max Heap)

                if (arrray[right - 1] > arrray[left - 1])  //If Right Child Is Greater
                {
                    if (arrray[p - 1] < arrray[right - 1])
                    {
                        large = right - 1;
                        heap = false;
                    }
                    else
                        if ((p - 1) != 0 || arrray.Length <= 3)
                            heap = true;
                }
                else if (arrray[left - 1] > arrray[right - 1])//If Left Child Is Greater
                {
                    if (arrray[p - 1] < arrray[left - 1])
                    {
                        large = left - 1;
                        heap = false;
                    }
                    else
                        if ((p - 1) != 0 || arrray.Length <= 3)
                            heap = true;
                }



                if (arrray[large] > arrray[p - 1] && heap == false) //The Greater Child Swap From the parent
                {
                    int temp = arrray[p - 1];
                    arrray[p - 1] = arrray[large];
                    arrray[large] = temp;
                    p--;

                    left = left - 2;
                    if (reach == false)
                    {
                        right = right - 2;
                    }
                    if (reach == true)
                    {
                        reach = false;
                    }
                }
                else
                {
                    p--;
                    left = left - 2;
                    if (reach == false)
                    {
                        right = right - 2;
                    }
                    if (reach == true)
                    {
                        reach = false;
                    }
                }
            }

            if (heap == false) //Checking That Array Is Heapify or Not
            {
                arrray = Heapify(arrray, heap, reach1, reach, p, TempP, right, TempR, left, TempL);
            }
            return arrray;
        }
        /////////If we Want To make Minimum Heap, We Will use "Small" Variable to store the smallest element of subtree's(Min Heap Will Give Descending Order) 


    }
}
