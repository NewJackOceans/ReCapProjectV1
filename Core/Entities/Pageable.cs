using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Pageable<T>
    {
        public Pageable()
        {

        }
        public Pageable(int currentPage, int pageSize, int totalCount, List<T> contents)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalCount = totalCount;
            Contents = contents;
            PageCount = totalCount / (pageSize + (totalCount % pageSize > 0 ? 1 : 0)); // Sayfa içeriği sayfada sergileyebileceğimizden fazla ise sonra ki sayfaya geçmesi için.

        }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int TotalCount { get; set; }
        public List<T> Contents { get; set; }
    }
}
