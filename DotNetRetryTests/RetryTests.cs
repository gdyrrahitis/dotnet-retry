using System;
using System.Threading.Tasks;
using DotNetRetry;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetRetryTests
{
    [TestClass]
    public class RetryTests
    {
        private IRetry _retry;

        [TestInitialize]
        public void Initialization()
        {
            _retry = new Retry();
        }

        [TestMethod]
        public void Attempt_Void_Success_At_First_Try()
        {
            // Arrange
            int valueToReturn = 0;
            Action successFullAction = () =>
            {
                var intAsString = "15";
                var stringToInt = int.Parse(intAsString);
                valueToReturn = stringToInt;
            };

            // Act
            _retry.Attempt(successFullAction, 3, TimeSpan.FromSeconds(2));

            // Assert
            Assert.AreEqual(15, valueToReturn);
        }

        [TestMethod]
        public void Attempt_Void_Success_At_Third_Try()
        {
            // Arrange
            int valueToReturn = 0;
            var count = 0;
            Action successAtThirdTryAction = () =>
            {
                var intAsString = "ab123";
                if (count == 3)
                {
                    intAsString = intAsString.Replace("ab", "");
                    valueToReturn = int.Parse(intAsString);
                }
                else
                {
                    count++;
                    // This will raise an exception
                    valueToReturn = int.Parse(intAsString);
                }
            };

            // Act
            _retry.Attempt(successAtThirdTryAction, 5, TimeSpan.FromSeconds(1));

            // Assert
            Assert.AreEqual(3, count);
            Assert.AreEqual(123, valueToReturn);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))] // Assert
        public void Attempt_Void_Failure()
        {
            // Arrange
            int valueToTest = 0;
            Action failureAction = () =>
            {
                var nonIntegerValue = "123abc";
                // This will raise an exception
                valueToTest = int.Parse(nonIntegerValue);
            };

            // Act
            _retry.Attempt(failureAction, 3, TimeSpan.FromSeconds(1));

            // Assert
            Assert.AreEqual(0, valueToTest);
        }

        [TestMethod]
        public void Attempt_Void_With_Parameters_Success_At_First_Try()
        {
            // Arrange
            var parameter = "123456";
            var valueToReturn = 0;
            Action<string> convertToIntAction = s =>
            {
                valueToReturn = int.Parse(s);
            };

            // Act
            _retry.Attempt(() => convertToIntAction(parameter), 3, TimeSpan.FromSeconds(1));

            // Assert
            Assert.AreEqual(123456, valueToReturn);
        }

        [TestMethod]
        public void Attempt_Void_With_Parameters_Success_At_Second_Try()
        {
            // Arrange
            var parameter = "abc123456";
            var valueToReturn = 0;
            var count = 0;
            Action<string> convertToIntAction = s =>
            {
                if (count == 2)
                {
                    s = s.Replace("abc", "");
                    valueToReturn = int.Parse(s);
                }
                else
                {
                    count++;
                    valueToReturn = int.Parse(s);
                }
            };

            // Act
            _retry.Attempt(() => convertToIntAction(parameter), 6, TimeSpan.FromSeconds(1));

            // Assert
            Assert.AreEqual(2, count);
            Assert.AreEqual(123456, valueToReturn);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))] // Assert
        public void Attempt_Void_With_Parameters_Failure()
        {
            // Arrange
            var parameter = "abcd123";
            var valueToReturn = 0;
            var count = 0;
            Action<string> failureAction = s =>
            {
                count++;
                valueToReturn = int.Parse(s);
            };

            // Act
            _retry.Attempt(() => failureAction(parameter), 3, TimeSpan.FromSeconds(1));

            // Assert
            Assert.AreEqual(3, count);
            Assert.AreEqual(0, valueToReturn);
        }

        [TestMethod]
        public void AttemptAsync_Void_Without_Parameters_AsyncFunc_Success()
        {
            // Arrange
            var valueToReturn = 0;
            const int delay = 2000;

            Func<Task> asyncAction = async () =>
            {
                await Task.Delay(delay);
                valueToReturn = delay;
            };

            // Act
            _retry.AttemptAsync(asyncAction, 3, TimeSpan.FromSeconds(1));

            // Assert
            Assert.AreEqual(2000, valueToReturn);
        }

        [TestMethod]
        public void AttemptAsync_Void_Without_Parameters_AsyncFunc_Success_At_Second_Try()
        {
            // Arrange
            var valueToReturn = 0;
            const int delay = 2000;
            var count = 0;
            Func<Task> asyncAction = async () =>
            {
                await Task.Delay(delay);
                if (count == 2)
                    valueToReturn = delay;
                else
                {
                    count++;
                    throw new Exception("On purpose exception");
                }
            };

            // Act
            _retry.AttemptAsync(asyncAction, 5, TimeSpan.FromSeconds(1));

            // Assert
            Assert.AreEqual(2, count);
            Assert.AreEqual(2000, valueToReturn);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))] // Assert
        public void AttemptAsync_Void_Without_Parameters_AsyncFunc_Failure()
        {
            // Arrange
            Func<Task> asyncAction = async () =>
            {
                await Task.Delay(2000);
                throw new Exception("On purpose exception");
            };

            // Act
            _retry.AttemptAsync(asyncAction, 3, TimeSpan.FromSeconds(1));
        }

        [TestMethod]
        public void AttemptAsync_Void_Without_Parameters_AsyncMethod_Success()
        {
            // Arrange
            const int delay = 2000;

            // Act
            var result = _retry.AttemptAsync(() => ReturnValueAsync(delay), 3, TimeSpan.FromSeconds(1));

            // Assert
            Assert.AreEqual(2000, result);
        }

        [TestMethod]
        public void AttemptAsync_Void_Without_Parameters_AsyncMethod_Success_At_Fourth_Try()
        {
            // Arrange
            const int delay = 2000;
            var count = 0;
            Func<Task<int>> returnValueAsyncFunc = async () =>
            {
                if (count == 4) return await ReturnValueAsync(delay);
                else
                {
                    count++;
                    throw new Exception("On purpose exception.");
                }
            };

            // Act
            var result = _retry.AttemptAsync(returnValueAsyncFunc, 5, TimeSpan.FromSeconds(1));

            // Assert
            Assert.AreEqual(4, count);
            Assert.AreEqual(2000, result);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))] // Assert
        public void AttemptAsync_Void_Without_Parameters_AsyncMethod_Failure()
        {
            // Arrange
            const int delay = 2000;
            Func<Task<int>> returnValueAsyncFunc = async () =>
            {
                await Task.Delay(delay);
                throw new Exception("On purpose exception.");
            };

            // Act
            _retry.AttemptAsync(returnValueAsyncFunc, 4, TimeSpan.FromSeconds(1));
        }

        private async Task<int> ReturnValueAsync(int value)
        {
            await Task.Delay(value);
            return value;
        }
    }
}
