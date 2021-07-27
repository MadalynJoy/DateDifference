using System;
using System.Security.Cryptography.X509Certificates;
using static System.Console;

namespace DateDifferenceProject
{
    public class DateDifference
    {
        //defining parameters of a date
        public class MyDate
        {
            public int day, month, year;

            public MyDate(int day, int month, int year)
            {
                this.day = day;
                this.month = month;
                this.year = year;
            }
        }

        //store number of days in each month
        //e.g Jan = 31 days, Feb = 28 days ... Dec = 31 days
        public static int[] daysInMonths = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        //count the number of leap years
        public static int countLeapYears(MyDate day)
        {
            int years = day.year;

            //check if input date is a leap year
            if (day.month <= 2)
            {
                years--;
            }

            //A leap year occurs if it is divisible by 4 or 400, and not 100
            //e.g. 1900 = not a leap year, 2000 = a leap year
            return years / 4 - years / 100 + years / 400;

        }

        //calculate the difference (days) between the two inputted dates 
        //by calculating the number of days from 01/01/0000 for each inputted date.

        public static int calcDaysDifference(MyDate date1, MyDate date2)
        {
            //Count the days before date1

            //count using years and day
            int count1 = date1.year * 365 + date1.day;

            //add days to months for inputted date
            for (int i = 0; i < date1.month - 1; i++)
            {
                count1 += daysInMonths[i];
            }

            //for a leap year, add a day
            count1 += countLeapYears(date1);


            //repeat above for date2
            int count2 = date2.year * 365 + date2.day;
            for (int i = 0; i < date2.month - 1; i++)
            {
                count2 += daysInMonths[i];
            }
            count2 += countLeapYears(date2);

            return (count2 - count1);
        }
        public static int getDaysInMonth(int month, int year)
        {
            if (month != 2)
            {
                return daysInMonths[month - 1];
            }

            //special case for February
            //A leap year occurs if it is divisible by 4 or 400, and not 100
            else
            {
                if ((year % 4 == 0 && year % 400 == 0) || (year % 4 == 0 && !(year % 100 == 0)))
                {
                    return 29;
                }

                else
                {
                    return 28;
                }
            }
        }

        public static MyDate parseInputandValidate(string inputDate)
        {
            //to split the user input into day/month/year
            int day, month, year;
            bool daySuccess = false, monthSuccess = false, yearSuccess = false;

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
            
            //add constraints for date input
            if (invalidDateNumbers ||
                month < 1 || month > 12 ||
                day < 1 || day > getDaysInMonth(month, year) ||
                year < 1900 || year > 2010)
            {
                WriteLine("Invalid date input. Please try again");
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
                date1 = parseInputandValidate(firstDate);
            }

            //second date input
            while (date2 == null)
            {
                WriteLine("Enter the latest date in the format DD MM YYYY: ");
                secondDate = ReadLine();
                date2 = parseInputandValidate(secondDate);
                if (date2 == null)
                {
                    continue;
                }
                //ensure earliest date entered first
                if (calcDaysDifference(date1, date2) < 0)
                {
                    WriteLine("Earliest date not entered first. Try again");
                    date2 = null;
                }
            }

            WriteLine($"{firstDate}, {secondDate}, " + calcDaysDifference(date1, date2) + " days");
            ReadLine();

        }
    }
}


