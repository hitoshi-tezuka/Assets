using System.Collections.Generic;
using UnityEngine;
using Photon;
using ExitGames.Client.Photon;
using System.Linq;

namespace BattleScene
{
    public class BattleTurnManager : PunBehaviour
    {

        /// <summary>
        /// Wraps accessing the "turn" custom properties of a room.
        /// </summary>
        /// <value>The Player OwnerID</value>
        public PhotonPlayer PlayerTurn
        {
            get { return PhotonNetwork.room.GetPlayer(); }
            private set { PhotonNetwork.room.SetPlayer(value, true); }
        }

        // 1ターンの時間
        public float TurnDuration = 20f;

        // 経過時間
        public float ElapsedTimeInTurn
        {
            get { return ((float)(PhotonNetwork.ServerTimestamp - PhotonNetwork.room.GetTurnStart()) / 1000.0f); }
        }

        // 残り時間
        public float RemainingSecondsInTurn
        {
            get { return Mathf.Max(0f, this.TurnDuration - this.ElapsedTimeInTurn); }
        }

        // 制限時間超過判定
        public bool IsOver
        { get { return this.RemainingSecondsInTurn <= 0f; } }

        public ITurnManagerCallbacks TurnManagerListener;
        private int m_PlayerIndex = 1;

        private void Start()
        {

        }

        private void Update()
        {
            if (false)//IsOver)
            {
                this.TurnManagerListener.OnTurnTimeEnds();
            }
        }

        public override void OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged)
        {
            if(propertiesThatChanged.ContainsKey(BattleTurnExtensions.PlayerPropKey))
            {
                TurnManagerListener.OnTurnBegins();
                Debug.Log("m_PlayerIndex : " + m_PlayerIndex);
                m_PlayerIndex++;
                if (m_PlayerIndex > PhotonNetwork.playerList.Length) m_PlayerIndex = 1;
            }
        }

        public void BeginTurn()
        {
            PlayerTurn = PhotonPlayer.Find(m_PlayerIndex);
        }
    }

    public interface ITurnManagerCallbacks
    {
        // ターン開始時
        void OnTurnBegins();
        // ターン完了後
        void OnTurnCompleted();
        // 残り時間なしでターン終了
        void OnTurnTimeEnds();
    }

    public static class BattleTurnExtensions
    {
        /// <summary>
        /// プロパティキー（ターン数）
        /// </summary>
        public static readonly string PlayerPropKey = "Player";

        /// <summary>
        /// プロパティキー（サーバータイム）
        /// </summary>
        public static readonly string TurnStartPropKey = "TStart";

        /// <summary>
        /// プロパティキー（ターン終了）
        /// </summary>
        public static readonly string FinishedTurnPropKey = "FToA";

        /// <summary>
        /// Sets the turn.
        /// </summary>
        /// <param name="room">Room reference</param>
        /// <param name="turn">Turn index</param>
        /// <param name="setStartTime">If set to <c>true</c> set start time.</param>
        public static void SetPlayer(this Room room, PhotonPlayer player, bool setStartTime = false)
        {
            if (room == null || room.CustomProperties == null)
            {
                return;
            }

            Hashtable PlayerProps = new Hashtable();
            PlayerProps[PlayerPropKey] = player;
            if (setStartTime)
            {
                PlayerProps[TurnStartPropKey] = PhotonNetwork.ServerTimestamp;
            }

            room.SetCustomProperties(PlayerProps);
        }

        /// <summary>
        /// Gets the current Player turn from a RoomInfo
        /// </summary>
        /// <returns>The turn index </returns>
        /// <param name="room">RoomInfo reference</param>
        public static PhotonPlayer GetPlayer(this RoomInfo room)
        {
            if (room == null || room.CustomProperties == null || !room.CustomProperties.ContainsKey(PlayerPropKey))
            {
                return null;
            }
            return (PhotonPlayer)room.CustomProperties[PlayerPropKey];
        }


        /// <summary>
        /// Returns the start time when the turn began. This can be used to calculate how long it's going on.
        /// </summary>
        /// <returns>The turn start.</returns>
        /// <param name="room">Room.</param>
        public static int GetTurnStart(this RoomInfo room)
        {
            if (room == null || room.CustomProperties == null || !room.CustomProperties.ContainsKey(TurnStartPropKey))
            {
                return 0;
            }

            return (int)room.CustomProperties[TurnStartPropKey];
        }

        /// <summary>
        /// gets the player's finished turn (from the ROOM properties)
        /// </summary>
        /// <returns>The finished turn index</returns>
        /// <param name="player">Player reference</param>
        //public static int GetFinishedTurn(this PhotonPlayer player)
        //{
        //    Room room = PhotonNetwork.room;
        //    if (room == null || room.CustomProperties == null || !room.CustomProperties.ContainsKey(TurnPropKey))
        //    {
        //        return 0;
        //    }

        //    string propKey = FinishedTurnPropKey + player.ID;
        //    return (int)room.CustomProperties[propKey];
        //}

        /// <summary>
        /// Sets the player's finished turn (in the ROOM properties)
        /// </summary>
        /// <param name="player">Player Reference</param>
        /// <param name="turn">Turn Index</param>
        //public static void SetFinishedTurn(this PhotonPlayer player, int turn)
        //{
        //    Room room = PhotonNetwork.room;
        //    if (room == null || room.CustomProperties == null)
        //    {
        //        return;
        //    }

        //    string propKey = FinishedTurnPropKey + player.ID;
        //    Hashtable finishedTurnProp = new Hashtable();
        //    finishedTurnProp[propKey] = turn;

        //    room.SetCustomProperties(finishedTurnProp);
        //}
    }
}
