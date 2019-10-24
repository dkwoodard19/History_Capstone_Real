using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class ContextDAL : IDisposable   // garbage disposal; removes heavy data
    {
        #region Connection
        SqlConnection _con = new SqlConnection();

        public void Dispose()
        {
            _con.Dispose();
        }

        void EnsureConnected()
        {
            switch (_con.State)
            {
                case (System.Data.ConnectionState.Closed):
                    _con.Open();
                    break;
                case (System.Data.ConnectionState.Broken):
                    _con.Close();
                    _con.Open();
                    break;
                case (System.Data.ConnectionState.Open):
                    // do nothing; it's already open
                    break;
            }
        }

        public string ConnectionString
        {
            get
            {
                return _con.ConnectionString;
            }
            set
            {
                _con.ConnectionString = value;
            }
        }
        #endregion

        #region Role_SP

        public RoleDAL RoleFindByID(int RoleID)
        {
            RoleDAL proposedreturnValue = null;
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("RoleFindByID", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@RoleID", RoleID);     //QuotesQuotesQuotes
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    RoleMapper rm = new RoleMapper(reader);
                    int count = 0;
                    while (reader.Read())
                    {
                        proposedreturnValue = rm.ToRole(reader);
                        count++;
                    }
                    if (count > 1)
                    {
                        throw new Exception($"Multiple Roles found for ID {RoleID}");
                    }
                }
            }
            return proposedreturnValue;
        }

        public List<RoleDAL> RolesGetAll(int Skip, int Take)
        {
            List<RoleDAL> proposedReturnValue = new List<RoleDAL>();
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("RolesGetAll", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@skip", Skip);
                command.Parameters.AddWithValue("@take", Take);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    RoleMapper rm = new RoleMapper(reader);
                    while (reader.Read())
                    {
                        RoleDAL item = rm.ToRole(reader);
                        proposedReturnValue.Add(item);
                    }
                }
            }
            return proposedReturnValue;
        }

        public int RolesObtainCount()
        {
            int proposedReturnValue = 0;
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("RolesObtainCount", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                proposedReturnValue = (int)command.ExecuteScalar();
            }
            return proposedReturnValue;
        }

        public int RoleCreate(string RoleName)
        {
            int proposedReturnValue = 0;
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("RoleCreate", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@RoleName", RoleName);
                command.Parameters.AddWithValue("@RoleID", 0);
                command.Parameters["@RoleID"].Direction = System.Data.ParameterDirection.Output;
                command.ExecuteNonQuery();
                proposedReturnValue = (int)command.Parameters["@RoleID"].Value;
            }
            return proposedReturnValue;
        }

        public void RoleDelete(int RoleID)
        {
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("RoleDelete", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@RoleID", RoleID);
                command.ExecuteNonQuery();
            }
        }

        public void RoleUpdateJust(int RoleID, string RoleName)
        {
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("RoleUpdateJust", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@RoleID", RoleID);
                command.Parameters.AddWithValue("@RoleName", RoleName);
                command.ExecuteNonQuery();
            }
        }
        #endregion

        #region User_SP
        public UserDAL UserFindByID(int UserID)
        {
            UserDAL proposedReturnValue = null;
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("UserFindByID", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", UserID);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    UserMapper um = new UserMapper(reader);
                    int count = 0;
                    while (reader.Read())
                    {
                        proposedReturnValue = um.ToUser(reader);
                        count++;
                    }
                    if (count > 1)
                    {
                        throw new Exception($"Multiple Users found for ID {UserID}");
                    }
                }
            }
            return proposedReturnValue;
        }

        public UserDAL UserFindByEmail(string Email)
        {
            UserDAL proposedReturnValue = null;
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("UserFindByEmail", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Email", Email);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    UserMapper um = new UserMapper(reader);
                    int count = 0;
                    while (reader.Read())
                    {
                        proposedReturnValue = um.ToUser(reader);
                        count++;
                    }
                    if (count > 1)
                    {
                        throw new Exception($"Multiple Users found for Email {Email}");
                    }
                }
            }
            return proposedReturnValue;
        }

        public UserDAL UserFindByUserName(string UserName)
        {
            UserDAL proposedReturnValue = null;
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("UserFindByUserName", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserName", UserName);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    UserMapper um = new UserMapper(reader);
                    int count = 0;
                    while (reader.Read())
                    {
                        proposedReturnValue = um.ToUser(reader);
                        count++;
                    }
                    if (count > 1)
                    {
                        throw new Exception($"Multiple Users found for UserName {UserName}");
                    }
                }
            }
            return proposedReturnValue;
        }

        public int UserCreate(int UserID, string UserName, string Email, string Hash, string Salt, int RoleID)
        {
            int proposedReturnValue = 0;
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("UserCreate", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", 0);
                command.Parameters.AddWithValue("@UserName", UserName);
                command.Parameters.AddWithValue("@Email", Email);
                command.Parameters.AddWithValue("@Hash", Hash);
                command.Parameters.AddWithValue("@Salt", Salt);
                command.Parameters.AddWithValue("@RoleID", RoleID);
                command.Parameters["@UserID"].Direction = System.Data.ParameterDirection.Output;
                command.ExecuteNonQuery();
                proposedReturnValue = (int)command.Parameters["@UserID"].Value;
            }
            return proposedReturnValue;
        }

        public void UserDelete(int UserID)
        {
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("UserDelete", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", UserID);
                command.ExecuteNonQuery();
            }
        }

        public List<UserDAL> UsersGetAll(int Skip, int Take)
        {
            List<UserDAL> proposedReturnValue = new List<UserDAL>();
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("UsersGetAll", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Skip", Skip);
                command.Parameters.AddWithValue("@Take", Take);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    UserMapper um = new UserMapper(reader);
                    while (reader.Read())
                    {
                        UserDAL item = um.ToUser(reader);
                        proposedReturnValue.Add(item);
                    }
                }
            }
            return proposedReturnValue;
        }

        public List<UserDAL> UsersGetRelatedToRoleID(int Skip, int Take, int RoleID)
        {
            List<UserDAL> proposedReturnValue = new List<UserDAL>();
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("UsersGetAll", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Skip", Skip);
                command.Parameters.AddWithValue("@Take", Take);
                command.Parameters.AddWithValue("@RoleID", RoleID);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    UserMapper um = new UserMapper(reader);
                    while (reader.Read())
                    {
                        UserDAL item = um.ToUser(reader);
                        proposedReturnValue.Add(item);
                    }
                }
            }
            return proposedReturnValue;
        }

        public void UserUpdateJust(int UserID, string UserName, string Email, string Hash, string Salt, int RoleID)
        {
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("UserUpdateJust", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", UserID);
                command.Parameters.AddWithValue("@UserName", UserName);
                command.Parameters.AddWithValue("@Email", Email);
                command.Parameters.AddWithValue("@Hash", Hash);
                command.Parameters.AddWithValue("@Salt", Salt);
                command.Parameters.AddWithValue("@RoleID", RoleID);
                command.ExecuteNonQuery();
            }
        }

        public int UsersObtainCount()
        {
            int proposedReturnValue = 0;
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("UsersObtainCount", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                proposedReturnValue = (int)command.ExecuteScalar();
            }
            return proposedReturnValue;
        }
        #endregion

        #region Figure_SP
        public FigureDAL FigureFindByID(int FigureID)
        {
            FigureDAL proposedReturnValue = null;
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("FigureFindByID", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FigureID", FigureID);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    FigureMapper fm = new FigureMapper(reader);
                    int count = 0;
                    while (reader.Read())
                    {
                        proposedReturnValue = fm.ToFigure(reader);
                        count++;
                    }
                    if (count > 1)
                    {
                        throw new Exception($"Multiple Figures found for ID {FigureID}");
                    }
                }
            }
            return proposedReturnValue;
        }

        public int FigureCreate(int FigureID, string FigureName, DateTime FigureDOB, DateTime FigureDOD, int CivID)
        {
            int proposedReturnValue = 0;
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("FigureCreate", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FigureID", 0);
                command.Parameters.AddWithValue("@FigureName", FigureName);
                command.Parameters.AddWithValue("@FigureDOB", FigureDOB);
                command.Parameters.AddWithValue("@FigureDOD", FigureDOD);
                command.Parameters.AddWithValue("@CivID", CivID);
                command.Parameters["@FigureID"].Direction = System.Data.ParameterDirection.Output;
                command.ExecuteNonQuery();
                proposedReturnValue = (int)command.Parameters["@FigureID"].Value;
            }
            return proposedReturnValue;
        }

        public void FigureDelete(int FigureID)
        {
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("FigureDelete", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FigureID", FigureID);
                command.ExecuteNonQuery();
            }
        }

        public List<FigureDAL> FiguresGetAll(int Skip, int Take)
        {
            List<FigureDAL> proposedReturnValue = new List<FigureDAL>();
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("FiguresGetAll", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Skip", Skip);
                command.Parameters.AddWithValue("@Take", Take);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    FigureMapper fm = new FigureMapper(reader);
                    while (reader.Read())
                    {
                        FigureDAL item = fm.ToFigure(reader);
                        proposedReturnValue.Add(item);
                    }
                }
            }
            return proposedReturnValue;
        }

        public List<FigureDAL> FiguresGetRelatedToCivID(int Skip, int Take, int CivID)
        {
            List<FigureDAL> proposedReturnValue = new List<FigureDAL>();
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("FiguresGetAll", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Skip", Skip);
                command.Parameters.AddWithValue("@Take", Take);
                command.Parameters.AddWithValue("@CivID", CivID);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    FigureMapper fm = new FigureMapper(reader);
                    while (reader.Read())
                    {
                        FigureDAL item = fm.ToFigure(reader);
                        proposedReturnValue.Add(item);
                    }
                }
            }
            return proposedReturnValue;
        }

        public int FiguresObtainCount()
        {
            int proposedReturnValue = 0;
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("FiguresObtainCount", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                proposedReturnValue = (int)command.ExecuteScalar();
            }
            return proposedReturnValue;
        }

        public void FigureUpdateJust(int FigureID, string FigureName, DateTime FigureDOB, DateTime FigureDOD, int CivID)
        {
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("FigureUpdateJust", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FigureID", FigureID);
                command.Parameters.AddWithValue("@FigureName", FigureName);
                command.Parameters.AddWithValue("@FigureDOB", FigureDOB);
                command.Parameters.AddWithValue("@FigureDOD", FigureDOD);
                command.Parameters.AddWithValue("@CivID", CivID);
                command.ExecuteNonQuery();
            }
        }
        #endregion

        #region Events_SP
        public EventDAL EventFindByID(int EventID)
        {
            EventDAL proposedReturnValue = null;
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("EventFindByID", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@EventID", EventID);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    EventMapper em = new EventMapper(reader);
                    int count = 0;
                    while (reader.Read())
                    {
                        proposedReturnValue = em.ToEvent(reader);
                        count++;
                    }
                    if (count > 1)
                    {
                        throw new Exception($"Multiple Events found for ID {EventID}");
                    }
                }
            }
            return proposedReturnValue;
        }

        public int EventCreate(int EventID, string EventName, DateTime EventDate, int FigureID, int CivID)
        {
            int proposedReturnValue = 0;
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("EventCreate", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@EventID", 0);
                command.Parameters.AddWithValue("@EventName", EventName);
                command.Parameters.AddWithValue("@EventDate", EventDate);
                command.Parameters.AddWithValue("@FigureID", FigureID);
                command.Parameters.AddWithValue("@CivID", CivID);
                command.Parameters["@EventID"].Direction = System.Data.ParameterDirection.Output;
                command.ExecuteNonQuery();
                proposedReturnValue = Convert.ToInt32(command.Parameters["@EventID"].Value);
            }
            return proposedReturnValue;
        }

        public void EventDelete(int EventID)
        {
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("EventDelete", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@EventID", EventID);
                command.ExecuteNonQuery();
            }
        }

        public List<EventDAL> EventsGetRelatedToCivID(int Skip, int Take, int CivID)
        {
            List<EventDAL> proposedReturnValue = new List<EventDAL>();
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("EventsGetAll", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Skip", Skip);
                command.Parameters.AddWithValue("@Take", Take);
                command.Parameters.AddWithValue("@CivID", CivID);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    EventMapper em = new EventMapper(reader);
                    while (reader.Read())
                    {
                        EventDAL item = em.ToEvent(reader);
                        proposedReturnValue.Add(item);
                    }
                }
            }
            return proposedReturnValue;
        }

        public List<EventDAL> EventsGetRelatedToFigureID(int Skip, int Take, int FigureID)
        {
            List<EventDAL> proposedReturnValue = new List<EventDAL>();
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("EventsGetAll", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Skip", Skip);
                command.Parameters.AddWithValue("@Take", Take);
                command.Parameters.AddWithValue("@FigureID", FigureID);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    EventMapper em = new EventMapper(reader);
                    while (reader.Read())
                    {
                        EventDAL item = em.ToEvent(reader);
                        proposedReturnValue.Add(item);
                    }
                }
            }
            return proposedReturnValue;
        }

        public int EventsObtainCount()
        {
            int proposedReturnValue = 0;
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("EventsObtainCount", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                proposedReturnValue = (int)command.ExecuteScalar();
            }
            return proposedReturnValue;
        }

        public List<EventDAL> EventsGetAll(int Skip, int Take)
        {
            List<EventDAL> proposedReturnValue = new List<EventDAL>();
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("EventsGetAll", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Skip", Skip);
                command.Parameters.AddWithValue("@Take", Take);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    EventMapper fm = new EventMapper(reader);
                    while (reader.Read())
                    {
                        EventDAL item = fm.ToEvent(reader);
                        proposedReturnValue.Add(item);
                    }
                }
            }
            return proposedReturnValue;
        }

        public void EventUpdateJust(int EventID, string EventName, DateTime EventDate, int FigureID, int CivID)
        {
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("EventUpdateJust", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@EventID", EventID);
                command.Parameters.AddWithValue("@EventName", EventName);
                command.Parameters.AddWithValue("@EventDate", EventDate);
                command.Parameters.AddWithValue("@FigureID", FigureID);
                command.Parameters.AddWithValue("@CivID", CivID);
                command.ExecuteNonQuery();
            }
        }
        #endregion

        #region Civilizations
        public CivilizationDAL CivilizationFindByID(int CivID)
        {
            CivilizationDAL proposedreturnValue = null;
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("CivilizationFindByID", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CivID", CivID);     //QuotesQuotesQuotes
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    CivilizationMapper cm = new CivilizationMapper(reader);
                    int count = 0;
                    while (reader.Read())
                    {
                        proposedreturnValue = cm.ToCivilization(reader);
                        count++;
                    }
                    if (count > 1)
                    {
                        throw new Exception($"Multiple Civilizations found for ID {CivID}");
                    }
                }
            }
            return proposedreturnValue;
        }

        public List<CivilizationDAL> CivilizationsGetAll(int Skip, int Take)
        {
            List<CivilizationDAL> proposedReturnValue = new List<CivilizationDAL>();
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("CivilizationsGetAll", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@skip", Skip);
                command.Parameters.AddWithValue("@take", Take);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    CivilizationMapper cm = new CivilizationMapper(reader);
                    while (reader.Read())
                    {
                        CivilizationDAL item = cm.ToCivilization(reader);
                        proposedReturnValue.Add(item);
                    }
                }
            }
            return proposedReturnValue;
        }

        public int CivilizationsObtainCount()
        {
            int proposedReturnValue = 0;
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("CivilizationsObtainCount", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                proposedReturnValue = (int)command.ExecuteScalar();
            }
            return proposedReturnValue;
        }

        public int CivilizationCreate(int CivID, string CivName, DateTime CivStart, DateTime CivEnd)
        {
            int proposedReturnValue = 0;
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("CivilizationCreate", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CivID", 0);
                command.Parameters.AddWithValue("@CivName", CivName);
                command.Parameters.AddWithValue("@CivStart", CivStart);
                command.Parameters.AddWithValue("@CivEnd", CivEnd);
                command.Parameters["@CivID"].Direction = System.Data.ParameterDirection.Output;
                command.ExecuteNonQuery();
                proposedReturnValue = (int)command.Parameters["@CivID"].Value;
            }
            return proposedReturnValue;
        }

        public void CivilizationDelete(int CivID)
        {
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("CivilizationDelete", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CivID", CivID);
                command.ExecuteNonQuery();
            }
        }

        public void CivilizationsUpdateJust(int CivID, string CivName, DateTime CivStart, DateTime CivEnd)
        {
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("CivilizationsUpdateJust", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CivID", CivID);
                command.Parameters.AddWithValue("@CivName", CivName);
                command.Parameters.AddWithValue("@CivStart", CivStart);
                command.Parameters.AddWithValue("@CivEnd", CivEnd);
                command.ExecuteNonQuery();
            }
        }

        public CivilizationDAL CivilizationFindByName(string CivName)
        {
            CivilizationDAL proposedreturnValue = null;
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("CivilizationFindByName", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CivilizationName", CivName);     //QuotesQuotesQuotes
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    CivilizationMapper cm = new CivilizationMapper(reader);
                    int count = 0;
                    while (reader.Read())
                    {
                        proposedreturnValue = cm.ToCivilization(reader);
                        count++;
                    }
                    if (count > 1)
                    {
                        throw new Exception($"Multiple Civilizations found for Name {CivName}");
                    }
                }
            }
            return proposedreturnValue;
        }
        #endregion

        #region Articles
        public ArticleDAL ArticleFindByID(int ArticleID)
        {
            ArticleDAL proposedReturnValue = null;
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("ArticleFindByID", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ArticleID", ArticleID);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    ArticleMapper am = new ArticleMapper(reader);
                    int count = 0;
                    while (reader.Read())
                    {
                        proposedReturnValue = am.ToArticle(reader);
                        count++;
                    }
                    if (count > 1)
                    {
                        throw new Exception($"Multiple Articles found for ID {ArticleID}");
                    }
                }
            }
            return proposedReturnValue;
        }

        public int ArticleCreate(string ArticleName, string ArticleText, int EventID, int UserID)
        {
            int proposedReturnValue = 0;
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("ArticleCreate", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ArticleID", 0);
                command.Parameters.AddWithValue("@ArticleName", ArticleName);
                command.Parameters.AddWithValue("@ArticleText", ArticleText);
                command.Parameters.AddWithValue("@EventID", EventID);
                command.Parameters.AddWithValue("@UserID", UserID);
                command.Parameters["@ArticleID"].Direction = System.Data.ParameterDirection.Output;
                command.ExecuteNonQuery();
                proposedReturnValue = (int)command.Parameters["@ArticleID"].Value;
            }
            return proposedReturnValue;
        }

        public void ArticleDelete(int ArticleID)
        {
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("ArticleDelete", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ArticleID", ArticleID);
                command.ExecuteNonQuery();
            }
        }

        public List<ArticleDAL> ArticlesGetRelatedToUserID(int Skip, int Take, int UserID)
        {
            List<ArticleDAL> proposedReturnValue = new List<ArticleDAL>();
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("ArticlesGetAll", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Skip", Skip);
                command.Parameters.AddWithValue("@Take", Take);
                command.Parameters.AddWithValue("@UserID", UserID);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    ArticleMapper fm = new ArticleMapper(reader);
                    while (reader.Read())
                    {
                        ArticleDAL item = fm.ToArticle(reader);
                        proposedReturnValue.Add(item);
                    }
                }
            }
            return proposedReturnValue;
        }

        public List<ArticleDAL> ArticlesGetRelatedToEventID(int Skip, int Take, int EventID)
        {
            List<ArticleDAL> proposedReturnValue = new List<ArticleDAL>();
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("ArticlesGetAll", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Skip", Skip);
                command.Parameters.AddWithValue("@Take", Take);
                command.Parameters.AddWithValue("@EventID", EventID);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    ArticleMapper fm = new ArticleMapper(reader);
                    while (reader.Read())
                    {
                        ArticleDAL item = fm.ToArticle(reader);
                        proposedReturnValue.Add(item);
                    }
                }
            }
            return proposedReturnValue;
        }
        public ArticleDAL ArticleFindByName(int ArticleName)
        {
            ArticleDAL proposedReturnValue = null;
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("ArticleFindByName", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ArticleName", ArticleName);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    ArticleMapper am = new ArticleMapper(reader);
                    int count = 0;
                    while (reader.Read())
                    {
                        proposedReturnValue = am.ToArticle(reader);
                        count++;
                    }
                    if (count > 1)
                    {
                        throw new Exception($"Multiple Articles found for Name {ArticleName}");
                    }
                }
            }
            return proposedReturnValue;
        }

        public List<ArticleDAL> ArticlesGetAll(int Skip, int Take)
        {
            List<ArticleDAL> proposedReturnValue = new List<ArticleDAL>();
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("ArticlesGetAll", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Skip", Skip);
                command.Parameters.AddWithValue("@Take", Take);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    ArticleMapper fm = new ArticleMapper(reader);
                    while (reader.Read())
                    {
                        ArticleDAL item = fm.ToArticle(reader);
                        proposedReturnValue.Add(item);
                    }
                }
            }
            return proposedReturnValue;
        }

        public int ArticlesObtainCount()
        {
            int proposedReturnValue = 0;
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("ArticlesObtainCount", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                proposedReturnValue = (int)command.ExecuteScalar();
            }
            return proposedReturnValue;
        }

        public void ArticlesUpdateJust(int ArticleID, string ArticleName, string ArticleText, int EventID, int UserID)
        {
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("ArticleUpdateJust", _con))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ArticleID", ArticleID);
                command.Parameters.AddWithValue("@ArticleName", ArticleName);
                command.Parameters.AddWithValue("@ArticleText", ArticleText);
                command.Parameters.AddWithValue("@EventID", EventID);
                command.Parameters.AddWithValue("@UserID", UserID);
                command.ExecuteNonQuery();
            }
        }
        #endregion
    }
}
