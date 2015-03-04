using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using OverflowVictor.Domain.Entities;

namespace OverflowVictor.Data
{
    public class AccountRespository : IAccountRespository, IDisposable
    {
        private OverflowVictorContext context;

        public AccountRespository(OverflowVictorContext context)
        {
            this.context = context;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return context.Accounts.ToList();
        }

        public Account GetAccountById(Guid id)
        {
            return context.Accounts.Find(id);
        }

        public void InsertAccount(Account account)
        {
            context.Accounts.Add(account);
        }

        public void DeleteAccount(Guid accountId)
        {
            Account account = context.Accounts.Find(accountId);
            context.Accounts.Remove(account);
        }

        public void UpdateAccount(Account account)
        {
            context.Entry(account).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}