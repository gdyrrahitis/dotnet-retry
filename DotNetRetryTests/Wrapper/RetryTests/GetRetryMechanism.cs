namespace DotNetRetry.Tests.Wrapper.RetryTests
{
    using NUnit.Framework;
    using static NUnit.Framework.Assert;

    public class GetRetryMechanism
    {
        [Test]
        public void ReturnsInstanceOfRetryAsIRetry()
        {
            // Arrange 
            var retry = new DotNetRetry.Wrapper.Retry();

            // Act
            var result = retry.GetRetryMechanism();

            // Assert
            IsInstanceOf<Retry>(result);
            IsInstanceOf<IRetry>(result);
            IsInstanceOf<IRetryAction>(result);
            IsInstanceOf<IRetryFunction>(result);
        }

        [Test]
        public void IsTrulySingleton()
        {
            // Arrange
            var retry = new DotNetRetry.Wrapper.Retry();

            // Act
            var actual = retry.GetRetryMechanism();
            var instance = retry.GetRetryMechanism();

            // Assert
            AreEqual(instance.GetHashCode(), actual.GetHashCode());
        }
    }
}