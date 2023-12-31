﻿using System;
using System.Collections.Generic;

namespace Archero.Repositories
{
    public class RepositoryDb : IInitializable
    {
        private Dictionary<Type, IRepository> _repositories;
        public RepositoryDb()
        {
            _repositories = new();
        }
        public void Initialize()
        {
            IRepository[] repositories =
            {
                new LevelRepository(),
            };

            foreach (var repository in repositories)
            {
                _repositories.Add(repository.GetType(), repository);

                repository.Initialize();
            }
        }

        public T Get<T>() where T : IRepository
        {
            return (T)_repositories[typeof(T)];
        }
    }
}
