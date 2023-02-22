using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace Pixeltheory.Editor
{
    public class PixeltheoryBaseSettingsProvider : SettingsProvider
    {
        #region Class
        #region Fields
        class PixeltheoryBaseSettingsDisplayStyle
        {
            public static GUIContent Title = new GUIContent("  Pixeltheory Helper Package Settings");
        }
        #endregion //Fields

        #region Methods
        [SettingsProvider]
        public static SettingsProvider CreateMyCustomSettingsProvider()
        {
            return new PixeltheoryBaseSettingsProvider("Project/Pixeltheory", SettingsScope.Project);
        }
        #endregion //Methods
        #endregion //Class

        #region Instance
        #region Constructor
        public PixeltheoryBaseSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) :
            base(path, scopes, keywords)
        {
            
        }
        #endregion //Constructor

        #region Methods
        #region SettingsProvider
        public override void OnGUI(string searchContext)
        {
            EditorGUILayout.LabelField(PixeltheoryBaseSettingsDisplayStyle.Title);
        }
        #endregion //SettingsProvider
        #endregion //Methods
        #endregion //Instance
    }
}