using EntFrm.Business.IDAL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace EntFrm.Business.SQLServerDAL
{
  public class ViewOperatFlowsDAL: IViewOperatFlows
  {
        #region sql
        private const string SQL_GET_ALL_RECORDS = @"Select *  From ViewOperatFlows Where AppCode like @AppCode And ValidityState=1";
        private const string SQL_GET_RECORDS_BY_NO = @"Select * From ViewOperatFlows Where   AppCode like @AppCode And   ValidityState=1 And OFlowNo=@OFlowNo";
        private const string SQL_GET_NAME_BY_NO = @"Select CnName From ViewOperatFlows Where   AppCode like @AppCode And   ValidityState=1 And OFlowNo=@OFlowNo";
        private const string SQL_ADD_RECORD = @"Insert into ViewOperatFlows
                                              (OFlowNo,DataFlag,OperatType,DataFrom,RUserNo,CnName,EnName,Age,Sex,Nation,CardType,IdCardNo,RiCardNo,Address,PostCode,Telphone,HeadPhoto,Summary,StafferNo,RegistDate,OperatTime,RoomNo,RoomName,OptIndex,OptName,OptDesc,Opter1No,Opter1Name,Opter2No,Opter2Name,Opter3No,Opter3Name,Opter4No,Opter4Name,Opter5No,Opter5Name,OperatState,BranchNo,AddOptor,AddDate,ModOptor,ModDate,ValidityState,Comments,AppCode)
                                              values(@OFlowNo,@DataFlag,@OperatType,@DataFrom,@RUserNo,@CnName,@EnName,@Age,@Sex,@Nation,@CardType,@IdCardNo,@RiCardNo,@Address,@PostCode,@Telphone,@HeadPhoto,@Summary,@StafferNo,@RegistDate,@OperatTime,@RoomNo,@RoomName,@OptIndex,@OptName,@OptDesc,@Opter1No,@Opter1Name,@Opter2No,@Opter2Name,@Opter3No,@Opter3Name,@Opter4No,@Opter4Name,@Opter5No,@Opter5Name,@OperatState,@BranchNo,@AddOptor,@AddDate,@ModOptor,@ModDate,@ValidityState,@Comments,@AppCode)";
        private const string SQL_UPDATE_RECORD = @"Update ViewOperatFlows set
                                                 OFlowNo=@OFlowNo,DataFlag=@DataFlag,OperatType=@OperatType,DataFrom=@DataFrom,RUserNo=@RUserNo,CnName=@CnName,EnName=@EnName,Age=@Age,Sex=@Sex,Nation=@Nation,CardType=@CardType,IdCardNo=@IdCardNo,RiCardNo=@RiCardNo,Address=@Address,PostCode=@PostCode,Telphone=@Telphone,HeadPhoto=@HeadPhoto,Summary=@Summary,StafferNo=@StafferNo,RegistDate=@RegistDate,OperatTime=@OperatTime,RoomNo=@RoomNo,RoomName=@RoomName,OptIndex=@OptIndex,OptName=@OptName,OptDesc=@OptDesc,Opter1No=@Opter1No,Opter1Name=@Opter1Name,Opter2No=@Opter2No,Opter2Name=@Opter2Name,Opter3No=@Opter3No,Opter3Name=@Opter3Name,Opter4No=@Opter4No,Opter4Name=@Opter4Name,Opter5No=@Opter5No,Opter5Name=@Opter5Name,OperatState=@OperatState,BranchNo=@BranchNo,AddOptor=@AddOptor,AddDate=@AddDate,ModOptor=@ModOptor,ModDate=@ModDate,ValidityState=@ValidityState,Comments=@Comments,AppCode=@AppCode 
                                                 Where  AppCode like @AppCode And   ValidityState=1 And OFlowNo=@OFlowNo  And Version=@Version";
        private const string SQL_HARD_DELETE_RECORD = @"Delete From ViewOperatFlows Where   AppCode like @AppCode And   OFlowNo=@OFlowNo ";
        private const string SQL_SOFT_DELETE_RECORD = @"Update ViewOperatFlows set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 And OFlowNo=@OFlowNo";
        private const string SQL_HARD_DELETE_BY_CONDTION = @"Delete From ViewOperatFlows Where   AppCode like @AppCode ";
        private const string SQL_SOFT_DELETE_BY_CONDTION = @"Update ViewOperatFlows set ValidityState=0 Where   AppCode like @AppCode And   ValidityState=1 ";
        private const string SQL_GET_RECORDS_BY_CLASSNO = @"Select * From ViewOperatFlows Where    AppCode like @AppCode And   ValidityState=1 And StafferNo=@StafferNo";
        private const string SQL_GET_COUNT_BY_CONDITION = @"Select Count(*) From ViewOperatFlows Where   AppCode like @AppCode  And   ValidityState=1 ";
        #endregion

        #region param
        private const string PARAM_ID = "@ID";
        private const string PARAM_OFLOWNO = "@OFlowNo";
        private const string PARAM_DATAFLAG = "@DataFlag";
        private const string PARAM_OPERATTYPE = "@OperatType";
        private const string PARAM_DATAFROM = "@DataFrom";
        private const string PARAM_RUSERNO = "@RUserNo";
        private const string PARAM_CNNAME = "@CnName";
        private const string PARAM_ENNAME = "@EnName";
        private const string PARAM_AGE = "@Age";
        private const string PARAM_SEX = "@Sex";
        private const string PARAM_NATION = "@Nation";
        private const string PARAM_CARDTYPE = "@CardType";
        private const string PARAM_IDCARDNO = "@IdCardNo";
        private const string PARAM_RICARDNO = "@RiCardNo";
        private const string PARAM_ADDRESS = "@Address";
        private const string PARAM_POSTCODE = "@PostCode";
        private const string PARAM_TELPHONE = "@Telphone";
        private const string PARAM_HEADPHOTO = "@HeadPhoto";
        private const string PARAM_SUMMARY = "@Summary";
        private const string PARAM_STAFFERNO = "@StafferNo";
        private const string PARAM_REGISTDATE = "@RegistDate";
        private const string PARAM_OPERATTIME = "@OperatTime";
        private const string PARAM_ROOMNO = "@RoomNo";
        private const string PARAM_ROOMNAME = "@RoomName";
        private const string PARAM_OPTINDEX = "@OptIndex";
        private const string PARAM_OPTNAME = "@OptName";
        private const string PARAM_OPTDESC = "@OptDesc";
        private const string PARAM_OPTER1NO = "@Opter1No";
        private const string PARAM_OPTER1NAME = "@Opter1Name";
        private const string PARAM_OPTER2NO = "@Opter2No";
        private const string PARAM_OPTER2NAME = "@Opter2Name";
        private const string PARAM_OPTER3NO = "@Opter3No";
        private const string PARAM_OPTER3NAME = "@Opter3Name";
        private const string PARAM_OPTER4NO = "@Opter4No";
        private const string PARAM_OPTER4NAME = "@Opter4Name";
        private const string PARAM_OPTER5NO = "@Opter5No";
        private const string PARAM_OPTER5NAME = "@Opter5Name";
        private const string PARAM_OPERATSTATE = "@OperatState";
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

        public ViewOperatFlowsDAL(string sConnStr,string sAppCode)
        {
           this.connStr = sConnStr;
           this.appCode = sAppCode;
        }

        public ViewOperatFlowsCollections GetAllRecords()
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            ViewOperatFlowsCollections infos = null;
            ViewOperatFlows info = null;

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
                    infos = new ViewOperatFlowsCollections();
                    while (reader.Read())
                    {
                        info = new ViewOperatFlows();
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

        public ViewOperatFlowsCollections GetRecordsByClassNo(string sClassNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            ViewOperatFlowsCollections infos = null;
            ViewOperatFlows info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_STAFFERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sClassNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_CLASSNO,paras);

                if (reader.HasRows)
                {
                    infos = new ViewOperatFlowsCollections();
                    while (reader.Read())
                    {
                        info = new ViewOperatFlows();
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

        public ViewOperatFlowsCollections GetRecordsByNo(string sNo)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            ViewOperatFlowsCollections infos = null;
            ViewOperatFlows info = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_OFLOWNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = sNo;
                paras[1].Value = "%" + appCode + ";%";

                connection = SqlHelper.GetConnection(connStr);
                reader = SqlHelper.ExecuteReader(connection, CommandType.Text, SQL_GET_RECORDS_BY_NO,paras);

                if (reader.HasRows)
                {
                    infos = new ViewOperatFlowsCollections();
                    while (reader.Read())
                    {
                        info = new ViewOperatFlows();
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
                    new SqlParameter(PARAM_OFLOWNO,SqlDbType.NVarChar,20),
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

        public int AddNewRecord(ViewOperatFlows info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_OFLOWNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_DATAFLAG,SqlDbType.Int),
                    new SqlParameter(PARAM_OPERATTYPE,SqlDbType.Int),
                    new SqlParameter(PARAM_DATAFROM,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_RUSERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_CNNAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_ENNAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_AGE,SqlDbType.Int),
                    new SqlParameter(PARAM_SEX,SqlDbType.Int),
                    new SqlParameter(PARAM_NATION,SqlDbType.NVarChar,10),
                    new SqlParameter(PARAM_CARDTYPE,SqlDbType.Int),
                    new SqlParameter(PARAM_IDCARDNO,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_RICARDNO,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_ADDRESS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_POSTCODE,SqlDbType.NVarChar,10),
                    new SqlParameter(PARAM_TELPHONE,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_HEADPHOTO,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_SUMMARY,SqlDbType.NVarChar,2000),
                    new SqlParameter(PARAM_STAFFERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_REGISTDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_OPERATTIME,SqlDbType.DateTime),
                    new SqlParameter(PARAM_ROOMNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ROOMNAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_OPTINDEX,SqlDbType.Int),
                    new SqlParameter(PARAM_OPTNAME,SqlDbType.NVarChar,200),
                    new SqlParameter(PARAM_OPTDESC,SqlDbType.NVarChar,200),
                    new SqlParameter(PARAM_OPTER1NO,SqlDbType.NVarChar,200),
                    new SqlParameter(PARAM_OPTER1NAME,SqlDbType.NVarChar,200),
                    new SqlParameter(PARAM_OPTER2NO,SqlDbType.NVarChar,200),
                    new SqlParameter(PARAM_OPTER2NAME,SqlDbType.NVarChar,200),
                    new SqlParameter(PARAM_OPTER3NO,SqlDbType.NVarChar,200),
                    new SqlParameter(PARAM_OPTER3NAME,SqlDbType.NVarChar,200),
                    new SqlParameter(PARAM_OPTER4NO,SqlDbType.NVarChar,200),
                    new SqlParameter(PARAM_OPTER4NAME,SqlDbType.NVarChar,200),
                    new SqlParameter(PARAM_OPTER5NO,SqlDbType.NVarChar,200),
                    new SqlParameter(PARAM_OPTER5NAME,SqlDbType.NVarChar,200),
                    new SqlParameter(PARAM_OPERATSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_BRANCHNO,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_ADDOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MODOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MODDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_VALIDITYSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_COMMENTS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256)
                };
                paras[0].Value = info.sOFlowNo;
                paras[1].Value = info.iDataFlag;
                paras[2].Value = info.iOperatType;
                paras[3].Value = info.sDataFrom;
                paras[4].Value = info.sRUserNo;
                paras[5].Value = info.sCnName;
                paras[6].Value = info.sEnName;
                paras[7].Value = info.iAge;
                paras[8].Value = info.iSex;
                paras[9].Value = info.sNation;
                paras[10].Value = info.iCardType;
                paras[11].Value = info.sIdCardNo;
                paras[12].Value = info.sRiCardNo;
                paras[13].Value = info.sAddress;
                paras[14].Value = info.sPostCode;
                paras[15].Value = info.sTelphone;
                paras[16].Value = info.sHeadPhoto;
                paras[17].Value = info.sSummary;
                paras[18].Value = info.sStafferNo;
                paras[19].Value = info.dRegistDate;
                paras[20].Value = info.dOperatTime;
                paras[21].Value = info.sRoomNo;
                paras[22].Value = info.sRoomName;
                paras[23].Value = info.iOptIndex;
                paras[24].Value = info.sOptName;
                paras[25].Value = info.sOptDesc;
                paras[26].Value = info.sOpter1No;
                paras[27].Value = info.sOpter1Name;
                paras[28].Value = info.sOpter2No;
                paras[29].Value = info.sOpter2Name;
                paras[30].Value = info.sOpter3No;
                paras[31].Value = info.sOpter3Name;
                paras[32].Value = info.sOpter4No;
                paras[33].Value = info.sOpter4Name;
                paras[34].Value = info.sOpter5No;
                paras[35].Value = info.sOpter5Name;
                paras[36].Value = info.iOperatState;
                paras[37].Value = info.sBranchNo;
                paras[38].Value = info.sAddOptor;
                paras[39].Value = info.dAddDate;
                paras[40].Value = info.sModOptor;
                paras[41].Value = info.dModDate;
                paras[42].Value = info.iValidityState;
                paras[43].Value = info.sComments;
                paras[44].Value = info.sAppCode;

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

        public int UpdateRecord(ViewOperatFlows info)
        {
            SqlConnection connection = null;

            try
            {
                SqlParameter[] paras = new SqlParameter[]
                {
                    new SqlParameter(PARAM_OFLOWNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_DATAFLAG,SqlDbType.Int),
                    new SqlParameter(PARAM_OPERATTYPE,SqlDbType.Int),
                    new SqlParameter(PARAM_DATAFROM,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_RUSERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_CNNAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_ENNAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_AGE,SqlDbType.Int),
                    new SqlParameter(PARAM_SEX,SqlDbType.Int),
                    new SqlParameter(PARAM_NATION,SqlDbType.NVarChar,10),
                    new SqlParameter(PARAM_CARDTYPE,SqlDbType.Int),
                    new SqlParameter(PARAM_IDCARDNO,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_RICARDNO,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_ADDRESS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_POSTCODE,SqlDbType.NVarChar,10),
                    new SqlParameter(PARAM_TELPHONE,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_HEADPHOTO,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_SUMMARY,SqlDbType.NVarChar,2000),
                    new SqlParameter(PARAM_STAFFERNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_REGISTDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_OPERATTIME,SqlDbType.DateTime),
                    new SqlParameter(PARAM_ROOMNO,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ROOMNAME,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_OPTINDEX,SqlDbType.Int),
                    new SqlParameter(PARAM_OPTNAME,SqlDbType.NVarChar,200),
                    new SqlParameter(PARAM_OPTDESC,SqlDbType.NVarChar,200),
                    new SqlParameter(PARAM_OPTER1NO,SqlDbType.NVarChar,200),
                    new SqlParameter(PARAM_OPTER1NAME,SqlDbType.NVarChar,200),
                    new SqlParameter(PARAM_OPTER2NO,SqlDbType.NVarChar,200),
                    new SqlParameter(PARAM_OPTER2NAME,SqlDbType.NVarChar,200),
                    new SqlParameter(PARAM_OPTER3NO,SqlDbType.NVarChar,200),
                    new SqlParameter(PARAM_OPTER3NAME,SqlDbType.NVarChar,200),
                    new SqlParameter(PARAM_OPTER4NO,SqlDbType.NVarChar,200),
                    new SqlParameter(PARAM_OPTER4NAME,SqlDbType.NVarChar,200),
                    new SqlParameter(PARAM_OPTER5NO,SqlDbType.NVarChar,200),
                    new SqlParameter(PARAM_OPTER5NAME,SqlDbType.NVarChar,200),
                    new SqlParameter(PARAM_OPERATSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_BRANCHNO,SqlDbType.NVarChar,50),
                    new SqlParameter(PARAM_ADDOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_ADDDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_MODOPTOR,SqlDbType.NVarChar,20),
                    new SqlParameter(PARAM_MODDATE,SqlDbType.DateTime),
                    new SqlParameter(PARAM_VALIDITYSTATE,SqlDbType.Int),
                    new SqlParameter(PARAM_COMMENTS,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_APPCODE,SqlDbType.NVarChar,256),
                    new SqlParameter(PARAM_VERSION,SqlDbType.Timestamp)
                };
                paras[0].Value = info.sOFlowNo;
                paras[1].Value = info.iDataFlag;
                paras[2].Value = info.iOperatType;
                paras[3].Value = info.sDataFrom;
                paras[4].Value = info.sRUserNo;
                paras[5].Value = info.sCnName;
                paras[6].Value = info.sEnName;
                paras[7].Value = info.iAge;
                paras[8].Value = info.iSex;
                paras[9].Value = info.sNation;
                paras[10].Value = info.iCardType;
                paras[11].Value = info.sIdCardNo;
                paras[12].Value = info.sRiCardNo;
                paras[13].Value = info.sAddress;
                paras[14].Value = info.sPostCode;
                paras[15].Value = info.sTelphone;
                paras[16].Value = info.sHeadPhoto;
                paras[17].Value = info.sSummary;
                paras[18].Value = info.sStafferNo;
                paras[19].Value = info.dRegistDate;
                paras[20].Value = info.dOperatTime;
                paras[21].Value = info.sRoomNo;
                paras[22].Value = info.sRoomName;
                paras[23].Value = info.iOptIndex;
                paras[24].Value = info.sOptName;
                paras[25].Value = info.sOptDesc;
                paras[26].Value = info.sOpter1No;
                paras[27].Value = info.sOpter1Name;
                paras[28].Value = info.sOpter2No;
                paras[29].Value = info.sOpter2Name;
                paras[30].Value = info.sOpter3No;
                paras[31].Value = info.sOpter3Name;
                paras[32].Value = info.sOpter4No;
                paras[33].Value = info.sOpter4Name;
                paras[34].Value = info.sOpter5No;
                paras[35].Value = info.sOpter5Name;
                paras[36].Value = info.iOperatState;
                paras[37].Value = info.sBranchNo;
                paras[38].Value = info.sAddOptor;
                paras[39].Value = info.dAddDate;
                paras[40].Value = info.sModOptor;
                paras[41].Value = info.dModDate;
                paras[42].Value = info.iValidityState;
                paras[43].Value = info.sComments;
                paras[44].Value = info.sAppCode;
                paras[45].Value = StringHelper.ConvertToBytes(info.sVersion);

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
                    new SqlParameter(PARAM_OFLOWNO,SqlDbType.NVarChar,20),
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
                    new SqlParameter(PARAM_OFLOWNO,SqlDbType.NVarChar,20),
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
        public ViewOperatFlowsCollections GetRecords_Paging(SqlModel s_model)
        {
            SqlConnection connection = null;
            SqlDataReader reader = null;
            ViewOperatFlowsCollections infos = null;
            ViewOperatFlows info = null;

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
                    infos = new ViewOperatFlowsCollections();
                    while (reader.Read())
                    {
                        info = new ViewOperatFlows();
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
        internal static void PutObjectProperty(ViewOperatFlows obj_info, SqlDataReader reader)
        {
            obj_info.iID= int.Parse(reader["ID"].ToString());
            obj_info.sOFlowNo= reader["OFlowNo"].ToString();
            obj_info.iDataFlag= int.Parse(reader["DataFlag"].ToString());
            obj_info.iOperatType= int.Parse(reader["OperatType"].ToString());
            obj_info.sDataFrom= reader["DataFrom"].ToString();
            obj_info.sRUserNo= reader["RUserNo"].ToString();
            obj_info.sCnName= reader["CnName"].ToString();
            obj_info.sEnName= reader["EnName"].ToString();
            obj_info.iAge= int.Parse(reader["Age"].ToString());
            obj_info.iSex= int.Parse(reader["Sex"].ToString());
            obj_info.sNation= reader["Nation"].ToString();
            obj_info.iCardType= int.Parse(reader["CardType"].ToString());
            obj_info.sIdCardNo= reader["IdCardNo"].ToString();
            obj_info.sRiCardNo= reader["RiCardNo"].ToString();
            obj_info.sAddress= reader["Address"].ToString();
            obj_info.sPostCode= reader["PostCode"].ToString();
            obj_info.sTelphone= reader["Telphone"].ToString();
            obj_info.sHeadPhoto= reader["HeadPhoto"].ToString();
            obj_info.sSummary= reader["Summary"].ToString();
            obj_info.sStafferNo= reader["StafferNo"].ToString();
            obj_info.dRegistDate= DateTime.Parse(reader["RegistDate"].ToString());
            obj_info.dOperatTime= DateTime.Parse(reader["OperatTime"].ToString());
            obj_info.sRoomNo= reader["RoomNo"].ToString();
            obj_info.sRoomName= reader["RoomName"].ToString();
            obj_info.iOptIndex= int.Parse(reader["OptIndex"].ToString());
            obj_info.sOptName= reader["OptName"].ToString();
            obj_info.sOptDesc= reader["OptDesc"].ToString();
            obj_info.sOpter1No= reader["Opter1No"].ToString();
            obj_info.sOpter1Name= reader["Opter1Name"].ToString();
            obj_info.sOpter2No= reader["Opter2No"].ToString();
            obj_info.sOpter2Name= reader["Opter2Name"].ToString();
            obj_info.sOpter3No= reader["Opter3No"].ToString();
            obj_info.sOpter3Name= reader["Opter3Name"].ToString();
            obj_info.sOpter4No= reader["Opter4No"].ToString();
            obj_info.sOpter4Name= reader["Opter4Name"].ToString();
            obj_info.sOpter5No= reader["Opter5No"].ToString();
            obj_info.sOpter5Name= reader["Opter5Name"].ToString();
            obj_info.iOperatState= int.Parse(reader["OperatState"].ToString());
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