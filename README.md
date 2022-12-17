# Pixeltheory Unity Messaging Package #

Scripts and classes for a system wide messaging system in Unity.

## TO-DO
Fix Message control panel generation (control panel namespace and title)
Fix current unit tests and add more.
Add offline cached MessagingKeys.


### Install Instructions ###

Add the following line to your manifest.json file in your Unity project.

> "com.pixeltheory.messaging" : "ssh://git@gitlab.com/pixeltheory/pixeltheory-unity-messaging.git#package"

After installing the messaging package, you need to generate a stubbed MessagingExtensions.cs and MessagingControlPanel.cs. When doing so for the first time, the package will ask for locations for each of the above files. Subsequent generating will simply overwrite the current file in it's current location.



### How do I use the Messaging subsystem? ###

First, create an interface and tag it with the attribute "MessagingInterface". Go back to the Unity editor, and click "Pixeltheory->Messaging->Generate Messages Extensions file" in the top menu. Then implement the interface in any component that inherits from "MessagingBehaviour". You can fire the event from anywhere in the code by calling the interface name postfixed with "Event" in "MessagingManager", for example the event for the interface message "ExampleMessage(GameObject gameObject)" would be "messagingManagerReference.ExampleMessageEvent(exampleGameObject);". The messaging subsystem, consisting of the base "MessagingBehaviour" implementation and the "MessagingManager", and the events auto-generator editor script, will take care of the rest.

You can stack interfaces by having an interface inherit another interface, i.e. "interface InterfaceOne : InterfaceTwo". As long as both interfaces are tagged with the "MessagingInterface" attribute, the sub-system will take care of the rest.

If you implement a messaging interface and realize you do not need all messages defined in said inteface, you can tag the unnecessary method implmentations in your "MessagingBehaviour" inheriting component with the attribute "MessagingNOP". This will effectively stop registration of the method implementation with the MessagingManager.



### Are there any gotchas/caveats I need to be aware of? ###

Yes. 

One, currently, as of Unity 2019.4, ulong (UInt64) is not supported as a parameter type for messaging. This is due to a bug in Unity when entering ulong values into the editor.

Two, the editor scripts that auto-generate events can be a little brittle, due to the Unity editor having a rather complex hot-reloading feature. Anytime you get into a situation where the auto-generating code is not being cleared or regenerated through the editor, try to see if there are any code errors in the Unity console. Unity will stop compilation and asset reloading dead in it's tracks once it encounters a compilation error. I have included a menu item that allows you to generate a stubbed version of the message extensions, hopefully you can use this get around any compilation erros tha may result from changing any interfaces/message events by hand.



### What's in this repository? ###

As of 2021.02.02, a messaging system built on PixelBehaviourSingle called MessagingManager, an Editor script that auto-generates messaging events, and Editor script that auto-generates a control panel that can be used to fire messages from the editor, attributes for tagging messaging interfaces and methods, and MessagingBehaviour which should be the super class for all components that want to receive messages.


### Who do I talk to? ###

If you have any questions or problems with the files in this repository, please email gitlab@ellistalley.com.
