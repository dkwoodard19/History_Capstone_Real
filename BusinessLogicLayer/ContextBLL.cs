using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLogicLayer
{
    public class ContextBLL : IDisposable
    {
        ContextDAL _context = new ContextDAL();

        public ContextBLL()
        {
            _context.ConnectionString = 
                ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        #region Roles
        public List<RoleBLL> RolesGetAll(int Skip, int Take)
        {
            List<RoleBLL> proposedReturnValue = new List<RoleBLL>();
            List<RoleDAL> items = _context.RolesGetAll(Skip, Take);
            foreach (RoleDAL item in items)
            {
                RoleBLL correctedItem = new RoleBLL(item);
                proposedReturnValue.Add(correctedItem);
            }
            return proposedReturnValue;
        }

        public RoleBLL RoleFindByID(int RoleID)
        {
            RoleBLL proposedReturnValue = null;
            RoleDAL item = _context.RoleFindByID(RoleID);
            if (item != null)
            {
                proposedReturnValue = new RoleBLL(item);
            }
            return proposedReturnValue;
        }

        public int RolesObtainCount()
        {
            int proposedReturnValue = 0;
            proposedReturnValue = _context.RolesObtainCount();
            return proposedReturnValue;
        }
        public int RoleCreate(RoleBLL Role)
        {
            int ret = _context.RoleCreate(Role.RoleName);
            return ret;
        }
        public int RoleCreate(string RoleName)
        {
            int proposedReturnValue = 0;
            proposedReturnValue = _context.RoleCreate(RoleName);
            return proposedReturnValue;
        }

        public void RoleUpdateJust(RoleBLL role)
        {
            _context.RoleUpdateJust(role.RoleID, role.RoleName);
        }

        public void RoleUpdateJust(int RoleID, string RoleName)
        {
            _context.RoleUpdateJust(RoleID, RoleName);
        }

        public void RoleDelete(int RoleID)
        {
            _context.RoleDelete(RoleID);
        }
        #endregion

        #region Users
        public List<UserBLL> UsersGetAll(int Skip, int Take)
        {
            List<UserBLL> proposedReturnValue = new List<UserBLL>();
            List<UserDAL> items = _context.UsersGetAll(Skip, Take);
            foreach (UserDAL item in items)
            {
                UserBLL correctedItem = new UserBLL(item);
                proposedReturnValue.Add(correctedItem);
            }
            return proposedReturnValue;
        }

        public List<UserBLL> UsersGetRelatedToRoleID(int Skip, int Take, int RoleID)
        {
            List<UserBLL> proposedReturnValue = new List<UserBLL>();
            List<UserDAL> items = _context.UsersGetRelatedToRoleID(Skip, Take, RoleID);
            foreach (UserDAL item in items)
            {
                UserBLL correctedItem = new UserBLL(item);
                proposedReturnValue.Add(correctedItem);
            }
            return proposedReturnValue;
        }

        public UserBLL UserFindByEmail(string Email)
        {
            UserBLL proposedReturnValue = null;
            UserDAL item = _context.UserFindByEmail(Email);
            if (item != null)
            {
                proposedReturnValue = new UserBLL(item);
            }
            return proposedReturnValue;
        }

        public UserBLL UserFindByUserName(string UserName)
        {
            UserBLL proposedReturnValue = null;
            UserDAL item = _context.UserFindByUserName(UserName);
            if (item != null)
            {
                proposedReturnValue = new UserBLL(item);
            }
            return proposedReturnValue;
        }

        public UserBLL UserFindByID(int UserID)
        {
            UserBLL proposedReturnValue = null;
            UserDAL item = _context.UserFindByID(UserID);
            if (item != null)
            {
                proposedReturnValue = new UserBLL(item);
            }
            return proposedReturnValue;
        }

        public int UsersObtainCount()
        {
            int proposedReturnValue = 0;
            proposedReturnValue = _context.UsersObtainCount();
            return proposedReturnValue;
        }

        public int UserCreate(UserBLL user)
        {
            int proposedReturnValue = UserCreate(user.UserID, user.UserName, user.Email, user.Hash, user.Salt, user.RoleID);
            return proposedReturnValue;
        }

        public int UserCreate(int UserID, string UserName, string Email, string Hash, string Salt, int RoleID)
        {
            int proposedReturnValue = 0;
            proposedReturnValue = _context.UserCreate(UserID, UserName, Email, Hash, Salt, RoleID);
            return proposedReturnValue;
        }

        public void UserUpdateJust(UserBLL userUpdate)
        {
            _context.UserUpdateJust(userUpdate.UserID, userUpdate.UserName, userUpdate.Email, userUpdate.Hash, userUpdate.Salt, userUpdate.RoleID);
        }

        public void UserUpdateJust(int UserID, string UserName, string Email, string Hash, string Salt, int RoleID)
        {
            _context.UserUpdateJust(UserID, UserName, Email, Hash, Salt, RoleID);
        }

        public void UserDelete(int UserID)
        {
            _context.UserDelete(UserID);
        }
        #endregion

        #region Figures
        public List<FigureBLL> FiguresGetAll(int Skip, int Take)
        {
            List<FigureBLL> proposedReturnValue = new List<FigureBLL>();
            List<FigureDAL> items = _context.FiguresGetAll(Skip, Take);
            foreach (FigureDAL item in items)
            {
                FigureBLL correctedItem = new FigureBLL(item);
                proposedReturnValue.Add(correctedItem);
            }
            return proposedReturnValue;
        }

        public List<FigureBLL> FiguresGetRelatedToCivID(int Skip, int Take, int CivID)
        {
            List<FigureBLL> proposedReturnValue = new List<FigureBLL>();
            List<FigureDAL> items = _context.FiguresGetRelatedToCivID(Skip, Take, CivID);
            foreach (FigureDAL item in items)
            {
                FigureBLL correctedItem = new FigureBLL(item);
                proposedReturnValue.Add(correctedItem);
            }
            return proposedReturnValue;
        }

        public FigureBLL FigureFindByID(int FigureID)
        {
            FigureBLL proposedReturnValue = null;
            FigureDAL item = _context.FigureFindByID(FigureID);
            if (item != null)
            {
                proposedReturnValue = new FigureBLL(item);
            }
            return proposedReturnValue;
        }

        public int FiguresObtainCount()
        {
            int proposedReturnValue = 0;
            proposedReturnValue = _context.FiguresObtainCount();
            return proposedReturnValue;
        }

        public int FigureCreate(FigureBLL figure)
        {
            int proposedReturnValue = FigureCreate(figure.FigureID, figure.FigureName, figure.FigureDOB, figure.FigureDOD, figure.CivID);
            return proposedReturnValue;
        }

        public int FigureCreate(int FigureID, string FigureName, DateTime FigureDOB, DateTime FigureDOD, int CivID)
        {
            int proposedReturnValue = 0;
            proposedReturnValue = _context.FigureCreate(FigureID, FigureName, FigureDOB, FigureDOD, CivID);
            return proposedReturnValue;
        }

        public void FiguresUpdateJust(FigureBLL figureUpdate)
        {
            _context.FiguresUpdateJust(figureUpdate.FigureID, figureUpdate.FigureName, figureUpdate.FigureDOB, figureUpdate.FigureDOD, figureUpdate.CivID);
        }

        public void FiguresUpdateJust(int FigureID, string FigureName, DateTime FigureDOB, DateTime FigureDOD, int CivID)
        {
            _context.FiguresUpdateJust(FigureID, FigureName, FigureDOB, FigureDOD, CivID);
        }

        public void FigureDelete(int FigureID)
        {
            _context.FigureDelete(FigureID);
        }
        #endregion

        #region Events
        public List<EventBLL> EventsGetAll(int Skip, int Take)
        {
            List<EventBLL> proposedReturnValue = new List<EventBLL>();
            List<EventDAL> items = _context.EventsGetAll(Skip, Take);
            foreach (EventDAL item in items)
            {
                EventBLL correctedItem = new EventBLL(item);
                proposedReturnValue.Add(correctedItem);
            }
            return proposedReturnValue;
        }

        public List<EventBLL> EventsGetRelatedToCivID(int Skip, int Take, int CivID)
        {
            List<EventBLL> proposedReturnValue = new List<EventBLL>();
            List<EventDAL> items = _context.EventsGetRelatedToCivID(Skip, Take, CivID);
            foreach (EventDAL item in items)
            {
                EventBLL correctedItem = new EventBLL(item);
                proposedReturnValue.Add(correctedItem);
            }
            return proposedReturnValue;
        }

        public List<EventBLL> EventsGetRelatedToFigureID(int Skip, int Take, int FigureID)
        {
            List<EventBLL> proposedReturnValue = new List<EventBLL>();
            List<EventDAL> items = _context.EventsGetRelatedToFigureID(Skip, Take, FigureID);
            foreach (EventDAL item in items)
            {
                EventBLL correctedItem = new EventBLL(item);
                proposedReturnValue.Add(correctedItem);
            }
            return proposedReturnValue;
        }

        public EventBLL EventFindByID(int EventID)
        {
            EventBLL proposedReturnValue = null;
            EventDAL item = _context.EventFindByID(EventID);
            if (item != null)
            {
                proposedReturnValue = new EventBLL(item);
            }
            return proposedReturnValue;
        }

        public int EventsObtainCount()
        {
            int proposedReturnValue = 0;
            proposedReturnValue = _context.EventsObtainCount();
            return proposedReturnValue;
        }

        public void EventUpdateJust(EventBLL eventUpdate)
        {
            _context.EventUpdateJust(eventUpdate.EventID, eventUpdate.EventName, eventUpdate.EventDate, eventUpdate.FigureID, eventUpdate.CivID);
        }

        public void EventUpdateJust(int EventID, string EventName, DateTime EventDate, int FigureID, int CivID)
        {
            _context.EventUpdateJust(EventID, EventName, EventDate, FigureID, CivID);
        }

        public int EventCreate(EventBLL @event)
        {
            int proposedReturnValue = EventCreate(@event.EventID, @event.EventName, @event.EventDate, @event.FigureID, @event.CivID);
            return proposedReturnValue;
        }

        public int EventCreate(int EventID, string EventName, DateTime EventDate, int FigureID, int CivID)
        {
            int proposedReturnValue = 0;
            proposedReturnValue = _context.EventCreate(EventID, EventName, EventDate, FigureID, CivID);
            return proposedReturnValue;
        }

        public void EventDelete(int EventID)
        {
            _context.EventDelete(EventID);
        }
        #endregion

        #region Civilizations
        public List<CivilizationBLL> CivilizationsGetAll(int Skip, int Take)
        {
            List<CivilizationBLL> proposedReturnValue = new List<CivilizationBLL>();
            List<CivilizationDAL> items = _context.CivilizationsGetAll(Skip, Take);
            foreach (CivilizationDAL item in items)
            {
                CivilizationBLL correctedItem = new CivilizationBLL(item);
                proposedReturnValue.Add(correctedItem);
            }
            return proposedReturnValue;
        }

        public CivilizationBLL CivilizationFindByID(int CivID)
        {
            CivilizationBLL proposedReturnValue = null;
            CivilizationDAL item = _context.CivilizationFindByID(CivID);
            if (item != null)
            {
                proposedReturnValue = new CivilizationBLL(item);
            }
            return proposedReturnValue;
        }

        public CivilizationBLL CivilizationFindByName(string CivName)
        {
            CivilizationBLL proposedReturnValue = null;
            CivilizationDAL item = _context.CivilizationFindByName(CivName);
            if (item != null)
            {
                proposedReturnValue = new CivilizationBLL(item);
            }
            return proposedReturnValue;
        }

        public int CivilizationsObtainCount()
        {
            int proposedReturnValue = 0;
            proposedReturnValue = _context.CivilizationsObtainCount();
            return proposedReturnValue;
        }

        public void CivilizationsUpdateJust(CivilizationBLL civ)
        {
            _context.CivilizationsUpdateJust(civ.CivID, civ.CivName, civ.CivStart, civ.CivEnd);
        }

        public void CivilizationsUpdateJust(int CivID, string CivName, DateTime CivStart, DateTime CivEnd)
        {
            _context.CivilizationsUpdateJust(CivID, CivName, CivStart, CivEnd);
        }

        public int CivilizationCreate(CivilizationBLL civ)
        {
            int proposedReturnValue = CivilizationCreate(civ.CivID, civ.CivName, civ.CivStart, civ.CivEnd);
            return proposedReturnValue;
        }

        public int CivilizationCreate(int CivID, string CivName, DateTime CivStart, DateTime CivEnd)
        {
            int proposedReturnValue = 0;
            proposedReturnValue = _context.CivilizationCreate(CivID, CivName, CivStart, CivEnd);
            return proposedReturnValue;
        }

        public void CivilizationDelete(int CivID)
        {
            _context.CivilizationDelete(CivID);
        }
        #endregion

        #region Articles
        public List<ArticleBLL> ArticlesGetAll(int Skip, int Take)
        {
            List<ArticleBLL> proposedReturnValue = new List<ArticleBLL>();
            List<ArticleDAL> items = _context.ArticlesGetAll(Skip, Take);
            foreach (ArticleDAL item in items)
            {
                ArticleBLL correctedItem = new ArticleBLL(item);
                proposedReturnValue.Add(correctedItem);
            }
            return proposedReturnValue;
        }

        public List<ArticleBLL> ArticlesGetRelatedToEventID(int Skip, int Take, int EventID)
        {
            List<ArticleBLL> proposedReturnValue = new List<ArticleBLL>();
            List<ArticleDAL> items = _context.ArticlesGetRelatedToEventID(Skip, Take, EventID);
            foreach (ArticleDAL item in items)
            {
                ArticleBLL correctedItem = new ArticleBLL(item);
                proposedReturnValue.Add(correctedItem);
            }
            return proposedReturnValue;
        }

        public List<ArticleBLL> ArticlesGetRelatedToUserID(int Skip, int Take, int UserID)
        {
            List<ArticleBLL> proposedReturnValue = new List<ArticleBLL>();
            List<ArticleDAL> items = _context.ArticlesGetRelatedToUserID(Skip, Take, UserID);
            foreach (ArticleDAL item in items)
            {
                ArticleBLL correctedItem = new ArticleBLL(item);
                proposedReturnValue.Add(correctedItem);
            }
            return proposedReturnValue;
        }

        public ArticleBLL ArticleFindByID(int ArticleID)
        {
            ArticleBLL proposedReturnValue = null;
            ArticleDAL item = _context.ArticleFindByID(ArticleID);
            if (item != null)
            {
                proposedReturnValue = new ArticleBLL(item);
            }
            return proposedReturnValue;
        }

        public ArticleBLL ArticleFindByName(int ArticleName)
        {
            ArticleBLL proposedReturnValue = null;
            ArticleDAL item = _context.ArticleFindByName(ArticleName);
            if (item != null)
            {
                proposedReturnValue = new ArticleBLL(item);
            }
            return proposedReturnValue;
        }

        public int ArticleCreate(ArticleBLL article)
        {
            int proposedReturnValue = ArticleCreate(article.ArticleName, article.ArticleText, article.EventID, article.UserID);
            return proposedReturnValue;
        }

        public int ArticleCreate(string ArticleName, string ArticleText, int EventID, int UserID)
        {
            int proposedReturnValue = 0;
            proposedReturnValue = _context.ArticleCreate(ArticleName, ArticleText, EventID, UserID);
            return proposedReturnValue;
        }

        public void ArticlesUpdateJust(ArticleBLL articleUpdate)
        {
            _context.ArticlesUpdateJust(articleUpdate.ArticleID, articleUpdate.ArticleName, articleUpdate.ArticleText, articleUpdate.EventID, articleUpdate.UserID);
        }

        public void ArticlesUpdateJust(int ArticleID, string ArticleName, string ArticleText, int EventID, string EventName, int UserID, string UserName)
        {
            _context.ArticlesUpdateJust(ArticleID, ArticleName, ArticleText, EventID, UserID);
        }

        public void ArticleDelete(int ArticleID)
        {
            _context.ArticleDelete(ArticleID);
        }

        public int ArticlesObtainCount()
        {
            int proposedReturnValue = 0;
            proposedReturnValue = _context.ArticlesObtainCount();
            return proposedReturnValue;
        }
        #endregion
    }
}
