using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Deiver2
{
   
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           
            //J2 js2 = new J2();
            //js2.idnew = ;
            //liststr ls = new liststr();
            //ls.Data = new List<str>();
            //ls.Data.Add(s);
            //Serialize2<liststr, str>.AddData(ls, "str.json");



            //AllNews an = new AllNews();
            //an.News.Add(new News() { });

            //Serialize2<AllNews>.AddData(an, "news.json");


        }

        private void WriteData(object sender, RoutedEventArgs e)
        {
            IsEnabled = false;
            Task.Run(() =>
            {
                //CreateData();

                Parse pr = new Parse();
                pr.ActionGetData = GetData;
                pr.Init(false);
            });

            //System.Threading.Thread th = new System.Threading.Thread(CreateData(false));

            //Thread myThread = new Thread(new ThreadStart(() => 
            //{
            //    CreateData(false);
            //}));
            //myThread.Start(); // запускаем поток
        }

        private static void CreateData(bool IsAddData = true)
        {
            Parse pr = new Parse();
            //pr.ActionGetData = GetData;
            pr.Init(false);
        }

        private void GetData(bool obj)
        {
            Dispatcher.BeginInvoke(new Action(() => 
            {
                //if(obj)
                //{
                //    btn2.IsEnabled = true;
                //}
                //else
                //{
                //    btn1.IsEnabled = true;
                //}

                IsEnabled = true;

                MessageBox.Show("Данные получены!");
            }));
        }

        private void AddData(object sender, RoutedEventArgs e)
        {
            IsEnabled = false;
            Task.Run(() =>
            {
                Parse pr = new Parse();
                pr.ActionGetData = GetData;
                pr.Init();
            });
        }

        private void DeliteFile(object sender, RoutedEventArgs e)
        {
            List<string> vs= new List<string>() { "J1.json", "J2.json", "J3.json" };
           

            foreach(var el in vs)
            {
                if (File.Exists(el))
                    File.Delete(el);

            }
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(start));
            thread.Start();
        }

        private void start()
        {
            Parse p = new Parse();

            Thread th1 = new Thread(() => {
                p.InitJ1();
            });

            th1.Start();


        }
    }
}
