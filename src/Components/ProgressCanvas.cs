using Il2Cpp;
using UnityEngine;

namespace KnightPose;

public class ProgressCanvas : MonoBehaviour
{
    private void OnGUI()
    {
        // Only show in the lobby scene
        if (PhotonServerConnector.m_KnightFriend == null)
            return;
        
        // Wait for the friend progress dictionary to be initialized
        if
        (
            FriendProgressHandler.Instance.FriendProgressDic == null ||
            !FriendProgressHandler.Instance.FriendProgressDic.
            ContainsKey(PhotonServerConnector.m_KnightFriend.SteamID)
        )
            return;

        ulong steamID = PhotonServerConnector.m_KnightFriend.SteamID;
        string friendName = PhotonServerConnector.m_KnightFriend.PlayerName;
        int wins = FriendProgressHandler.Instance.FriendProgressDic[steamID].Progress;

        // Set position to the bottom-right of the screen
        GUIStyle style = new();
        style.fontSize = 20;
        style.richText = false;
        style.normal.textColor = Color.white;


        float rightPadding = 20f;
        float bottomPadding = 25f;
        string text = $"Duo wins with {friendName}: {wins}";

        // Calculate the size of the text
        Vector2 size = style.CalcSize(new GUIContent(text));

        // Draw the text in the bottom-right corner
        GUI.Label(new Rect(Screen.width - size.x - rightPadding, Screen.height - size.y - bottomPadding, size.x, size.y), text, style);
    }
}