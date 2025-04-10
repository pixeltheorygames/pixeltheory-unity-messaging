using System;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Pixeltheory.Debug;
using UnityEngine.Pool;


namespace Pixeltheory.Messaging
{
   public abstract class PixelSocket : PixelObject
   {
      #region Methods
      #region Public
      public abstract Awaitable Broadcast(bool parallel, CancellationToken ct);
      public abstract Awaitable Multicast(bool parallel, Type listenerType, CancellationToken ct);
      #if UNITY_EDITOR
      public abstract Awaitable Multicast(bool parallel, string typeName, CancellationToken ct);
      #endif //UNITY_EDITOR
      public abstract Awaitable Unicast(bool parallel, int listenerID, CancellationToken ct);
      #endregion //Public
      #endregion //Methods
   }
   
   public abstract class PixelSocket<T> : PixelSocket where T : PixelSocket
   {
      #region Class
      #region Fields
      #region Private
      private static readonly SemaphoreSlim asyncSemaphoreLock = new SemaphoreSlim(1);
      #endregion //Private
      #endregion //Fields
      #endregion //Class
      
      #region Instance
      #region Fields
      
      #region Public
      #region Delegates
      public delegate Awaitable PixelAction(float timestamp, T pixelSocket, CancellationToken ct);
      #endregion //Delegates
      #endregion //Public
      
      #region Private
      private T pixelSocketTypeCast;
      private List<PixelAction> broadcastListeners;
      private Dictionary<string, List<PixelAction>> multicastListeners;
      private Dictionary<int, List<PixelAction>> unicastListeners;
      #endregion //Private
      #endregion //Fields
      
      #region Methods
      #region PixelObject Overrides
      protected override void OnObjectAwake()
      {
         base.OnObjectAwake();
         this.pixelSocketTypeCast = this as T;
         this.broadcastListeners = ListPool<PixelAction>.Get();
         this.multicastListeners = DictionaryPool<string, List<PixelAction>>.Get();
         this.unicastListeners = DictionaryPool<int, List<PixelAction>>.Get();
      }
      
      protected override void OnObjectDisable()
      {
         this.broadcastListeners.Clear();
         ListPool<PixelAction>.Release(this.broadcastListeners);
         foreach (List<PixelAction> typeListeners in this.multicastListeners.Values)
         {
            typeListeners.Clear();
            ListPool<PixelAction>.Release(typeListeners);
         }
         this.multicastListeners.Clear();
         DictionaryPool<string, List<PixelAction>>.Release(this.multicastListeners);
         foreach (List<PixelAction> idListeners in this.unicastListeners.Values)
         {
            idListeners.Clear();
            ListPool<PixelAction>.Release(idListeners);
         }
         this.unicastListeners.Clear();
         DictionaryPool<int, List<PixelAction>>.Release(this.unicastListeners);
         base.OnObjectDisable();
      }
      #endregion //PixelObject Overrides
      
      #region Public
      public async Awaitable AddListener(PixelAction pixelAction, Type listenerType, int listenerID, CancellationToken ct)
      {
         await PixelSocket<T>.asyncSemaphoreLock.WaitAsync(ct);
         ct.Register(() => { PixelSocket<T>.asyncSemaphoreLock.Release(); });
         if (!this.broadcastListeners.Contains(pixelAction))
         {
            this.broadcastListeners.Add(pixelAction);
         }
         ct.ThrowIfCancellationRequested();
         if (!this.multicastListeners.ContainsKey(listenerType.Name))
         {
            this.multicastListeners.Add(listenerType.Name, ListPool<PixelAction>.Get());
         }
         ct.ThrowIfCancellationRequested();
         if (!this.multicastListeners[listenerType.Name].Contains(pixelAction))
         {
            this.multicastListeners[listenerType.Name].Add(pixelAction);  
         }
         ct.ThrowIfCancellationRequested();
         if (!this.unicastListeners.ContainsKey(listenerID))
         {
            this.unicastListeners.Add(listenerID, ListPool<PixelAction>.Get());
         }
         ct.ThrowIfCancellationRequested();
         if (!this.unicastListeners[listenerID].Contains(pixelAction))
         {
            this.unicastListeners[listenerID].Add(pixelAction);
         }
         PixelSocket<T>.asyncSemaphoreLock.Release();
      }

