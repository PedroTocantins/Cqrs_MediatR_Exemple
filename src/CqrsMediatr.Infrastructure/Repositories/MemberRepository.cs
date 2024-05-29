using CqrsMediatr.Domain.Abstractions;
using CqrsMediatr.Domain.Entities;
using CqrsMediatr.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CqrsMediatr.Infrastructure.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        protected readonly ApplicationDbContext _db;

        public MemberRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        public async Task<Member> AddMember(Member member)
        {
            if (member is null)
                throw new ArgumentNullException(nameof(member));

            await _db.Members.AddAsync(member);
            return member;
        }

        public async Task<Member> DeleteMember(int id)
        {
            var member = await _db.Members.FindAsync(id);

            if (member is null)
                throw new ArgumentNullException(nameof(member));

            _db.Members.Remove(member);
            return member;
        }

        public async Task<Member> GetMemberById(int id)
        {
            var member = await _db.Members.FindAsync(id);

            if (member is null)
                throw new ArgumentNullException(nameof(member));

            return member;
        }

        public async Task<IEnumerable<Member>> GetMembers()
        {
            var memberList = await _db.Members.ToListAsync();
            return memberList ?? Enumerable.Empty<Member>();
        }

        public void UpdateMember(Member member)
        {
            if (member is null)
                throw new ArgumentNullException(nameof(member));

            _db.Members.Update(member);
        }

    }
}