using OfficeOpenXml;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
            options.AddAdditionalCapability("useAutomationExtension", false);

            IWebDriver driver = new ChromeDriver(options);
            try
            {
                driver.Navigate().GoToUrl("https://www.baidu.com");
                IWebElement e1 = driver.FindElement(By.Name("wd"));
                IWebElement e2 = driver.FindElement(By.Id("su"));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
