using JetBrains.Annotations;
using UnityEngine;

public class HazardController : MonoBehaviour
{
    public HazardBehavior[] hazards;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        for (int  i = 0; i < hazards.Length; i++)
        {
            HazardBehavior hazard = hazards[i];
            StartCoroutine(hazard.ActivateHazard());
        }
    }

    // Update is called once per frame
   
}
