using NLog;
using System;
using System.Collections.Generic;
using System.Text;
using ILogger = NLog.Logger;

namespace aehyok.NLog
{
    /// <summary>
    /// 日志处理NLog
    /// </summary>
    public class LogWriter
    {
        private ILogger Logger { get; set; }

        /// <summary>
        /// 初始化Logger
        /// </summary>
        public LogWriter()
        {
            Logger = LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// 调试时写入日志
        /// </summary>
        /// <param name="message"></param>
        public void Debug(string message)
        {
            Logger.Debug(message);
        }

        /// <summary>
        /// 调试时写入日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void Debug(string message, params object[] args)
        {
            Logger.Debug(message, args);
        }

        /// <summary>
        /// 正常记录日志
        /// </summary>
        /// <param name="message"></param>
        public void Info(string message)
        {
            Logger.Info(message);
        }

        /// <summary>
        /// 正常记录日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void Info(string message, params object[] args)
        {
            Logger.Info(message, args);
        }

        /// <summary>
        /// 写入错误日志
        /// </summary>
        /// <param name="message"></param>
        public void Error(string message)
        {
            Logger.Error(message);
        }

        /// <summary>
        /// 写入错误日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void Error(string message, params object[] args)
        {
            Logger.Error(message, args);
        }
    }
}
