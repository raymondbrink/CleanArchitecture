﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// PLEASE DO NOT MODIFY!
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace Example.Domain.Entities
{
    public partial class Customer {

        public Customer()
        {
            OnCreated();
        }

        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual DateTime? ArchivedAtUtc { get; set; }

        public virtual string ArchivedBy { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
