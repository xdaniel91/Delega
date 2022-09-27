using Delega.Api.Database;
using Delega.Api.Interfaces.Repositories;
using Delega.Api.Models;
using Delega.Api.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Dapper;

namespace Delega.Api.Repositories.Implementation;

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

    public JudicialProcess Add(JudicialProcess JudicialProcess)
    {
        try
        {
            var entry = DbSet.Add(JudicialProcess);
            var commitResult = uow.Commit();
            return entry.Entity;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<JudicialProcess> AddAsync(JudicialProcess JudicialProcess)
    {
        try
        {
            var entry = await DbSet.AddAsync(JudicialProcess);

            return entry.Entity;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public IEnumerable<JudicialProcess> GetAll()
    {
        return DbSet;
    }

    public async Task<IEnumerable<JudicialProcess>> GetAllAsync()
    {
        return await DbSet.ToListAsync();
    }

    public JudicialProcessViewModel GetById(int id)
    {
        return GetResponse(id);
    }

    public async Task<JudicialProcessViewModel> GetByIdAsync(int id)
    {
        return GetResponse(id);
    }

    public JudicialProcessViewModel GetResponse(int id)
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

    public async Task<JudicialProcess> GetWithRelationsAsync(int id)
    {
        return await DbSet.FirstOrDefaultAsync(x => x.Id == id);
    }

    public JudicialProcess Update(JudicialProcess judicialProcess)
    {
       var entry = DbSet.Update(judicialProcess);
       
        return entry.Entity;
    }
}

