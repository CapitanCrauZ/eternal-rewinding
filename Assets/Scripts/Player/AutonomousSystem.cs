using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutonomousSystem : MonoBehaviour
{
    // LIFE VAR
    public float life = 0.8f;
    public float maxLife = 1.0f;
    // LIFE CANVAS
    public Slider lifeBar;

    // Start is called before the first frame update
    void Start()
    {
        lifeBar.value = life / maxLife;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
