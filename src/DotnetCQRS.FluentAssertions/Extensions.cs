namespace DotnetCQRS.Extensions.FluentAssertions
{
    public static class Extensions
    {
        public static ResultAssertions Should(this Result input)
        {
            return new ResultAssertions(input);
        }

        public static ResultAssertions<T> Should<T>(this Result<T> input)
        {
            return new ResultAssertions<T>(input);
        }
    }
}