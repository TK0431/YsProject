using Microsoft.VisualBasic;
using OfficeOpenXml;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using YsProject.Consts;
using YsProject.Models;
using YsProject.Utils;

namespace YsProject.Utility
{


    /// <summary>
    /// 作业区分
    /// </summary>
    public enum EnumSeleniumFind
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
    }

    /// <summary>
    /// 作业区分
    /// </summary>
    public enum EnumSeleniumEvent
    {
        [Value(""), Description("")]
        ALL,
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

    public class ChromeHelper : IDisposable
    {
        private IWebDriver _driver;

        public bool EndClose { get; set; } = true;

        public string Url { get; set; }

        public ChromeHelper()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddExcludedArgument("enable-automation");
            options.AddArguments("--test-type", "--ignore-certificate-errors");
            options.AddAdditionalCapability("useAutomationExtension", false);

            _driver = new ChromeDriver(options);
        }

        public void Dispose()
        {
            if (EndClose && _driver != null) _driver.Quit();
        }
    }

    public static class SeleniumUtiltity
    {
        public static SeleniumModel ReadSeleniumFile(string path)
        {
            SeleniumModel model = new SeleniumModel();

            using (ExcelUtility excel = new ExcelUtility(path))
            {
                ExcelWorksheet menu = excel.GetSheet("Menu");
                model.StartUrl = menu.Cells["C1"].Text;

                model.MenuList = new List<SeleniumMenuItem>();
                for (int i = 4; i <= excel.GetMaxRow(menu, 1); i++)
                {
                    if (string.IsNullOrWhiteSpace(menu.Cells[i, 1].Text)) continue;

                    SeleniumMenuItem item = new SeleniumMenuItem()
                    {
                        Case = menu.Cells[i, 2].Text,
                        View = menu.Cells[i, 3].Text,
                        ViewName = menu.Cells[i, 4].Text,
                        Event = menu.Cells[i, 5].Text,
                    };

                    if (string.IsNullOrWhiteSpace(item.Case) || string.IsNullOrWhiteSpace(item.View) || string.IsNullOrWhiteSpace(item.Event)) continue;

                    if (excel.GetSheet(item.View) == null)
                    {
                        App.ErrorList.Add(EnumMessage.E01.GetMessage(item.View));
                    }
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