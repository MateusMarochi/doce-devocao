using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

[Serializable]
public class ProductDatabase
{
    public static int[] productValues = new int[5];

    public static string ToString()
    {
        return string.Join(", ", productValues);
    }
}

public class DataPersistence : MonoBehaviour
{
    private BinaryFormatter formatter = new BinaryFormatter();
    private string dataPath;
    private int[] lastSavedValues;

    private void Awake()
    {
        dataPath = Application.persistentDataPath + "/productData.dat";
        LoadData();

        // Invoca a função SaveData a cada 5 segundos
        InvokeRepeating("SaveData", 2f, 1f);
    }

    private void SaveData()
    {
        // Verifica se os dados são diferentes dos dados salvos anteriormente
        if (lastSavedValues == null || !lastSavedValues.SequenceEqual(ProductDatabase.productValues))
        {
            using (FileStream fileStream = File.Create(dataPath))
            {
                try
                {
                    // Serializa os dados em formato binário
                    formatter.Serialize(fileStream, ProductDatabase.productValues);

                    // Atualiza a cópia dos dados salvos
                    lastSavedValues = (int[])ProductDatabase.productValues.Clone();
                }
                catch (Exception e)
                {
                    Debug.LogError("Failed to save data: " + e.Message);
                }
            }
            // Debug.Log(ProductDatabase.ToString());
        }
    }

    private void LoadData()
    {
        // Verifica se o arquivo de dados existe
        if (File.Exists(dataPath))
        {
            using (FileStream fileStream = File.Open(dataPath, FileMode.Open))
            {
                try
                {
                    // Desserializa os dados do arquivo binário
                    ProductDatabase.productValues = (int[])formatter.Deserialize(fileStream);

                    // Atualiza a cópia dos dados salvos
                    lastSavedValues = (int[])ProductDatabase.productValues.Clone();
                }
                catch (Exception e)
                {
                    Debug.LogError("Failed to load data: " + e.Message);
                }
            }
        }
    }
}
