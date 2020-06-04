using System;
using System.Reflection;
using System.Xml.Linq;
using System.Linq;

namespace YsProject.Utility
{
    public static class CommonUtil
    {
        public static void Map<S, T>(this T target, S source)
        {
            Type targetType = target.GetType();
            Type sourceType = source.GetType();

            foreach (PropertyInfo p in sourceType.GetProperties())
            {

                PropertyInfo targetP = targetType.GetProperty(p.Name);

                if (targetP != null)
                {
                    targetP.SetValue(target, p.GetValue(source));
                }
            }
        }

        public static bool isNull(Object obj)
        {
            return (obj == null || obj.ToString() == null || "".Equals(obj.ToString())) ? true : false;
        }

        public static string getXml(string title,string code, string xmlName = null)
        {
            XElement ex = XElement.Load(AppDomain.CurrentDomain.BaseDirectory + @"\"+ (xmlName != null ? xmlName : "Kebin1Settings") + ".xml");

            string value = ex.Elements(title).Where(e => e.Attribute("Code").Value == code).FirstOrDefault().Attribute("Value").Value;

            return value;
        }

        public static string getItemMaxCount(string code)
        {
            

            return " limit " + getXml("itemMaxCount", code);
        }

    }


}