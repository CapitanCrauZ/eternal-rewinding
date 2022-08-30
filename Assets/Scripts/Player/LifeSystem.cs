using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeSystem : MonoBehaviour
{
    public float life;
    public float maxLife;

    public Image lifeBar;


    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        lifeBar.fillAmount = life / maxLife;
    }
}
