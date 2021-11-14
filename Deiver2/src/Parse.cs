using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Deiver2
{
    public class Parse
    {

        private static ChromeOptions chromeOptions = null;
        private static ChromeDriver chromeDriver = null;
        public Action<bool> ActionGetData = null;

        public void Init(bool IsAddData = true)
        {
            try
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
                    return;
                List<IWebElement> webElementsNews = parent.FindElements(By.TagName("div")).ToList();




                AllNews allNews = new AllNews();
                Liststr liststr = new Liststr();
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

                    string strUrl = "";
                    string id = "";

                    try
                    {
                        var clA = item.FindElement(By.ClassName("page_post_thumb_wrap")).GetAttribute("style");

                        //string teststring = temp.GetAttribute("id").ToString();

                        var ClB = item.FindElement(By.ClassName("page_post_thumb_wrap"));

                        var id2 = ClB?.ToString()?.Replace("(", "").Replace(")","").Split("=");
                        if (id2 != null && id2.Length > 1)
                            id = id2[1].Trim();
                        var styleArr = clA.Split("url");

                        var res = GetLinks(styleArr[1]);
                        if (res != null && res.Count > 0)
                            strUrl = !string.IsNullOrEmpty(res[0]) ? res[0] : "";
                    }
                    catch { }

                    var arr = ParseStr(temp.Text);

                    var bytes = Encoding.UTF8.GetBytes(temp.Text);

                    var str = new string(bytes.Select(b => (char)b).ToArray());



                    News news = new News()
                    {
                        Header = arr[0],
                        Txt = arr[1],
                        UrlImg = strUrl

                    };
                    allNews.Data.Add(news);

                    // wall_post_text
                    // page_post_sized_thumbs  clear_fix

                    //var el = item.FindElement("wall_post_text");



                    J2 j2 = new J2()
                    {
                        idnew = id,
                        img=strUrl
                    };
                    liststr.Data.Add(j2);
                }

                //Serialize<AllNews> sr = new Serialize<AllNews>();

                //sr.Write(allNews, "news.json");
                if (IsAddData)
                {
                    Serialize2<AllNews, News>.AddData(allNews, "news.json");
                    Serialize2<Liststr, J2>.AddData(liststr, "J2");
                }
                else
                {
                    Serialize2<AllNews, News>.Write(allNews, "news.json");
                    Serialize2<Liststr, J2>.Write(liststr, "J2");
                }
                ActionGetData?.Invoke(IsAddData);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        public List<string> GetLinks(string message)
        {
            List<string> list = new List<string>();
            Regex urlRx = new Regex(@"((https?|ftp|file)\://|www.)[A-Za-z0-9\.\-]+(/[A-Za-z0-9\?\&\=;\+!'\(\)\*\-\._~%]*)*", RegexOptions.IgnoreCase);

            MatchCollection matches = urlRx.Matches(message);
            foreach (Match match in matches)
            {
                list.Add(match.Value);
            }
            return list;
        }

        private List<string> ParseStr(string str)
        {
            var arr = str.Split('\n');

            List<string> res = new List<string>();
            res.Add(arr[0].Trim());
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < arr.Length; ++i)
            {
                if (i > 0)
                {
                    if (arr[i].Contains("Оценили") || arr[i].Contains("Оценил")) break;
                    sb.Append(arr[i].Replace('\r', ' '));
                }
            }

            res.Add(sb.ToString());

            return res;
        }
    }
}
