using System;
using Singular.CommonData;

namespace MELib
{
  public class CommonData : CommonDataBase<MELib.CommonData.MECachedLists>
  {
    [Serializable]
    public class MECachedLists : CommonDataBase<MECachedLists>.CachedLists
    {
      /// <summary>
      /// Gets cached ROUserList
      /// </summary>
      /// 
      public MELib.RO.ROUserList ROUserList
            {
                get
                {
                    return RegisterList<MELib.RO.ROUserList>(Misc.ContextType.Application, c => c.ROUserList, () => { return MELib.RO.ROUserList.GetROUserList(); });
                }
            }

            // <summary>
            // Gets cached  ROTransactionTypeList
            // </summary>
            //
            public MELib.Transactions.TransactionTypeList TransactionTypeList
            {
                get
                {
                    return RegisterList<MELib.Transactions.TransactionTypeList>(Misc.ContextType.Application, c => c.TransactionTypeList, () => { return MELib.Transactions.TransactionTypeList.GetTransactionTypeList(); });
                }
            }

            /// <summary>
            /// Gets cached  ROCategoryLust
            /// </summary>
            ///
            public MELib.Orders.DeliveyList DeliveryList
            {
                get
                {
                    return RegisterList<MELib.Orders.DeliveyList>(Misc.ContextType.Application, c => c.DeliveryList, () => { return MELib.Orders.DeliveyList.GetDeliveyList(); });
                }
            }

            /// <summary>
            /// Gets cached ROCategoryList
            /// </summary>
            public Categories.CategoryList CategoryList
            {
        get
        {
          return RegisterList<MELib.Categories.CategoryList>(Misc.ContextType.Application, c => c.CategoryList, () => { return MELib.Categories.CategoryList.GetCategoryList(); });
        }
      }      
      /// <summary>
      /// Gets cached ROMovieGenreList
      /// </summary>
      public RO.ROMovieGenreList ROMovieGenreList
      {
        get
        {
          return RegisterList<MELib.RO.ROMovieGenreList>(Misc.ContextType.Application, c => c.ROMovieGenreList, () => { return MELib.RO.ROMovieGenreList.GetROMovieGenreList(); });
        }
      }

    }
  }

  public class Enums
  {
		public enum AuditedInd
		{
			Yes = 1,
			No = 0
		}
    public enum DeletedInd
    {
      Yes = 1,
      No = 0
    }
  }
}
