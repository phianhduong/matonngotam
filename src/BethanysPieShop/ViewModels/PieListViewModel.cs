using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.ViewModels
{
    public class PieListViewModel
    {

        public IEnumerable<Models.Pie> Pies { get; set; }

        public string CurrentCategory { get; set; }

    }
}
