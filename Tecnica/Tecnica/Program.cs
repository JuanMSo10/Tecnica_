using System;
using System.Collections.Generic;
using System.Linq;

public class WordFinder
{
    // Matriz de caracteres
    private readonly char[,] matrix;
    private readonly int rows;
    private readonly int cols;

    // Constructor que toma una matriz de cadenas como entrada
    public WordFinder(IEnumerable<string> matrix)
    {
        this.cols = matrix.First().Length;
        this.rows = matrix.Count();
        this.matrix = new char[rows, cols];

        // Poblar la matriz con caracteres
        for (int i = 0; i < rows; i++)
        {
            string row = matrix.ElementAt(i);
            for (int j = 0; j < cols; j++)
            {
                this.matrix[i, j] = row[j];
            }
        }
    }

    // Método para buscar palabras en la matriz
    public IEnumerable<string> Find(IEnumerable<string> wordstream)
    {
        var wordCount = new Dictionary<string, int>();

        foreach (string word in wordstream)
        {
            // Búsqueda horizontal
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j <= cols - word.Length; j++)
                {
                    string horizontal = new string(Enumerable.Range(j, word.Length).Select(col => matrix[i, col]).ToArray());
                    if (horizontal == word)
                    {
                        if (!wordCount.ContainsKey(word))
                        {
                            wordCount[word] = 1;
                        }
                        break;
                    }
                }
            }

            // Búsqueda vertical
            for (int i = 0; i <= rows - word.Length; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    string vertical = new string(Enumerable.Range(i, word.Length).Select(row => matrix[row, j]).ToArray());
                    if (vertical == word)
                    {
                        if (!wordCount.ContainsKey(word))
                        {
                            wordCount[word] = 1;
                        }
                        break;
                    }
                }
            }
        }

        // Obtener las 10 palabras más repetidas
        var topWords = wordCount.OrderByDescending(pair => pair.Value).Take(10).Select(pair => pair.Key);

        return topWords;
    }

}
