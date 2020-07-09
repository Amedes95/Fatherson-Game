using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyBoss : MonoBehaviour
{
    public GameObject CyclopsWalkTrigger;
    public int MaxHP;
    public int CurrentHP;
    // Start is called before the first frame update
    void Start()
    {
        CurrentHP = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SummonCyclops()
    {
        CyclopsWalkTrigger.SetActive(true);
    }

    public void Wakeup()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Alerted");
    }

    public void DamageMe()
    {
        CurrentHP -= 1;
    }

}
