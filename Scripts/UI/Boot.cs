using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Linq;

public class Boot : MonoBehaviour {
    public GameObject LoginWin;
    public GameObject LobbyWin;
    

    string[] CommandLineArgs;

    // Use this for initialization
    void Start () {
        CommandLineArgs = Environment.GetCommandLineArgs();

        Debug.Log(CommandLineArgs);
        string username;
        if ((username = ParseArg("--username")) != null) {
            
            //连接Photon服务器
            PhotonNetwork.ConnectUsingSettings("1.0");
            //PhotonNetwork.automaticallySyncScene = false;
            PhotonNetwork.autoCleanUpPlayerObjects = false;   //玩家离开时不清除游戏对象
            PhotonNetwork.player.NickName = username;          //设置玩家昵称
            Debug.Log(PhotonNetwork.connectionStateDetailed.ToString());
            LoginWin.SetActive(false);
            LobbyWin.SetActive(true);
        }
        else
        {
            LoginWin.SetActive(true);
            LobbyWin.SetActive(false);
        }
    }

    private string ParseArg(string arg) {
        for(int i = 0; i< CommandLineArgs.Length; i++)
        {
            if (CommandLineArgs[i] == arg && i != CommandLineArgs.Length -1)
                return CommandLineArgs[i + 1];
        }
        return null;
    }
}
