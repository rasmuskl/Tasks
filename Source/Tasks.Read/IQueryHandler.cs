namespace Tasks.Read
{
    public interface IQueryHandler<in TQ, out TR> where TQ : IQuery<TR>
    {
        TR Handle(TQ query);
    }
}