using System;
using System.Collections.Generic;
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
         PreMessageHook,
         PostMessageHook
      }
      #endregion //Public
      #endregion //Fields
      #endregion //Class

      #region Instance
      #region Fields
      #region Private
      private readonly Dictionary<int, Action> idToPreMessageHookActionMap = new Dictionary<int, Action>();
      private readonly Dictionary<int, Action> idToPostMessageHookActionMap = new Dictionary<int, Action>();
      private readonly Dictionary<int, Dictionary<int, TypeInterface>> channelToIDAndListenersMap
         = new Dictionary<int, Dictionary<int, TypeInterface>>();
      private readonly Dictionary<int, Dictionary<int, TypeInterface>.ValueCollection> channelToListenersCollectionMap
         = new Dictionary<int, Dictionary<int, TypeInterface>.ValueCollection>();
      #endregion //Private
      #endregion //Fields

      #region Methods
      #region Public
      public int AttachMessageHook(HookTypeFlag hookTypeFlag, Action callback)
      {
         int callbackID = callback.GetHashCode();
         if (hookTypeFlag.HasFlag(HookTypeFlag.PreMessageHook))
         {
            this.idToPreMessageHookActionMap[callbackID] = callback;
         }
         if (hookTypeFlag.HasFlag(HookTypeFlag.PostMessageHook))
         {
            this.idToPostMessageHookActionMap[callbackID] = callback;
         }
         return callbackID;
      }

      public bool RemoveMessageHook(HookTypeFlag hookTypeFlag, int callbackID)
      {
         bool removed = false;
         if (hookTypeFlag.HasFlag(HookTypeFlag.PreMessageHook))
         {
            if (this.idToPreMessageHookActionMap.ContainsKey(callbackID))
            {
               removed |= this.idToPreMessageHookActionMap.Remove(callbackID);
            }
         }
         if (hookTypeFlag.HasFlag(HookTypeFlag.PostMessageHook))
         {
            if (this.idToPostMessageHookActionMap.ContainsKey(callbackID))
            {
               removed |= this.idToPostMessageHookActionMap.Remove(callbackID);
            }
         }
         return removed;
      }

      public void Bind(TypeInterface listener, int channel)
      {
         Dictionary<int, TypeInterface> idAndListeners;
         if (this.channelToIDAndListenersMap.TryGetValue(channel, out idAndListeners))
         {
            idAndListeners.Add(listener.GetHashCode(), listener);
         }
         else
         {
            idAndListeners = DictionaryPool<int, TypeInterface>.Get();
            idAndListeners.Add(listener.GetHashCode(), listener);
            this.channelToIDAndListenersMap.Add(channel, idAndListeners);
            this.channelToListenersCollectionMap.Add(channel, idAndListeners.Values);
         }
      }

      public void Unbind(TypeInterface listener, int channel)
      {
         Dictionary<int, TypeInterface> idAndListeners;
         if (this.channelToIDAndListenersMap.TryGetValue(channel, out idAndListeners))
         {
            idAndListeners.Remove(listener.GetHashCode());
            if (idAndListeners.Count == 0)
            {
               this.channelToIDAndListenersMap.Remove(channel);
               this.channelToListenersCollectionMap.Remove(channel);
               DictionaryPool<int, TypeInterface>.Release(idAndListeners);
            }
         }
         else
         {
            // If we get here, we are trying to unbind a listener that never bound itself.
            // Something has gone wrong. We can use this opportunity to iterate through all
            // channels and remove any empty listener lists. By doing so, hopefully we get
            // the state of the channels and listeners back in sync with what is expected.
            List<int> channelsToRemove = ListPool<int>.Get();
            foreach (KeyValuePair<int, Dictionary<int, TypeInterface>> channelListenersPair in this.channelToIDAndListenersMap)
            {
               if (channelListenersPair.Value.Count == 0)
               {
                  channelsToRemove.Add(channelListenersPair.Key);
               }
            }
            for (int i = 0; i < channelsToRemove.Count; i++)
            {
               int emptyChannel = channelsToRemove[i];
               Dictionary<int, TypeInterface> emptyListeners = this.channelToIDAndListenersMap[emptyChannel];
               this.channelToIDAndListenersMap.Remove(emptyChannel);
               this.channelToListenersCollectionMap.Remove(emptyChannel);
               DictionaryPool<int, TypeInterface>.Release(emptyListeners);
            }
            ListPool<int>.Release(channelsToRemove);
         }
      }
      #endregion //Public

      #region Protected
      protected Dictionary<int, Action>.ValueCollection GetPreMessageHooks()
      {
         return this.idToPreMessageHookActionMap.Values;
      }

      protected Dictionary<int, Action>.ValueCollection GetPostMessageHooks()
      {
         return this.idToPostMessageHookActionMap.Values;
      }

      protected Dictionary<int, TypeInterface>.ValueCollection GetListenersCollection(int channel)
      {
         Dictionary<int, TypeInterface>.ValueCollection listeners;
         if (!this.channelToListenersCollectionMap.TryGetValue(channel, out listeners))
         {
            Dictionary<int, TypeInterface> idToListenersMap = DictionaryPool<int, TypeInterface>.Get();
            this.channelToIDAndListenersMap.Add(channel, idToListenersMap);
            this.channelToListenersCollectionMap.Add(channel, idToListenersMap.Values);
            listeners = idToListenersMap.Values;
         }
         return listeners;
      }
      #endregion //Protected
      #endregion //Method
      #endregion
   }
}