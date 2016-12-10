using UnityEngine;
using System.Collections;
using HoloToolkit.Unity;
using System;

public class OnTapTest : TappableGameObject
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnTap()
    {
        GetComponent<TextToSpeechManager>().SpeakText("Welcome to ShopLens. Try saying \"find this.\"");
    }

}
