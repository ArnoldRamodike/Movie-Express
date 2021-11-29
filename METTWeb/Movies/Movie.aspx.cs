using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Singular.Web;
using MELib.Movies;

namespace MEWeb.Movies
{
  public partial class Movie : MEPageBase<MovieVM>
  {
  }
  public class MovieVM : MEStatelessViewModel<MovieVM>
  {
        public MovieList MovieList { get; set; }
        public int MovieID { get; set; }

        public MovieVM()
    {

    }
    protected override void Setup()
    {
      base.Setup();
     //  MovieList = MovieList.GetMovieListID();

    }


  }
}

