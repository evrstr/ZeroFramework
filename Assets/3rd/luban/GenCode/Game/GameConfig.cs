//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;
using SimpleJSON;



namespace cfg.Game
{

public sealed partial class GameConfig :  Bright.Config.BeanBase 
{
    public GameConfig(JSONNode _json) 
    {
        { if(!_json["GeekServerIP"].IsString) { throw new SerializationException(); }  GeekServerIP = _json["GeekServerIP"]; }
        { if(!_json["GeekServerPort"].IsNumber) { throw new SerializationException(); }  GeekServerPort = _json["GeekServerPort"]; }
        PostInit();
    }

    public GameConfig(string GeekServerIP, int GeekServerPort ) 
    {
        this.GeekServerIP = GeekServerIP;
        this.GeekServerPort = GeekServerPort;
        PostInit();
    }

    public static GameConfig DeserializeGameConfig(JSONNode _json)
    {
        return new Game.GameConfig(_json);
    }

    public string GeekServerIP { get; private set; }
    public int GeekServerPort { get; private set; }

    public const int __ID__ = 1501714768;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, object> _tables)
    {
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
    }

    public override string ToString()
    {
        return "{ "
        + "GeekServerIP:" + GeekServerIP + ","
        + "GeekServerPort:" + GeekServerPort + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}
