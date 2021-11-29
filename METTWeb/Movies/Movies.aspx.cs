using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Singular.Web;
using MELib.Transactions;
using MELib.Accounts;
using MELib.Movies;
using Singular;

namespace MEWeb.Movies
{
  public partial class Movies : MEPageBase<MoviesVM>
  {
  }
  public class MoviesVM : MEStatelessViewModel<MoviesVM>
  {
    public MovieList MovieList { get; set; }
        public AccountList AccountList { get; set; }
        public TransactionList TransactionList { get; set; }
       // public UserMovieList UserMovieList { get; set; }

        // Filter Criteria
        public DateTime ReleaseFromDate { get; set; }
    public DateTime ReleaseToDate { get; set; }
        public decimal UserBalance { get; set; }


        /// <summary>
        /// Gets or sets the Movie Genre ID
        /// </summary>
        [Singular.DataAnnotations.DropDownWeb(typeof(MELib.RO.ROMovieGenreList), UnselectedText = "Select", ValueMember = "MovieGenreID", DisplayMember = "Genre")]
    [Display(Name = "Genre")]
    public int? MovieGenreID { get; set; }

    public MoviesVM()
    {

    }
    protected override void Setup()
    {
      base.Setup();

      MovieList = MELib.Movies.MovieList.GetMovieList();
            TransactionList = TransactionList.GetTransactionList();
            AccountList = AccountList.GetAccountList();
         //   UserMovieList = MELib.Movies.UserMovieList.GetUserMovieList();
        }

    [WebCallable(LoggedInOnly = true)]
      public Result RentMovie(int MovieID, MELib.Movies.MovieList movieList)
        {
            Result sr = new Result();
            try
            {
                UserBalance = AccountList.GetAccountList().Select(x => x.Balance).FirstOrDefault();
                MELib.Movies.Movie movie = movieList.GetItem(MovieID);
                

                if (UserBalance >= movie.Price)
                {
                    //Balance Reduced
                    var BalanceCheckout = UserBalance - movie.Price;
                    AccountList = AccountList.GetAccountList();
                    AccountList.ToList().ForEach(f => f.Balance = BalanceCheckout);
                    AccountList.TrySave();

                    //Oder Confirmation



                    //Transactions List
                    Transaction transaction = new Transaction();

                    transaction.UserID = Settings.CurrentUser.UserID;
                    transaction.Amount = movie.Price;
                    transaction.TransactionTypeID = 4;
                    transaction.Description = "Movie Rental";
                    transaction.TrySave(typeof(TransactionList));

                    sr.Success = true;

                }
                else
                {
                    sr.Success = false;
                    sr.ErrorText = "No Funds Please Deposit Money Into Your Account !";
                    return sr;
                }

            }
            catch (Exception e)
            {
                sr.Data = e.InnerException;
                sr.Success = true;

            }

            return sr;
            //var url = $"../Movies/Movie.aspx?MovieId={HttpUtility.UrlEncode(Singular.Encryption.EncryptString(MovieID.ToString()))}";
            //return url;
        }


        [WebCallable]
    public static Result WatchMovie(int MovieID)
    {
      Result sr = new Result();
      try
      {

        // ToDo Check User Balance
        // ToDo Insert Data in Transctions

        sr.Success = true;
      }
      catch (Exception e)
      {
        sr.Data = e.InnerException;
        sr.Success = false;
      }
      return sr;
    }

    [WebCallable]
    public Result FilterMovies(int MovieGenreID)
    {
      Result sr = new Result();
      try
      {
        sr.Data = MELib.Movies.MovieList.GetMovieList(MovieGenreID);
        sr.Success = true;
      }
      catch (Exception e)
      {
        WebError.LogError(e, "Page: LatestReleases.aspx | Method: FilterMovies", $"(int MovieGenreID, ({MovieGenreID})");
        sr.Data = e.InnerException;
        sr.ErrorText = "Could not filter movies by category.";
        sr.Success = false;
      }
      return sr;
    }

  }
}

