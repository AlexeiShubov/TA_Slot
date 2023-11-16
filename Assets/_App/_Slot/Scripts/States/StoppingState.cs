using AxGrid.FSM;
using AxGrid.Model;

[State(Keys.StoppingState)]
public class StoppingState : BaseFSMState
{
    [Enter]
    protected override void Enter()
    {
        Model.EventManager.Invoke(Keys.StoppingState);
    }

    [Bind(Keys.AllBlocksIsIdle)]
    private void Exit()
    {
        Parent.Change(Keys.BonusState);
    }
}