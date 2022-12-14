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

public sealed partial class UIPath :  Bright.Config.BeanBase 
{
    public UIPath(JSONNode _json) 
    {
        { if(!_json["Canvas"].IsString) { throw new SerializationException(); }  Canvas = _json["Canvas"]; }
        { if(!_json["UpdateInfoPanel"].IsString) { throw new SerializationException(); }  UpdateInfoPanel = _json["UpdateInfoPanel"]; }
        { if(!_json["LoginPanel"].IsString) { throw new SerializationException(); }  LoginPanel = _json["LoginPanel"]; }
        { if(!_json["DialogPanel"].IsString) { throw new SerializationException(); }  DialogPanel = _json["DialogPanel"]; }
        { if(!_json["LoadingIcon"].IsString) { throw new SerializationException(); }  LoadingIcon = _json["LoadingIcon"]; }
        PostInit();
    }

    public UIPath(string Canvas, string UpdateInfoPanel, string LoginPanel, string DialogPanel, string LoadingIcon ) 
    {
        this.Canvas = Canvas;
        this.UpdateInfoPanel = UpdateInfoPanel;
        this.LoginPanel = LoginPanel;
        this.DialogPanel = DialogPanel;
        this.LoadingIcon = LoadingIcon;
        PostInit();
    }

    public static UIPath DeserializeUIPath(JSONNode _json)
    {
        return new Game.UIPath(_json);
    }

    /// <summary>
    /// Canvas
    /// </summary>
    public string Canvas { get; private set; }
    /// <summary>
    /// 更新信息面板
    /// </summary>
    public string UpdateInfoPanel { get; private set; }
    /// <summary>
    /// 登陆面板
    /// </summary>
    public string LoginPanel { get; private set; }
    /// <summary>
    /// Dialog
    /// </summary>
    public string DialogPanel { get; private set; }
    /// <summary>
    /// 左下角loading图标
    /// </summary>
    public string LoadingIcon { get; private set; }

    public const int __ID__ = 248546869;
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
        + "Canvas:" + Canvas + ","
        + "UpdateInfoPanel:" + UpdateInfoPanel + ","
        + "LoginPanel:" + LoginPanel + ","
        + "DialogPanel:" + DialogPanel + ","
        + "LoadingIcon:" + LoadingIcon + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}
