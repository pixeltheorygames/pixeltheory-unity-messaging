using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.Pool;


namespace Pixeltheory.Messaging
{
   public abstract class PixelSocket<TypeInterface> : PixelObject
   {
      #region Class
      #region Fields
      #region Public
      [Flags]
      public enum HookTypeFlag
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
      private Dictionary<int, List<TypeInterface>> channelToListenersListMap;

      private readonly Dictionary<int, Dictionary<int, TypeInterface>> channelToIDAndListenersMap
         = new Dictionary<int, Dictionary<int, TypeInterface>>();
      private readonly Dictionary<int, Dictionary<int, TypeInterface>.ValueCollection> channelToListenersCollectionMap
         = new Dictionary<int, Dictionary<int, TypeInterface>.ValueCollection>();
      #endregion //Private
      #endregion //Fields

      #region Methods
      #region Unity Messages
      private void OnEnable()
      {
         this.preMessageHooks = ListPool<Func<Task>>.Get();
         this.postMessageHooks = ListPool<Func<Task>>.Get();
         this.channelToListenersListMap = DictionaryPool<int, List<TypeInterface>>.Get();
      }

      private void OnDisable()
      {
         this.preMessageHooks.Clear();
         ListPool<Func<Task>>.Release(this.preMessageHooks);
         this.postMessageHooks.Clear();
         ListPool<Func<Task>>.Release(this.postMessageHooks);
         foreach (List<TypeInterface> channelListeners in this.channelToListenersListMap.Values)
         {
            channelListeners.Clear();
            ListPool<TypeInterface>.Release(channelListeners);
         }
         this.channelToListenersListMap.Clear();
      }
      #endregion //Unity Messages
      
      #region Public
      public void AttachMessageHook(HookTypeFlag hookTypeFlag, Func<Task> callback)
      {
         if (hookTypeFlag.HasFlag(HookTypeFlag.PreMessageHook))
         {
            if (!this.preMessageHooks.Contains(callback))
            {
               this.preMessageHooks.Add(callback);  
            }
         }
         if (hookTypeFlag.HasFlag(HookTypeFlag.PostMessageHook))
         {
            if (!this.postMessageHooks.Contains(callback))
            {
               this.postMessageHooks.Add(callback);  
            }
         }
      }

      public void RemoveMessageHook(HookTypeFlag hookTypeFlag, Func<Task> callback)
      {
         if (hookTypeFlag.HasFlag(HookTypeFlag.PreMessageHook))
         {
            this.preMessageHooks.Remove(callback);
         }
         if (hookTypeFlag.HasFlag(HookTypeFlag.PostMessageHook))
         {
            this.postMessageHooks.Remove(callback);
         }
      }

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

      protected bool GetListeners(int channelID, out List<TypeInterface> listeners)
      {
         return this.channelToListenersListMap.TryGetValue(channelID, out listeners);
      }
      #endregion //Protected
      #endregion //Method
      #endregion
   }
}