﻿using UnityEngine;
using System.Collections;
using LiteNetLib;
using System.Collections.Generic;
using LiteNetLib.Utils;

namespace TinyBirdNet {

	public abstract class TinyNetManager : System.Object, INetEventListener {

		public virtual string TYPE { get { return "Abstract"; } }

		public HashSet<NetPeer> _peers { get; protected set; }
		protected NetManager _netManager;

		public TinyNetManager() {
			_peers = new HashSet<NetPeer>();
		}

		/// <summary>
		/// It is called from TinyNetGameManager Update(), handles PollEvents().
		/// </summary>
		public void InternalUpdate() {
			if (_netManager != null) {
				_netManager.PollEvents();
			}
		}

		public virtual void ClearNetManager() {
			if (_netManager != null) {
				_netManager.Stop();
			}
		}

		protected virtual void ConfigureNetManager(bool bUseFixedTime) {
			if (bUseFixedTime) {
				_netManager.UpdateTime = Mathf.FloorToInt(Time.fixedDeltaTime * 1000);
			} else {
				_netManager.UpdateTime = 15;
			}

			_netManager.NatPunchEnabled = TinyNetGameManager.instance.bNatPunchEnabled;
		}

		public virtual void ToggleNatPunching(bool bNewState) {
			_netManager.NatPunchEnabled = bNewState;
		}

		//============ INetEventListener methods ============//

		public virtual void OnPeerConnected(NetPeer peer) {
			Debug.Log("[" + TYPE + "] We have new peer: " + peer.EndPoint);
			_peers.Add(peer);
		}

		public virtual void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo) {
			Debug.Log("[" + TYPE + "] disconnected from: " + peer.EndPoint + "because " + disconnectInfo.Reason);
			_peers.Remove(peer);
		}

		public virtual void OnNetworkError(NetEndPoint endPoint, int socketErrorCode) {
			Debug.Log("[" + TYPE + "] error " + socketErrorCode + " at: " + endPoint);
		}

		public virtual void OnNetworkReceiveUnconnected(NetEndPoint remoteEndPoint, NetDataReader reader, UnconnectedMessageType messageType) {
			Debug.Log("[" + TYPE + "] Received Unconnected message from: " + remoteEndPoint);

			if (messageType == UnconnectedMessageType.DiscoveryRequest) {
				OnDiscoveryRequestReceived(remoteEndPoint, reader);
			}
		}

		public virtual void OnNetworkLatencyUpdate(NetPeer peer, int latency) {
			Debug.Log("[" + TYPE + "] Latency update for peer: " + peer.EndPoint + " " + latency + "ms");
		}

		public virtual void OnNetworkReceive(NetPeer peer, NetDataReader reader) {
			Debug.Log("[" + TYPE + "] On network receive from: " + peer.EndPoint);
		}

		//============ Network Events ============//

		protected virtual void OnDiscoveryRequestReceived(NetEndPoint remoteEndPoint, NetDataReader reader) {
			Debug.Log("[" + TYPE + "] Received discovery request. Send discovery response");
			_netManager.SendDiscoveryResponse(new byte[] { 1 }, remoteEndPoint);
		}
	}
}