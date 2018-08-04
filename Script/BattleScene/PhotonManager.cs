using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonManager : Photon.MonoBehaviour {

    [SerializeField] BattleScene.BattleSceneManager m_BattleSceneManager;
    [SerializeField] Button m_RoomCreateEnter;
    [SerializeField] Text m_RoomState;

    public void ConnectPhoton()
    {
        PhotonNetwork.ConnectUsingSettings("v1.0");
    }

    public void CreateRoom()
    {
        string userName = "ユーザー1";
        string userId = "user1";
        PhotonNetwork.autoCleanUpPlayerObjects = false;

        // カスタムプロパティ
        ExitGames.Client.Photon.Hashtable customProp = new ExitGames.Client.Photon.Hashtable();
        customProp.Add("userName", userName);
        customProp.Add("userId", userId);
        PhotonNetwork.SetPlayerCustomProperties(customProp);

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.CustomRoomProperties = customProp;
        // ロビーで見えるルーム情報としてカスタムプロパティのuserName,userIdを使うことを宣言
        roomOptions.CustomRoomPropertiesForLobby = new string[] { "userName", "userId" };
        roomOptions.MaxPlayers = 2;     // 部屋の最大人数
        roomOptions.IsOpen = true;      // 入室許可
        roomOptions.IsVisible = true;   // ロビーから見えるように
        // userIdが名前のルームがなければ作って入室、あれば普通に入室
        PhotonNetwork.JoinOrCreateRoom(userId, roomOptions, null);
    }

    public void JoinRoom()
    {
        var success = PhotonNetwork.JoinRoom("user1");
        //if(success) BattleScene.BattleSceneManager.SceneManager.
    }

    public void GameStart()
    {
        this.gameObject.SetActive(false);
        m_BattleSceneManager.GameStart();
    }

    private void OnJoinRoom()
    {
        Debug.Log("PhotonManager On Join Room");
        m_RoomState.text = "On Join Room";
    }

    private void OnJoinedLobby()
    {
        Debug.Log("Photonmanager on Joined Lobby");
        m_RoomCreateEnter.interactable = true;
    }

    private void OnJoinedRoom()
    {
        Debug.Log("PhotonManager on Joined Room");
        m_BattleSceneManager.Initialize();

    }

    private void OnReceivedRoomListUpdate()
    {
        // ルーム一覧をとる
        RoomInfo[] rooms = PhotonNetwork.GetRoomList();
        if(rooms.Length == 0)
        {
            Debug.Log("ルームが一つもありません");
        }
        else
        {
            // ルームが１件以上あるときでRoomInfo情報をログ出力
            for(int i = 0; i< rooms.Length; i++)
            {
                Debug.Log("RoomName:" + rooms[i].Name);
                Debug.Log("userName:" + rooms[i].CustomProperties["userName"]);
                Debug.Log("UserId:" + rooms[i].CustomProperties["userId"]);
                m_RoomState.text = rooms[i].Name;
            }
        }
    }
}
