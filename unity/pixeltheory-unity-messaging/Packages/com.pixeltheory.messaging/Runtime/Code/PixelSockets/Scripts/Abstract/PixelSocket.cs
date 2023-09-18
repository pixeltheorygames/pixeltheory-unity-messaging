using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.Pool;


namespace Pixeltheory.Messaging
{
   public class PixelSocket : PixelObject
   {
      #region Class
      #region Fields
      #region Public
      [Flags]
      public enum PixelSocketHookTypeFlag
      {
         PreMessageHook = 1,
         PostMessageHook = 2
      }
      #endregion //Public
      #endregion //Fields
      #endregion //Class

      #region Instance
      #region Fields
      #region Private
      private List<Func<Task>> preMessageHooks;
      private List<Func<Task>> postMessageHooks;
      #endregion //Private
      #endregion //Fields
      
      #region Methods
      #region Unity Messages
      protected virtual void OnEnable()
      {
         this.preMessageHooks = ListPool<Func<Task>>.Get();
         this.postMessageHooks = ListPool<Func<Task>>.Get();
      }

      protected virtual void OnDisable()
      {
         this.preMessageHooks.Clear();
         ListPool<Func<Task>>.Release(this.preMessageHooks);
         this.postMessageHooks.Clear();
         ListPool<Func<Task>>.Release(this.postMessageHooks);
      }
      #endregion //Unity Messages
      
      #region Public
      public void AttachMessageHook(PixelSocketHookTypeFlag pixelSocketHookTypeFlag, Func<Task> callback)
      {
         if (pixelSocketHookTypeFlag.HasFlag(PixelSocketHookTypeFlag.PreMessageHook))
         {
            if (!this.preMessageHooks.Contains(callback))
            {
               this.preMessageHooks.Add(callback);  
            }
         }
         if (pixelSocketHookTypeFlag.HasFlag(PixelSocketHookTypeFlag.PostMessageHook))
         {
            if (!this.postMessageHooks.Contains(callback))
            {
               this.postMessageHooks.Add(callback);  
            }
         }
      }

      public void RemoveMessageHook(PixelSocketHookTypeFlag pixelSocketHookTypeFlag, Func<Task> callback)
      {
         if (pixelSocketHookTypeFlag.HasFlag(PixelSocketHookTypeFlag.PreMessageHook))
         {
            this.preMessageHooks.Remove(callback);
         }
         if (pixelSocketHookTypeFlag.HasFlag(PixelSocketHookTypeFlag.PostMessageHook))
         {
            this.postMessageHooks.Remove(callback);
         }
      }
      #endregion //Public
      
      #region Protected
      protected async Task PreMessageHooksAsync()
      {
         for (int i = this.preMessageHooks.Count - 1; i > -1; i--)
         {
            await this.preMessageHooks[i]();
         }
      }

      protected async Task PostMessageHooksAsync()
      {
         for (int i = this.postMessageHooks.Count - 1; i > -1; i--)
         {
            await this.postMessageHooks[i]();
         }
      }
      #endregion //Protected
      #endregion //Methods
      #endregion //Instance
   }
   
   public abstract class PixelSocket<TypeInterface> : PixelSocket
   {
      #region Fields
      #region Private
      private Dictionary<int, List<TypeInterface>> channelToListenersListMap;
      #endregion //Private
      #endregion //Fields

      #region Methods
      #region Unity Messages
      protected override void OnEnable()
      {
         base.OnEnable();
         this.channelToListenersListMap = DictionaryPool<int, List<TypeInterface>>.Get();
      }

      protected override void OnDisable()
      {
         base.OnDisable();
         foreach (List<TypeInterface> channelListeners in this.channelToListenersListMap.Values)
         {
            channelListeners.Clear();
            ListPool<TypeInterface>.Release(channelListeners);
         }
         this.channelToListenersListMap.Clear();
      }
      #endregion //Unity Messages
      
      #region Public
      public void Bind(TypeInterface listener, int channel)
      {
         List<TypeInterface> currentListeners;
         if (this.channelToListenersListMap.TryGetValue(channel, out currentListeners))
         {
            currentListeners.Add(listener);
         }
         else
         {
            currentListeners = ListPool<TypeInterface>.Get();
            currentListeners.Add(listener);
            this.channelToListenersListMap.Add(channel, currentListeners);
         }
      }

      public void Unbind(TypeInterface listener, int channel)
      {
         List<TypeInterface> currentListeners;
         if (this.channelToListenersListMap.TryGetValue(channel, out currentListeners))
         {
            currentListeners.Remove(listener);
            if (currentListeners.Count == 0)
            {
               this.channelToListenersListMap.Remove(channel);
               ListPool<TypeInterface>.Release(currentListeners);
            }
         }
      }
      #endregion //Public

      #region Protected
      protected bool GetListeners(int channelID, out List<TypeInterface> listeners)
      {
         return this.channelToListenersListMap.TryGetValue(channelID, out listeners);
      }
      #endregion //Protected
      #endregion //Method
   }
}