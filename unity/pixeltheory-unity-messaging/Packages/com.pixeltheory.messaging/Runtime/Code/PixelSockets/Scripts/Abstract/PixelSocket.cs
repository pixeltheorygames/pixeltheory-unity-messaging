using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.Pool;


namespace Pixeltheory.Messaging
{
   public abstract class PixelSocket : PixelObject
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
      private List<Func<Task>> preMessageHooks = ListPool<Func<Task>>.Get();
      private List<Func<Task>> postMessageHooks = ListPool<Func<Task>>.Get();
      #endregion //Private
      #endregion //Fields
      
      #region Methods
      #region Unity Messages
      protected virtual void OnDestroy()
      {
         this.preMessageHooks.Clear();
         this.postMessageHooks.Clear();
         ListPool<Func<Task>>.Release(this.preMessageHooks);
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
      private Dictionary<int, List<TypeInterface>> channelToListenersListMap 
         = DictionaryPool<int, List<TypeInterface>>.Get();
      #endregion //Private
      #endregion //Fields

      #region Methods
      #region Unity Messages
      protected override void OnDestroy()
      {
         foreach (List<TypeInterface> channelListeners in this.channelToListenersListMap.Values)
         {
            channelListeners.Clear();
            ListPool<TypeInterface>.Release(channelListeners);
         }
         this.channelToListenersListMap.Clear();
         DictionaryPool<int, List<TypeInterface>>.Release(this.channelToListenersListMap);
         base.OnDestroy();
      }
      #endregion //Unity Messages
      
      #region Public
      public void Bind(TypeInterface listener, int channel)
      {
         List<TypeInterface> currentListeners;
         if (!this.channelToListenersListMap.TryGetValue(channel, out currentListeners))
         {
            currentListeners = ListPool<TypeInterface>.Get();
            this.channelToListenersListMap.Add(channel, currentListeners);
         }
         currentListeners.Add(listener);
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