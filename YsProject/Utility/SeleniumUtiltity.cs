using Microsoft.VisualBasic;
using OfficeOpenXml;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using YsProject.Consts;
using YsProject.Models;
using YsProject.Utils;

namespace YsProject.Utility
{
    public class SeleItem
    {
        public EnumSelenium Id { get; set; }
        public string Value { get; set; }
    }

    /// <summary>
    /// 作业区分
    /// </summary>
    public enum EnumSelenium
    {
        [Value(""), Description("")]
        ALL,
        [Value("id"), Description("元素查找ID")]
        ID,
        [Value("name"), Description("")]
        NAME,
        [Value("tag"), Description("")]
        TAG,
        [Value("css"), Description("")]
        CSS,
        [Value("xpath"), Description("")]
        XPATH,
        [Value("pic"), Description("截图")]
        PIC,
        [Value("click"), Description("单击")]
        CLICK,
        [Value("clear"), Description("清楚")]
        CLEAR,
        [Value("lostfocuse"), Description("失焦")]
        LOSTFOCUSE,
        [Value("key"), Description("按键")]
        KEY,
        [Value("combo"), Description("下拉框")]
        COMBO,
        [Value("table"), Description("表")]
        TABLE,
        [Value("back"), Description("回退")]
        BACK,
        [Value("forward"), Description("前进")]
        FORWARD,
        [Value("refresh"), Description("刷新")]
        REFRESH,
        [Value("winsize"), Description("窗口尺寸")]
        WINSIZE,
        [Value("winpoint"), Description("窗口坐标")]
        WINPOINT,
        [Value("fullscreen"), Description("全屏")]
        FULLSCREEN,
        [Value("maxisize"), Description("最大化")]
        MAXSIZE,
        [Value("minsize"), Description("最小化")]
        MINSIZE,
    }

    public class ChromeUtility : IDisposable
    {
        private IWebDriver _driver;

        public bool EndClose { get; set; } = true;

        public string Url { get; set; }

