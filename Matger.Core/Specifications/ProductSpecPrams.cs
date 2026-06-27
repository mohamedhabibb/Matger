using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matger.Core.Specifications
{
    public class ProductSpecPrams
    {
        public string? sort {  get; set; }
           
        public int? BrandId {  get; set; }
         
        public int? TypeId { get; set; }


        private int pageSize { get; set; } = 5;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > 10 ? 10 : value; }
        }

        public int PageIndex { get; set; } = 1;


        public string? SearchName { get; set; }
    }
}
