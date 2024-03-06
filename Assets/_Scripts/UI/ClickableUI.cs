using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableUI : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData) {
        Debug.Log("CLICKED CLICKED CLICKED CLICKED CLICKED CLICKED CLICKED CLICKED CLICKED CLICKED CLICKED CLICKED CLICKED CLICKED ");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
