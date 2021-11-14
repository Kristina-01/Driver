using Deiver2;
using System.Collections.Generic;

public class Rootobject
{
    public Class1[] Property1 { get; set; }
}

public class Class1
{
    public int ID { get; set; }
    public string Nmam { get; set; }
}

public class AllNews : IAddData<News>
{
    public List<News> Data { get; set; } = new List<News>();
}

public class News
{
    public string Header { get; set; }
    public string UrlImg { get; set; }
    public string Txt { get; set; }
}

public class J2
{
    public string idnew { get; set; }
    public string img { get; set; }
}

public interface IAddData<R> where R : class
{
    public List<R> Data { get; set; }
}

class liststr : IAddData<J2>
{
    public List<J2> Data { get; set; }
}