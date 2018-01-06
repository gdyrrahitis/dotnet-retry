namespace DotNetRetry.Rules.Loops
{
    using Core.Abstractions;
    using Templates;

    internal class Selector
    {
        public static Looper Pick(int attempts, Retriable retriable, ActionBodyTemplate actionBody) => 
            retriable.Options.Attempts > 0 ? 
                new Finite(actionBody, retriable) as Looper :
                new Forever(actionBody, retriable) as Looper;
    }
}