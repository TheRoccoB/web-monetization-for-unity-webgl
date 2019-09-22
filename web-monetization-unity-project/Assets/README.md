# Web Monetization API for Unity WebGL

> HEADS UP! This is currently in development, is a work in progress, and is completely broken!  Please check back soon.

This project allows you to register a web monetization `meta` tag to your Unity WebGL game so that you can make money with your game on the web. It also allows you to create an in-game benefit to players who are monetizing your game. 

The [Web Monetization API](https://webmonetization.org) is a proposed [W3C standard](https://adrianhopebailie.github.io/web-monetization/). There is currently just one Web Monetization provider, [coil.com](https://coil.com).

This project is provided for free by [SIMMER.io](https://simmer.io), because we want you to be able to make money with your web games whether or not you use our platform. If you have a cool game to share, consider [uploading it](https://simmer.io) to our site!

# How it works

You might want to first read [Writing a Web Monetized Game](https://coil.com/p/sharafian/Writing-a-Web-Monetized-Game/1i3t_1Frk) for an overview of a non-Unity explanation of monetizing a game.

## Required

1. [Export your game to Unity WebGL](https://www.youtube.com/watch?v=JZqTHjjtQHM]).
1. Set up a free creator account at [coil.com](https://coil.com), and get a payment pointer, like: `$coil.xrptipbot.com/JABJLDXNSje7h_bY26_6wg`
1. Add the `Assets/WebMonetization/Prefabs/WebMonetizatonBroadcaster` prefab onto the stage and add your payment pointer in the properties window.
    1. This will inject a web monetization `meta` tag into your game when the scene loads in the browser
1. Deploy your game to the web
1. ???
1. Profit!!!

## Player Benefits

It's always nice to give the player something in return to thank them for monetizing your game, and that's where monetization events come in.

The `WebMonetizationBroadcaster` sends out events when monetization starts, and when progressive payments are made via the browser. Payments are usually a small fraction of a cent and occur approximately every two seconds.

The events in the browser are `monetizationstart` and `monetizationprogress`. These get translated into Unity events via the `WebMonetizationBroadcaster`. Any `GameObject` can listen for these events to provide a benefit to the player.

Example code is available in `Assets/WebMonetization/Scripts/WebMonetizationReciever`.

### Register and Unregister Events
```
// register one or both events if you want to use them
void OnEnable()
{
    WebMonetization.OnMonetizationStart += OnMonetizationStart;
    WebMonetization.OnMonetizationProgress += OnMonetizationProgress;
}

// unregister events that you've registered
void OnDisable()
{
    WebMonetization.OnMonetizationStart -= OnMonetizationStart;
    WebMonetization.OnMonetizationProgress -= OnMonetizationProgress;
}
```

### Act upon those events

You might just want to provide a general benefit to the player when you know they're monetizing. In that case you could just set a boolean to true in the first time `onMonetizationProgress` is called. I would discourage use of `onMonetizationStart` because it won't be called if you load another scene. So my recommendation is to always use `onMonetizationProgress`.

Here is the code you'd add to your `GameObject` script for each of these handlers:

#### OnMonetizationStart
```
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
```

#### OnMonetizationProgress

The `detail` object here provides additional data, such as `amount`, `assetCode` (USD, XRP, etc), and `assetScale`. You could provide a certain amount of in-game currency as payments trickle in using these parameters. See [Coil Developer Docs](https://coil.com/docs/#browser-progress) for details. 
```
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
```
  
# Testing It
There's a checkbox in the `WebMonetizationBroadcaster` prefab, `Simulate Monetization`. If you check this, you'll get an `OnMonetizationStart` event when the scene starts, and an `OnMonetizationProgress` event every 2 seconds. You can modify these events in the `WebMonetizationBroadcaster` if you wish.

Be sure to uncheck `Simulate Monetization` before deploying your game.

You can then test in the browser with a paid Coil account, or go to [testwebmonetization.com](https://testwebmonetization.com), where I provide additional tools for testing.

# A note about deployment
Many game portals (itch.io, kongregate, etc) run your game in an `iframe`. Web monetization does not automatically work in `iframe`'s. On [SIMMER.io](https://simmer.io), we bubble up the web monetization `<meta>` tag to the main frame, and bubble down the monetization events so that you can get paid.

If this is something you want, I'd recommend reaching out to the operators of other game portals and request that they add code to support this type of monetization.   

# Todo:

icon 128/128
200/258
860/389
1200/630 (no text/logo)



# License

The project was developed by SIMMER.io, and you can use it in your projects for free. On Github it is released under the MIT License and on the Unity Asset Store it is licenced under the [Unity Asset Store EULA](https://unity3d.com/legal/as_terms)  

