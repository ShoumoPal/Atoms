using TMPro;
using UnityEngine;

public class AtomCheckBoxController : MonoBehaviour
{
    [SerializeField] private TextMeshPro _text;
    [SerializeField] private int _atomsRequired;
    private int _numberOfAtoms;
    private bool hasSatisfied;

    private void OnEnable()
    {
        EventService.Instance.HasSatisfiedAtomCondition += HasSatisfiedCondition;
    }
    private void Start()
    {
        
        _numberOfAtoms = _atomsRequired;
        hasSatisfied = false;
        DisplayNumberOfAtoms();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<AtomController>() != null && !hasSatisfied)
        {
            Debug.Log("Entered");
            _numberOfAtoms--;
            DisplayNumberOfAtoms();
        }

        if (_numberOfAtoms == 0)
        {
            ChangeColour();
            hasSatisfied = true;
        }    
    }

    private bool HasSatisfiedCondition()
    {
        return hasSatisfied;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<AtomController>() != null && !hasSatisfied)
        {
            Debug.Log("Exited");
            _numberOfAtoms++;
            DisplayNumberOfAtoms();
        }
    }

    private void DisplayNumberOfAtoms()
    {
        _text.text = _numberOfAtoms.ToString();
    }

    private void ChangeColour()
    {
        Debug.Log("Change color called");
        gameObject.GetComponent<MeshRenderer>().material.color = Color.cyan;
    }

    private void OnDisable()
    {
        EventService.Instance.HasSatisfiedAtomCondition -= HasSatisfiedCondition;
    }
}
