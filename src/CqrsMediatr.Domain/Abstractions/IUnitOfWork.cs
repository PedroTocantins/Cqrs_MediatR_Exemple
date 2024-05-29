namespace CqrsMediatr.Domain.Abstractions
{
    public interface IUnitOfWork
    {
        IMemberRepository MemberRepository { get; }
        Task CommitAssync();
    }
}