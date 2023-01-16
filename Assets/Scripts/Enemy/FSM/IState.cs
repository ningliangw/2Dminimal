public interface IState//实现状态的接口
{
    void OnEnter();//进入

    void OnUpdate();//执行

    void OnExit();//退出
}
