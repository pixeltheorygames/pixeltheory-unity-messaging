using System;
using System.Diagnostics;


namespace Pixeltheory.Debug
{
    public static class Logging
    {
        #region Class
        #region Methods
        #region Public
        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void Log(object message)
        { 
            UnityEngine.Debug.Log(message);
        }
        
        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void Log(UnityEngine.Object context, object message)
        {
            UnityEngine.Debug.Log(message, context);
        }
        
        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void Log(string format, params object[] args)
        {
            UnityEngine.Debug.LogFormat(format, args);
        }
        
        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void Log(UnityEngine.Object context, string format, params object[] args)
        {
            UnityEngine.Debug.LogFormat(context, format, args);
        }
        #endregion //Log

        #region Warn
        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void Warn(object warning)
        {
            UnityEngine.Debug.LogWarning(warning);
        }
        
        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void Warn(UnityEngine.Object context, object warning)
        {
            UnityEngine.Debug.LogWarning(warning, context);
        }
        
        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void Warn(string format, params object[] args)
        {
            UnityEngine.Debug.LogWarningFormat(format, args);
        }
        
        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void Warn(UnityEngine.Object context, string format, params object[] args)
        {
            UnityEngine.Debug.LogWarningFormat(context, format, args);
        }
        #endregion //Warn

        #region Exception
        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void Exception(Exception exception)
        {
            UnityEngine.Debug.LogException(exception);
        }
        
        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void Exception(UnityEngine.Object context, Exception exception)
        {
            UnityEngine.Debug.LogException(exception, context);
        }
        #endregion //Exception

        #region Error
        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void Error(object error)
        {
            UnityEngine.Debug.LogError(error);
        }
        
        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void Error(UnityEngine.Object context, object error)
        {
            UnityEngine.Debug.LogError(error, context);
        }
        
        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void Error(string format, params object[] args)
        {
            UnityEngine.Debug.LogErrorFormat(format, args);
        }
        
        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        public static void Error(UnityEngine.Object context, string format, params object[] args)
        {
            UnityEngine.Debug.LogErrorFormat(context, format, args);
        }
        #endregion //Public
        #endregion //Methods
        #endregion //Class
    }
}