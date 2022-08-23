using System;

namespace StepMediaAssignment.Models.Service
{
    public class StepmediaService : IStepmediaService
    {
        public StepmediaService()
        {
        }

        public List<long> Rearrange(List<long> numbers)
        {
            var result = new List<long>();

            numbers.Sort();
            //Grab the top 10s and removed them from original list
            var largest10s = GetLargestTens(numbers);
            numbers.RemoveAll(item => largest10s.Contains(item));

            //Grab the second top 10s and removed them from original list
            var secondLargest10s = GetLargestTens(numbers);
            numbers.RemoveAll(item => secondLargest10s.Contains(item));

            //Grab the third top 10s and removed them from original list
            var thirdLargest10s = GetLargestTens(numbers);
            numbers.RemoveAll(item => thirdLargest10s.Contains(item));

            var remainingOthers = numbers.ToArray();

            if (remainingOthers.Length == 0)
            {
                result.AddRange(largest10s);
                result.AddRange(secondLargest10s);
                result.AddRange(thirdLargest10s);

                return result;
            }

            var others1 = remainingOthers.Take(remainingOthers.Length/2).ToList();
            var others2 = remainingOthers.Skip(remainingOthers.Length/2).ToList();

            result.AddRange(secondLargest10s); //second largest group goes in the list first
            result.AddRange(others1);
            result.AddRange(largest10s);
            result.AddRange(others2);
            result.AddRange(thirdLargest10s);

            return result;
        }

        private static List<long> GetLargestTens(List<long> sortedList)
        {
            var result = new List<long>();

            for (var i = 0; i < 10; i++)
            {
                var max1 = long.MinValue;
                foreach (var t in sortedList)
                {
                    if (t > max1)
                        max1 = t;
                }

                sortedList.Remove(max1);
                result.Add(max1);
            }
            
            return result;
        }
    }
}
