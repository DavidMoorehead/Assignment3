using System;
using System.Collections.Generic;
using System.Linq;


/*  Formatting/Rule Changes: 
 * Rule(Class Names): Changed Class Names to Pascal Casing (average -> Average, sum -> Sum, etc.)
 *  Rule(Variable Names): Changed variable Names to Camel Casing (incneg -> incNeg, sum -> sumValue, median -> medianValue, etc.)
 * Changed variable names for clarity (incneg -> negValues, data/x -> numberList, c -> listCounter, s-> sumValue, etc.)
 *  Made variables more consistent ie using listCounter instead of c/n for each list counter, using numberList instead of x or data for double list parameter
 * Rule(Abbreviations): Removed all abbreviations (was done with changing things like incneg and such)
 *  Rule(Noun Class Names): Changed Class name to be a noun instead of just A3 -> changed to Assignment
    
 * 
 * Code Changes:
 * (using): Imported Linq to allow for Linq functions, especially since we're doing math and using lists.
 * 
 * (sum function): if (numberList.Count < 0) -> (numberList.Count <=0)  //didn't check if there's nothing
 * 
 * (several functions): I created a function NumbersCheck. As it seemed kinda silly to have a calculator esc class that did math calculations, but forced you
 * to convert everything to a double before passing (especially if you have decimals that need calculating). So Instead I made a function that
 * tests if there's any non-numerical values, then put that at the beginning of each function. It just auto converts to double for every item in the list,
 * and handles all the error checking that each function would need by default.
 * 
 * It just makes this entire class more modular and useful for any other function we do going forward in this "company".
 * 
 * Also made it so several repeated checks (like each function checking if theres a count > 0 and such) are just done once in the same function. Just makes
 * less code bloat.
 * 
 * I'd rather have the function in the beginning of the class declaration (like you create an instance of the class, throw values in, then it gets deleted) but I digress, working with what
 * Joey gave me.
 * 
 * (list Size Change): Previously, all the list size errors were if it had to be enough to do the base calculation (like Sum for example needs only 1 value).
 * This was changed to 2 since every function except sum needs 2, and the sum of a single value is just... itself, so it seemed silly to keep it that way
 * 
 * (average function & all functions):  int listCounter = 0;
                for (int i = 0; i < numberList.Count; i++)
                {
                    if (negativeValues || numberList[i] >= 0)
                    {
                        listCounter++;
                    }
                }

    deleted the above and just used .Average();

    I did the same for every other funtcion. So for Sum, I did the same.

    To keep the negative check possible, I just made another LINQ criteria as follows:
    decimal sumValue = numberList
                   .Where(item => item >= 0)
                   .Average();

    This just checks for each item in the numberList, and then if it's greater than or equal to 0 it includes it in the LINQ function. I did this for all lists.

    (median & stdev func): Was odd that you have the ability to use negative values for sum and average, but not stdev and median so I just
    coded the control to be able to do negatives or not for those as well. Consistency essentially.

    

    (mode function): Was weird this had mean(average) and median, but not mode. So I made a mode function
    (median function): Changed the return value type to be a double array instead of a single double in the case
    that there are two or more medians (even number list)
 */


namespace Assignment3
{
    /// <summary>
    /// Library of statistical functions using Generics for different statistical calculatuions
    /// see http://www.calculator.net/standard-deviation-calculator.html for sample standard deviation calculations
    ///  
    /// @author Joey Programmer
    /// @author David Moorehead
    /// 
/// </summary>

public class Assignment
    {
        /// <summary>
        /// This function calculates the average of the list that is given to it, and returns a double datatype average. All error checks are handled in the NumbersCheck() function,
        /// which parses the info sent to this function.
        /// 
        /// The function also uses LINQ instead of previously using for loops and such as it communicates the information easier, and is more maintainable/modular
        /// </summary>
        /// <param name="objectList"> This is the object list that will take any values. It will validate only if each item in the list is numerical</param>
        /// <param name="negativeValues">This is the boolean value that represents whether negative numbers are wanted to be included in the calculation or not</param>
        /// <returns>returns a double value that represents the average of the objectList </returns>
        public static double Average(List<object> objectList, bool negativeValues)
        {
            List<double> numberList = NumbersCheck(objectList);
    
            if(negativeValues == true)
            {
                //uses LINQ average function to calculate average
                double averageValue = numberList.Average();
                return averageValue;
            }   
            else
            {
                //uses LINQ average function to calculate average but puts condition for items to be non-negative
                double averageValue = numberList
                   .Where(item => item >= 0)
                   .Average();
                return averageValue;
            }
            
        }


