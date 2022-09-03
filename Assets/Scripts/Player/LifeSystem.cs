using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeSystem : MonoBehaviour
{
    // LIFE VAR
    public float life = 80;
    // SKILLS VAR
    public float punch, punchUsed;
    public float ultimate, ultimateUsed;
    public float kick, kickUsed;
    public float dash, dashUsed;
    public float uppercut, uppercutUsed;

    // LIFE CANVAS
    public Slider lifeBar;
    // SKILLS CANVAS
    public Image punchSquare;
    public Image ultimateSquare;
    public Image kickSquare;
    public Image dashSquare;
    public Image uppercutSquare;

    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        lifeBar.value = life;

        punchSquare.fillAmount = punch / punchUsed;
        ultimateSquare.fillAmount = ultimate / ultimateUsed;
        kickSquare.fillAmount = kick / kickUsed;
        dashSquare.fillAmount = dash / dashUsed;
        uppercutSquare.fillAmount = uppercut / uppercutUsed;
    }
}
