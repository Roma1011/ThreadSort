namespace ThreadSorting
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] myArray = new int[]{11,1,2,3,4,5,6,7,8,10,5,11,12,13,14};
            int[] container = default;
            int[] container2 = default;
            int[] sortedarray = new int[myArray.Length+1];
            int[]compare = new int[2];
            int localindex = 0;
            
            Task<int>[] tasks = new Task<int>[2];
            //FillArray(ref myArray);
            Rearrangement(ref myArray, ref container, ref container2);
            while (true)
            {
                if (container.Any())
                {
                    tasks[0] =  Task.Run(() => GreatOrNot(ref container));
                }

                if (container2.Any())
                {
                    tasks[1] = Task.Run(() => GreatOrNot(ref container2));
                }
                
                Task.WaitAll(tasks[0],tasks[1]);
                if(container.Length==0&&container2.Length==0)
                {
                    if(tasks[0].Result!=null)
                    {
                        sortedarray[localindex]=tasks[0].Result;
                        localindex++;
                        sortedarray[localindex]=tasks[1].Result;
                        localindex++;
                    }
                    else
                    {
                        sortedarray[localindex]=tasks[1].Result;
                        localindex++;
                        sortedarray[localindex]=tasks[0].Result;
                        localindex++;
                    }
                    Console.WriteLine("All elements are sorted");
                    break;
                }
                if(tasks[0].Result!=null)
                {
                    sortedarray[localindex]=tasks[0].Result;
                    localindex++;
                    sortedarray[localindex]=tasks[1].Result;
                    localindex++;
                }
                else
                {
                    sortedarray[localindex]=tasks[1].Result;
                    localindex++;
                    sortedarray[localindex]=tasks[0].Result;
                    localindex++;
                }
            }
            Console.ReadKey();
        }
        
        static void Rearrangement(ref int[]array,ref int[]container1,ref int[]container2)
        {
            if(array.Length%2==0)
            {
                container1 = new int[array.Length / 2];
                container2 = new int[array.Length / 2];
                
                container1 = array.Take(array.Length / 2).ToArray();
                container2 = array.Skip(array.Length / 2).ToArray();
            }
            else
            {
                container1 = new int[array.Length / 2];
                container2 = new int[array.Length / 2 + 1];
                
                container1 = array.Take(array.Length / 2).ToArray();
                container2 = array.Skip(array.Length / 2).ToArray();
            }
        }
        public static void FillArray(ref int[]array)
        {
            Random random = new Random();

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next();
            }
        }
        
        public static int GreatOrNot(ref int[]array)
        {
            int lower = array[0];
            int lowerindex = 0;
            int[]newarray = new int[array.Length-1];
            bool isRemoved = false;
            
            for(int index=0;index<array.Length-1;index++)
            {
                if (lower < array[index + 1])
                {
                    continue;
                }
                lower = array[index+1];
                lowerindex = index+1;
            }
            
            for(int index=0;index<array.Length-1;index++)
            {
                if (index == lowerindex)
                {
                    Console.WriteLine(array[lowerindex+1]);
                    newarray[index]=array[lowerindex+1];
                    isRemoved = true;
                    continue;
                }
                if (isRemoved)
                {
                    newarray[index] = array[index + 1];
                    continue;
                }
                newarray[index] = array[index];
            }
            array = newarray;
            return lower;
        }
    }
}