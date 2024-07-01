using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class CSVReader
{
    public static List<string[]> getData(string path, string splitStr = ", ")
    {
        if (path == "")
        {
            throw new Exception("should be pass csv path.");
        }
        List<string[]> data = new List<string[]>();
        TextAsset csv = Resources.Load(path) as TextAsset;
        StringReader reader = new StringReader(csv.text);
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            string[] items = line.Split(splitStr.ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries);
            data.Add(items);
        }
        return data;
    }
}