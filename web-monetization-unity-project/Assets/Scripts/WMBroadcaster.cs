using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using System.Collections.Generic;
using System.Runtime.InteropServices;
//using Object = System.Object;



public class WMBroadcaster : MonoBehaviour
{

    public string paymentPointer = "$coil.xrptipbot.com/JABJLDXNSje7h_bY26_6wg";
    public bool simulateMonetization = false;
    
    public delegate void MonetizationStartAction(Dictionary<string,object> detail);
    public static event MonetizationStartAction OnMonetizationStart;

    public delegate void MonetizationProgressAction(Dictionary<string,object> detail);
    public static event MonetizationProgressAction OnMonetizationProgress;
    
    private string _simulatedStartDetail =
        "{\"requestId\":\"344d36cf-a27c-45b9-9e85-86ed48364ff5\",\"id\":\"344d36cf-a27c-45b9-9e85-86ed48364ff5\",\"resolvedEndpoint\":\"https://coil.xrptipbot.com/JABJLDXNSje7h_bY26_6wg\",\"metaContent\":\"$coil.xrptipbot.com/JABJLDXNSje7h_bY26_6wg\"}";

    private string _simulatedProgressDetail = "{\"amount\":\"200000\",\"assetCode\":\"USD\",\"assetScale\":9}";

#if UNITY_WEBGL
#if UNITY_EDITOR
    private static void InitializeMonetization(string paymentPointer)
    {
    }
#else
    [DllImport("__Internal")]
    private static extern void InitializeMonetization(string paymentPointer);
#endif
#endif
    
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_WEBGL
        InitializeMonetization(paymentPointer);
#else
        Debug.Log("Web Monetization is defined only for WebGL.");
#endif

        if (simulateMonetization)
        {
            Debug.Log("Simulating Monetization Events! Don't forget to shut this off for release!");
        }
    }

    public void monetizationstart(String detailJson)
    {
        var detail = wm_MiniJSON.Json.Deserialize(detailJson) as Dictionary<string,object>;
        OnMonetizationStart?.Invoke(detail);

    }

    public void monetizationprogress(String detailJson)
    {
        var detail = wm_MiniJSON.Json.Deserialize(detailJson) as Dictionary<string,object>;
        OnMonetizationProgress?.Invoke(detail);
    }

    void Update()
    {
        if (simulateMonetization)
        {
            StartCoroutine(SimulateEventsCoroutine());
        }
    }

    IEnumerator SimulateEventsCoroutine()
    {
        monetizationstart(_simulatedStartDetail);

        while (true)
        {
            monetizationprogress(_simulatedProgressDetail);
            yield return new WaitForSeconds(2f);
        }
        
    }


}
