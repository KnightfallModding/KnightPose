using Il2Cpp;

namespace KnightPose;

public static class ProgressHelper
{
    public static void AddWin(ulong friendSteamID)
    {
        FriendProgress friendProgress = FriendProgressHandler.Instance.GetFriendProgress(friendSteamID);
        byte progress = friendProgress.Progress;

        // Prevent byte overflow error. Max wins is 255.
        if (progress < 255)
        {
            friendProgress.Progress = (byte)(progress + 1);
        }

        FriendProgressHandler.Instance.AddProgress(friendSteamID, progress);
    }

    public static int GetPoseTier(int progress)
    {
        // New poses are unlocked:
        // 0. default
        // 1. 1 win
        // 2. 3 wins
        // 3. 8 wins
        // 4. 15 wins
        // 5. 25+ wins
        // Last pose existing in the game is 5.

        return progress switch
        {
            0 => 0,    // No win
            < 3 => 1,  // [1; 2]
            < 8 => 2,  // [3; 7]
            < 15 => 3, // [8; 14]
            < 25 => 4, // [15;24]
            _ => 5     // [25; +]
        };
    }
}