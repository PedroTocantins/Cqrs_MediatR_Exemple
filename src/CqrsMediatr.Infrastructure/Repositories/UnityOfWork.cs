using CqrsMediatr.Domain.Abstractions;
using CqrsMediatr.Infrastructure.Context;

namespace CqrsMediatr.Infrastructure.Repositories
{
    public class UnityOfWork : IUnitOfWork, IDisposable
    {
        private IMemberRepository? _memberRepo;
        private readonly ApplicationDbContext _db;

        public UnityOfWork(ApplicationDbContext db)
        {
            _db = db;
        }

        public IMemberRepository MemberRepository
        {
            get
            {
                return _memberRepo = _memberRepo ?? new MemberRepository(_db);
            }
        }

        public async Task CommitAssync()
        {
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}