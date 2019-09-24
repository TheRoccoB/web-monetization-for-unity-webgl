using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SpawnPrefabOnMonetizationProgress : MonoBehaviour
{
    public GameObject prefab;
    
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
        GetComponent<SpriteRenderer>().color = Color.green;
    }

    // A monetization progress event should occur roughly every two seconds after the monetization progress occurs
    void OnMonetizationProgress(Dictionary<string, object> detail)
    {
        var rnd = new Random();
        if (prefab)
        {
            var coin = Instantiate(prefab, transform.position, transform.rotation);

            double rndX = rnd.NextDouble() - 0.5d; // a little push in a random direction
            
            coin.GetComponent<Rigidbody2D>().velocity = new Vector2((float) rndX, 0.0f);
        }
    }
}
