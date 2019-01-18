using Pigeon.DataAccess.Entities;

namespace Pigeon.DataAccess.Repository.Implementation
{
    public class InstituteCategoryRepository : BaseRepository<InstituteCategory>, IInstituteCategoryRepository
    {
        public InstituteCategoryRepository(PigeonDbContext context) : base(context)
        {

        }
    }
}
