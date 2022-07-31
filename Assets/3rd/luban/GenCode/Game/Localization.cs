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

public sealed partial class Localization :  Bright.Config.BeanBase 
{
    public Localization(JSONNode _json) 
    {
        { if(!_json["key"].IsString) { throw new SerializationException(); }  Key = _json["key"]; }
        { if(!_json["text_cn"].IsString) { throw new SerializationException(); }  TextCn = _json["text_cn"]; }
        { if(!_json["text_en"].IsString) { throw new SerializationException(); }  TextEn = _json["text_en"]; }
        { if(!_json["text_jp"].IsString) { throw new SerializationException(); }  TextJp = _json["text_jp"]; }
        PostInit();
    }

    public Localization(string key, string text_cn, string text_en, string text_jp ) 
    {
        this.Key = key;
        this.TextCn = text_cn;
        this.TextEn = text_en;
        this.TextJp = text_jp;
        PostInit();
    }

    public static Localization DeserializeLocalization(JSONNode _json)
    {
        return new Game.Localization(_json);
    }

    /// <summary>
    /// key
    /// </summary>
    public string Key { get; private set; }
    /// <summary>
    /// 简体中文
    /// </summary>
    public string TextCn { get; private set; }
    /// <summary>
    /// English
    /// </summary>
    public string TextEn { get; private set; }
    /// <summary>
    /// 日本語
    /// </summary>
    public string TextJp { get; private set; }

    public const int __ID__ = -348212459;
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
        + "Key:" + Key + ","
        + "TextCn:" + TextCn + ","
        + "TextEn:" + TextEn + ","
        + "TextJp:" + TextJp + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}