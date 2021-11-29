﻿// Generated 03 Nov 2021 08:35 - Singular Systems Object Generator Version 2.2.694
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
using MELib.Products;


namespace MELib.Cart
{
    [Serializable]
    public class CartList
     : MEBusinessListBase<CartList, Cart>
    {
        #region " Business Methods "

        public Cart GetItem(int CartID)
        {
            foreach (Cart child in this)
            {
                if (child.CartID == CartID)
                {
                    return child;
                }
            }
            return null;
        }

        public override string ToString()
        {
            return "Carts";
        }

        #endregion

        #region " Data Access "

        [Serializable]
        public class Criteria
          : CriteriaBase<Criteria>
        {
            public int? CartID = null;
            public int? ProductID = null;

            public Criteria()
            {
            }
            public Criteria(int? CartID)
            {
                this.CartID = CartID;
            }
            public int? UserID { get; set; }

            public static CartList NewCartList()
            {
                return new CartList();
            }
        }
        public CartList()
        {
            // must have parameter-less constructor
        }

        public static CartList GetCartList()
        {
            return DataPortal.Fetch<CartList>(new Criteria());
        }

        public static CartList GetCartList(int? ProductID)
        {
            return DataPortal.Fetch<CartList>(new Criteria() { ProductID = ProductID });
        }


        protected void Fetch(SafeDataReader sdr)
        {
            this.RaiseListChangedEvents = false;
            while (sdr.Read())
            {
                this.Add(Cart.GetCart(sdr));
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
                        cm.CommandText = "GetProcs.getCartList";
                        cm.Parameters.AddWithValue("@UserID", Singular.Security.Security.CurrentIdentity.UserID);
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