using UnityEngine;

public class AtomIdleState : AtomBaseState
{

    public AtomIdleState(AtomStateMachine stateMachine) : base(stateMachine) { }

    public override void OnStateEnter()
    {
        if (_atomSM.GetComponent<AtomController>()?.GetAtomType() == AtomType.PLAYER)
        {
            _atomSM.ChangeAtomState(AtomState.ACTIVATED);
        }
    }

    public override void OnStateExit()
    {
        
    }

    public override void Tick()
    {
        if(_atomSM.GetComponent<AtomController>()?.GetAtomType() == AtomType.ENEMY && PlayerService.Instance.ArePlayersPresent())
        {
            if(Vector3.Distance(PlayerService.Instance._players[0].transform.position, _atomSM.transform.position) < 10f)
                _atomSM.ChangeAtomState(AtomState.CHASE);
        }

        else if(PlayerService.Instance.ArePlayersPresent())
        {
            if(Vector3.Distance(PlayerService.Instance._players[0].transform.position, _atomSM.transform.position) < 7.5f)
            {
                SoundManager.Instance.Play(SourceType.FX1, SoundType.Connect);
                _atomSM.ChangeAtomState(AtomState.ACTIVATED);
            }
        }
    }
}
