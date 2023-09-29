using TMPro;
using UnityEngine;

public class LevelCompleteBoxController : MonoBehaviour
{
    [SerializeField] private TextMeshPro _text;
    private int _numberOfAtoms;

    private void Start()
    {
        _numberOfAtoms = 0;
        DisplayNumberOfAtoms();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<AtomController>() != null)
        {
            _numberOfAtoms++;
            DisplayNumberOfAtoms();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<AtomController>() != null)
        {
            _numberOfAtoms--;
            DisplayNumberOfAtoms();
        }
    }

    private void DisplayNumberOfAtoms()
    {
        _text.text = _numberOfAtoms.ToString();
    }
}
