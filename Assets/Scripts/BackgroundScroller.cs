using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float backgroundSpeedY = 0.5f;
    [SerializeField] float backgroundSpeedX = 0f;
    Material myMaterial;
    Vector2 offest;
    // Start is called before the first frame update
    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        offest = new Vector2(this.backgroundSpeedX, this.backgroundSpeedY);
    }

    // Update is called once per frame
    void Update()
    {
        myMaterial.mainTextureOffset += offest * Time.deltaTime;
    }
}
