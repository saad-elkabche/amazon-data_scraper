using ConsoleApp1;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace test_selenium
{
    class Program
    {
        
        static async Task Main(string[] args)
        {
            excel ex = new excel(@$"{Environment.CurrentDirectory}\data\amazonData.xlsx");
            sqlite sql = new sqlite();
            var driver = new ChromeDriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            driver.Navigate().GoToUrl("https://www.amazon.com/");
            Thread.Sleep(30000);
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("twotabsearchtextbox")));
            var input = driver.FindElement(By.Id("twotabsearchtextbox"));
            input.Click();
            input.SendKeys("gaming keyboard"+Keys.Enter);
            List <product> products;
            for (int i = 1; i <= 5; i++)
            {
                products = await getProducts(driver);
                await Task.Run(() => ex.AddToDataSet(products, $"page-{i}"));
                await Task.Run(() => sql.addProducts(products));
                try
                {
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath($"//a[@ aria-label='Go to page {i + 1}']")));
                    var navigBtn = driver.FindElement(By.XPath($"//a[@ aria-label='Go to page {i + 1}']"));
                    navigBtn.Click();
                    //Thread.Sleep(2000);
                  wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("(//div [@class='a-section']//span [@class='a-size-medium a-color-base a-text-normal'])[position()=21]")));

                }
                catch (WebDriverTimeoutException exce)
                {
                    Console.WriteLine(exce.Message+" "+i);
                    Thread.Sleep(1500);
                }
                catch(Exception exc) { }
            }
            ex.saveToExcelFile();
        }

        static Task<List<product>> getProducts(ChromeDriver driver)
        {
            Regex rg = new Regex("\r\n");
            IWebElement element;
            List<product> listProd = new List<product>();
            string title, shiping, nbBuyer, link;
            float price;
            string Mainpath = "//div [@class='a-section']";
            string[] paths =
            {
                "//span [@class='a-size-medium a-color-base a-text-normal']",//title
                "//span [@class='a-price']",//price
                "//div [@class='a-row a-size-base a-color-secondary s-align-children-center']/span [@class='a-size-small a-color-base']",//shiping country
                "//a [@class='a-link-normal s-underline-text s-underline-link-text s-link-style']/span [@class='a-size-base s-underline-text']",//number of buyer
                "//span [@data-component-type='s-product-image' and @class='rush-component']//a [@class='a-link-normal s-no-outline']"   //linq
            };
            var items = driver.FindElements(By.XPath(Mainpath + paths[0]));
            int count = items.Count;

            listProd.Clear();
            for (int i = 1; i <= count; i++)
            {
                //title
                try
                {
                    element = driver.FindElement(By.XPath($"(//div [@class='a-section'])[position()={i}]" + paths[0]));
                    title = element.Text;
                }
                catch (Exception ex1)
                {
                    title = "None";
                }

                //price
                try
                {
                    element = driver.FindElement(By.XPath($"(//div [@class='a-section'])[position()={i}]" + paths[1]));
                    string str = rg.Replace(element.Text, ".");
                    price = float.Parse(str.Replace('$', ' ').Trim());
                }
                catch (Exception ex2)
                {
                    price = 0;
                }

                //shipingCountry
                try
                {
                    element = driver.FindElement(By.XPath($"(//div [@class='a-section'])[position()={i}]" + paths[2]));
                    shiping = element.Text;
                }
                catch (Exception ex3)
                {
                    shiping = "None";
                }
                //nbBuyer
                try
                {
                    element = driver.FindElement(By.XPath($"(//div [@class='a-section'])[position()={i}]" + paths[3]));
                    nbBuyer = element.Text;
                }
                catch (Exception ex4)
                {
                    nbBuyer = "None";
                }

                //link
                try
                {
                    element = driver.FindElement(By.XPath($"(//div [@class='a-section'])[position()={i}]" + paths[4]));
                    link = element.GetAttribute("href");
                }
                catch (Exception ex5)
                {
                    link = "None";
                }

                listProd.Add(new product(title, price, shiping, nbBuyer, link));
              
            }
            return Task.FromResult(listProd);

        }
    }
}
