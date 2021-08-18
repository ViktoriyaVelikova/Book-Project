using BookBeing.Services.Home.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBeing.Services.Home
{
  public  interface IHomeService
    {
        List<HomeServiceBooksModel> TakeThreeBooks();
    }
}
