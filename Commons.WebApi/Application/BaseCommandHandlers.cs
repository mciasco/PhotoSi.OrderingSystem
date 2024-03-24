using FluentValidation;

namespace Commons.WebApi.Application
{
    public abstract class BaseCommandHandlerNoInputNoOutput
    {
        public abstract Task Execute();
    }

    public abstract class BaseCommandHandlerNoInputWithOutput<TOutput>
    {
        public abstract Task<TOutput> Execute();
    }

    public abstract class BaseCommandHandlerWithInputNoOutput<TInput>
    {
        public abstract Task Execute(TInput input);
    }

    public abstract class BaseCommandHandlerWithInputWithOutput<TInput, TOutput>
    {
        public abstract Task<TOutput> Execute(TInput input);
    }

    public abstract class BaseCommandHandlerWithInputNoOutput<TInput, TInputValidator>
        where TInputValidator : AbstractValidator<TInput>
    {
        protected BaseCommandHandlerWithInputNoOutput(TInputValidator validator)
        {
            Validator = validator;
        }

        protected TInputValidator Validator { get; private set; }

        public async Task Execute(TInput input)
        {
            var validationResult = await Validator.ValidateAsync(input);
            if (!validationResult.IsValid)
                throw new ArgumentException(
                    $"Command validation failed. Errors: [" + string.Join("; ", validationResult.Errors?.Select(vf => $"Property={vf.PropertyName}, ErrorCode={vf.ErrorCode}, ErrorMessage={vf.ErrorMessage}")) + "]");

            await OnExecute(input);
        }

        protected abstract Task OnExecute(TInput validatedInput);
    }

    public abstract class BaseCommandHandlerWithInputWithOutput<TInput, TOutput, TInputValidator>
        where TInputValidator : AbstractValidator<TInput>
    {
        protected BaseCommandHandlerWithInputWithOutput(TInputValidator validator)
        {
            Validator = validator;
        }

        protected TInputValidator Validator { get; private set; }

        public async Task<TOutput> Execute(TInput input)
        {
            var validationResult = await Validator.ValidateAsync(input);
            if (!validationResult.IsValid)
                throw new ArgumentException(
                    $"Command validation failed. Errors: [" + string.Join("; ", validationResult.Errors?.Select(vf => $"Property={vf.PropertyName}, ErrorCode={vf.ErrorCode}, ErrorMessage={vf.ErrorMessage}")) + "]");

            return await OnExecute(input);
        }

        protected abstract Task<TOutput> OnExecute(TInput validatedInput);
    }
}
