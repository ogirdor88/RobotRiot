using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel : MonoBehaviour
{
    [SerializeField]
    GameObject panel;
    
    // Start is called before the first frame update
    void Start()
    {
        panel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ControlPress()
    {
        panel.gameObject.SetActive(true);
    }

    public void ClosePanel()
    {
        panel.gameObject.SetActive(false);
    }
}
