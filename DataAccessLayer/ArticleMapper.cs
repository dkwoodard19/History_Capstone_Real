using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class ArticleMapper : Mapper
    {
        int OffsetToArticleID;       //0
        int OffsetToArticleName;     // 1
        int OffsetToArticleText;     // 2
        int OffsetToEventID;         // 3
        int OffsetToEventName;       // 4
        int OffsetToUserID;          // 5
        int OffsetToUserName;        // 6

        public ArticleMapper(SqlDataReader reader)
        {
            OffsetToArticleID = reader.GetOrdinal("ArticleID");
            Assert(0 == OffsetToArticleID, $"ArticleID is {OffsetToArticleID} instead of 0 as expected");
            OffsetToArticleName = reader.GetOrdinal("ArticleName");
            Assert(1 == OffsetToArticleName, $"ArticleName is {OffsetToArticleName} instead of 1 as expected");
            OffsetToArticleText = reader.GetOrdinal("ArticleText");
            Assert(2 == OffsetToArticleText, $"ArticleText is {OffsetToArticleText} instead of 2 as expected");
            OffsetToEventID = reader.GetOrdinal("EventID");
            Assert(3 == OffsetToEventID, $"EventID is {OffsetToEventID} instead of 3 as expected");
            OffsetToEventName = reader.GetOrdinal("EventName");
            Assert(4 == OffsetToEventName, $"EventName is {OffsetToEventName} instead of 4 as expected");
            OffsetToUserID = reader.GetOrdinal("UserID");
            Assert(5 == OffsetToUserID, $"UserID is {OffsetToUserID} instead of 5 as expected");
            OffsetToUserName = reader.GetOrdinal("UserName");
            Assert(6 == OffsetToUserName, $"UserName is {OffsetToUserName} instead of 6 as expected");
        }

        public ArticleDAL ToArticle(SqlDataReader reader)
        {
            ArticleDAL proposedReturnValue = new ArticleDAL();
            proposedReturnValue.ArticleID = reader.GetInt32(OffsetToArticleID);
            proposedReturnValue.ArticleName = reader.GetString(OffsetToArticleName);
            proposedReturnValue.ArticleText = reader.GetString(OffsetToArticleText);
            proposedReturnValue.EventID = reader.GetInt32(OffsetToEventID);
            proposedReturnValue.EventName = reader.GetString(OffsetToEventName);
            proposedReturnValue.UserID = reader.GetInt32(OffsetToUserID);
            proposedReturnValue.UserName = reader.GetString(OffsetToUserName);
            return proposedReturnValue;
        }
    }
}
