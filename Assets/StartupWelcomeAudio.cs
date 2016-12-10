using UnityEngine;
using System.Collections;
using HoloToolkit.Unity;

public class StartupWelcomeAudio : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        StartCoroutine(SayWelcome());
    }

    public IEnumerator SayWelcome()
    {
        var ttsMgr = GetComponent<TextToSpeechManager>();
        if (ttsMgr != null)
        {
            ttsMgr.SpeakText("Welcome to ShopLens. Try saying \"find this.\"");
        }

        yield return null;
    }
}
