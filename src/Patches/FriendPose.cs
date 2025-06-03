using Steamworks;
using HarmonyLib;
using Landfall.Poses;

namespace KnightPose;

[HarmonyPatch]
public static class FriendPose
{
    [HarmonyPatch(typeof(PhotonServerConnector), nameof(PhotonServerConnector.AddProgressForFriend))]
    [HarmonyPrefix]
    public static bool AddProgressForFriendPatch()
    {
        CSteamID steamLobbyID = PhotonServerConnector.SteamLobby.CurrentLobby;

        // Always 0 outside of lobby scene. Can't use this.
        // int nbPlayers = PhotonServerConnector.SteamLobby.NumberOfMembers;

        int nbPlayers = SteamMatchmaking.GetNumLobbyMembers(steamLobbyID);
        for (int i = 0; i < nbPlayers; i++)
        {
            CSteamID lobbyMember = SteamMatchmaking.GetLobbyMemberByIndex(steamLobbyID, i);

            if (lobbyMember != SteamUser.GetSteamID() && !(lobbyMember == (CSteamID)0UL))
                ProgressHelper.AddWin(lobbyMember.m_SteamID);
        }
        return false;
    }

    [HarmonyPatch(typeof(PhotonServerConnector), nameof(PhotonServerConnector.UpdateFriendPose))]
    [HarmonyPrefix]
    public static bool UpdateFriendPosePatch(PhotonServerConnector __instance)
    {
        /*  
            * We are forced to rewrite this function to call
            * the patched GetPoseTier function, because the GetPoseTier function
            * is not called (and thus, not even present) in the Il2Cpp build.
        */

        if (PhotonServerConnector.m_KnightFriend == null)
        {
            __instance.localArmorKnight.GetComponent<Poseable>().ChangePose(0);
            return false;
        }

        Poseable poseable1 = __instance.localArmorKnight.GetComponent<Poseable>();
        Poseable poseable2 = __instance.m_FriendJoinedObject.GetComponent<Poseable>();

        // Crashes immediately without logging error.
        // Probably missing parts of the function due to Il2Cpp.
        // int poseTier = __instance.GetPoseTier((int)progress);
        ulong friendSteamID = PhotonServerConnector.m_KnightFriend.SteamID;
        byte progress = FriendProgressHandler.Instance.GetFriendProgress(friendSteamID).Progress;

        // Crashes immediately without logging error.
        // Probably missing parts of the function due to Il2Cpp.
        // int poseTier = __instance.GetPoseTier((int)progress);
        int poseTier = ProgressHelper.GetPoseTier((int)progress);

        poseable1.ChangePose(poseTier);
        poseable2.ChangePose(poseTier);

        return false;
    }
}