      public async Awaitable RemoveListener(PixelAction pixelAction, Type listenerType, int listenerID, CancellationToken ct)
      {
         await PixelSocket<T>.asyncSemaphoreLock.WaitAsync(ct);
         ct.Register(() => { PixelSocket<T>.asyncSemaphoreLock.Release(); });
         if (this.broadcastListeners.Contains(pixelAction))
         {
            this.broadcastListeners.Remove(pixelAction);
         }
         ct.ThrowIfCancellationRequested();
         if (this.multicastListeners.ContainsKey(listenerType.Name))
         {
            List<PixelAction> typeListeners = this.multicastListeners[listenerType.Name];
            if (typeListeners.Contains(pixelAction))
            {
               typeListeners.Remove(pixelAction);
            }
            if (typeListeners.Count == 0)
            {
               this.multicastListeners.Remove(listenerType.Name);
               ListPool<PixelAction>.Release(typeListeners);
            }
         }
         ct.ThrowIfCancellationRequested();
         if (this.unicastListeners.ContainsKey(listenerID))
         {
            List<PixelAction> idListeners = this.unicastListeners[listenerID];
            if (idListeners.Contains(pixelAction))
            {
               idListeners.Remove(pixelAction);
            }
            if (idListeners.Count == 0)
            {
               this.unicastListeners.Remove(listenerID);
               ListPool<PixelAction>.Release(idListeners);
            }
         }
         PixelSocket<T>.asyncSemaphoreLock.Release();
      }
      
      public override async Awaitable Broadcast(bool parallel, CancellationToken ct)
      {
         await this.SendMessage("Broadcast", parallel, this.broadcastListeners, ct);
      }
      
      public override async Awaitable Multicast(bool parallel, Type listenerType, CancellationToken ct)
      {
         await this.SendMessage("Multicast", parallel, this.multicastListeners[listenerType.Name], ct);
      }

      #if UNITY_EDITOR
      public override async Awaitable Multicast(bool parallel, string typeName, CancellationToken ct)
      {
         await this.SendMessage("Multicast", parallel, this.multicastListeners[typeName], ct);
      }
      #endif

      public override async Awaitable Unicast(bool parallel, int listenerID, CancellationToken ct)
      {
         await this.SendMessage("Unicast", parallel, this.unicastListeners[listenerID], ct);
      }
      #endregion //Public

      #region Private
      private async Awaitable SendMessage(string castTypeName, bool parallel, List<PixelAction> listeners, CancellationToken ct)
      {
         ct.Register(() => { PixelLog.Warn("[{0}] {1} operation cancelled.", this.name, castTypeName); });
         await PixelSocket<T>.asyncSemaphoreLock.WaitAsync(ct);
         ct.Register(() => { PixelSocket<T>.asyncSemaphoreLock.Release(); });
         float timestamp = Time.realtimeSinceStartup;
         List<Awaitable> listenerAwaitables = ListPool<Awaitable>.Get();
         ct.Register(() =>
         {
            listenerAwaitables.Clear();
            ListPool<Awaitable>.Release(listenerAwaitables);
         });
         if (parallel)
         {
            foreach (PixelAction pixelAction in listeners)
            {
               listenerAwaitables.Add(pixelAction(timestamp, this.pixelSocketTypeCast, ct));
            }
            ct.ThrowIfCancellationRequested();
            foreach (Awaitable pixelActionAwaitable in listenerAwaitables)
            {
               await pixelActionAwaitable;
               ct.ThrowIfCancellationRequested();
            }
         }
         else
         {
            foreach (PixelAction pixelAction in listeners)
            {
               await pixelAction(timestamp, this.pixelSocketTypeCast, ct);
               ct.ThrowIfCancellationRequested();
            }
         }
         listenerAwaitables.Clear();
         ListPool<Awaitable>.Release(listenerAwaitables);
         PixelSocket<T>.asyncSemaphoreLock.Release();
      }
      #endregion //Private
      #endregion //Methods
      #endregion //Instance
   }
}