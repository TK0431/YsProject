using log4net;
using System;

namespace YsProject.Utils
{
    public class LogUtility
    {
        /// <summary>
        /// 異常
        /// </summary>
        /// <param name="e"></param>
        public static void WriteError(object text, Exception e)
        {
            ILog log = LogManager.GetLogger("異常ログ");
            log.Error(text, e);
        }

        /// <summary>
        /// デバッグ
        /// </summary>
        /// <param name="text"></param>
        public static void WriteDebug(object text)
        {
            ILog log = LogManager.GetLogger("デバッグログ");
            log.Debug(text);
        }

        /// <summary>
        /// 输出程序运行信息
        /// </summary>
        /// <param name="text"></param>
        public static void WarnInfo(object text)
        {
            ILog log = LogManager.GetLogger("警告ログ");
            log.Warn(text);
        }

        /// <summary>
        /// 输出程序运行信息
        /// </summary>
        /// <param name="text"></param>
        public static void WriteInfo(object text)
        {
            ILog log = LogManager.GetLogger("正常ログ");
            log.Info(text);
        }
    }
}