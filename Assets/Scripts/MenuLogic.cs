using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLogic : MonoBehaviour
{
    
    [SerializeField] GameObject StartText;
    [SerializeField] GameObject ExitText;

    [SerializeField] GameObject PressAnyKey;
    [SerializeField] float flashFrequency=0.7f;

    private float timeCounter = 0;
    private bool showMenu = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!showMenu)
        {
            this.timeCounter += Time.deltaTime;
            if (this.timeCounter >= flashFrequency)
            {
                this.timeCounter = 0;
                PressAnyKey.SetActive(!PressAnyKey.active);
            }
        }
        
        {
            if(Input.anyKey)
            {
                showMenu = true;
                PressAnyKey.SetActive(false);
                StartText.SetActive(true);
                ExitText.SetActive(true);
            }
        }
    }
}
