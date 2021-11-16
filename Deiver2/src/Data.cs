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

public class AllNews : IAddData<J1>
{
    public List<J1> Data { get; set; } = new List<J1>();
}

public class J1
{
    //public string Header { get; set; }
   // public string UrlImg { get; set; }
    public string Txt { get; set; }

    public string idnew { get; set; }
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

class Liststr : IAddData<J2>
{
    public List<J2> Data { get; set; } = new List<J2>();
}

public class J3
{
    public string idnew { get; set; }

    public string SH { get; set; }
}

class ListJ3 : IAddData<J3>
{
    public List<J3> Data { get; set; } = new List<J3>();
}

