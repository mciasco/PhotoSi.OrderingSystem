namespace AddressBook.WebApi.Application
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
}
