namespace DotNetRetry.Tests.Static.RetryTests
{
    using DotNetRetry.Static;
    using NUnit.Framework;
    using static NUnit.Framework.Assert;

    [TestFixture]
    public class Instance
    {
        [Test]
        public void ReturnsInstanceOfRetryAsIRetry()
        {
            // Arrange | Act
            var result = Retry.Instance;

            // Assert
            IsInstanceOf<DotNetRetry.Retry>(result);
            IsInstanceOf<IRetry>(result);
            IsInstanceOf<IRetryAction>(result);
            IsInstanceOf<IRetryFunction>(result);
        }

        [Test]
        public void IsTrulySingleton()
        {
            // Arrange
            var instance = Retry.Instance;

            // Act
            var actual = Retry.Instance;

            // Assert
            AreEqual(instance.GetHashCode(), actual.GetHashCode());
        }
    }
}