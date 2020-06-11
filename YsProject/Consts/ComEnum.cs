using System;
using System.Collections.Generic;
using System.ComponentModel;
using YsProject.Models;
using YsProject.Models.DB;
using YsProject.Utility;
using YsProject.Utils;

namespace YsProject.Consts
{
    /// <summary>
    /// 画面
    /// </summary>
    public enum PageEnum
    {
        [Description("Main"), Value("Pages/UI000.xaml")]
        UI000,
        [Description("WBS"), Value("Pages/UI001.xaml")]
        UI001,
        [Description("User"), Value("Pages/UI002.xaml")]
        UI002,
        [Description("Group"), Value("Pages/UI003.xaml")]
        UI003,
        [Description("App Analyse"), Value("Pages/UI101.xaml")]
        UI101,
        [Description("App Test"), Value("Pages/UI102.xaml")]
        UI102,
        [Description("Web Analyse"), Value("Pages/UI201.xaml")]
        UI201,
        [Description("Web Test"), Value("Pages/UI202.xaml")]
        UI202,
        [Description("Model Help"), Value("Pages/UI902.xaml")]
        UI902,
        [Description("Code Add"), Value("Pages/UI903.xaml")]
        UI903,
    }

    /// <summary>
    /// 权限
    /// </summary>
    public enum EnumType
    {
        [Value("01"), Description("权限")]
        Type_01,
        [Value("02"), Description("开发语言")]
        Type_02,
    }

    /// <summary>
    /// 权限
    /// </summary>
    public enum EnumLevel
    {
        [Value("0"), Description("")]
        LEVEL_0,
        [Value("1"), Description("组员")]
        LEVEL_1,
        [Value("2"), Description("组长")]
        LEVEL_2,
        [Value("3"), Description("项目负责人")]
        LEVEL_3,
        [Value("4"), Description("项目经理")]
        LEVEL_4,
        [Value("9"), Description("超级")]
        LEVEL_9,
    }

    /// <summary>
    /// 开发语言
    /// </summary>
    public enum EnumDevType
    {
        [Value("00"), Description("")]
        ALL,
        [Value("01"), Description("基本设计")]
        BS,
        [Value("02"), Description("基本设计检证")]
        BS_RE,
        [Value("03"), Description("详细设计")]
        DL,
        [Value("04"), Description("详细设计检证")]
        DL_RE,
        [Value("05"), Description("单体制造")]
        CD,
        [Value("06"), Description("单体制造检证")]
        CD_RE,
        [Value("07"), Description("单体测试")]
        UT,
        [Value("08"), Description("单体测试检证")]
        UT_RE,
        [Value("09"), Description("结合测试")]
        IT,
        [Value("10"), Description("结合测试检证")]
        IT_RE,
        [Value("11"), Description("综合测试")]
        CT,
        [Value("12"), Description("综合测试检证")]
        CT_RE,
    }

    /// <summary>
    /// 开发语言
    /// </summary>
    public enum EnumDevLang
    {
        [Value("0"), Description("")]
        ALL,
        [Value("1"), Description("C#")]
        CSHUP,
        [Value("2"), Description("JAVA")]
        JAVA,
        [Value("3"), Description("Python")]
        PYTHON,
        [Value("4"), Description("C")]
        C,
        [Value("5"), Description("C++")]
        CPLUS,
    }

    /// <summary>
    /// 将枚举更新到数据库
    /// </summary>
    public static class UpdateEnum
    {
        public static void Update()
        {
            using (EntityDao db = new EntityDao())
            {
                db.DeleteAll("delete from tb_type");

                AddDatas(db, typeof(EnumLevel), EnumType.Type_01.GetValue());
            }
        }

        private static void AddDatas(EntityDao db,Type type, string typeValue)
        {
            List<EnumItem> list = type.GetList();
            List<TB_Type> datas = new List<TB_Type>();

            list.ForEach(x => datas.Add(new TB_Type() { 
                Type = typeValue,
                Value = x.Value,
                Name = x.Description,
                InserterCd = "CD0000",
                InserteTime = db.DbConectTime,
                UpdaterCd = "CD0000",
                UpdateTime = db.DbConectTime,
            }));

            db.Add(datas);
        }
    }
}
