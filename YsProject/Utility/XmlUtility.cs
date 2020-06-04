using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace YsProject.Utility
{
    public static class XmlUtility
    {
        /// <summary>
        /// メッセージ取得
        /// </summary>
        /// <returns></returns>
        public static List<XElement> GetXElements(string path, string group)
            => XElement.Load(path).Elements(group).ToList();

        public static List<XElement> GetXElements(XElement parent, string group)
            => parent.Elements(group).ToList();

        public static XElement GetXElement(string path, string group)
            => XElement.Load(path).Element(group);

        /// <summary>
        /// メッセージ取得
        /// </summary>
        /// <param name="code"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string GetMessage(string msgCode, params string[] para)
        {
            //return code.GetDescription(parameters);
            XElement ex = XElement.Load(AppDomain.CurrentDomain.BaseDirectory + @"\Message.xml");
            string value = ex.Elements("Message").Where(e => e.Attribute("Code").Value == msgCode).FirstOrDefault().Attribute("Value").Value;

            return para.Length > 0 ? string.Format(value, para) : value;
        }
    }
}
