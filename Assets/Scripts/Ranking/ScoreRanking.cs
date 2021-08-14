using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class ScoreRanking : MonoBehaviour
{
    private static readonly int[] playerData = new int[2];
    public static void SendPlayScore(int highScore)
    {
        var statisticUpdate = new StatisticUpdate
        {
            // 統計情報名を指定します。
            StatisticName = "HighScoreRanking",
            Value = highScore,
        };

        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                statisticUpdate
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request, OnSuccess, OnError);

        void OnSuccess(UpdatePlayerStatisticsResult result)
        {
            Debug.Log("success!");
        }

        void OnError(PlayFabError error)
        {
            Debug.Log($"{error.Error}");
        }
    }
    
    
    private void GetRateRanking()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "HighScoreRanking", // 統計情報名を指定します。
            StartPosition = 0, // 何位以降のランキングを取得するか指定します。
            MaxResultsCount = 100 // ランキングデータを何件取得するか指定します。最大が100です。
        };

        PlayFabClientAPI.GetLeaderboard(request, OnSuccess, OnError);

        void OnSuccess(GetLeaderboardResult leaderboardResult)
        {
            // 実際は良い感じのランキングを表示するコードにします。
            foreach (var item in leaderboardResult.Leaderboard)
            {
                // Positionは順位です。0から始まるので+1して表示しています。
                Debug.Log($"{item.Position + 1}位: {item.DisplayName} - {item.StatValue}回");
            }
        }

        void OnError(PlayFabError error)
        {
            Debug.Log($"{error.Error}");
        }
    }
    
     public static int[] GetPlayerData()
    {
        var request = new GetLeaderboardAroundPlayerRequest()
        {
            StatisticName = "HighScoreRanking",
            MaxResultsCount = 1 
        };

        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnSuccess, OnError);
        
        return playerData;
        
        void OnSuccess(GetLeaderboardAroundPlayerResult leaderboardResult)
        {
            Debug.Log($"取得成功!playerHighScore:{leaderboardResult.Leaderboard[0].StatValue}player順位:{leaderboardResult.Leaderboard[0].Position}");
            playerData[0] = leaderboardResult.Leaderboard[0].StatValue;
            playerData[1] = leaderboardResult.Leaderboard[0].Position;
        }

        void OnError(PlayFabError error)
        {
            Debug.Log($"{error.Error}");
        }
    }
}
