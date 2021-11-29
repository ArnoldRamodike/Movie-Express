﻿// Generated 08 Nov 2021 11:17 - Singular Systems Object Generator Version 2.2.694
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


namespace MELib.Transactions
{
    [Serializable]
    public class TransactionTypeList
     : MEBusinessListBase<TransactionTypeList, TransactionType>
    {
        #region " Business Methods "

        public TransactionType GetItem(int TransactionTypeID)
        {
            foreach (TransactionType child in this)
            {
                if (child.TransactionTypeID == TransactionTypeID)
                {
                    return child;
                }
            }
            return null;
        }

        public override string ToString()
        {
            return "Transaction Types";
        }

        #endregion

        #region " Data Access "

        [Serializable]
        public class Criteria
          : CriteriaBase<Criteria>
        {
            public Criteria()
            {
            }
            public bool? IsActiveInd;

        }

        public static TransactionTypeList NewTransactionTypeList()
        {
            return new TransactionTypeList();
        }

        public TransactionTypeList()
        {
            // must have parameter-less constructor
        }

        public static TransactionTypeList GetTransactionTypeList()
        {
            return DataPortal.Fetch<TransactionTypeList>(new Criteria());
        }

        public static TransactionTypeList GetTransactionTypeList(bool IsActiveInd)
        {
            return DataPortal.Fetch<TransactionTypeList>(new Criteria() { IsActiveInd = IsActiveInd });
        }

        protected void Fetch(SafeDataReader sdr)
        {
            this.RaiseListChangedEvents = false;
            while (sdr.Read())
            {
                this.Add(TransactionType.GetTransactionType(sdr));
            }
            this.RaiseListChangedEvents = true;
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
                        cm.CommandText = "GetProcs.getTransactionTypeList";
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