using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ArticleDAL
    {
        public int ArticleID { get; set; }
        public string ArticleName { get; set; }
        public string ArticleText { get; set; }
        public int EventID { get; set; }
        public string EventName { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
    }
}
