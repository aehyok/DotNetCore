using aehyok.Dapper.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace aehyok.Dapper.GuideLine
{
    public class GuideLineHelper
    {
        /// <summary>
        /// 取指标参数定义
        /// </summary>
        /// <param name="_metaStr">元数据</param>
        /// <returns></returns>
        public static List<MD_GuideLineParameter> GetParametersFromMeta(string _metaStr)
        {
            List<MD_GuideLineParameter> _ret = new List<MD_GuideLineParameter>();
            RegexOptions options = RegexOptions.None;
            Regex regeMeta = new Regex(@"<CS>[^<]{1,}</CS>", options);

            MatchCollection _mc = regeMeta.Matches(_metaStr);
            foreach (Match _m in _mc)
            {
                string _s2 = _m.Value.Substring(4, _m.Length - 9);
                string[] _s3 = _s2.Split(':');
                if (_s3.Length > 2)
                {
                    string _pname = _s3[0];
                    string _title = _s3[1];
                    string _retTable = "";
                    string _selectAllCode = "";
                    bool _incldeChildren = false;
                    string _type = ParseParamType(_s3[2], ref _retTable, ref _incldeChildren, ref _selectAllCode);
                    int _order = (_s3.Length > 3) ? int.Parse((_s3[3] == "") ? "0" : _s3[3]) : 0;
                    int _inputWidth = (_s3.Length > 4) ? int.Parse((_s3[4] == "") ? "200" : _s3[4]) : 200;
                    MD_GuideLineParameter _mgp = new MD_GuideLineParameter(_pname, _title, _type, _order, _inputWidth, _retTable, _incldeChildren, _selectAllCode);
                    _ret.Add(_mgp);
                }

            }
            _ret.Sort((p1, p2) => { return p1.DisplayOrder.CompareTo(p2.DisplayOrder); });
            return _ret;
        }

        private static string ParseParamType(string _typeStr, ref string _retTable, ref bool _incldeChildren, ref string _selectAllCode)
        {
            if (_typeStr.Contains("代码表"))
            {
                int _index = _typeStr.IndexOf('[');
                string[] _retTableStrings = _typeStr.Substring(_index + 1, _typeStr.Length - _index - 2).Split(',');
                _retTable = _retTableStrings[0].ToUpper();
                _incldeChildren = (_retTableStrings.Length > 1) ? (_retTableStrings[1] == "1") : false;
                _selectAllCode = (_retTableStrings.Length > 2) ? _retTableStrings[2] : "";
                return "代码表";
            }
            else
            {
                return _typeStr;
            }
        }

        /// <summary>
        /// 取指标组的定义
        /// </summary>
        /// <param name="metaStr">元数据</param>
        /// <returns></returns>
        public static List<MD_GuideLineFieldGroup> GetFieldGroupsFromMeta(string metaStr)
        {
            List<MD_GuideLineFieldGroup> ret = new List<MD_GuideLineFieldGroup>();
            Dictionary<string, MD_GuideLineFieldGroup> dict = new Dictionary<string, MD_GuideLineFieldGroup>();

            RegexOptions options = RegexOptions.None;
            Regex regeMeta = new Regex(@"<FG>[^<]{1,}</FG>", options);

            MatchCollection mc = regeMeta.Matches(metaStr);
            foreach (Match m in mc)
            {
                string s2 = m.Value.Substring(4, m.Length - 9);
                string[] s3 = s2.Split(':');
                if (s3.Length > 1)
                {
                    string name = s3[0].ToUpper();
                    string title = s3[1];
                    int order = (s3.Length > 2) ? int.Parse((s3[2] == "") ? "0" : s3[2]) : 0;
                    string align = (s3.Length > 3) ? s3[3] : "LEFT";
                    bool hide = (s3.Length > 4) && (int.Parse(s3[4]) > 0);
                    string status = (s3.Length > 5) ? s3[5] : "SHOW";
                    MD_GuideLineFieldGroup glg = new MD_GuideLineFieldGroup(name, title, align, order, hide, status);
                    glg.Fields = new List<MD_GuideLineFieldName>();
                    dict.Add(name, glg);
                    ret.Add(glg);
                }
            }
            GetFieldNamesFromMeta(metaStr, dict, ret);
            if (ret.Count < 1)
            {
                MD_GuideLineFieldGroup glg = new MD_GuideLineFieldGroup("DEFAULT", "(默认组)", "CENTER", 1, false, "SHOW");
                glg.Fields = new List<MD_GuideLineFieldName>();
                ret.Add(glg);
                dict.Add("DEFAULT", glg);
            }
            foreach (MD_GuideLineFieldGroup group in ret)
            {
                group.Fields.Sort((g1, g2) => { return g1.DisplayOrder.CompareTo(g2.DisplayOrder); });
            }
            ret.Sort((g1, g2) => { return g1.DisplayOrder.CompareTo(g2.DisplayOrder); });
            return ret;
        }

        private static void GetFieldNamesFromMeta(string metaStr, Dictionary<string, MD_GuideLineFieldGroup> dict, List<MD_GuideLineFieldGroup> ret)
        {
            RegexOptions options = RegexOptions.None;
            Regex regeMeta = new Regex(@"<FN>[^<]{1,}</FN>", options);

            MatchCollection _mc = regeMeta.Matches(metaStr);
            foreach (Match _m in _mc)
            {
                string _s2 = _m.Value.Substring(4, _m.Length - 9);
                string[] _s3 = _s2.Split(':');
                if (_s3.Length > 1)
                {
                    string _fname = _s3[0].ToUpper();
                    string _orifname = _s3[0];
                    string _title = _s3[1];
                    int _order = (_s3.Length > 2) ? int.Parse((_s3[2] == "") ? "0" : _s3[2]) : 0;
                    int _width = (_s3.Length > 3) ? int.Parse((_s3[3] == "") ? "100" : _s3[3]) : 100;
                    string _center = (_s3.Length > 4) ? _s3[4] : "LEFT";
                    if (_center == "1") _center = "CENTER";
                    if (_center == "0") _center = "LEFT";
                    string _display = (_s3.Length > 5) ? _s3[5] : "";
                    string _group = (_s3.Length > 6) ? _s3[6] : "DEFAULT";
                    bool _canHide = (_s3.Length > 7) ? ((_s3[7] == "0") ? false : true) : false;
                    MD_GuideLineFieldName _gfn = new MD_GuideLineFieldName(_orifname, _fname, _title, _order, _width, _center, _display, _canHide);
                    if (dict.ContainsKey(_group))
                    {
                        MD_GuideLineFieldGroup _glg = dict[_group];
                        if (_glg.Fields == null) _glg.Fields = new List<MD_GuideLineFieldName>();
                        _glg.Fields.Add(_gfn);
                    }
                    else
                    {
                        MD_GuideLineFieldGroup _glg = new MD_GuideLineFieldGroup(_group, "(默认组)", "CENTER", 1, false, "SHOW");
                        ret.Add(_glg);
                        dict.Add(_group, _glg);
                        _glg.Fields = new List<MD_GuideLineFieldName>();
                        _glg.Fields.Add(_gfn);
                    }
                }
            }
        }

        /// <summary>
        /// 取指标链接定义
        /// </summary>
        /// <param name="_metaStr">元数据</param>
        /// <returns></returns>
        public static List<MD_GuideLineDetailDefine> GetDetaiDefinelFromMeta(string _metaStr)
        {
            List<MD_GuideLineDetailDefine> _ret = new List<MD_GuideLineDetailDefine>();
            RegexOptions options = RegexOptions.None;
            Regex regeMeta = new Regex(@"<MX>[^<]{1,}</MX>", options);

            MatchCollection _mc = regeMeta.Matches(_metaStr);
            foreach (Match _m in _mc)
            {
                string _s2 = _m.Value.Substring(4, _m.Length - 9);
                string[] _s3 = _s2.Split(':');
                if (_s3.Length > 3)
                {
                    string _fname = _s3[0].ToUpper();
                    string _type = _s3[1];
                    string _qid = _s3[2];
                    string _qcs = _s3[3];
                    string _links = (_s3.Length > 4) ? _s3[4] : "";
                    MD_GuideLineDetailDefine _gdd = new MD_GuideLineDetailDefine(_fname, _type, _qid, _qcs, _links);

                    _ret.Add(_gdd);
                }
            }
            return _ret;
        }
    }
}
