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

            var largest10 = GrabTop10LargestAndRemoveFromList(numbers);

            var secondLargest10 = GrabTop10LargestAndRemoveFromList(numbers);

            var thirdLargest10 = GrabTop10LargestAndRemoveFromList(numbers);

            var remainingOthers = numbers.ToArray();

            if (remainingOthers.Length == 0)
            {
                result.AddRange(largest10);
                result.AddRange(secondLargest10);
                result.AddRange(thirdLargest10);

                return result;
            }

            var others1 = remainingOthers.Take(remainingOthers.Length/2).ToList();
            var others2 = remainingOthers.Skip(remainingOthers.Length/2).ToList();

            result.AddRange(secondLargest10); //second largest group goes in the list first
            result.AddRange(others1);
            result.AddRange(largest10);
            result.AddRange(others2);
            result.AddRange(thirdLargest10);

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

        private static List<long> GrabTop10LargestAndRemoveFromList(List<long> originalList)
        {
            var largest10 = GetLargestTens(originalList);

            originalList.RemoveAll(i => largest10.Contains(i));

            return largest10;
        }
    }
}
