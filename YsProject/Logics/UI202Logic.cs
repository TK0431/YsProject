using OfficeOpenXml;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using YsProject.Models.DB;
using YsProject.Utility;
using YsProject.Utils;
using YsProject.ViewModels;
using YsTool.ViewModels;

namespace YsProject.Logics
{
    public class UI202Logic : BaseLogic
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public void Test(UI202ViewModel model)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddExcludedArgument("enable-automation");
            options.AddArguments("--test-type", "--ignore-certificate-errors");
            options.AddAdditionalCapability("useAutomationExtension", false);

            IWebDriver driver = new ChromeDriver(options);
            try
            {
                driver.Navigate().GoToUrl("https://www.baidu.com");
                IWebElement e1 = driver.FindElement(By.Name("wd"));
                IWebElement e2 = driver.FindElement(By.Id("su"));
                //string url = driver.Url;
                //string title = driver.Title;
                //string handle = driver.CurrentWindowHandle;  -- ???
                //driver.Navigate().Back();
                //driver.Navigate().Forward();
                //driver.Navigate().Refresh();
                //System.Drawing.Size s = driver.Manage().Window.Size;
                //driver.Manage().Window.Size = new System.Drawing.Size(1024, 768);
                //System.Drawing.Point point = driver.Manage().Window.Position;
                //driver.Manage().Window.Position = new System.Drawing.Point(0, 0);
                //driver.Manage().Window.Maximize();
                //driver.Manage().Window.Minimize();


                //ChromeOptions options = new ChromeOptions();
                //options.AddExcludedArgument("enable-automation");
                //options.AddArguments("--test-type", "--ignore-certificate-errors");
                //options.AddAdditionalCapability("useAutomationExtension", false);

                //IWebDriver driver = new ChromeDriver(options);
                //driver.Manage().Window.Maximize();

                //List<string> pages = new List<string>();
                //try
                //{
                //    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30))
                //    {
                //        PollingInterval = TimeSpan.FromSeconds(3),
                //    };
                //    wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

                //    string url = "https://172.21.4.200/log_in";
                //    driver.Navigate().GoToUrl(url);
                //    IWebElement e1 = wait.Until(e => e.FindElement(By.Id("name")));
                //    e1.SendKeys("tomohiro.matsumura");

                //    pages.Add(driver.CurrentWindowHandle);

                //    IWebElement e2 = driver.FindElement(By.ClassName("functionButton"));
                //    e2.Click();

                //    IWebElement e3 = wait.Until(e => e.FindElement(By.ClassName("mainmenu")));
                //    e3.Click();

                //    IWebElement e4 = wait.Until(e => e.FindElement(By.ClassName("linkButton")));
                //    e4.Click();

                //    IWebElement e5 = wait.Until(e => e.FindElement(By.Name("limit_level")));

                //    SelectElement select = new SelectElement(e5);
                //    while (select == null || select.AllSelectedOptions.Count == 0)
                //    {
                //        Thread.Sleep(100);
                //        e5 = driver.FindElement(By.Name("limit_level"));
                //        select = new SelectElement(e5);
                //    }
                //    select.SelectByText("担当外秘");
                //    //select.SelectByIndex(2);

                //    IWebElement e6 = driver.FindElement(By.CssSelector("input.border.border-glay.form-control-sm.form-control.datePickerCustom"));
                //    e6.SendKeys("2019/01/03");
                }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                driver.Quit();
            }
        }



        public void ChangeWait(IWebDriver driver)
        {
            driver = new ChromeDriver();
            driver.Url = "https://www.google.com/ncr";
            driver.FindElement(By.Name("q")).SendKeys("cheese" + Keys.Enter);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement firstResult = wait.Until(e => e.FindElement(By.XPath("//a/h3")));

            Console.WriteLine(firstResult.Text);
        }

        public void ChangeWait2(IWebDriver driver)
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Url = "http://somedomain/url_that_delays_loading";
            IWebElement dynamicElement = driver.FindElement(By.Name("dynamicElement"));
        }

        public void ChangeWait3(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, timeout: TimeSpan.FromSeconds(30))
            {
                PollingInterval = TimeSpan.FromSeconds(5),
            };
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            var foo = wait.Until(drv => drv.FindElement(By.Id("foo")));
        }

        public void Alerts(IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(2000));

            //Click the link to activate the alert
            driver.FindElement(By.LinkText("See an example alert")).Click();

            //Wait for the alert to be displayed and store it in a variable
            //IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());
            IAlert alert = driver.SwitchTo().Alert();

            //Store the alert text in a variable
            string text = alert.Text;

            //Press the OK button
            alert.Accept();
        }

        /// <summary>
        /// 点击打开等待新窗口
        /// </summary>
        /// <param name="driver"></param>
        public void ChangeHandle(IWebDriver driver)
        {

            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(2000));

            // 存储原始窗口的 ID
            string originalWindow = driver.CurrentWindowHandle;

            // 检查一下，我们还没有打开其他的窗口
            if (driver.WindowHandles.Count == 1) return;

            // 单击在新窗口中打开的链接
            driver.FindElement(By.LinkText("new window")).Click();

            // 等待新窗口或标签页
            wait.Until(wd => wd.WindowHandles.Count == 2);

            // 循环执行，直到找到一个新的窗口句柄
            foreach (string window in driver.WindowHandles)
            {
                if (originalWindow != window)
                {
                    driver.SwitchTo().Window(window);
                    break;
                }
            }
            // 等待新标签页完成加载内容
            wait.Until(wd => wd.Title == "Selenium documentation");


            // Other
            //driver.Close();
            //driver.SwitchTo().Window("");
        }
    }
}
