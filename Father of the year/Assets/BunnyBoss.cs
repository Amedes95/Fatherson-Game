using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyBoss : MonoBehaviour
{
    public GameObject CyclopsWalkTrigger;
    public float MaxHP;
    public float CurrentHP;
    public GameObject HpBar;
    public bool WakePhase;
    public GameObject BossCanvas;
    public bool dead;


    public GameObject DeathParticles;
    public static GameObject ParticlesClone;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHP = 0f;
        WakePhase = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentHP < MaxHP && WakePhase == true)
        {
            CurrentHP += .05f;
        }
        else if (CurrentHP == MaxHP && WakePhase == true)
        {
            WakePhase = false;
        }
        else if (CurrentHP > MaxHP)
        {
            CurrentHP = MaxHP;
            WakePhase = false;
        }

        if (CurrentHP <= 0 && WakePhase == false && BossCanvas.activeInHierarchy)
        {
            dead = true;
            BossCanvas.SetActive(false);
            Debug.Log("I am vanquished");
            ParticlesClone = Instantiate(DeathParticles, transform.position, Quaternion.identity);
            Destroy(ParticlesClone, 5f);
            gameObject.SetActive(false);
        }
        SetHPSize(CurrentHP / MaxHP);
    }

    public void SummonCyclops()
    {
        CyclopsWalkTrigger.SetActive(true);
    }

    public void Wakeup()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Alerted");
        WakePhase = true;
        BossCanvas.SetActive(true);
    }

    public void DamageMe()
    {
        CurrentHP -= 1;
        SetHPSize(CurrentHP / MaxHP);
        gameObject.GetComponent<Animator>().SetTrigger("Hurt");
        gameObject.GetComponent<AudioSource>().Play();
    }

    public void SetHPSize(float sizeNormalized)
    {
        HpBar.transform.localScale = new Vector2(sizeNormalized, 1f);
    }

}
