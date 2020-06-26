using EntFrm.Business.IDAL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace EntFrm.Business.SQLServerDAL
{
  public class RecipeHistoryDAL: IRecipeHistory
  {
        #region sql
        private const string SQL_GET_ALL_RECORDS = @"Select *  From RecipeHistory Where AppCode like @AppCode And ValidityState=1";
        private const string SQL_GET_RECORDS_BY_NO = @"Select * From RecipeHistory Where   AppCode like @AppCode And   ValidityState=1 And HFlowNo=@HFlowNo";
        private const string SQL_GET_NAME_BY_NO = @"Select TicketNo From RecipeHistory Where   AppCode like @AppCode And   ValidityState=1 And HFlowNo=@HFlowNo";
        private const string SQL_ADD_RECORD = @"Insert into RecipeHistory
                                              (HFlowNo,DataFlag,RegistNo,TicketNo,RUserNo,CnName,Age,Sex,CounterNo,EnqueueTime,BeginTime,FinishTime,RecipeState,RecipeOpter,RecipeDate,ProcessState,ProcessedTime,PrcsCounterNo,DataFrom,BranchNo,AddOptor,AddDate,ModOptor,ModDate,ValidityState,Comments,AppCode)
                                              values(@HFlowNo,@DataFlag,@RegistNo,@TicketNo,@RUserNo,@CnName,@Age,@Sex,@CounterNo,@EnqueueTime,@BeginTime,@FinishTime,@RecipeState,@RecipeOpter,@RecipeDate,@ProcessState,@ProcessedTime,@PrcsCounterNo,@DataFrom,@BranchNo,@AddOptor,@AddDate,@ModOptor,@ModDate,@ValidityState,@Comments,@AppCode)";
        private const string SQL_UPDATE_RECORD = @"Update RecipeHistory set
                                                 HFlowNo=@HFlowNo,DataFlag=@DataFlag,RegistNo=@RegistNo,TicketNo=@TicketNo,RUserNo=@RUserNo,CnName=@CnName,Age=@Age,Sex=@Sex,CounterNo=@CounterNo,EnqueueTime=@EnqueueTime,BeginTime=@BeginTime,FinishTime=@FinishTime,RecipeState=@RecipeState,RecipeOpter=@RecipeOpter,RecipeDate=@RecipeDate,ProcessState=@ProcessState,ProcessedTime=@ProcessedTime,PrcsCounterNo=@PrcsCounterNo,DataFrom=@DataFrom,BranchNo=@BranchNo,AddOptor=@AddOptor,AddDate=@AddDate,ModOptor=@ModOptor,ModDate=@ModDate,ValidityState=@ValidityState,Comments=@Comments,AppCode=@AppCode 
                                                 Where  AppCode like @AppCode And   ValidityState=1 And HFlowNo=@HFlowNo  And Version=@Version";
        private const string SQL_HARD_DELETE_RECORD = @"Delete From RecipeHistory Where   AppCode like @AppCode And   HFlowNo=@HFlowNo ";
        private const string SQL_SOFT_DELETE_RECORD = @"Update RecipeHistory set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 And HFlowNo=@HFlowNo";
        private const string SQL_HARD_DELETE_BY_CONDTION = @"Delete From RecipeHistory Where   AppCode like @AppCode ";
        private const string SQL_SOFT_DELETE_BY_CONDTION = @"Update RecipeHistory set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 ";
        private const string SQL_GET_RECORDS_BY_CLASSNO = @"Select * From RecipeHistory Where    AppCode like @AppCode And   ValidityState=1 And CounterNo=@CounterNo";
        private const string SQL_GET_COUNT_BY_CONDITION = @"Select Count(*) From RecipeHistory Where   AppCode like @AppCode  And   ValidityState=1 ";
        #endregion

        #region param
        private const string PARAM_ID = "@ID";
        private const string PARAM_HFLOWNO = "@HFlowNo";
        private const string PARAM_DATAFLAG = "@DataFlag";
        private const string PARAM_REGISTNO = "@RegistNo";
        private const string PARAM_TICKETNO = "@TicketNo";
        private const string PARAM_RUSERNO = "@RUserNo";
        private const string PARAM_CNNAME = "@CnName";
        private const string PARAM_AGE = "@Age";
        private const string PARAM_SEX = "@Sex";
        private const string PARAM_COUNTERNO = "@CounterNo";
        private const string PARAM_ENQUEUETIME = "@EnqueueTime";
        private const string PARAM_BEGINTIME = "@BeginTime";
        private const string PARAM_FINISHTIME = "@FinishTime";
        private const string PARAM_RECIPESTATE = "@RecipeState";
        private const string PARAM_RECIPEOPTER = "@RecipeOpter";
        private const string PARAM_RECIPEDATE = "@RecipeDate";
        private const string PARAM_PROCESSSTATE = "@ProcessState";
        private const string PARAM_PROCESSEDTIME = "@ProcessedTime";
        private const string PARAM_PRCSCOUNTERNO = "@PrcsCounterNo";
        private const string PARAM_DATAFROM = "@DataFrom";
        private const string PARAM_BRANCHNO = "@BranchNo";
        private const string PARAM_ADDOPTOR = "@AddOptor";
        private const string PARAM_ADDDATE = "@AddDate";
        private const string PARAM_MODOPTOR = "@ModOptor";
        private const string PARAM_MODDATE = "@ModDate";
        private const string PARAM_VALIDITYSTATE = "@ValidityState";
        private const string PARAM_COMMENTS = "@Comments";
        private const string PARAM_APPCODE = "@AppCode";
        private const string PARAM_VERSION = "@Version";
        #endregion

        private string connStr;
        private string appCode;

        public RecipeHistoryDAL(string sConnStr,string sAppCode)
        {
           this.connStr = sConnStr;
           this.appCode = sAppCode;
        }

        public RecipeHistoryCollections GetAllRecords()
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            RecipeHistoryCollections infos = null;
            RecipeHistory info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                 };
                paras[0].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_ALL_RECORDS,paras);

                if (reader.HasRows)
                {
                    infos = new RecipeHistoryCollections();
                    while (reader.Read())
                    {
                        info = new RecipeHistory();
                        // 设置对象属性
                        PutObjectProperty(info, reader);
                        infos.Add(info);
                    }
                }
                return infos;
            }
            catch (Exception ex)
            {
                throw new Exception(" 查询所有记录(DAL层|GetAllRecords)时出错;" + ex.Message);
            }
            finally
            {
                if (reader != null)
                    ((IDisposable)reader).Dispose();
                if (connection != null)
                    connection.Dispose();
            }
        }

        public RecipeHistoryCollections GetRecordsByClassNo(string sClassNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            RecipeHistoryCollections infos = null;
            RecipeHistory info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_COUNTERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sClassNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_CLASSNO,paras);

                if (reader.HasRows)
                {
                    infos = new RecipeHistoryCollections();
                    while (reader.Read())
                    {
                        info = new RecipeHistory();
                        // 设置对象属性
                        PutObjectProperty(info, reader);
                        infos.Add(info);
                    }
                }
                return infos;
            }
            catch (Exception ex)
            {
                throw new Exception(" 通过sClassNo查询记录(DAL层)时出错;" + ex.Message);
            }
            finally
            {
                if (reader != null)
                    ((IDisposable)reader).Dispose();
                if (connection != null)
                    connection.Dispose();
            } 
        }

        public RecipeHistoryCollections GetRecordsByNo(string sNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            RecipeHistoryCollections infos = null;
            RecipeHistory info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_HFLOWNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_NO,paras);

                if (reader.HasRows)
                {
                    infos = new RecipeHistoryCollections();
                    while (reader.Read())
                    {
                        info = new RecipeHistory();
                        //设置对象属性
                        PutObjectProperty(info, reader);
                        infos.Add(info);
                    }
                }
                return infos;
            }
            catch (Exception ex)
            {
                throw new Exception(" 通过No查询记录(DAL层)时出错;" + ex.Message);
            }
            finally
            {
                if (reader != null)
                    ((IDisposable)reader).Dispose();
                if (connection != null)
                    connection.Dispose();
            }
        }

        public string GetRecordNameByNo(string sNo)
        {
            SqlConnection connection = null;
            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_HFLOWNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                return (string)SqlHelper.ExecuteScalar(connection, CommandType.Text, SQL_GET_NAME_BY_NO, paras);
            }
            catch (Exception ex)
            {
                throw new Exception(" 通过No查询记录名称(DAL层)时出错;" + ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Dispose();
            }
        }

        public int AddNewRecord(RecipeHistory info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_HFLOWNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_DATAFLAG,SqlDbType.Int),
                    new SqlParameter(PARAM_REGISTNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_TICKETNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_RUSERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_CNNAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_AGE,SqlDbType.Int),
                    new SqlParameter(PARAM_SEX,SqlDbType.Int),
                    new SqlParameter(PARAM_COUNTERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ENQUEUETIME,SqlDbType.DateTime),
                    new SqlParameter(PARAM_BEGINTIME,SqlDbType.DateTime),
                    new SqlParameter(PARAM_FINISHTIME,SqlDbType.DateTime),
                    new SqlParameter(PARAM_RECIPESTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_RECIPEOPTER,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_RECIPEDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_PROCESSSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_PROCESSEDTIME,SqlDbType.DateTime),
                    new SqlParameter(PARAM_PRCSCOUNTERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_DATAFROM,SqlDbType.NVarChar,500),
                    new SqlParameter(PARAM_BRANCHNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MODOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MODDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_VALIDITYSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_COMMENTS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = info.sHFlowNo;
                paras[1].Value = info.iDataFlag;
                paras[2].Value = info.sRegistNo;
                paras[3].Value = info.sTicketNo;
                paras[4].Value = info.sRUserNo;
                paras[5].Value = info.sCnName;
                paras[6].Value = info.iAge;
                paras[7].Value = info.iSex;
                paras[8].Value = info.sCounterNo;
                paras[9].Value = info.dEnqueueTime;
                paras[10].Value = info.dBeginTime;
                paras[11].Value = info.dFinishTime;
                paras[12].Value = info.iRecipeState;
                paras[13].Value = info.sRecipeOpter;
                paras[14].Value = info.dRecipeDate;
                paras[15].Value = info.iProcessState;
                paras[16].Value = info.dProcessedTime;
                paras[17].Value = info.sPrcsCounterNo;
                paras[18].Value = info.sDataFrom;
                paras[19].Value = info.sBranchNo;
                paras[20].Value = info.sAddOptor;
                paras[21].Value = info.dAddDate;
                paras[22].Value = info.sModOptor;
                paras[23].Value = info.dModDate;
                paras[24].Value = info.iValidityState;
                paras[25].Value = info.sComments;
                paras[26].Value = info.sAppCode;

                connection = SqlHelper.GetConnection(connStr);
                return SqlHelper.ExecuteNonQuery(connection, CommandType.Text, SQL_ADD_RECORD, paras);
            }
            catch (Exception ex)
            {
                throw new Exception(" 添加(DAL层)记录时出错;" + ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Dispose();
            }
        }

        public int UpdateRecord(RecipeHistory info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_HFLOWNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_DATAFLAG,SqlDbType.Int),
                    new SqlParameter(PARAM_REGISTNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_TICKETNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_RUSERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_CNNAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_AGE,SqlDbType.Int),
                    new SqlParameter(PARAM_SEX,SqlDbType.Int),
                    new SqlParameter(PARAM_COUNTERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ENQUEUETIME,SqlDbType.DateTime),
                    new SqlParameter(PARAM_BEGINTIME,SqlDbType.DateTime),
                    new SqlParameter(PARAM_FINISHTIME,SqlDbType.DateTime),
                    new SqlParameter(PARAM_RECIPESTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_RECIPEOPTER,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_RECIPEDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_PROCESSSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_PROCESSEDTIME,SqlDbType.DateTime),
                    new SqlParameter(PARAM_PRCSCOUNTERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_DATAFROM,SqlDbType.NVarChar,500),
                    new SqlParameter(PARAM_BRANCHNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MODOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MODDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_VALIDITYSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_COMMENTS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_VERSION,SqlDbType.Timestamp)
                };
                paras[0].Value = info.sHFlowNo;
                paras[1].Value = info.iDataFlag;
                paras[2].Value = info.sRegistNo;
                paras[3].Value = info.sTicketNo;
                paras[4].Value = info.sRUserNo;
                paras[5].Value = info.sCnName;
                paras[6].Value = info.iAge;
                paras[7].Value = info.iSex;
                paras[8].Value = info.sCounterNo;
                paras[9].Value = info.dEnqueueTime;
                paras[10].Value = info.dBeginTime;
                paras[11].Value = info.dFinishTime;
                paras[12].Value = info.iRecipeState;
                paras[13].Value = info.sRecipeOpter;
                paras[14].Value = info.dRecipeDate;
                paras[15].Value = info.iProcessState;
                paras[16].Value = info.dProcessedTime;
                paras[17].Value = info.sPrcsCounterNo;
                paras[18].Value = info.sDataFrom;
                paras[19].Value = info.sBranchNo;
                paras[20].Value = info.sAddOptor;
                paras[21].Value = info.dAddDate;
                paras[22].Value = info.sModOptor;
                paras[23].Value = info.dModDate;
                paras[24].Value = info.iValidityState;
                paras[25].Value = info.sComments;
                paras[26].Value = info.sAppCode;
                paras[27].Value = StringHelper.ConvertToBytes(info.sVersion);

                connection = SqlHelper.GetConnection(connStr);
                return SqlHelper.ExecuteNonQuery(connection, CommandType.Text, SQL_UPDATE_RECORD, paras);
            }
            catch (Exception ex)
            {
                throw new Exception(" 更新记录(DAL层)时出错;" + ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Dispose();
            }
        }

        public int HardDeleteRecord(string sNo)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_HFLOWNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                return SqlHelper.ExecuteNonQuery(connection, CommandType.Text, SQL_HARD_DELETE_RECORD, paras);
            }
            catch (Exception ex)
            {
                throw new Exception(" 硬删除记录(DAL层)时出错;" + ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Dispose();
            }
        }

        public int SoftDeleteRecord(string sNo)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_HFLOWNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                return SqlHelper.ExecuteNonQuery(connection, CommandType.Text, SQL_SOFT_DELETE_RECORD, paras);
            }
            catch (Exception ex)
            {
                throw new Exception(" 软删除记录(DAL层)时出错;" + ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Dispose();
            }
        }

