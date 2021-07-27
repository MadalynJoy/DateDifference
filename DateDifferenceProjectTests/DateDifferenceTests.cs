using Microsoft.VisualStudio.TestTools.UnitTesting;
using DateDifferenceProject;
using System;
using System.Collections.Generic;
using System.Text;
using static DateDifferenceProject.DateDifference;

namespace DateDifferenceProject.Tests
{
    [TestClass()]
    public class DateDifferenceTests
    {
        private void compareDates(MyDate expected, MyDate result)
        {
            Assert.AreEqual(expected.day, result.day);
            Assert.AreEqual(expected.month, result.month);
            Assert.AreEqual(expected.year, result.year);
        }

        [TestMethod()]
        public void ShouldReturnNullBadInput()
        {
            // Arrange
            string inputDate = "a a a";
            MyDate expected = null;

            // Act
            MyDate result = DateDifference.parseInputandValidate(inputDate);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void ShouldReturnNullInvalidSpacing()
        {
            // Arrange
            string inputDate = "20/02/1998";
            MyDate expected = null;

            // Act
            MyDate result = DateDifference.parseInputandValidate(inputDate);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void ShouldReturnNullInvalidMonth()
        {
            // Arrange
            string inputDate = "20 13 1998";
            MyDate expected = null;

            // Act
            MyDate result = DateDifference.parseInputandValidate(inputDate);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void ShouldReturnNullOutsideLowerYearBounds()
        {
            // Arrange
            string inputDate = "31 12 1899";
            MyDate expected = null;

            // Act
            MyDate result = DateDifference.parseInputandValidate(inputDate);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void ShouldReturnNullOutsideUpperYearBounds()
        {
            // Arrange
            string inputDate = "01 01 2011";
            MyDate expected = null;

            // Act
            MyDate result = DateDifference.parseInputandValidate(inputDate);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void ShouldReturnNullIfInvalidDay()
        {
            // Arrange
            string inputDate = "99 01 1992";
            MyDate expected = null;

            // Act
            MyDate result = DateDifference.parseInputandValidate(inputDate);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void ShouldReturnDateOnLowerBound()
        {
            // Arrange
            //lower date range
            string inputDate = "01 01 1900";
            MyDate expected = new MyDate(1, 1, 1900);

            // Act
            MyDate result = DateDifference.parseInputandValidate(inputDate);

            // Assert
            compareDates(expected, result);
        }

        [TestMethod()]
        public void ShouldReturnDateOnUpperBound()
        {
            // Arrange
            //upper date range
            string inputDate = "31 12 2010";
            MyDate expected = new MyDate(31, 12, 2010);

            // Act
            MyDate result = DateDifference.parseInputandValidate(inputDate);

            // Assert
            compareDates(expected, result);
        }

        [TestMethod()]
        public void ShouldReturnNullIfInvalidFebDate()
        {
            // Arrange
            string inputDate = "29 02 1991";
            MyDate expected = null;

            // Act
            MyDate result = DateDifference.parseInputandValidate(inputDate);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void ShouldCalcDaysBetweenTwoDates()
        {
            // Arrange
            //date1, date2
            MyDate date1 = new MyDate(04, 02, 1992);
            MyDate date2 = new MyDate(04, 02, 1993);
            int expected = 366;

            // Act
            var result = DateDifference.calcDaysDifference(date1, date2);

            // Assert
            Assert.AreEqual(expected, result);

        }

        [TestMethod()]
        public void ShouldCalcDaysLatestDateFirst()
        {
            // Arrange
            //date1, date2
            MyDate date1 = new MyDate(04, 02, 1993);
            MyDate date2 = new MyDate(04, 02, 1992);
            int expected = -366;

            // Act
            var result = DateDifference.calcDaysDifference(date1, date2);

            // Assert
            Assert.AreEqual(expected, result);

        }

        [TestMethod()]
        public void ShouldGetDaysInMonth()
        {
            // Arrange
            int inputMonth = 1;
            int inputYear = 1991;
            int expected = 31;

            // Act
            var result = DateDifference.getDaysInMonth(inputMonth, inputYear);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void ShouldGetDaysInMonthFebLeap()
        {
            // Arrange
            int inputMonth = 2;
            int inputYear = 1992;
            int expected = 29;

            // Act
            var result = DateDifference.getDaysInMonth(inputMonth, inputYear);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void ShouldGetDaysInMonthFebNotLeap()
        {
            // Arrange
            int inputMonth = 2;
            int inputYear = 1900;
            int expected = 28;

            // Act
            var result = DateDifference.getDaysInMonth(inputMonth, inputYear);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void ShouldCountCorrectlyOnLeapYears()
        {
            // Arrange
            MyDate date = new MyDate(04, 02, 1992);
            int expected = 482;

            // Act
            var result = DateDifference.countLeapYears(date);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void ShouldCountCorrectlyOnNotLeapYears()
        {
            // Arrange
            MyDate date = new MyDate(27, 05, 2005);
            int expected = 486;

            // Act
            var result = DateDifference.countLeapYears(date);

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}