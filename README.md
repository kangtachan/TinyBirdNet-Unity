# TinyBirdNet
A high level API for making networking games in Unity, utilizes [RevenantX/LiteNetLib](https://github.com/RevenantX/LiteNetLib)

The goal of TinyBirdNet is to create an API similar to Unet's HLAPI while using the LiteNetLib behind the scenes.

## Features
- All code available, no obsfucation, no dlls.
- Similar to Unet, easier to migrate projects from.
- Free for noncommercial projects! Try it fully without paying.
- No additional charges for CCU, bandwith, nothing, it's yours to use.

**Version: 1.1.2**
- Fixed initial serialization not sending all properties.
- Fixed many logs without a log filter.
- Fixed some changes made to the TinyNetGameManagerEditor not being saved.

**Supports Unity 2017 and above**

Current version has been tested across Windows and Linux, please open an issue if you find any bugs.

![Two players connected](https://i.imgur.com/pQJhZEd.png)


## How To Use

Adding the TinyBirdNet folder under Assets to your project will give you everything needed to start networking on your game.

Inside the Examples folder you will find a simple working demo of a networked game.

You will need a `TinyNetGameManager` or derived instance always enabled on your game, so please add one to your first scene and mark it as [Don't Destroy on Load](https://docs.unity3d.com/ScriptReference/Object.DontDestroyOnLoad.html).
An unique "ConnectKey" is required for your game to be able to connect, preferably include the current version of your application on the key string.

The recommended workflow is to create a class derived from `TinyNetPlayerController` and implement your player control logic there, for each new player on the game a new `TinyNetPlayerController` is spawned. You are able to send messages between the client Player Controllers and the server one by using an `TinyNetInputMessage`.

By creating classes derived from `TinyNetBehaviour` you are able to enjoy most of the automatic serialization and deserialization, though manual syncing by use of `ITinyNetMessage` is possible and might be necessary in many applications.

You are able to sync up to 32 Properties per `TinyNetBehaviour` by using the `[TinyNetSyncVar]` Attribute.
You may at any time bypass this limit by implementing your own `TinySerialize` and `TinyDeserialize` Methods.

You are able to spawn and destroy objects in the network by merely calling `TinyNetServer.instance.SpawnObject` and `TinyNetServer.instance.DestroyObject` on a server, given a valid GameObject that contains a `TinyNetIdentity`.

Remember to register all prefabs that have `TinyNetIdentity` on your `TinyNetGameManager`, manually or by clicking the __Register all TinyNetIdentity prefabs__ button on your `TinyNetGameManager` inspector.


#### Comming soon: Wiki and documentation
