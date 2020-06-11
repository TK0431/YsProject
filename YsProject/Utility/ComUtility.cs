using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Xml.Linq;
using YsProject.Consts;

namespace YsProject.Utility
{
    public static class ComUtility
    {
        public static IPAddress GetLocalIPV4()
        {
            string name = Dns.GetHostName();
            IPAddress[] ipadrlist = Dns.GetHostAddresses(name);
            foreach (IPAddress ipa in ipadrlist)
            {
                if (ipa.AddressFamily == AddressFamily.InterNetwork)
                    return ipa;
            }
            return null;
        }

        public static string GetMD5(string myString)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = System.Text.Encoding.UTF8.GetBytes(myString);//
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String = byte2String + targetData[i].ToString("x2");
            }

            return byte2String;
        }

        /// <summary>
        /// Net-KINDサーバーのIP取得
        /// </summary>
        /// <param name="code"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetRecordValue(string key)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            XElement ex = XElement.Load(AppDomain.CurrentDomain.BaseDirectory + @"\setting.xml");
            ex.Elements(key).ToList().ForEach(e => result.Add(e.Attribute("name").Value, e.Attribute("value").Value));

            return result;
        }

        /// <summary>
        /// Net-KINDサーバーのIP取得
        /// </summary>
        /// <param name="code"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static void SetRecordValue(string key, Dictionary<string, string> dic)
        {
            XElement ex = XElement.Load(AppDomain.CurrentDomain.BaseDirectory + @"\setting.xml");

            foreach (string k in dic.Keys)
            {
                XElement e = ex.Elements(key).Where(x => x.Attribute("name").Value == k).FirstOrDefault();
                if (e != null)
                {
                    e.SetAttributeValue("value", dic[k]);
                }
                else
                {
                    XElement node = new XElement(key);
                    node.Attribute("name").Value = k;
                    node.Attribute("value").Value = dic[k];
                    ex.Add(node);
                }
            }

            ex.Save(AppDomain.CurrentDomain.BaseDirectory + @"\setting.xml");
        }
    }

    public static class DataUtility
    {
        /// <summary>
        /// NULL判断
        /// </summary>
        /// <param name="value">判断値</param>
        /// <returns></returns>
        public static bool IsDBNull(object value) => value == DBNull.Value ? true : false;

        /// <summary>
        /// stringへの変更
        /// </summary>
        /// <param name="value">変更前値</param>
        /// <param name="failValue">変更失敗時の値</param>
        /// <returns></returns>
        public static string CStrDB(object value, string failValue = "") => (value == null || IsDBNull(value) || string.IsNullOrEmpty(value.ToString().Trim())) ? failValue : value.ToString().Trim();

        /// <summary>
        /// boolへの変更
        /// </summary>
        /// <param name="value">変更前値</param>
        /// <param name="failValue">変更失敗時の値</param>
        /// <returns></returns>
        public static bool CBoolDB(object value, bool failValue = false)
        {
            // 異常変更の場合
            if ((value == null || IsDBNull(value))) return failValue;

            // stringへの変更
            string result = value.ToString().Trim();
            return (result.ToUpper() == "FALSE" || result == "0" || result == string.Empty) ? false : true;
        }

        /// <summary>
        /// Decimalへの変更
        /// </summary>
        /// <param name="value">変更前値</param>
        /// <param name="failValue">変更失敗時の値</param>
        /// <returns></returns>
        public static Decimal CDecDB(object value, Decimal failValue = 0)
        {
            // 異常変更の場合
            if ((value == null || IsDBNull(value))) return failValue;

            // Decimalへの変更
            Decimal result;
            return Decimal.TryParse(value.ToString().Trim(), out result) ? result : failValue;
        }

        /// <summary>
        /// Intへの変更
        /// </summary>
        /// <param name="value">変更前値</param>
        /// <param name="failValue">変更失敗時の値</param>
        /// <returns></returns>
        public static int CIntDB(object value, int failValue = 0)
        {
            // 異常変更の場合
            if ((value == null || IsDBNull(value))) return failValue;

            // Decimalへの変更
            int result;
            return int.TryParse(value.ToString().Trim(), out result) ? result : failValue;
        }

        /// <summary>
        /// DateTimeへの変更
        /// </summary>
        /// <param name="value">変更前値</param>
        /// <param name="failValue">変更失敗時の値</param>
        /// <returns></returns>
        public static DateTime? CDateDB(object value, DateTime? failValue = null)
        {
            // 異常変更の場合
            if ((value == null || IsDBNull(value))) return failValue;

            // DateTimeへの変更
            DateTime? resultDate = value as DateTime?;
            return resultDate == null ? failValue : resultDate;
        }

        /// <summary>
        /// DateTimeからStringへの変更
        /// </summary>
        /// <param name="value">変更前値</param>
        /// <param name="type">変更タイプ</param>
        /// <param name="failValue">変更失敗時の値</param>
        /// <returns></returns>
        public static string CDateToStrDB(object value, string type = ComConst.DATA_YYYY_MM_DD, string failValue = "")
        {
            // 異常変更Falseへ戻る
            if ((value == null || IsDBNull(value))) return failValue;

            // DateTimeへの変更
            string result = value.ToString().Trim();
            DateTime resultDate;
            if (!DateTime.TryParse(result, out resultDate)) return failValue;

            // Stringへの変更
            return resultDate.ToString(type);
        }

        /// <summary>
        /// Intへの変更
        /// </summary>
        /// <param name="value">変更前値</param>
        /// <param name="failValue">変更失敗時の値</param>
        /// <returns></returns>
        public static string CIPDB(string value, string failValue = null)
        {
            // 異常変更の場合
            if ((value == null || IsDBNull(value))) return failValue;

            // Decimalへの変更
            return IPAddress.TryParse(value, out _) ? value : failValue;
        }
    }
}