        /// <summary>
        /// This function calculates the total value of the list of items given to it. Requires at least 2 items, as you can't sum a value alone. All error checks are handled in the NumbersCheck() function,
        /// which parses the info sent to this function.
        /// 
        /// The function also uses LINQ instead of previously using for loops and such as it communicates the information easier, and is more maintainable/modular
        /// </summary>
        /// <param name="objectList">This is the object list that will take any values. It will validate only if each item in the list is numerical</param>
        /// <param name="negativeValues">This is the boolean value that represents whether negative numbers are wanted to be included in the calculation or not</param>
        /// <returns>returns a double value that represents the sum of the entire objectList</returns>
        public static double Sum(List<object> objectList, bool negativeValues)
        {
           

            List<double> numberList = NumbersCheck(objectList);

            if (negativeValues == true)
            {
                //uses LINQ sum function to calculate sum
                double sumValue = numberList.Sum();
                return sumValue;
            }
            else
            {
                //uses LINQ sum function, but after putting the conditional for items to be non-negative (greater than or equal to 0)
                double sumValue = numberList
                    .Where(item => item >= 0)
                    .Sum();
                return sumValue;
            }
              
        }


        /// <summary>
        /// Calculates the Median by getting the List. It allows to do negative values counted or not.
        /// 
        /// This also returns a double array instead of a single double, in the case that there are 2 medians instead of just one.
        /// </summary>
        /// <param name="objectList">This is the object list that will take any values. It will validate only if each item in the list is numerical</param>
        /// <param name="negativeValues">This is the boolean value that represents whether negative numbers are wanted to be included in the calculation or not</param>
        /// <returns>returns a double array that represents the middle numbers in the sorted list</returns>

        public static double[] Median(List<object>objectList, bool negativeValues)
        {
            List<double> numberList = NumbersCheck(objectList);

            if (negativeValues == true)
            {
                //sorts the list in ascending order
                numberList.Sort();

                //checks if the list is even or odd numbered, then returns either one or two doubles
                if(numberList.Count %2 == 1)
                {
                    return new double[] { numberList[numberList.Count / 2] };
                }
                else
                {
                    int midIndex = numberList.Count / 2;
                    return new double[] { numberList[midIndex - 1], numberList[midIndex] };
                }
                
            }
            else
            {
                //creates a filtered list that removes all negative numbers
                List<double> filteredList = numberList
                    .Where(item => item >= 0)
                    .ToList();

                //sorts the filtered list in ascending order
                filteredList.Sort();

                //checks if the list is even or odd numbered, then returns either one or two doubles
                if (filteredList.Count % 2 == 1)
                {
                    return new double[] { filteredList[filteredList.Count / 2] };
                }
                else
                {
                    int midIndex = filteredList.Count / 2;
                    return new double[] { filteredList[midIndex - 1], filteredList[midIndex] };
                }
            }              
        }

        /// <summary>
        /// This function gets the mode for the object list. Unlike the other functions, this one returns a list object.
        /// 
        /// This is because there's more relevant information when returning the mode versus the other options. Modes are for each item, so this returns
        /// a list of each number item and the count for them.
        /// 
        /// All error checks are handled in the NumbersCheck() function, which parses the info sent to this function.
        /// 
        /// The function also uses LINQ instead of previously using for loops and such as it communicates the information easier, and is more maintainable/modular
        /// </summary>
        /// <param name="objectList">This is the object list that will take any values. It will validate only if each item in the list is numerical</param>
        /// <param name="negativeValues">This is the boolean value that represents whether negative numbers are wanted to be included in the calculation or not</param>
        /// <returns>returns a List Object that represents the numbers in the list, and the count of their occurences</returns>
        public static List<object> Mode(List<object>objectList, bool negativeValues)
        {
            List<double> numberList = NumbersCheck(objectList);

           

            if (negativeValues == true)
            {
                //Uses LINQ function groups the Number List by their number value, and the count of each of those items.
                var resultList = numberList
                    .GroupBy(item => item)
                    .Select(group => new
                    {
                        Number = group.Key,
                        Count = group.Count()
                    })
                    .OrderByDescending(item => item.Count)
                    .ToList();

                return resultList.Cast<object>().ToList();
            }
            else
            {
                //creates a filtered list that removes all negative numbers
                List<double> filteredList = numberList
                    .Where(item => item >= 0)
                    .ToList();

                //Uses LINQ function groups the Number List by their number value, and the count of each of those items.
                var resultList = filteredList
                    .GroupBy(item => item)
                    .Select(group => new
                    {
                        Number = group.Key,
                        Count = group.Count()
                    })
                    .OrderByDescending(item => item.Count)
                    .ToList();

                
                return resultList.Cast<object>().ToList();
              
            }

          

            
        }


