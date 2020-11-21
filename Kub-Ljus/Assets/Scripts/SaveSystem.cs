using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//en static class så alla object kan nå den
public static class SaveSystem
{
    
    //void som sparar level data (en bool array) i en fil som är binär
    //Jag vet att jag inte i detta fall behöver spara datan i en binär fil, men jag hade sparat brackeys tutorial sen innan och årkade inte hitta ett annat sätt
    public static void SaveLevels(bool[] array, int[] clicks)
    {
        //öh vet inte riktigt vad formatter ska betyda, men om jag skulle gissa så är "formattern" en sorts blueprint för streamen som säger åt den att spara i binärt
        BinaryFormatter formatter = new BinaryFormatter();

        //skapar en path till vart filen ska sparas på datorn
        //Application.persistentDataPath ger path som är konsekvent
        string path = Application.persistentDataPath + "/hejhej.fun";
        //googlade och det är tydligen här den tar en C:\Users\username\AppData\Local\company\game


        //Skapar en stream som ska skapa en fil och som skapar vid pathen ovanför
        FileStream stream = new FileStream(path, FileMode.Create);

        //skapar en ny SaveData variabel
        SaveData data = new SaveData(array,clicks);

       
        //Skapar filen
        formatter.Serialize(stream, data);

        
        //stänger ner streamen
        stream.Close();
    }

    public static SaveData LoadLevelData()
    {
        string path = Application.persistentDataPath + "/hejhej.fun";

        //kollar om det finns en fil i denna path
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            //skapar en stream som ska öppna en fil
            FileStream stream = new FileStream(path, FileMode.Open);

            //skapar en SaveData variabel som får värdet som filen har
            SaveData data = formatter.Deserialize(stream) as SaveData;

            //stänger ner streamen
            stream.Close();

            //skickar SaveDatan
            return data;

            
        }
        else
        {
            Debug.Log("Filen finns inte");

            //skickar tillbaka null eftersom det inte finns någon fil
            return null;
        }
    }

    //Hemligt som bara är till för mig
    public static void Delete()
    {
        string path = Application.persistentDataPath + "/hejhej.fun";
        if (File.Exists(path)) File.Delete(path);
    }
}
