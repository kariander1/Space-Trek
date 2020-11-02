using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    TextMeshProUGUI healthText;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        healthText = GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            this.healthText.text = player.GetHealth().ToString();
        }
        else
        {
            this.healthText.text = "0";
        }
    }
}
