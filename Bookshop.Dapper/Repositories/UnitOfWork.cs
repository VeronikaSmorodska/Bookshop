using Bookshop.Dapper.Entities;
using Bookshop.Dapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Bookshop.Dapper.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        public IRepository<User> _userRepository;
        public IAuthorRepository _authorRepository;
        public IBookRepository _bookRepository;
        private bool _disposed;

        public UnitOfWork(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }


        public IBookRepository Books
        {
            get { return _bookRepository ?? (_bookRepository = new BookRepository(_transaction)); }
        }
        public IAuthorRepository Authors
        {
            get { return _authorRepository ?? (_authorRepository = new AuthorRepository(_transaction)); }
        }
        public IRepository<User> Users
        {
            get { return _userRepository ?? (_userRepository = new UserRepository(_transaction)); }
        }


        public void Save()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                resetRepositories();
            }
        }

        private void resetRepositories()
        {
            _bookRepository = null;
            _authorRepository = null;
            _userRepository = null;
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~UnitOfWork()
        {
            dispose(false);
        }
    }
}
