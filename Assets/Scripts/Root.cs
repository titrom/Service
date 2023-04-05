using System;

[Serializable]
public class Root
{    
    public Segment[] segments;
}

[Serializable]
public class Segment
{
    public string arrival;
    public string departure;
    public From from;
    public To to;
}
[Serializable]

public class From
{
    public string type;
    public string title;
    public string short_title;
    public string popular_title;
    public string code;
    public string station_type;
    public string station_type_name;
    public string transport_type;
}
[Serializable]
public class To
{
    public string type;
    public string title;
    public string short_title;
    public string popular_title;
    public string code;
    public string station_type;
    public string station_type_name;
    public string transport_type;
}