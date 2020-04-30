using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Timesheet.Functions;
using System.Configuration;
using System.Data.SqlClient;

namespace Timesheet.Tests
{
    [TestFixture]
    public class Tests
    {
        private static TMfunctions GetSut()
        {
            return new TMfunctions(new SqlConnection
        ("Data Source=LINDO;initial catalog=Timesheets;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"));
        }

        
        [Test]
        public void searchUsers_WhenInputIsNull_ShouldResultIn_ZeroRecords()
        {
            //Arrange
            var sut = GetSut();

            // Act
            var result = sut.searchUsers().Count();

            // Assert
            Assert.AreEqual(0, result);
        }
        
        [TestCase("yv", 1)]
        [TestCase("Tim", 1)]
        [TestCase("No", 3)]
        [TestCase("gn", 0)]
        public void searchUsers_WhenInputIsNotEmptyString_ShouldResultIn_NumberOfRecords(string input, int expected)
        {
            //Arrange
            var sut = GetSut();

            // Act
            var result = sut.searchUsers(input).Count();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void SearchHour_WhenInputIsEmptyDate_ShouldResultIn_ZeroRecords()
        {
            //Arrange
            var sut = GetSut();

            // Act
            var result = sut.SearchHour().PersonH.Count();

            // Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public void SearchHour_WhenInputIsInvalidDate_ShouldResultIn_ZeroRecords()
        {
            //Arrange
            var sut = GetSut();

            // Act
            var result = sut.SearchHour("jghjkh").PersonH.Count();

            // Assert
            Assert.AreEqual(0, result);
        }

        //[Test]
        [TestCase("2018/02/12 12:00:00 AM", 11)]
        [TestCase("2018/02/14 12:00:00 AM", 11)]
        [TestCase("2018/03/12 12:00:00 AM", 14)]
        public void SearchHour_WhenInputIsNotEmptyDate_ShouldResultIn_NumberOfRecords(string input, int expected)
        {
            //Arrange
            var sut = GetSut();

            // Act
            var result = sut.SearchHour(input).PersonH.Count();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestCase(null, null, 0)]
        [TestCase("2018/07/11 12:00:00 AM", null, 0)]
        [TestCase(null, "Lesego", 0)]
        public void SearchUser_WhenInputIsNotComplete_ShouldResultIn_ZeroRecords(string input, string input2, int expected)
        {
            //Arrange
            var sut = GetSut();

            // Act
            var result = sut.SearchUser(input,input2).PeHours;

            // Assert
            Assert.AreEqual(expected, result);
        }
        
        [TestCase("afadsfvxz", null, 0)]
        [TestCase("afadsfvxz", "Lesego", 0)]
        public void SearchUser_WhenInputDateIsInvalid_ShouldResultIn_ZeroRecords(string input, string input2, int expected)
        {
            //Arrange
            var sut = GetSut();

            // Act
            var result = sut.SearchUser(input, input2).PeHours;

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestCase("2018/07/11 12:00:00 AM", "Lesego", 150)]
        [TestCase("2018/02/13 12:00:00 AM", "Nokubonga", 100)]
        public void SearchUser_WhenInputIsValid_ShouldResultIn_ZeroRecords(string input, string input2, int expected)
        {
            //Arrange
            var sut = GetSut();

            // Act
            var result = sut.SearchUser(input, input2).PeHours;

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}
