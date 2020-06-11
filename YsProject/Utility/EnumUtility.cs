
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using YsProject.Consts;
using YsProject.Models;

namespace YsProject.Utility
{
    /// <summary>
    /// EnumUtil
    /// </summary>
    public static class EnumUtil
    {
        /// <summary>
        /// 获取消息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        //public static string GetMessage(this MessageCode code, params string[] para)
        //    => para.Length > 0 ? string.Format(code.GetDescription(), para) : code.GetDescription();

        /// <summary>
        /// EnumのDescriptionを取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="code"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public static List<EnumItem> GetList(this Type enumType,bool flgRemoveAll = false)
        {
            List<EnumItem> result = new List<EnumItem>();

            foreach (var e in Enum.GetValues(enumType))
            {
                EnumItem item = new EnumItem();

                item.Index = (int)e;
                if (flgRemoveAll && item.Index == 0) continue;

                // Description
                object[] objArr = e.GetType().GetField(e.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (objArr != null && objArr.Length > 0)
                {
                    DescriptionAttribute da = objArr[0] as DescriptionAttribute;
                    item.Description = da.Description;
                }

                // DBValue
                object[] dbValueArr = e.GetType().GetField(e.ToString()).GetCustomAttributes(typeof(ValueAttribute), true);
                if (dbValueArr != null && dbValueArr.Length > 0)
                {
                    ValueAttribute da = dbValueArr[0] as ValueAttribute;
                    item.Value = da.Value;
                }

                result.Add(item);
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="code"></param>
        /// <returns></returns>
        public static EnumItem GetItem<T>(this T code) where T : Enum
            => new EnumItem()
            {
                Index = Convert.ToInt32(code),
                Description = code.GetDescription(),
                Value = code.GetValue(),
            };

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetValue<T>(this T code) where T : Enum
            => code.GetAttribute<T, ValueAttribute>();

        /// <summary>
        /// Value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="code"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public static string GetAttribute<T, M>(this T code) where T : Enum where M : BaseAttribute
        {
            object[] titleValueArr = code.GetType().GetField(code.ToString()).GetCustomAttributes(typeof(M), true);
            if (titleValueArr != null && titleValueArr.Length > 0)
            {
                M da = titleValueArr[0] as M;
                return da.Value;
            }

            return null;
        }

        /// <summary>
        /// Description
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="code"></param>
        /// <param name="para"></param>
        /// <returns></returns>
        public static string GetDescription<T>(this T code) where T : Enum
        {
            Type type = code.GetType();
            MemberInfo[] memberInfos = type.GetMember(code.ToString());
            if (memberInfos != null && memberInfos.Length > 0)
            {
                DescriptionAttribute[] attrs = memberInfos[0].GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
                if (attrs != null && attrs.Length > 0)
                {
                    return attrs[0].Description;
                }
            }
            return code.ToString();
        }
    }
}