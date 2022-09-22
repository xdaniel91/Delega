namespace Delega.Api.Database
{
    public class UnitOfWok : IUnitOfWork
    {
        private readonly DelegaContext Context;

        public UnitOfWok(DelegaContext context)
        {
            Context = context;
        }

        public bool Commit()
        {
            return Context.SaveChanges() > 0;
        }

        public async Task<bool> CommitAsync(CancellationToken ct)
        {
            return await Context.SaveChangesAsync(ct) > 0;
        }
    }
}
