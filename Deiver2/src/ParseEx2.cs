using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deiver2
{
    public partial class Parse
    {
        public void InitJ13(bool IsAddData = true)
        {
            try
            {
                var webElementsNews = Connect3();
                if (webElementsNews == null || webElementsNews.Count == 0)
                {
                    return;
                }

                ListJ3 listJ3 = new ListJ3();

                foreach (var item in webElementsNews)
                {
                    if (!item.Displayed)
                        continue;
                    if (item.GetAttribute("class") == null)
                        continue;
                    if (!item.GetAttribute("class").ToString().ToLower().Trim().Equals("feed_row"))
                        continue;
                    string TextNews = item.Text;


                    //var el1 = item.FindElement(By.ClassName("feed_row_unshown"));

                    IWebElement temp = null;
                    temp = item.FindElement(By.TagName("div"));

                    //var tempImg = item.FindElements(By.TagName("a"));

                    
                    string id = "";
                    string copyright = "";

                    try
                    {
                        var clA = item.FindElement(By.ClassName("page_post_thumb_wrap")).GetAttribute("style");
                        var ClB = item.FindElement(By.ClassName("page_post_thumb_wrap"));
                        var id2 = ClB?.ToString()?.Replace("(", "").Replace(")", "").Split("=");
                        var s = item.FindElement(By.ClassName("page_post_thumb_wrap")).GetAttribute("href");
                        var sys = s?.ToString()?.Replace("(", "").Replace(")", "").Split("=");

                        if (id2 != null && id2.Length > 1)
                            id = id2[1].Trim();
                       

                        if (sys != null && sys.Length > 1)
                            copyright = sys[1].Trim();

                    }
                    catch { }

                    var arr = ParseStr(temp.Text);

                    var bytes = Encoding.UTF8.GetBytes(temp.Text);

                    var str = new string(bytes.Select(b => (char)b).ToArray());



                    J3 j3 = new J3()
                    {
                        SH = copyright,
                        idnew = id,

                    };
                    listJ3.Data.Add(j3);
                   

                    if (IsAddData)
                    {
                        Serialize2<ListJ3, J3>.AddData(listJ3, "J3.json");
                    }
                    else
                    {
                        Serialize2<ListJ3, J3>.Write(listJ3, "J3.json");
                    }
                    ActionGetData?.Invoke(IsAddData);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }

        }

        private List<IWebElement> Connect3()
        {
            if (chromeOptions == null)
            {
                chromeOptions = new ChromeOptions();
                //chromeOptions.AddArgument(@"user-data-dir=C:\Users\Админ\AppData\Local\Google\Chrome\User Data");
                chromeOptions.AddArgument(@"user-data-dir=C:\Users\rcif7\AppData\Local\Google\Chrome\User Data");
                // C:\Users\haost\AppData\Local\Google\Chrome\User Data
                //chromeOptions.AddArgument(@"user-data-dir=C:\Users\haost\AppData\Local\Google\Chrome\User Data");
                chromeDriver = new ChromeDriver(chromeOptions);
                chromeDriver.Navigate().GoToUrl("https://vk.com/feed");
            }

            IWebElement parent = null;
            //List<IWebElement> webElements = chromeDriver.FindElementsById("feed_rows").ToList();
            List<IWebElement> webElements = chromeDriver.FindElements(By.Id("feed_rows")).ToList();

            foreach (var item in webElements)
            {
                if (!item.Displayed)
                    continue;
                parent = item;
                break;
            }
            if (parent == null)
                return null;
            return parent.FindElements(By.TagName("div")).ToList();
        }

    }
}
