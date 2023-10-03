using UnityEngine;
using TMPro;

public class AtomActivatedState : AtomBaseState
{
    public AtomActivatedState(AtomStateMachine stateMachine) : base(stateMachine) { }

    public override void OnStateEnter()
    {
        if(_atomSM.gameObject.GetComponent<AtomController>() == null)
        {
            _atomSM.gameObject.AddComponent<AtomController>();
            _atomSM.gameObject.GetComponent<AtomController>()._enemyMat = _atomSM._enemyMat;
            _atomSM.gameObject.GetComponent<AtomController>().SetHealthText(_atomSM.gameObject.GetComponentInChildren<TextMeshPro>());
        }
        // Setting AtomController parameters
        _atomSM.gameObject.GetComponent<AtomController>().SetHealth(PlayerService.Instance._players[0].GetHealthValue());
        _atomSM.gameObject.GetComponent<AtomController>().SetHealthTextValue();
        _atomSM.gameObject.GetComponent<AtomController>().ShowText();
        _atomSM.gameObject.GetComponent<AtomController>().SetAtomType(AtomType.FRIENDLY);
        _atomSM.gameObject.GetComponent<AtomController>()._playerLayer = PlayerService.Instance._players[0].GetComponent<AtomController>()._playerLayer;
    }
}
