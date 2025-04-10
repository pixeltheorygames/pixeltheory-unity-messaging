using System;
using System.Threading;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;


namespace Pixeltheory.Messaging.Editor
{
    [CustomPropertyDrawer(typeof(PixelMessage), true)]
    public class PixelMessageDrawer : PropertyDrawer
    {
        #region Methods
        #region PropertyDrawer Overrides
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement rootContainer = new VisualElement();
            
            SerializedProperty pixelSocketSerializedProperty = 
                property.FindPropertyRelative("pixelSocket");
            PropertyField pixelSocketPropertyField = 
                new PropertyField(pixelSocketSerializedProperty);
            pixelSocketPropertyField.RegisterValueChangeCallback
            (
                (changeEvent) =>
                {
                    pixelSocketSerializedProperty = changeEvent.changedProperty;
                    pixelSocketPropertyField = new PropertyField(pixelSocketSerializedProperty);
                }
            );
            rootContainer.Add(pixelSocketPropertyField);

            VisualElement controlPanelContainer = new VisualElement();
            rootContainer.Add(controlPanelContainer);
            
            SerializedProperty parallelFlagSerializedProperty = 
                property.FindPropertyRelative("parallelFlag");
            PropertyField parallelFlagPropertyField = 
                new PropertyField(parallelFlagSerializedProperty);
            controlPanelContainer.Add(parallelFlagPropertyField);
            
            SerializedProperty multicastTypeNameSerializedProperty = 
                property.FindPropertyRelative("multicastTypeName");
            PropertyField multicastTypeNamePropertyField = 
                new PropertyField(multicastTypeNameSerializedProperty);
            controlPanelContainer.Add(multicastTypeNamePropertyField);
            
            SerializedProperty unicastIDSerializedProperty =
                property.FindPropertyRelative("unicastID");
            PropertyField unicastIDPropertyField = 
                new PropertyField(unicastIDSerializedProperty);
            controlPanelContainer.Add(unicastIDPropertyField);

            CancellationTokenSource cts = null;
            
            VisualElement buttonsContainer = new VisualElement();
            buttonsContainer.style.height = new StyleLength(new Length(25f, LengthUnit.Pixel));
            buttonsContainer.style.justifyContent = new StyleEnum<Justify>(Justify.SpaceAround);
            buttonsContainer.style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row);
            Button broadcastButton = new Button();
            broadcastButton.text = "Broadcast";
            broadcastButton.clicked += async () =>
            {
                if (cts == null && pixelSocketSerializedProperty.objectReferenceValue != null)
                {
                    PixelSocket pixelSocket = 
                        pixelSocketSerializedProperty.objectReferenceValue as PixelSocket;
                    cts = new CancellationTokenSource();
                    try
                    {
                        await pixelSocket.Broadcast
                        (
                            parallelFlagSerializedProperty.boolValue,
                            cts.Token
                        );
                    }
                    catch (OperationCanceledException) { }
                    finally
                    {
                        cts.Dispose();
                        cts = null;    
                    }
                }
            };
            Button multicastButton = new Button();
            multicastButton.text = "Multicast";
            multicastButton.clicked += async () =>
            {
                if (cts == null && pixelSocketSerializedProperty.objectReferenceValue != null)
                {
                    PixelSocket pixelSocket = 
                        pixelSocketSerializedProperty.objectReferenceValue as PixelSocket;
                    cts = new CancellationTokenSource();
                    try
                    {
                        await pixelSocket.Multicast
                        (
                            parallelFlagSerializedProperty.boolValue,
                            multicastTypeNameSerializedProperty.stringValue,
                            cts.Token
                        );
                    }
                    catch (OperationCanceledException) { }
                    finally
                    {
                        cts.Dispose();
                        cts = null;  
                    }
                }
            };
            Button unicastButton = new Button();
            unicastButton.text = "Unicast";
            unicastButton.clicked += async () =>
            {
                if (cts == null && pixelSocketSerializedProperty.objectReferenceValue != null)
                {
                    PixelSocket pixelSocket = 
                        pixelSocketSerializedProperty.objectReferenceValue as PixelSocket;
                    cts = new CancellationTokenSource();
                    try
                    {
                        await pixelSocket.Unicast
                        (
                            parallelFlagSerializedProperty.boolValue,
                            unicastIDSerializedProperty.intValue,
                            cts.Token
                        );
                    }
                    catch (OperationCanceledException) { }
                    finally
                    {
                        cts.Dispose();
                        cts = null;  
                    }
                }
            };
            Button cancelButton = new Button();
            cancelButton.text = "Cancel";
            cancelButton.clicked += () =>
            {
                if (cts != null)
                {
                    cts.Cancel(false);
                }
            };
            buttonsContainer.Add(broadcastButton);
            buttonsContainer.Add(multicastButton);
            buttonsContainer.Add(unicastButton);
            buttonsContainer.Add(cancelButton);
            controlPanelContainer.Add(buttonsContainer);
            
            return rootContainer;
        }
        #endregion //PropertyDrawer Overrides
        #endregion //Methods
    }
}