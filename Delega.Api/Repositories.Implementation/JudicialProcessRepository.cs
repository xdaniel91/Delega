using Delega.Api.Database;
using Delega.Api.Interfaces.Repositories;
using Delega.Api.Models;
using Delega.Api.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Dapper;

namespace Delega.Api.Repositories.Implementation
{
    public class JudicialProcessRepository : IJudicialProcessRepository
    {
        private readonly DelegaContext Context;
        private readonly DbSet<JudicialProcess> DbSet;
        private readonly IUnitOfWork uow;

        public JudicialProcessRepository(DelegaContext context, IUnitOfWork uow)
        {
            Context = context;
            DbSet = Context.judicialprocess;
            this.uow = uow;
        }

        public async Task<JudicialProcessViewModel> AddAsync(JudicialProcess JudicialProcess)
        {
            try
            {
                var entry = await DbSet.AddAsync(JudicialProcess);

                var commitResult = uow.Commit();
                
                var entity = entry.Entity;
                
                return GetResponse(entity.Id);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public IEnumerable<JudicialProcess> GetAllWithRelationships()
        {
            return DbSet.AsNoTracking();
        }

        public JudicialProcessViewModel GetById(int id)
        {
            return GetResponse(id);
        }

        private JudicialProcessViewModel GetResponse(int id)
        {
            var sql = $@"SELECT
                      jp.id Id,
                      jp.accusedid AccusedId,
                      jp.authorid AuthorId,
                      jp.lawyerid LawyerId,
                      jp.requestedvalue RequestedValue,
                      jp.value Value,
                      jp.reason Reason,
                      jp.verdict Verdict,
                      jp.status Status,
                      jp.datehourcreate DateHourCreated,
                      jp.datehourinprogress DateHourInProgress,
                      jp.datehourfinished DateHourFinished,
                      au.name AuthorName,
                      ac.name AccusedName,
                      lw.name LawyerName
                    FROM
                      judicialprocess jp
                    JOIN accused ac ON ac.id = jp.accusedid
                    JOIN author au ON au.id = jp.authorid
                    JOIN lawyer lw ON lw.id = jp.lawyerid
                    WHERE jp.id = {id};";

            using var connection = Context.Database.GetDbConnection();
            var result = connection.QueryFirstOrDefault<JudicialProcessViewModel>(sql);
            return result;

        }
    }
}
