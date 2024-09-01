using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway.Models.Donation
{
    public class DonationsModel
    {
        public int status { get; set; }
        

        //public PaginationFilter filter { get; set; }
    }


    public class PaginationFilter
    {
        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalRecords { get; }

        public PaginationFilter()
        {
            PageNumber = 1;
            PageSize = 10;
        }

        public PaginationFilter(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 10 ? 10 : pageSize;
        }
    }
}
