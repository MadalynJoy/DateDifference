using System;
using System.Security.Cryptography.X509Certificates;
using static System.Console;

namespace DateDifferenceProject
{
    public class DateDifference
    {
        public static MyDate ParseInputandValidate(string inputDate)
        {
            //to split the user input into day/month/year
            int day, month, year;
            bool daySuccess, monthSuccess, yearSuccess;

            try
            {
                String[] arrDateSplit = inputDate.Split(' ');
                daySuccess = int.TryParse(arrDateSplit[0], out day);
                monthSuccess = int.TryParse(arrDateSplit[1], out month);
                yearSuccess = int.TryParse(arrDateSplit[2], out year);
            }

            catch
            {
                WriteLine("Invalid date input. Please try again");
                return null;
            }

            bool invalidDateNumbers = daySuccess == false || monthSuccess == false || yearSuccess == false;

            //validate date inputs are valid
            if (invalidDateNumbers)
            {
                WriteLine($"Invalid input {inputDate}. Format must be DD MM YYYY");
                return null;
            }

            if (month < 1 || month > 12)
            {
                WriteLine($"Invalid month {month} was inputted. Please enter a valid month between 01 and 12");
                return null;
            }

            if (day < 1 || day > MyDate.GetDaysInMonth(month, year))
            {
                WriteLine($"Invalid day {day} was inputted. Please enter a valid day");
                return null;
            }

            if (year < 1900 || year > 2010)
            {
                WriteLine($"Invalid year {year} was inputted. Please enter a valid year between 1900 and 2010");
                return null;
            }

            var result = new MyDate(day, month, year);

            return result;
        }

        public static void Main(string[] args)
        {
            MyDate date1 = null;
            MyDate date2 = null;

            string firstDate = string.Empty;
            string secondDate = string.Empty;

            //first date input

            while (date1 == null)
            {
                WriteLine("Enter the earliest date in the format DD MM YYYY: ");
                firstDate = ReadLine();
                date1 = ParseInputandValidate(firstDate);
            }

            //second date input
            while (date2 == null)
            {
                WriteLine("Enter the latest date in the format DD MM YYYY: ");
                secondDate = ReadLine();
                date2 = ParseInputandValidate(secondDate);
                if (date2 == null)
                {
                    continue;
                }
                //ensure earliest date entered first
                if (MyDate.CountDaysBetweenTwoDates(date1, date2) < 0)
                {
                    WriteLine("Earliest date not entered first. Try again");
                    date2 = null;
                }
            }

            WriteLine($"{firstDate}, {secondDate}, " + MyDate.CountDaysBetweenTwoDates(date1, date2) + " days");
            ReadLine();

        }
    }
}


