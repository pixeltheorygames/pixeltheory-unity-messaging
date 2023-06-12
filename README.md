# Pixeltheory Messaging
Messaging library for Unity games/applications. Currently there are two different messaging systems in 
this package. The first is MessagingManager, which is deprecated and will be removed in a future update. 
The second and highly recommended system is the PixelSocket system. Moving forward PixelSockets will be 
updated and maintained.
###
### Installation
Pick a protocol to use for downloading the package. SSH if you have already uploaded a public key to Github, 
and can use SSH to access repositories. HTTPS if you would rather "Login with Github" in your browsewr rather 
than upload a public key.

Copy one of the below links, depending on which protocol you have chosen:
* SSH : ssh://git<area>@github.com/pixeltheorygames/pixeltheory-unity-messaging.git?path=/unity/pixeltheory-unity-messaging/Packages/com.pixeltheory.messaging
* HTTPS : https:/<area>/github.com/pixeltheorygames/pixeltheory-unity-messaging.git?path=/unity/pixeltheory-unity-messaging/Packages/com.pixeltheory.messaging

Open up the PackageManager in the Unity Editor.
![OpenPackageManagerUnityEditor.png](github%2FREADME_Images%2FOpenPackageManagerUnityEditor.png)

Add the link you selected above to the PackageManager.
![AddGitURLPackageManagerUnityEditor.png](github%2FREADME_Images%2FAddGitURLPackageManagerUnityEditor.png)

The package should now be added and available to use in your Unity project.
###
### Usage
#### PixelSocket
Inherits from PixelObject. Used for asset oriented message passing "sockets". Abstract class. Inherit, and 
create an interface that can be used to enforce message reception. Message receiver who implement said Interface
need to Bind and Unbind themselves to the socket in their OnEnable and OnDisable methods. Message senders need only
call "Send" method, which is defined by the implementing concrete class. Implementing classes need to also add the
"[CreateAssetMenu(fileName = "ExampleFilename", menuName = "Example/Create/Menu/Path"]" attribute to the concrete
class.
###
#### PixelBehaviourSocketed
Abstract class that inherits from PixelBehaviour. Has an inspector property called "prefabRootGameObject". This 
GameObject's InstanceID can be used as a unique channel indentifier for all PixelBehaviourSocketed components in 
a prefab, creating a local channel for any PixelSocket messages you want to send/receive. Inheriting classes can 
access this unique channel identifier by using the property UniqueSocketChannel. In the case that a prefab root 
GameObject is not set on a PixelBehaviourSocketed concrete implementation, UniqueSocketChannel will return the 
InstanceID of the GameObject the PixelBehaviourSocketed is attached to.
###
#### PixelConstants
Partial static class that stores constants used by this package. The class builds upon the partial class of the same 
name in Pixeltheory Base package.
###
#### MessagingManager (DEPRECATED)
A PixelBehaviour class that MessagingBehaviour components can register/unregister themselves to in order to receive 
messages.
###
#### MessagingBehaviour (DEPRECATED)
Inherits from PixelBehaviour. Auto-registers itself with the MessagingManager for any implemented messaging interfaces.
Implement messaging interfaces to receive the message calls.
###
#### MessagingExtensionsGenerator (DEPRECATED)
Editor script. Generates static message event methods that can be called from anywhere, as long as you have a reference to the MessagingManager 
component.
###
#### MessagingExtensionsGenerator (DEPRECATED)
Editor script. Generates editor panels that allow you to call MessagingManager events from the editor itself, as long as the editor is in Play mode.
###
### Known Issues
1. MessagingExtensionsGenerator does not properly generate used namespaces.
2. MessagingControlPanelGenerator does not properly generate control panel class namespace and MenuItem attribute.
3. Unit tests for PixelSockets are non-existant.
###
####
### To-do List
1. Implement unit tests for PixelSocket.
2. Create PixelSocket custom editor inspector class generator.
3. Fully remove old MessagingManager system.

