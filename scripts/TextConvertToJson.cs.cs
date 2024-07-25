using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;

class Program
{
    static void Main(string[] args)
    {
        string inputFile = "input.txt";
        string outputFile = "output.json";

        List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();

        // 파일 읽기
        string[] lines = File.ReadAllLines(inputFile);

        // 첫 번째 줄을 기준으로 열 이름 생성
        string[] columnNames = new string[lines[0].Split(';').Length];
        for (int i = 0; i < columnNames.Length; i++)
        {
            columnNames[i] = $"Column{i + 1}";
        }

        // 데이터 파싱
        foreach (string line in lines)
        {
            string[] values = line.Split(';');
            Dictionary<string, string> row = new Dictionary<string, string>();

            for (int i = 0; i < values.Length; i++)
            {
                row[columnNames[i]] = values[i];
            }

            data.Add(row);
        }

        // JSON으로 변환 및 파일 쓰기
        string jsonString = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(outputFile, jsonString);

        Console.WriteLine($"JSON 파일이 생성되었습니다: {outputFile}");
    }
}