        public WebDriverWait Wait { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="url"></param>
        public ChromeUtility(string url)
        {
            // Options
            ChromeOptions options = new ChromeOptions();
            options.AddExcludedArgument("enable-automation");
            options.AddArguments("--test-type", "--ignore-certificate-errors");
            options.AddAdditionalCapability("useAutomationExtension", false);
            _driver = new ChromeDriver(options);

            // Wait
            Wait = new WebDriverWait(_driver, timeout: TimeSpan.FromSeconds(30))
            {
                PollingInterval = TimeSpan.FromSeconds(5),
            };
            Wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            // GotoUrl
            Url = url;
            _driver.Navigate().GoToUrl(url);
        }

        public void DoEvent(SeleniumEventItem item, List<SeleniumControlItem> ControlList)
        {
            IWebElement e = null;
            if(!string.IsNullOrWhiteSpace(item.No))
            {
                string id = ControlList.Where(x => x.No == item.No).Select(x => x.Id).FirstOrDefault();
                if (string.IsNullOrWhiteSpace(id))
                    App.ErrorList.Add("err er 1");
                else
                { 
                    e = Wait.GetSeleItemControl(id);
                    if(e == null) App.ErrorList.Add($"{id}not find err er 2");
                }
            }

            SeleItem eve = SeleniumUtiltity.GetSeleItem(item.Event);
            switch (eve.Id)
            {
                case EnumSelenium.ALL:
                    e.SendKeys(eve.Value);
                    break;
                case EnumSelenium.BACK:
                    break;
                case EnumSelenium.CLEAR:
                    e.Clear();
                    break;
                case EnumSelenium.CLICK:
                    e.Click();
                    break;
                case EnumSelenium.COMBO:
                    break;
                case EnumSelenium.FORWARD:
                    break;
                case EnumSelenium.FULLSCREEN:
                    break;
                case EnumSelenium.KEY:
                    break;
                case EnumSelenium.LOSTFOCUSE:
                    break;
                case EnumSelenium.MAXSIZE:
                    break;
                case EnumSelenium.MINSIZE:
                    break;
                case EnumSelenium.PIC:
                    break;
                case EnumSelenium.REFRESH:
                    break;
                case EnumSelenium.TABLE:
                    break;
                default:
                    break;
            }
        }

        public void Dispose()
        {
            if (EndClose && _driver != null) _driver.Quit();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class SeleniumUtiltity
    {
        public static IWebElement GetSeleItemControl(this WebDriverWait wait, string idStr)
        {
            SeleItem ctrl = string.IsNullOrWhiteSpace(idStr) ? null : GetSeleItem(idStr);
            By by = null;
            switch (ctrl.Id)
            {
                case EnumSelenium.ID:
                    by = By.Id(ctrl.Value);
                    break;
                case EnumSelenium.NAME:
                    by = By.Name(ctrl.Value);
                    break;
                case EnumSelenium.TAG:
                    by = By.TagName(ctrl.Value);
                    break;
                case EnumSelenium.CSS:
                    by = By.CssSelector(ctrl.Value);
                    break;
                case EnumSelenium.XPATH:
                    by = By.XPath(ctrl.Value);
                    break;
                default:
                    App.ErrorList.Add("");
                    return null;
            }

            return wait.Until(drv => drv.FindElement(by));
        }

        public static SeleItem GetSeleItem(string str)
        {
            SeleItem result = new SeleItem();

            if (str.Substring(0, 1) != "$")
                result.Value = str;
            else if (str.IndexOf(":") <= 0)
                result.Id = GetSeleniumType(str.Substring(1));
            else
            {
                result.Id = GetSeleniumType(str.Substring(1, str.IndexOf(":") - 1));
                result.Value = str.Substring(str.IndexOf(":") + 1);
            };

            return result;
        }

        /// <summary>
        /// $id转枚举
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static EnumSelenium GetSeleniumType(string str)
        {
            foreach (EnumItem item in typeof(EnumSelenium).GetValues())
            {
                if (item.Value == str) return (EnumSelenium)item.Index;
            }

            return EnumSelenium.ALL;
        }

        /// <summary>
        /// 读取Excel
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static SeleniumModel ReadSeleniumFile(string path)
        {
            SeleniumModel model = new SeleniumModel();

            // 文件读取
            using (ExcelUtility excel = new ExcelUtility(path))
            {
                // 获取Menu表
                ExcelWorksheet menu = excel.GetSheet("Menu");

                // 启动URL
                model.StartUrl = menu.Cells["C1"].Text;
                if (string.IsNullOrWhiteSpace(model.StartUrl))
                {
                    App.ErrorList.Add("error");
                }

                // 测试案列获取
                model.MenuList = new List<SeleniumMenuItem>();
                List<ExcelWorksheet> sheets = new List<ExcelWorksheet>();
                for (int i = 4; i <= menu.GetMaxRow(1); i++)
                {
                    // No空跳过
                    if (string.IsNullOrWhiteSpace(menu.Cells[i, 1].Text)) continue;

                    // 获取一条数据
                    SeleniumMenuItem item = new SeleniumMenuItem()
                    {
                        Case = menu.Cells[i, 2].Text,
                        View = menu.Cells[i, 3].Text,
                        ViewName = menu.Cells[i, 4].Text,
                        Event = menu.Cells[i, 5].Text,
                    };

                    // Case,View,Event为空就跳过
                    if (string.IsNullOrWhiteSpace(item.Case) || string.IsNullOrWhiteSpace(item.View) || string.IsNullOrWhiteSpace(item.Event)) continue;

                    // 判断View对应的Sheet是否存在
                    ExcelWorksheet sh = excel.GetSheet(item.View);
                    if (sh == null)
                    {
                        App.ErrorList.Add(EnumMessage.E01.GetMessage(item.View));
                        if (!sheets.Select(x => x.Name).ToList().Contains(sh.Name)) sheets.Add(sh);
                    }
                    else
                    {
                        // 添加
                        model.MenuList.Add(item);
                    }
                }

                // 画面数据获取
                model.ViewList = new Dictionary<string, SeleniumViewItem>();
                foreach (ExcelWorksheet sh in sheets)
                {
                    SeleniumViewItem viewItem = new SeleniumViewItem();

                    // 控件数据获取
                    viewItem.ControlList = new List<SeleniumControlItem>();
                    for (int i = 3; i < sh.GetMaxRow(1); i++)
                    {
                        SeleniumControlItem item = new SeleniumControlItem()
                        {
                            No = sh.Cells[i, 1].Text,
                            Id = sh.Cells[i, 2].Text,
                            Name = sh.Cells[i, 3].Text,
                        };

                        // No Id为空跳过
                        if (!string.IsNullOrWhiteSpace(item.No) && !string.IsNullOrWhiteSpace(item.Id))
                            viewItem.ControlList.Add(item);
                    }

                    // 获取事件数据
                    viewItem.EventList = new Dictionary<string, List<SeleniumEventItem>>();
                    for (int j = 6; j <= sh.GetMaxColumn(1); j += 2)
                    {
                        // 事件头ID为空跳过
                        if (string.IsNullOrWhiteSpace(sh.Cells[j, 1].Text)) continue;

                        // 事件数据获取
                        List<SeleniumEventItem> events = new List<SeleniumEventItem>();
                        for (int i = 3; i < sh.GetMaxRow(j); i++)
                        {
                            SeleniumEventItem item = new SeleniumEventItem()
                            {
                                No = sh.Cells[i, j].Text,
                                Event = sh.Cells[i, j + 1].Text,
                            };

                            // Id Event 为空跳过
                            if (!string.IsNullOrWhiteSpace(item.No) && !string.IsNullOrWhiteSpace(item.Event))
                                events.Add(item);
                        }
                        viewItem.EventList.Add(sh.Cells[j, 1].Text, events);
                    }

                    // 添加画面数据
                    model.ViewList.Add(sh.Name, viewItem);
                }
            }

            return model;
        }

        /// <summary>
        /// ASCII
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static void Save(ChromeDriver driver, string path, ScreenshotImageFormat type, bool isFull = false)
        {
            if (isFull)
            {
                var filePath = path;

                Dictionary<string, Object> metrics = new Dictionary<string, Object>();
                metrics["width"] = driver.ExecuteScript("return Math.max(window.innerWidth,document.body.scrollWidth,document.documentElement.scrollWidth)");
                metrics["height"] = driver.ExecuteScript("return Math.max(window.innerHeight,document.body.scrollHeight,document.documentElement.scrollHeight)");
                //返回当前显示设备的物理像素分辨率与 CSS 像素分辨率的比率
                metrics["deviceScaleFactor"] = driver.ExecuteScript("return window.devicePixelRatio");
                metrics["mobile"] = driver.ExecuteScript("return typeof window.orientation !== 'undefined'");
                driver.ExecuteChromeCommand("Emulation.setDeviceMetricsOverride", metrics);

                driver.GetScreenshot().SaveAsFile(filePath, ScreenshotImageFormat.Png);
            }
            else
            {
                Screenshot shot = driver.TakeScreenshot();
                shot.SaveAsFile(path, type);
            }
        }


    }

    public class ChromeDriverEx : ChromeDriver
    {
        private const string SendChromeCommandWithResult = "sendChromeCommandWithResponse";
        private const string SendChromeCommandWithResultUrlTemplate = "/session/{sessionId}/chromium/send_command_and_get_result";

        public ChromeDriverEx(string chromeDriverDirectory, ChromeOptions options)
            : base(chromeDriverDirectory, options)
        {
            CommandInfo commandInfoToAdd = new CommandInfo(CommandInfo.PostCommand, SendChromeCommandWithResultUrlTemplate);
            this.CommandExecutor.CommandInfoRepository.TryAddCommand(SendChromeCommandWithResult, commandInfoToAdd);
        }

        public ChromeDriverEx(ChromeDriverService service, ChromeOptions options)
            : base(service, options)
        {
            CommandInfo commandInfoToAdd = new CommandInfo(CommandInfo.PostCommand, SendChromeCommandWithResultUrlTemplate);
            this.CommandExecutor.CommandInfoRepository.TryAddCommand(SendChromeCommandWithResult, commandInfoToAdd);
        }

        public Screenshot GetFullPageScreenshot()
        {

            string metricsScript = @"({
width: Math.max(window.innerWidth,document.body.scrollWidth,document.documentElement.scrollWidth)|0,
height: Math.max(window.innerHeight,document.body.scrollHeight,document.documentElement.scrollHeight)|0,
deviceScaleFactor: window.devicePixelRatio || 1,
mobile: typeof window.orientation !== 'undefined'
})";
            Dictionary<string, object> metrics = this.EvaluateDevToolsScript(metricsScript);
            this.ExecuteChromeCommand("Emulation.setDeviceMetricsOverride", metrics);

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["format"] = "png";
            parameters["fromSurface"] = true;
            object screenshotObject = this.ExecuteChromeCommandWithResult("Page.captureScreenshot", parameters);
            Dictionary<string, object> screenshotResult = screenshotObject as Dictionary<string, object>;
            string screenshotData = screenshotResult["data"] as string;

            this.ExecuteChromeCommand("Emulation.clearDeviceMetricsOverride", new Dictionary<string, object>());

            Screenshot screenshot = new Screenshot(screenshotData);
            return screenshot;
        }

        public object ExecuteChromeCommandWithResult(string commandName, Dictionary<string, object> commandParameters)
        {
            if (commandName == null)
            {
                throw new ArgumentNullException("commandName", "commandName must not be null");
            }

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["cmd"] = commandName;
            parameters["params"] = commandParameters;
            Response response = this.Execute(SendChromeCommandWithResult, parameters);
            return response.Value;
        }

        private Dictionary<string, object> EvaluateDevToolsScript(string scriptToEvaluate)
        {

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["returnByValue"] = true;
            parameters["expression"] = scriptToEvaluate;
            object evaluateResultObject = this.ExecuteChromeCommandWithResult("Runtime.evaluate", parameters);
            Dictionary<string, object> evaluateResultDictionary = evaluateResultObject as Dictionary<string, object>;
            Dictionary<string, object> evaluateResult = evaluateResultDictionary["result"] as Dictionary<string, object>;


            Dictionary<string, object> evaluateValue = evaluateResult["value"] as Dictionary<string, object>;
            return evaluateValue;
        }
    }
}