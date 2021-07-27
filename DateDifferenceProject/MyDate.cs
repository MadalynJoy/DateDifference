using System;
using System.Collections.Generic;
using System.Text;

namespace DateDifferenceProject
{
    //defining parameters of a date
    public class MyDate
    {
        //store number of days in each month
        //e.g Jan = 31 days, Feb = 28 days ... Dec = 31 days
        public static readonly int[] DAYS_IN_MONTH = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        public int day, month, year;

        public MyDate(int day, int month, int year)
        {
            this.day = day;
            this.month = month;
            this.year = year;
        }

        //count the number of leap years
        public static int CountNumberOfLeapYearsFromDayZero(MyDate day)
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

        public static int CountDaysBetweenTwoDates(MyDate date1, MyDate date2)
        {
            //Count the days before date1

            //count using years and day
            int count1 = date1.year * 365 + date1.day;

            //add days to months for inputted date
            for (int i = 0; i < date1.month - 1; i++)
            {
                count1 += DAYS_IN_MONTH[i];
            }

            //for a leap year, add a day
            count1 += CountNumberOfLeapYearsFromDayZero(date1);


            //repeat above for date2
            int count2 = date2.year * 365 + date2.day;
            for (int i = 0; i < date2.month - 1; i++)
            {
                count2 += DAYS_IN_MONTH[i];
            }
            count2 += CountNumberOfLeapYearsFromDayZero(date2);

            return (count2 - count1);
        }
        public static int GetDaysInMonth(int month, int year)
        {
            if (month != 2)
            {
                return DAYS_IN_MONTH[month - 1];
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
    }
}