        /// <summary>
        /// This function calculates the standard deviation for the list of values in the objectList. It uses the Math Functions with the LINQ selects in order to filter the values that are
        /// returned from the LINQ query. 
        /// 
        /// All error checks are handled in the NumbersCheck() function, which parses the info sent to this function.
        /// 
        /// The function also uses LINQ instead of previously using for loops and such as it communicates the information easier, and is more maintainable/modular
        /// </summary>
        /// <param name="objectList">This is the object list that will take any values. It will validate only if each item in the list is numerical</param>
        /// <param name="negativeValues">This is the boolean value that represents whether negative numbers are wanted to be included in the calculation or not</param>
        /// <returns>returns a double value that represents the standard deviationof all the items in the list.</returns>
        public static double StandardDeviation(List<object> objectList, bool negativeValues)
        {
            List<double> numberList = NumbersCheck(objectList);
            double averageValue;

            if (negativeValues == true)
            {
                //grabs the average of the list
                averageValue = numberList.Average();

                //calculates the standard deviation of the list
                double standardDeviationValue = Math.Sqrt(numberList
                    .Select(item => Math.Pow(item - averageValue, 2))  
                    .Average());
                return standardDeviationValue;
            }
            else
            {
                //creates a filtered list that removes all negative numbers
                List<double> filteredList = numberList
                    .Where(item => item >= 0)
                    .ToList();

                //grabs the average of the list
                averageValue = filteredList.Average();

                //calculates the standard deviation of the list
                double standardDeviationValue = Math.Sqrt(filteredList
                    .Select(item => Math.Pow(item - averageValue, 2))  // Calculate squared deviations
                    .Average());
                return standardDeviationValue;
            }

           
      
        }


        /// <summary>
        /// This function is the main pre-function check. It handles all the error checks for the math function.
        /// 
        /// If any items in the list are non-numeric, the function throws an error.  If not, it takes every numeric value, casts them as doubles,
        /// and then returns that list for the other math functions to use.
        /// </summary>
        /// <param name="objectList">This is the object list that will take any values. It will validate only if each item in the list is numerical</param>
        /// <returns>returns a double list thats based on the casted values of the objectList to become doubles (assuming they pass the error checks)</returns>
        /// <exception cref="ArugmentException">thrown when any value inside the objectList is a non-numeric number</exception>
        /// <exception cref="ArugmentException">thrown when the list size is less than the required size</exception>
        public static List<double> NumbersCheck(List<Object> objectList)
        {
            if(objectList.Count > 1)
            {
                foreach (var item in objectList)
                {
                    //checks if any of the items inside the list are not of the allowed type (numeric)
                    if (!(item.GetType() == typeof(int) || item.GetType() == typeof(double) || item.GetType() == typeof(decimal) || item.GetType() == typeof(float)))
                    {
                        throw new ArgumentException("Cannot calculate math functions with non-numeric values.");
                    }


                }
                List<double> numberList = objectList
                    .Select(x => Convert.ToDouble(x))
                    .ToList();
                return numberList;
            }
            else
            {
                throw new ArgumentException("Need at least 2 numbers to do any math function comparisons.");
            }
            
        }
        /// <summary>
        /// This is the main line method. It's used to pass values into the A3 class from itself. When finalizing A3 class and using in prod, remove this.
        /// 
        /// Currently has all the test cases used to test the functions and examples of how to use each function (with median and mode having different printing to console requirements).
        /// </summary>
        /// <param name="args">Standard preset value for main line functions in java and C#</param>
        static void Main(string[] args)
        {
            Console.WriteLine("Sample Output for Statistical Functions Library");
            List<Object> testDataD = new List<Object> {-1, -1, 2, 2, 4, 4};
            List<Object> testDataC = new List<Object> { 10, 12, 23, 23, 16, 23, 21, 16, -1 };
            List<Object> testDataB = new List<Object> { 10, 20, 20, 10, 30 , 4};
            List<Object> testDataA = new List<Object> { 10, 30, 10, 45, 4, 5,7 , 8, 8};
            Console.WriteLine("Sum values = " + Assignment.Sum(testDataD, false));
            Console.WriteLine("Average value = " + Assignment.Average(testDataD, false));
            Console.WriteLine("Sum values = " + Assignment.Sum(testDataD, true));
            Console.WriteLine("Average value = " + Assignment.Average(testDataD, true));

            List<object> mode_list = Assignment.Mode(testDataD, false);
            Console.WriteLine(mode_list[1]);

            Console.WriteLine("Standard Deviation = " + Assignment.StandardDeviation(testDataC, false));
            Console.WriteLine("Standard Deviation = " + Assignment.StandardDeviation(testDataC, true));

            double[] result = Assignment.Median(testDataB, false);
            Console.Write("Median = ");
           foreach(double item in result)
            {
                Console.Write(item + ",");
            }
            Console.WriteLine("");

           Console.ReadLine();
        }
    }


}
