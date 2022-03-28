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
            this.Subject.IsSuccess.Should().BeTrue();
            return new AndConstraint<ResultAssertions>(this);
        }

        public AndConstraint<ResultAssertions> BeFailure()
        {
            this.Subject.IsFailure.Should().BeTrue();
            return new AndConstraint<ResultAssertions>(this);
        }

        public AndConstraint<ResultAssertions> HaveErrorCode(string errorCode)
        {
            this.Subject.ErrorCode.Should().Be(errorCode);
            return new AndConstraint<ResultAssertions>(this);
        }
    }

    public class ResultAssertions<T> : ReferenceTypeAssertions<Result<T>, ResultAssertions<T>>
    {
        public ResultAssertions(Result<T> subject) : base(subject)
        { }

        protected override string Identifier => nameof(ResultAssertions<T>);
        
        public AndConstraint<ResultAssertions<T>> BeSuccess()
        {
            this.Subject.IsSuccess.Should().BeTrue();
            return new AndConstraint<ResultAssertions<T>>(this);
        }

        public AndConstraint<ResultAssertions<T>> BeFailure()
        {
            this.Subject.IsFailure.Should().BeTrue();
            return new AndConstraint<ResultAssertions<T>>(this);
        }

        public AndConstraint<ResultAssertions<T>> HaveErrorCode(string errorCode)
        {
            this.Subject.ErrorCode.Should().Be(errorCode);
            return new AndConstraint<ResultAssertions<T>>(this);
        }
        
        public AndConstraint<ResultAssertions<T>> BeEquivalentTo(T expected)
        {
            this.Subject.Value.Should().BeEquivalentTo(expected);
            return new AndConstraint<ResultAssertions<T>>(this);
        }
    }
}
