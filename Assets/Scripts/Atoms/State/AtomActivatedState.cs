
public class AtomActivatedState : AtomBaseState
{
    public AtomActivatedState(AtomStateMachine stateMachine) : base(stateMachine) { }

    public override void OnStateEnter()
    {
        if(_atomSM.gameObject.GetComponent<AtomController>() == null)
        {
            _atomSM.gameObject.AddComponent<AtomController>();
            _atomSM.gameObject.GetComponent<AtomController>()._enemyMat = _atomSM._enemyMat;
        }
        // Setting AtomController parameters
        _atomSM.gameObject.GetComponent<AtomController>().SetAtomType(AtomType.FRIENDLY);
        _atomSM.gameObject.GetComponent<AtomController>()._playerLayer = PlayerService.Instance._players[0].GetComponent<AtomController>()._playerLayer;
    }
}
