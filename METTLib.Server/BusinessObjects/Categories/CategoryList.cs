﻿// Generated 18 Jan 2021 13:22 - Singular Systems Object Generator Version 2.2.694
//<auto-generated/>
using System;
using Csla;
using Csla.Serialization;
using Csla.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Singular;
using System.Data;
using System.Data.SqlClient;


namespace MELib.Categories
{
  [Serializable]
  public class CategoryList
   : MEBusinessListBase<CategoryList, Category>
  {
    #region " Business Methods "

    public Category GetItem(int CategoryID)
    {
      foreach (Category child in this)
      {
        if (child.CategoryID == CategoryID)
        {
          return child;
        }
      }
      return null;
    }

    public override string ToString()
    {
      return "Categorys";
    }

    public SubCategory GetSubCategory(int SubCategoryID)
    {
      SubCategory obj = null;
      foreach (Category parent in this)
      {
        obj = parent.SubCategoryList.GetItem(SubCategoryID);
        if (obj != null)
        {
          return obj;
        }
      }
      return null;
    }

    #endregion

    #region " Data Access "

    [Serializable]
    public class Criteria
      : CriteriaBase<Criteria>
    {
            public bool? IsActiveInd ;
            public int? CategoryID = null;

      public Criteria()
      {
      }

    }

    public static CategoryList NewCategoryList()
    {
      return new CategoryList();
    }

    public CategoryList()
    {
      // must have parameter-less constructor
    }

    public static CategoryList GetCategoryList()
    {
      return DataPortal.Fetch<CategoryList>(new Criteria());
    }
        public static CategoryList GetCategoryList(bool IsActiveInd)
        {
            return DataPortal.Fetch<CategoryList>(new Criteria() { IsActiveInd = IsActiveInd });
        }

        protected void Fetch(SafeDataReader sdr)
    {
      this.RaiseListChangedEvents = false;
      while (sdr.Read())
      {
        this.Add(Category.GetCategory(sdr));
      }
      this.RaiseListChangedEvents = true;

      Category parent = null;
      if (sdr.NextResult())
      {
        while (sdr.Read())
        {
          if (parent == null || parent.CategoryID != sdr.GetInt32(1))
          {
            parent = this.GetItem(sdr.GetInt32(1));
          }
          parent.SubCategoryList.RaiseListChangedEvents = false;
          parent.SubCategoryList.Add(SubCategory.GetSubCategory(sdr));
          parent.SubCategoryList.RaiseListChangedEvents = true;
        }
      }


      foreach (Category child in this)
      {
        child.CheckRules();
        foreach (SubCategory SubCategory in child.SubCategoryList)
        {
          SubCategory.CheckRules();
        }
      }
    }

    protected override void DataPortal_Fetch(Object criteria)
    {
      Criteria crit = (Criteria)criteria;
      using (SqlConnection cn = new SqlConnection(Singular.Settings.ConnectionString))
      {
        cn.Open();
        try
        {
          using (SqlCommand cm = cn.CreateCommand())
          {
            cm.CommandType = CommandType.StoredProcedure;
            cm.CommandText = "GetProcs.getCategoryList";
            cm.Parameters.AddWithValue("@IsActiveInd", Singular.Misc.NothingDBNull(crit.IsActiveInd));
            using (SafeDataReader sdr = new SafeDataReader(cm.ExecuteReader()))
            {
              Fetch(sdr);
            }
          }
        }
        finally
        {
          cn.Close();
        }
      }
    }

    #endregion

  }

}