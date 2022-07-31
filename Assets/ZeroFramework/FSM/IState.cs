namespace ZeroFramework
{
    public interface IState<T> where T : System.Enum
    {
        void OnEnter(T fromStateType); //进入状态时

        void OnUpdate();    //状态更新时

        //状态更新时
        void OnExit(T toStateType);  //退出状态时

        // void OnEnter(params object[] args); //进入状态时
        // void OnUpdate(params object[] args);    //状态更新时
        // void OnExit(params object[] args);  //退出状态时
    }
}