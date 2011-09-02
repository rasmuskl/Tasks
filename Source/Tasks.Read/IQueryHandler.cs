namespace Tasks.Read
{
    public interface IQueryHandler<in T, out R> where T : IQuery<R>
    {
        R Handle(T query);
    }
}