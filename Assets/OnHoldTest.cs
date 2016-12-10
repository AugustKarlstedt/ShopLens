using UnityEngine;
using System.Collections;
using System;

public class OnHoldTest : HoldableGameObject
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnHoldStarted()
    {
        Destroy(this.gameObject);
    }

    public override void OnHoldCanceled()
    {
        
    }

    public override void OnHoldCompleted()
    {
        
    }
}
