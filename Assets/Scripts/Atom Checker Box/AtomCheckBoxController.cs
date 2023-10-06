using TMPro;
using UnityEngine;

/* Script responsible for the atom checker box */

public class AtomCheckBoxController : MonoBehaviour
{
    [SerializeField] private TextMeshPro _text;
    [SerializeField] private int _atomsRequired;
    private int _numberOfAtoms;
    private bool hasSatisfied;

    private void Start()
    {
        EventService.Instance.HasSatisfiedAtomCondition += HasSatisfiedCondition; // Subscribing to event
        _numberOfAtoms = _atomsRequired;
        hasSatisfied = false;
        DisplayNumberOfAtoms();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<AtomController>() != null && !hasSatisfied)
        {
            if(other.gameObject.GetComponent<AtomController>().GetAtomType() == AtomType.FRIENDLY)
            {
                _numberOfAtoms--;
                DisplayNumberOfAtoms();
            }
        }

        if (_numberOfAtoms == 0 && !hasSatisfied) // Atom checker true
        {
            SoundManager.Instance.Play(SourceType.FX2, SoundType.Pass_Sound);
            ChangeColour();
            hasSatisfied = true;
        }    
    }

    // Returns the present satisfy condition
    private bool HasSatisfiedCondition()
    {
        return hasSatisfied;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<AtomController>() != null && other.gameObject.GetComponent<AtomController>().GetAtomType() == AtomType.FRIENDLY && !hasSatisfied)
        {
            _numberOfAtoms++;
            if (_numberOfAtoms > _atomsRequired)
                _numberOfAtoms = _atomsRequired;

            DisplayNumberOfAtoms();
        }
    }

    // Changes the text on the checker box
    private void DisplayNumberOfAtoms()
    {
        _text.text = _numberOfAtoms.ToString();
    }

    private void ChangeColour()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = Color.cyan;
    }

    private void OnDisable()
    {
        EventService.Instance.HasSatisfiedAtomCondition -= HasSatisfiedCondition; // Unsubscribing function
    }
}
