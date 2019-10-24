using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class ArticleBLL
    {
        public int ArticleID { get; set; }
        public string ArticleName { get; set; }
        public string ArticleText { get; set; }
        public int EventID { get; set; }
        public string EventName { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }

        public ArticleBLL(DataAccessLayer.ArticleDAL art)
        {
            ArticleID = art.ArticleID;
            ArticleName = art.ArticleName;
            ArticleText = art.ArticleText;
            EventID = art.EventID;
            EventName = art.EventName;
            UserID = art.UserID;
            UserName = art.UserName;
        }

        public ArticleBLL()
        {
            // default constructor(ctor) is REQUIRED for MVC
        }
    }
}
