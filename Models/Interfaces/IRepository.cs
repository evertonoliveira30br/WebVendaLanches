﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Web.Configuration;

namespace WebVendaLanches.Models.Interfaces
{
    public interface IRepository<TEntity>
    where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int id);
        void Save(TEntity entity);
        void Update(TEntity entity);        
        void DeleteById(int id);
    }
}