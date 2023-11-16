using AxGrid.FSM;
using AxGrid.Model;

[State(Keys.BonusState)]
public class BonusState : BaseFSMState
{
    [Enter]
    protected override void Enter()
    {
        Model.EventManager.Invoke(Keys.BonusState);
    }

    [Bind(Keys.AllBonusesHaveBeenPaid)]
    private void Exit()
    {
        Parent.Change(Keys.IdleState);
    }
}
