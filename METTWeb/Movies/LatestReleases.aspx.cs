using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Singular.Web;
using MELib.Transactions;
using MELib.Accounts;
using MELib.Movies;
using Singular;


namespace MEWeb.Movies
{
  public partial class LatestReleases : MEPageBase<LatestReleasesVM>
  {
  }
  public class LatestReleasesVM : MEStatelessViewModel<LatestReleasesVM>
  {
    public MovieList MovieList { get; set; }
        public AccountList AccountList { get; set; }
        public TransactionList TransactionList { get; set; }

    // Filter Criteria
    public String MovieTitle { get; set; }
    public DateTime ReleaseFromDate { get; set; }
    public DateTime ReleaseToDate { get; set; }
        public decimal UserBalance { get; set; }

        /// <summary>
        /// Gets or sets the Movie Genre ID
        /// </summary>
        [Singular.DataAnnotations.DropDownWeb(typeof(MELib.RO.ROMovieGenreList), UnselectedText = "Select", ValueMember = "MovieGenreID", DisplayMember = "Genre")]
    [Display(Name = "Genre")]
    public int? MovieGenreID { get; set; }

    public LatestReleasesVM()
    {

    }
    protected override void Setup()
    {
      base.Setup();
      MovieList = MovieList.GetMovieList();
            TransactionList = TransactionList.GetTransactionList();
            AccountList = AccountList.GetAccountList();
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
    public Result FilterMovies(int MovieGenreID, int ResetInd)
    {
      Result sr = new Result();
      try
      {
        if (ResetInd == 0)
        {
          MELib.Movies.MovieList MovieList = MELib.Movies.MovieList.GetMovieList(MovieGenreID);
          sr.Data = MovieList;
        }
        else
        {
          MELib.Movies.MovieList MovieList = MELib.Movies.MovieList.GetMovieList();
          sr.Data = MovieList;
        }
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