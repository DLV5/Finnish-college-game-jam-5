using System.Collections.Generic;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    [SerializeField] private List<GameObject> _texts = new List<GameObject>();
    private int _index = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShowNext();
        }
    }

    private void ShowNext()
    {
        if(_index < _texts.Count)
        {
            _texts[_index].SetActive(true);
            _index++;
        } else
        {
            gameObject.SetActive(false);
        }
    }
}
