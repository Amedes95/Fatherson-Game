using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateChain : MonoBehaviour
{

    public Transform PivotPoint;
    Vector3 SawPosition;
    public float IncrementValue;
    public GameObject ChainLinkPrefab;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        SawPosition = gameObject.transform.position;
    }


    [ContextMenu("CreateChain")]
    void MyFunction()
    {
        SawPosition = gameObject.transform.position;
        RemoveChains();
        SpawnChain();
    }


    void SpawnChain()
    {
        int step;
        Vector3 originalVector = SawPosition - PivotPoint.position;
        Vector3 placementVector = (originalVector).normalized * IncrementValue;
        step = 1;
        while (placementVector.sqrMagnitude < originalVector.sqrMagnitude)
        {
            GameObject chain = Instantiate(ChainLinkPrefab);
            chain.transform.parent = transform;
            chain.transform.position = PivotPoint.position + placementVector;
            chain.transform.right = originalVector.normalized;
            step++;
            placementVector = originalVector.normalized * IncrementValue * step;
        }
    }

    public void RemoveChains()
    {
        Transform[] ChainClones;
        ChainClones = GetComponentsInChildren<Transform>();
        foreach (Transform Clone in ChainClones)
        {
            if (Clone.tag == "Chain")
            {
                DestroyImmediate(Clone.gameObject);
            }
        }
    }
}