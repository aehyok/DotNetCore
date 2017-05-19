using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Base
{
    /// <summary>
    /// 权限枚举 added by lqm 20151011 暂时未使用
    /// </summary>
    [Flags]
    public enum AccessEnum : long
    {
        /// <summary>
        /// 无任何权限
        /// </summary>
        None = 0,
        #region 前台权限
        /// <summary>
        /// 是否允许访问
        /// </summary>
        U_Visit = 1,
        /// <summary>
        /// 是否允许发表应用
        /// </summary>
        U_PostApp = 2,
        /// <summary>
        /// 是否允许发表内容
        /// </summary>
        U_PostContent = 4,
        /// <summary>
        /// 是否允许上传相册
        /// </summary>
        U_PostAlbum = 8,
        /// <summary>
        /// 是否允许发表评论
        /// </summary>
        U_PostComment = 16,
        /// <summary>
        /// 是否允许发表问卷调查
        /// </summary>
        U_PostPoll = 64,
        /// <summary>
        /// 是否允许发起投票
        /// </summary>
        U_PostVote = 128,
        /// <summary>
        /// 是否允许参与投票
        /// </summary>
        U_ReplayVote = 256,
        /// <summary>
        /// 是否允许上传
        /// </summary>
        U_Upload = 512,
        /// <summary>
        /// 是否允许下载
        /// </summary>
        U_Download = 1024,
        /// <summary>
        /// 是否允许设置积分阅读权限
        /// </summary>
        U_SetReadperm = 2048,
        /// <summary>
        /// 是否允许设置积分附件查看权限
        /// </summary>
        U_SetAttachperm = 4096,
        /// <summary>
        /// 是否允许查看用户信息
        /// </summary>
        U_ViewUserList = 8192,
        /// <summary>
        /// 是否允许查看统计数据
        /// </summary>
        U_ViewStatis = 16384,
        /// <summary>
        /// 是否允许忽略验证码检测
        /// </summary>
        U_IgnoreVerify = 32768,
        /// <summary>
        /// 是否受到时间段限制
        /// </summary>
        U_TimesLimit = 65536,
        /// <summary>
        /// 发帖是否需要审核
        /// </summary>
        U_PostAudit = 131072,
        /// <summary>
        /// 发表内容是否需要审核
        /// </summary>
        U_MustContentAudit = 262144,
        /// <summary>
        /// 发表应用是否需要审核
        /// </summary>
        U_MustAppAudit = 524288,
        /// <summary>
        /// 发表评论是否需要审核
        /// </summary>
        U_MustCommentAudit = 1048576,
        #endregion

        #region 后台权限
        /// <summary>
        /// 是否允许删除用户组
        /// </summary>
        A_DelUserGroup = 137438953472,
        /// <summary>
        /// 是否允许添加用户组
        /// </summary>
        A_AddUserGroup = 274877906944,
        /// <summary>
        /// 是否允许修改用户组
        /// </summary>
        A_EditUserGroup = 549755813888,
        /// <summary>
        /// 是否允许修改内容
        /// </summary>
        A_EditContent = 1099511627776,
        /// <summary>
        /// 是否允许修改调查问卷
        /// </summary>
        A_EditPoll = 2199023255552,
        /// <summary>
        /// 是否允许修改投票
        /// </summary>
        A_EditVote = 4398046511104,
        /// <summary>
        /// 是否允许修改评论
        /// </summary>
        A_EditComment = 8796093022208,
        /// <summary>
        /// 是否允许修改帖子
        /// </summary>
        A_EditPost = 17592186044416,
        /// <summary>
        /// 是否允许修改回帖
        /// </summary>
        A_EditReply = 35184372088832,
        /// <summary>
        /// 是否允许修改用户
        /// </summary>
        A_EditUser = 70368744177664,
        /// <summary>
        /// 是否允许删除内容
        /// </summary>
        A_DelContent = 140737488355328,
        /// <summary>
        /// 是否允许删除问卷调查
        /// </summary>
        A_DelPoll = 281474976710656,
        /// <summary>
        /// 是否允许删除投票
        /// </summary>
        A_DelVote = 562949953421312,
        /// <summary>
        /// 是否允许删除评论
        /// </summary>
        A_DelComment = 1125899906842624,
        /// <summary>
        /// 是否允许删除帖子
        /// </summary>
        A_DelPost = 2251799813685248,
        /// <summary>
        /// 是否允许删除回帖
        /// </summary>
        A_DelReply = 4503599627370496,
        /// <summary>
        /// 是否允许删除用户
        /// </summary>
        A_DelUser = 9007199254740992,
        /// <summary>
        /// 是否允许禁用用户
        /// </summary>
        A_BanUser = 18014398509481984,
        /// <summary>
        /// 是否允许禁用ip
        /// </summary>
        A_BanIP = 36028797018963968,
        /// <summary>
        /// 是否允许审核帖子
        /// </summary>
        A_AuditPost = 72057594037927936,
        /// <summary>
        /// 是否允许审核内容
        /// </summary>
        A_AuditContent = 144115188075855872,
        /// <summary>
        /// 是否允许审核新用户
        /// </summary>
        A_AuditUser = 288230376151711744,
        /// <summary>
        /// 是否允许审核合作商申请
        /// </summary>
        A_AuditCoop = 576460752303423488,
        /// <summary>
        /// 是否允许接收举报信息
        /// </summary>
        A_Receivreport = 1152921504606846976,
        /// <summary>
        /// 是否允许查看系统运行日志
        /// </summary>
        A_ViewLog = 2305843009213693952,

        A_Other = 4611686018427387904,

        A_Max = 9223372036854775807

        #endregion
    }
}
