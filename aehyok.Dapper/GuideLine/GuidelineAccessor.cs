using aehyok.Dapper.Model;
using aehyok.NLog;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace aehyok.Dapper.GuideLine
{
    public class GuidelineAccessor
    {
        private static LogWriter Logger = new LogWriter();
        //#region 通用指标定义
        /// <summary>
        /// 缓存指标定义
        /// </summary>
        protected static Dictionary<string, MD_GuideLine> GuidelineDefine = new Dictionary<string, MD_GuideLine>();

        /// <summary>
        /// 清空指标定义
        /// </summary>
        public static void ClearGuideLineDefine()
        {
            GuidelineDefine.Clear();
        }

        private const string SqlGetGuidelineDefine = @"select ID,ZBMC,ZBZT,ZBMETA,FID,JSMX_ZBMETA,XSXH,ZBSM,ZBSF from TJ_ZDYZBDYB where ID=@Id ";
        /// <summary>
        /// 取指标定义
        /// </summary>
        /// <param name="guideLineId"></param>
        /// <returns></returns>
        public static MD_GuideLine GetGuidelineDefine(string guideLineId)
        {
            MD_GuideLine define = new MD_GuideLine();
            if (!string.IsNullOrEmpty(guideLineId))
            {
                if (GuidelineDefine.ContainsKey(guideLineId))
                    define = GuidelineDefine[guideLineId];
                else
                {
                    try
                    {
                        IDbConnection conn = new SqlConnection(DataBaseConfig.DefaultSqlConnectionString);
                        var temp=conn.QueryFirstOrDefault<dynamic>(SqlGetGuidelineDefine, new { Id=guideLineId});

                        define.Id = temp.ID.ToString();
                        define.GuideLineName = temp.ZBMC;
                        define.GroupName = temp.ZBZT;
                        define.GuideLineMeta = temp.ZBMETA;
                        define.FatherId = temp.FID.ToString();
                        define.DisplayOrder = Convert.ToInt32(temp.XSXH);
                        define.GuideLineMethod = temp.ZBSF;
                        //using (SqlDataReader dr = cmd.ExecuteReader())
                        //{
                        //    while (dr.Read())
                        //    {
                        //        string id = dr.IsDBNull(0) ? "" : dr.GetOracleDecimal(0).Value.ToString();
                        //        string name = dr.IsDBNull(1) ? "" : dr.GetString(1);
                        //        string groupname = dr.IsDBNull(2) ? "" : dr.GetString(2);
                        //        string zbmeta1 = dr.IsDBNull(3) ? "" : dr.GetString(3);
                        //        string fatherid = dr.IsDBNull(4) ? "0" : dr.GetOracleDecimal(4).Value.ToString();
                        //        string zbmeta2 = dr.IsDBNull(5) ? "" : dr.GetString(5);
                        //        int displayorder = dr.IsDBNull(6) ? 0 : Convert.ToInt32(dr.GetOracleDecimal(6).Value);
                        //        string descript = dr.IsDBNull(7) ? "" : dr.GetString(7);
                        //        string fullMeta = zbmeta1 + zbmeta2;

                        //        define = new MD_GuideLine(id, name, groupname, fatherid, displayorder, descript);
                        //        define.Parameters = MC_GuideLine.GetParametersFromMeta(fullMeta);
                        //        define.ResultGroups = MC_GuideLine.GetFieldGroupsFromMeta(fullMeta);
                        //        define.DetailDefines = MC_GuideLine.GetDetaiDefinelFromMeta(fullMeta);
                        //        define.Children = GetChildGuidelineDefine(define.Id, cn);
                        //        GuidelineDefine.Add(guideLineId, define);
                        //    }
                        //}
                        GuidelineDefine[guideLineId] = define;
                    }
                    catch (Exception exception)
                    {
                        string errorMessage = string.Format("取指标[{0}]的定义出错,错误信息为{1}", guideLineId, exception.Message);
                        Logger.Error(errorMessage);
                    }
                }
            }
            return define;
        }

        private const string SqlGetChildGuidelineDefine = @"select ID,ZBMC,ZBZT,ZBMETA,FID,JSMX_ZBMETA,XSXH,ZBSM from TJ_ZDYZBDYB where FID=:Id ";

        //public static List<MD_GuideLine> GetChildGuidelineDefine(string guideLineId, OracleConnection cn)
        //{
        //    List<MD_GuideLine> _ret = new List<MD_GuideLine>();
        //    MD_GuideLine define = null;
        //    if (!string.IsNullOrEmpty(guideLineId))
        //    {
        //        try
        //        {
        //            OracleCommand _cmd = new OracleCommand(SqlGetChildGuidelineDefine, cn);
        //            _cmd.Parameters.Add(":Id", decimal.Parse(guideLineId));
        //            OracleDataReader dr = _cmd.ExecuteReader();
        //            while (dr.Read())
        //            {
        //                string id = dr.IsDBNull(0) ? "" : dr.GetOracleDecimal(0).Value.ToString();
        //                string name = dr.IsDBNull(1) ? "" : dr.GetString(1);
        //                string groupname = dr.IsDBNull(2) ? "" : dr.GetString(2);
        //                string zbmeta1 = dr.IsDBNull(3) ? "" : dr.GetString(3);
        //                string fatherid = dr.IsDBNull(4) ? "0" : dr.GetOracleDecimal(4).Value.ToString();
        //                string zbmeta2 = dr.IsDBNull(5) ? "" : dr.GetString(5);
        //                int displayorder = dr.IsDBNull(6) ? 0 : Convert.ToInt32(dr.GetOracleDecimal(6).Value);
        //                string descript = dr.IsDBNull(7) ? "" : dr.GetString(7);
        //                string fullMeta = zbmeta1 + zbmeta2;

        //                define = new MD_GuideLine(id, name, groupname, fatherid, displayorder, descript);
        //                define.Parameters = MC_GuideLine.GetParametersFromMeta(fullMeta);
        //                define.ResultGroups = MC_GuideLine.GetFieldGroupsFromMeta(fullMeta);
        //                define.DetailDefines = MC_GuideLine.GetDetaiDefinelFromMeta(fullMeta);
        //                define.Children = GetChildGuidelineDefine(define.Id, cn);
        //                _ret.Add(define);
        //            }
        //            dr.Close();
        //        }
        //        catch (Exception exception)
        //        {
        //            string errorMessage = string.Format("取GetChildGuidelineDefine子指标[{0}]的定义出错,错误信息为{1}", guideLineId, exception.Message);
        //            OralceLogWriter.WriteSystemLog(errorMessage, "ERROR");
        //        }

        //    }
        //    return _ret;
        //}


        ///// <summary>
        ///// 取结果记录数
        ///// </summary>
        ///// <param name="guideLineId"></param>
        ///// <param name="param"></param>
        ///// <param name="filterWord"></param>
        ///// <param name="requestUser"></param>
        ///// <returns></returns>
        //public static int GetQueryResultCount(string guideLineId, Dictionary<string, object> param, string filterWord, SinoRequestUser requestUser)
        //{
        //    int ret = 0;
        //    string queryStr = GetGuidelineMethod(guideLineId);
        //    MD_GuideLine define = GetGuidelineDefine(guideLineId);
        //    if (define != null)
        //    {
        //        List<MDQuery_GuideLineParameter> glPara = new List<MDQuery_GuideLineParameter>();
        //        if (param != null && define.Parameters != null)
        //        {
        //            foreach (var p in param)
        //            {
        //                MD_GuideLineParameter md_pa = define.Parameters.Find(pa => pa.ParameterName == p.Key);
        //                if (md_pa != null)
        //                {
        //                    glPara.Add(new MDQuery_GuideLineParameter(md_pa, p.Value));
        //                }
        //            }
        //        }
        //        foreach (MDQuery_GuideLineParameter gp in glPara)
        //        {
        //            queryStr = OraQueryBuilder.RebuildGuideLineQueryString(queryStr, gp);
        //        }
        //        if (requestUser != null)
        //        {
        //            queryStr = OraQueryBuilder.ReplaceExtSecret(null, queryStr, requestUser);
        //        }
        //        if (!string.IsNullOrEmpty(filterWord))
        //        {
        //            queryStr = OraQueryBuilder.GetFilterQueryStr(define, filterWord, queryStr);
        //        }
        //        try
        //        {
        //            ret = Convert.ToInt32(OracleHelper.ExecuteScalar(OracleHelper.ConnectionStringProfile, CommandType.Text, string.Format("select count(*) from (\n {0} \n) ", queryStr)));
        //        }
        //        catch (Exception exception)
        //        {
        //            string errorMessage = string.Format("取指标[{0}]的结果记录数出错,错误信息为{1}", guideLineId, exception.Message);
        //            OralceLogWriter.WriteSystemLog(errorMessage, "ERROR");
        //        }
        //    }
        //    return ret;
        //}

        //public static int GetQueryResultCount(string guideLineId, Dictionary<string, object> param, string filterWord, SinoRequestUser requestUser, OracleConnection cn)
        //{
        //    int ret = 0;
        //    string queryStr = GetGuidelineMethod(guideLineId);
        //    MD_GuideLine define = GetGuidelineDefine(guideLineId);
        //    if (define != null)
        //    {
        //        List<MDQuery_GuideLineParameter> glPara = new List<MDQuery_GuideLineParameter>();
        //        if (param != null && define.Parameters != null)
        //        {
        //            foreach (var p in param)
        //            {
        //                MD_GuideLineParameter md_pa = define.Parameters.Find(pa => pa.ParameterName == p.Key);
        //                if (md_pa != null)
        //                {
        //                    glPara.Add(new MDQuery_GuideLineParameter(md_pa, p.Value));
        //                }
        //            }
        //        }
        //        foreach (MDQuery_GuideLineParameter gp in glPara)
        //        {
        //            queryStr = OraQueryBuilder.RebuildGuideLineQueryString(queryStr, gp);
        //        }
        //        if (requestUser != null)
        //        {
        //            queryStr = OraQueryBuilder.ReplaceExtSecret(null, queryStr, requestUser);
        //        }
        //        if (!string.IsNullOrEmpty(filterWord))
        //        {
        //            queryStr = OraQueryBuilder.GetFilterQueryStr(define, filterWord, queryStr);
        //        }
        //        try
        //        {
        //            ret = Convert.ToInt32(OracleHelper.ExecuteScalar(cn, CommandType.Text, string.Format("select count(*) from (\n {0} \n) ", queryStr)));
        //        }
        //        catch (Exception exception)
        //        {
        //            string errorMessage = string.Format("取指标[{0}]的结果记录数出错,错误信息为{1}", guideLineId, exception.Message);
        //            OralceLogWriter.WriteSystemLog(errorMessage, "ERROR");
        //        }
        //    }
        //    return ret;
        //}

        //#region  added by lqm 2014.12.26
        ///// <summary>
        ///// added by lqm 2014.03.27 指标查询三个参数
        ///// </summary>
        ///// <param name="guideLineId"></param>
        ///// <param name="param"></param>
        ///// <param name="requestUser"></param>
        ///// <returns></returns>
        //public static DataTable QueryGuideline(string guideLineId, Dictionary<string, object> param, SinoRequestUser requestUser)
        //{
        //    return QueryGuideline(guideLineId, param, "", requestUser);
        //}
        ///// <summary>
        ///// added by lqm 2014.12.26 
        ///// </summary>
        ///// <param name="guideLineId"></param>
        ///// <param name="param"></param>
        ///// <param name="requestUser"></param>
        ///// <returns></returns>
        //public static DataTable QueryGuideline(string guideLineId, Dictionary<string, object> param, SinoRequestUser requestUser, OracleConnection oracleConnection)
        //{
        //    return QueryGuideline(guideLineId, param, "", requestUser, oracleConnection);
        //}
        ///// <summary>
        ///// 取指标结果集
        ///// </summary>
        ///// <param name="guideLineId"></param>
        ///// <param name="param"></param>
        ///// <param name="filterWord"></param>
        ///// <param name="requestUser"></param>
        ///// <returns></returns>
        //public static DataTable QueryGuideline(string guideLineId, Dictionary<string, object> param, string filterWord, SinoRequestUser requestUser, OracleConnection oracleConnection)
        //{
        //    int getQueryStartTime = Environment.TickCount;
        //    int count = 0;
        //    DataTable tb = new DataTable("ResultTable");
        //    MD_GuideLine define = GetGuidelineDefine(guideLineId);
        //    if (define != null)
        //    {
        //        string queryStr = GetGuidelineMethod(guideLineId);

        //        List<MDQuery_GuideLineParameter> glPara = new List<MDQuery_GuideLineParameter>();
        //        if (param != null && define.Parameters != null)
        //        {
        //            foreach (var p in param)
        //            {
        //                MD_GuideLineParameter md_pa = define.Parameters.Find(pa => pa.ParameterName == p.Key);
        //                if (md_pa != null)
        //                {
        //                    glPara.Add(new MDQuery_GuideLineParameter(md_pa, p.Value));
        //                }
        //            }
        //        }
        //        foreach (MDQuery_GuideLineParameter gp in glPara)
        //        {
        //            queryStr = OraQueryBuilder.RebuildGuideLineQueryString(queryStr, gp);
        //        }
        //        if (requestUser != null)
        //        {
        //            queryStr = OraQueryBuilder.ReplaceExtSecret(null, queryStr, requestUser);
        //        }
        //        if (!string.IsNullOrEmpty(filterWord))
        //        {
        //            queryStr = OraQueryBuilder.GetFilterQueryStr(define, filterWord, queryStr);
        //        }
        //        tb = OraQueryModelHelper.FillResultData(queryStr, "ResultTable", ref count, oracleConnection);
        //        if (requestUser != null && requestUser.BaseInfo != null)
        //        {
        //            OralceLogWriter.WriteQueryLog(BuildQueryLogStr(guideLineId, param, requestUser), Environment.TickCount - getQueryStartTime, count.ToString(), requestUser.BaseInfo.UserId, "2");
        //        }
        //    }
        //    return tb;
        //}
        //#endregion

        ///// <summary>
        ///// 取指标结果集
        ///// </summary>
        ///// <param name="guideLineId"></param>
        ///// <param name="param"></param>
        ///// <param name="filterWord"></param>
        ///// <param name="requestUser"></param>
        ///// <returns></returns>
        //public static DataTable QueryGuideline(string guideLineId, Dictionary<string, object> param, string filterWord, SinoRequestUser requestUser)
        //{
        //    int getQueryStartTime = Environment.TickCount;
        //    int count = 0;
        //    DataTable tb = new DataTable("ResultTable");
        //    MD_GuideLine define = GetGuidelineDefine(guideLineId);
        //    if (define != null)
        //    {
        //        string queryStr = GetGuidelineMethod(guideLineId);

        //        List<MDQuery_GuideLineParameter> glPara = new List<MDQuery_GuideLineParameter>();
        //        if (param != null && define.Parameters != null)
        //        {
        //            foreach (var p in param)
        //            {
        //                MD_GuideLineParameter md_pa = define.Parameters.Find(pa => pa.ParameterName == p.Key);
        //                if (md_pa != null)
        //                {
        //                    glPara.Add(new MDQuery_GuideLineParameter(md_pa, p.Value));
        //                }
        //            }
        //        }
        //        foreach (MDQuery_GuideLineParameter gp in glPara)
        //        {
        //            queryStr = OraQueryBuilder.RebuildGuideLineQueryString(queryStr, gp);
        //        }
        //        if (requestUser != null)
        //        {
        //            queryStr = OraQueryBuilder.ReplaceExtSecret(null, queryStr, requestUser);
        //        }
        //        if (!string.IsNullOrEmpty(filterWord))
        //        {
        //            queryStr = OraQueryBuilder.GetFilterQueryStr(define, filterWord, queryStr);
        //        }
        //        tb = OraQueryModelHelper.FillResultData(queryStr, "ResultTable", ref count);
        //        if (requestUser != null && requestUser.BaseInfo != null)
        //        {
        //            OralceLogWriter.WriteQueryLog(BuildQueryLogStr(guideLineId, param, requestUser), Environment.TickCount - getQueryStartTime, count.ToString(), requestUser.BaseInfo.UserId, "2");
        //        }
        //    }
        //    return tb;
        //}

        ///// <summary>
        ///// 分页取指标结果集
        ///// </summary>
        ///// <param name="guideLineId"></param>
        ///// <param name="param"></param>
        ///// <param name="filterWord"></param>
        ///// <param name="pageIndex"></param>
        ///// <param name="pageSize"></param>
        ///// <param name="sortBy"></param>
        ///// <param name="sortDirection"></param>
        ///// <param name="requestUser"></param>
        ///// <param name="recordCount"></param>
        ///// <returns></returns>
        //public static DataTable QueryGuideline(string guideLineId, Dictionary<string, object> param, string filterWord, decimal pageIndex, decimal pageSize, string sortBy, string sortDirection, SinoRequestUser requestUser, ref int recordCount)
        //{
        //    int getQueryStartTime = Environment.TickCount;
        //    int count = 0;
        //    DataTable tb = new DataTable("ResultTable");
        //    MD_GuideLine define = GetGuidelineDefine(guideLineId);
        //    if (define != null)
        //    {
        //        string queryStr = GetGuidelineMethod(guideLineId);

        //        List<MDQuery_GuideLineParameter> glPara = new List<MDQuery_GuideLineParameter>();
        //        if (param != null && define.Parameters != null)
        //        {
        //            foreach (var p in param)
        //            {
        //                MD_GuideLineParameter md_pa = define.Parameters.Find(pa => pa.ParameterName == p.Key);
        //                if (md_pa != null)
        //                {
        //                    glPara.Add(new MDQuery_GuideLineParameter(md_pa, p.Value));
        //                }
        //            }
        //        }
        //        foreach (MDQuery_GuideLineParameter gp in glPara)
        //        {
        //            queryStr = OraQueryBuilder.RebuildGuideLineQueryString(queryStr, gp);
        //        }
        //        if (requestUser != null)
        //        {
        //            queryStr = OraQueryBuilder.ReplaceExtSecret(null, queryStr, requestUser);
        //        }
        //        if (!string.IsNullOrEmpty(filterWord))
        //        {
        //            queryStr = OraQueryBuilder.GetFilterQueryStr(define, filterWord, queryStr);
        //        }

        //        try
        //        {
        //            recordCount = Convert.ToInt32(OracleHelper.ExecuteScalar(OracleHelper.ConnectionStringProfile, CommandType.Text, string.Format("select count(*) from (\n {0} \n) ", queryStr)));
        //        }
        //        catch (Exception e)
        //        {
        //            OralceLogWriter.WriteSystemLog("Exception :QueryGuideline310行异常，异常信息为" + e.Message, "ERROR");
        //        }

        //        queryStr = OraQueryBuilder.BuildPagingSQL(queryStr, pageIndex, pageSize, sortBy, sortDirection);
        //        tb = OraQueryModelHelper.FillResultData(queryStr, "ResultTable", ref count);
        //        if (requestUser != null && requestUser.BaseInfo != null)
        //        {
        //            OralceLogWriter.WriteQueryLog(BuildQueryLogStr(guideLineId, param, requestUser), Environment.TickCount - getQueryStartTime, count.ToString(), requestUser.BaseInfo.UserId, "2");
        //        }
        //    }
        //    return tb;
        //}
        //#endregion

        ///// <summary>
        ///// 取所有主键的值
        ///// </summary>
        ///// <param name="guideLineId"></param>
        ///// <param name="param"></param>
        ///// <param name="keyField"></param>
        ///// <param name="requestUser"></param>
        ///// <returns></returns>
        //public static string GetAllKeyField(string guideLineId, Dictionary<string, object> param, string keyField, SinoRequestUser requestUser)
        //{
        //    int count = 0;
        //    DataTable tb = new DataTable("ResultTable");
        //    MD_GuideLine define = GetGuidelineDefine(guideLineId);
        //    if (define != null)
        //    {
        //        string queryStr = GetGuidelineMethod(guideLineId);

        //        List<MDQuery_GuideLineParameter> glPara = new List<MDQuery_GuideLineParameter>();
        //        if (param != null && define.Parameters != null)
        //        {
        //            foreach (var p in param)
        //            {
        //                MD_GuideLineParameter md_pa = define.Parameters.Find(pa => pa.ParameterName == p.Key);
        //                if (md_pa != null)
        //                {
        //                    glPara.Add(new MDQuery_GuideLineParameter(md_pa, p.Value));
        //                }
        //            }
        //        }
        //        foreach (MDQuery_GuideLineParameter gp in glPara)
        //        {
        //            queryStr = OraQueryBuilder.RebuildGuideLineQueryString(queryStr, gp);
        //        }
        //        if (requestUser != null)
        //        {
        //            queryStr = OraQueryBuilder.ReplaceExtSecret(null, queryStr, requestUser);
        //        }
        //        tb = OraQueryModelHelper.FillResultData(queryStr, "ResultTable", ref count);

        //        try
        //        {
        //            tb.PrimaryKey = new DataColumn[] { tb.Columns[keyField] };
        //        }
        //        catch
        //        {
        //            string errMsg = string.Format("取指标[zbid={0}]的结果集中所有主键[KeyField={1}]的值时设置主键出错，请检查主键是否唯一！");
        //            OralceLogWriter.WriteSystemLog(errMsg, "ERROR");
        //            return "";
        //        }
        //    }

        //    List<string> listKeyField = new List<string>();
        //    foreach (DataRow row in tb.Rows)
        //    {
        //        listKeyField.Add(row[keyField].ToString());
        //    }
        //    return string.Join(",", listKeyField);
        //}

        //private const string SqlGetGuideLineMethod = @"select ZBSF  from TJ_ZDYZBDYB where ID=:Id";
        ///// <summary>
        ///// 取指标的sql语句
        ///// </summary>
        ///// <param name="guideLineId"></param>
        ///// <returns></returns>
        //public static string GetGuidelineMethod(string guideLineId)
        //{
        //    OracleParameter[] param = { new OracleParameter(":Id", OracleDbType.Decimal) };
        //    try
        //    {
        //        param[0].Value = decimal.Parse(guideLineId);

        //        object sfobj = OracleHelper.ExecuteScalar(OracleHelper.ConnectionStringProfile, CommandType.Text, SqlGetGuideLineMethod, param);
        //        if (sfobj == null || sfobj == DBNull.Value) return "";
        //        return sfobj.ToString();
        //    }
        //    catch (Exception e)
        //    {
        //        string errorMessage = string.Format("取指标[{0}]的sql语句出错,错误信息为{1}", guideLineId, e.Message);
        //        OralceLogWriter.WriteSystemLog(errorMessage, "ERROR");
        //        return "";
        //    }
        //}

        ///// <summary>
        ///// 组织指标查询日志语句
        ///// </summary>
        ///// <param name="zbid"></param>
        ///// <param name="param"></param>
        ///// <param name="requestUser"></param>
        ///// <returns></returns>
        //private static string BuildQueryLogStr(string zbid, Dictionary<string, object> param, SinoRequestUser requestUser)
        //{
        //    string paraStr = "";
        //    if (param != null)
        //    {
        //        foreach (var p in param)
        //        {
        //            paraStr += p.Key + ":" + p.Value + ";";
        //        }
        //    }
        //    return string.Format("用户{0}查看了 {1}（指标id={2}，参数={3}）", requestUser.BaseInfo.UserName, GetGuidelineDefine(zbid).GuideLineName, zbid, paraStr);
        //}
    }
}