public int HardDeleteByCondition(string sCondtion)
{
    SqlConnection connection = null;
    try
    {
        string sql = SQL_HARD_DELETE_BY_CONDTION;
        if (!string.IsNullOrEmpty(sCondtion))
        {
           sql += " And " + sCondtion;
       }
       SqlParameter[] paras = new SqlParameter[]
      {
      new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
       };
       paras[0].Value = "%" + appCode + ";%";
      connection = SqlHelper.GetConnection(connStr);
       return SqlHelper.ExecuteNonQuery(connection, CommandType.Text, sql, paras);
  }
  catch (Exception ex)
  {
      throw new Exception("硬删除记录(DAL层)时出错; " + ex.Message);
  }
  finally
  {
      if (connection != null)
          connection.Dispose();
  }
 }
public int SoftDeleteByCondition(string sCondtion)
{
   SqlConnection connection = null;
    try
   {
       string sql = SQL_SOFT_DELETE_BY_CONDTION;
       if (!string.IsNullOrEmpty(sCondtion))
       {
           sql += " And " + sCondtion;
       }
       SqlParameter[] paras = new SqlParameter[]
       {
      new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
      };
      paras[0].Value = "%" + appCode + ";%";
      connection = SqlHelper.GetConnection(connStr);
      return SqlHelper.ExecuteNonQuery(connection, CommandType.Text, sql, paras);
  }
  catch (Exception ex)
 {
     throw new Exception(" 硬删除记录(DAL层)时出错; " + ex.Message);
  }
  finally
  {
     if (connection != null)
         connection.Dispose();
 }
 }
        public RecipeHistoryCollections GetRecords_Paging(SqlModel s_model)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            RecipeHistoryCollections infos = null;
            RecipeHistory info = null;

            try
            {
                 if (s_model.sCondition.Length==0)
                {
                    s_model.sCondition = " Where  AppCode like '%" + appCode + ";%' And ValidityState=1";
                }
                else
                {
                    s_model.sCondition = " Where   AppCode like '%" + appCode + ";%' And ValidityState=1 And  " + s_model.sCondition;
                }

                string strSql = SqlHelper.GetSQL_Paging(s_model);
                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, strSql);
                if (reader.HasRows)
                {
                    infos = new RecipeHistoryCollections();
                    while (reader.Read())
                    {
                        info = new RecipeHistory();
                        // 设置对象属性
                        PutObjectProperty(info, reader);
                        infos.Add(info);
                    }
                }
                return infos;
            }
            catch (Exception ex)
            {
                throw new Exception(" 分页查询(DAL层)记录时出错;;" + ex.Message);
            }
            finally
            {
                if (reader != null)
                    ((IDisposable)reader).Dispose();
                if (connection != null)
                    connection.Dispose();
            }
        }

        public int GetCountByCondition(string sCondition)
        {
            SqlConnection connection = null;

            try
            {
                string strSql = SQL_GET_COUNT_BY_CONDITION;
                if(sCondition.Length>0)
                {
                    strSql +="  And " + sCondition;
                }

                SqlParameter[] paras = new SqlParameter[]
                {
                   new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                return Convert.ToInt32(SqlHelper.ExecuteScalar(connection, CommandType.Text, strSql, paras));
            }
            catch (Exception ex)
            {
                throw new Exception(" 计算记录总数(DAL层)时出错;" + ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Dispose();
            }
        }

        #region PutObjectProperty 设置对象属性
        /// <summary>
        /// 从 SqlDataReader 类对象中读取并设置对象属性
        /// </summary>
       /// <param name=" obj_info">主题对象</param>
        /// <param name="dr">读入数据</param>
        internal static void PutObjectProperty(RecipeHistory obj_info, SqlDataReader reader)
        {
            obj_info.iID= int.Parse(reader["ID"].ToString());
            obj_info.sHFlowNo= reader["HFlowNo"].ToString();
            obj_info.iDataFlag= int.Parse(reader["DataFlag"].ToString());
            obj_info.sRegistNo= reader["RegistNo"].ToString();
            obj_info.sTicketNo= reader["TicketNo"].ToString();
            obj_info.sRUserNo= reader["RUserNo"].ToString();
            obj_info.sCnName= reader["CnName"].ToString();
            obj_info.iAge= int.Parse(reader["Age"].ToString());
            obj_info.iSex= int.Parse(reader["Sex"].ToString());
            obj_info.sCounterNo= reader["CounterNo"].ToString();
            obj_info.dEnqueueTime= DateTime.Parse(reader["EnqueueTime"].ToString());
            obj_info.dBeginTime= DateTime.Parse(reader["BeginTime"].ToString());
            obj_info.dFinishTime= DateTime.Parse(reader["FinishTime"].ToString());
            obj_info.iRecipeState= int.Parse(reader["RecipeState"].ToString());
            obj_info.sRecipeOpter= reader["RecipeOpter"].ToString();
            obj_info.dRecipeDate= DateTime.Parse(reader["RecipeDate"].ToString());
            obj_info.iProcessState= int.Parse(reader["ProcessState"].ToString());
            obj_info.dProcessedTime= DateTime.Parse(reader["ProcessedTime"].ToString());
            obj_info.sPrcsCounterNo= reader["PrcsCounterNo"].ToString();
            obj_info.sDataFrom= reader["DataFrom"].ToString();
            obj_info.sBranchNo= reader["BranchNo"].ToString();
            obj_info.sAddOptor= reader["AddOptor"].ToString();
            obj_info.dAddDate= DateTime.Parse(reader["AddDate"].ToString());
            obj_info.sModOptor= reader["ModOptor"].ToString();
            obj_info.dModDate= DateTime.Parse(reader["ModDate"].ToString());
            obj_info.iValidityState= int.Parse(reader["ValidityState"].ToString());
            obj_info.sComments= reader["Comments"].ToString();
            obj_info.sAppCode= reader["AppCode"].ToString();
            obj_info.sVersion= StringHelper.ConvertToString((byte[])reader["Version"]);
        }
        #endregion
    }
}