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

namespace Example.Domain.Entities;

public partial class StoreProduct {

    public StoreProduct()
    {
        OnCreated();
    }

    public virtual long Id { get; set; }

    public virtual Guid StoreId { get; set; }

    public virtual int ProductId { get; set; }

    public virtual int InStock { get; set; }

    public virtual Product Product { get; set; }

    #region Extensibility Method Definitions

    partial void OnCreated();

    #endregion
}
