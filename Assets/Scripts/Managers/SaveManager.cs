using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class SaveManager
{
    private PlayerData _data;

    public PlayerData PlayerData => _data;

    public void Initialize()
    {
        _data = LoadData();
    }

    public void UpdatePlayerCoins(int coins)
    {
        _data.UpdateCoins(coins);
    }

    public void SetLevelAvaliable(int index, int coinsCost)
    {
        _data.SetLevelAvaliable(index, coinsCost);
        ApplicationController.Instance.Managers.EventManager.OnUpdatePlayerState?.Invoke(this, new KeyValuePair<ObjectType, int>(ObjectType.Coin, _data.Coins));
    }
    public void Save()
    {
        SaveData(_data);
    }
    private void SaveData(PlayerData data)
    {
        var savePath = Application.persistentDataPath + ApplicationController.Instance.Constants.SavePath;
        using (FileStream fs = new FileStream(savePath, FileMode.Create))
        {
            using (BinaryWriter bw = new BinaryWriter(fs, Encoding.UTF8, false))
            {
                var jsonData = JsonUtility.ToJson(data);
                fs.SetLength(0);
                bw.Write(jsonData);
            }
        }
    }

    public PlayerData LoadData()
    {
        var failedToLoad = false;
        PlayerData result = new PlayerData();
        result.AvaliableLevels.Add(0);
        var savePath = Application.persistentDataPath + ApplicationController.Instance.Constants.SavePath;
        using (FileStream fs = new FileStream(savePath, FileMode.OpenOrCreate))
        {
            using (BinaryReader br = new BinaryReader(fs, Encoding.UTF8, false))
            {
                while (br.BaseStream.Length != br.BaseStream.Position)
                {
                    try
                    {
                        var jsonData = br.ReadString();
                        result = JsonUtility.FromJson<PlayerData>(jsonData);
                    }
                    catch
                    {
                        Debug.LogError("Save data corrupted!");
                        failedToLoad = true;
                        br.Close();
                        break;
                    }
                }
            }
        }
        if (failedToLoad)
        {
            SaveData(result);
        }
        return result;
    }
}

[System.Serializable]
public struct PlayerData
{
    [SerializeField]
    private int _coins;
    [SerializeField]
    private List<int> _avaliableLevels;

    public int Coins => _coins;
    public List<int> AvaliableLevels
    {
        get
        {
            if(_avaliableLevels == null)
            {
                _avaliableLevels = new List<int>();
            }
            return _avaliableLevels;
        }
    }

    public void UpdateCoins(int coinsChange)
    {
        _coins += coinsChange;
    }

    public void SetLevelAvaliable(int levelIndex, int coinsCost)
    {
        if(_avaliableLevels.Contains(levelIndex))
        {
            return;
        }
        _avaliableLevels.Add(levelIndex);
        UpdateCoins(-coinsCost);
    }

    public bool IsLevelAvaliable(int levelIndex)
    {
        return _avaliableLevels.Contains(levelIndex);
    }
}
