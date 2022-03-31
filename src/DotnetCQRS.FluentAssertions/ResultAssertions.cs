using FluentAssertions;
using FluentAssertions.Primitives;

namespace DotnetCQRS.Extensions.FluentAssertions
{
    public class ResultAssertions : ReferenceTypeAssertions<Result, ResultAssertions>
    {
        public ResultAssertions(Result subject) : base(subject)
        {
        }

        protected override string Identifier => nameof(ResultAssertions);

        public AndConstraint<ResultAssertions> BeSuccess()
        {
            Subject.IsSuccess.Should().BeTrue();
            return new AndConstraint<ResultAssertions>(this);
        }

        public AndConstraint<ResultAssertions> BeFailure()
        {
            Subject.IsFailure.Should().BeTrue();
            return new AndConstraint<ResultAssertions>(this);
        }

        public AndConstraint<ResultAssertions> HaveErrorCode(string errorCode)
        {
            Subject.ErrorCode.Should().Be(errorCode);
            return new AndConstraint<ResultAssertions>(this);
        }
    }

    public class ResultAssertions<T> : ReferenceTypeAssertions<Result<T>, ResultAssertions<T>>
    {
        public ResultAssertions(Result<T> subject) : base(subject)
        {
        }

        protected override string Identifier => nameof(ResultAssertions<T>);

        public AndConstraint<ResultAssertions<T>> BeSuccess()
        {
            Subject.IsSuccess.Should().BeTrue();
            return new AndConstraint<ResultAssertions<T>>(this);
        }

        public AndConstraint<ResultAssertions<T>> BeFailure()
        {
            Subject.IsFailure.Should().BeTrue();
            return new AndConstraint<ResultAssertions<T>>(this);
        }

        public AndConstraint<ResultAssertions<T>> HaveErrorCode(string errorCode)
        {
            Subject.ErrorCode.Should().Be(errorCode);
            return new AndConstraint<ResultAssertions<T>>(this);
        }

        public AndConstraint<ResultAssertions<T>> BeEquivalentTo(T expected)
        {
            Subject.Value.Should().BeEquivalentTo(expected);
            return new AndConstraint<ResultAssertions<T>>(this);
        }
    }
}