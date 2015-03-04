using System;
using System.Collections.Generic;
using OverflowVictor.Domain.Entities;

namespace OverflowVictor.Data
{
    public interface IAccountRespository : IDisposable
    {
        IEnumerable<Account> GetAccounts();
        Account GetAccountById(Guid accountId);
        void InsertAccount(Account account);
        void DeleteAccount(Guid accountId);
        void UpdateAccount(Account account);
        void Save();
    }
}