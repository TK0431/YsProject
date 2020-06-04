using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Xml.Linq;

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
}
