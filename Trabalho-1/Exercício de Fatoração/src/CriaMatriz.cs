namespace Matriz;

class Matriz
{
    public static int[,] CriaMatriz(int tamanho) // Função estática para criar matriz.
    {
        int[,] matriz = new int[tamanho, tamanho]; // Criação de matriz regular.

        for(int linha = 0; linha < tamanho; linha++) // For para entrar em cada linha
        {
            for(int coluna = 0; coluna < tamanho; coluna++) // For para entrar em cada coluna (alternando os valores)
            {
                Console.WriteLine($"Informe o valor da matriz para a linha {linha} e coluna {coluna}");
                matriz[linha, coluna] = int.Parse(Console.ReadLine()); // Altera o valor do elemento baseado na posição da linha e coluna atual.
            }
        }

        return matriz; // Retorna a matriz regular.
    }
}