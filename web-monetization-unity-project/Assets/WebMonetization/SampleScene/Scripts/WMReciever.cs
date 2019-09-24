using System.Collections.Generic;
using UnityEngine;

public class WebMonetizationReciever : MonoBehaviour
{
    // register one or both events if you want to use them
    void OnEnable()
    {
        WMBroadcaster.OnMonetizationStart += OnMonetizationStart;
        WMBroadcaster.OnMonetizationProgress += OnMonetizationProgress;
    }
    
    // unregister events that you've registered
    void OnDisable()
    {
        WMBroadcaster.OnMonetizationStart -= OnMonetizationStart;
        WMBroadcaster.OnMonetizationProgress -= OnMonetizationProgress;
    }

    // A monetization start event should occur roughly after a second or two after your game loads as WebGL.
    void OnMonetizationStart(Dictionary<string, object> detail)
    {
        // these are the parameters that you can read from the detail dictionary.
        // recommended: wrap parsing of each of these values in a try/catch in case the spec changes.
        // https://coil.com/docs/#browser-start

        // string requestId = detail["requestId"] as string;
        // string id = detail["id"] as string;
        // string resolvedEndpoint = detail["resolvedEndpoint"] as string;
        // string metaContent = detail["metaContent"] as string;

        // Debug.Log("MonetizationStart requestId: " + requestId + ", id: " + id + ", resolvedEndpoint: " + resolvedEndpoint + ", metaContent" + metaContent);
 
        Debug.Log("MonetizationStart");
    }

    // A monetization progress event should occur roughly every two seconds after the monetization progress occurs
    void OnMonetizationProgress(Dictionary<string, object> detail)
    {
        // these are the parameters that you can read from the detail dictionary.
        // recommended: wrap parsing of each of these values in a try/catch in case the spec changes.
        // https://coil.com/docs/#browser-progress
        
        // string amount = detail["amount"] as string;
        // long amountAsLong = Convert.ToInt64(amount);
        // string assetCode = detail["assetCode"] as string;
        // long scale = (long) detail["assetScale"];
        
        // Debug.Log("MonetizationProgress amount " + amountAsLong + ", assetCode: " + assetCode + ", scale: " + scale);

        Debug.Log("MonetizationProgress");
    }
}
