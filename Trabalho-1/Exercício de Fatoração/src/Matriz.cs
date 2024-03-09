namespace Matriz;

class Matriz
{
    public static int[,] CriaMatriz(int tamanho) // Função estática para criar matriz.
    {
        int[,] matrizA = new int[tamanho, tamanho]; // Criação de matriz regular.

        for(int linha = 0; linha < tamanho; linha++) // For para entrar em cada linha
        {
            for(int coluna = 0; coluna < tamanho; coluna++) // For para entrar em cada coluna (alternando os valores)
            {
                Console.WriteLine($"Informe o valor da matriz para a linha {linha} e coluna {coluna}");
                matrizA[linha, coluna] = int.Parse(Console.ReadLine()); // Altera o valor do elemento baseado na posição da linha e coluna atual.
            }
        }
        return matrizA; // Retorna a matriz regular.
    }

    public static int[] CriaMatrizB(int tamanho)
    {
        int[] matrizB = new int[tamanho];

        for(int i = 0; i < tamanho; i++)
        {
            Console.WriteLine($"Informe o valor resultante para a linha {i}");
            matrizB[i] = int.Parse(Console.ReadLine());
        }

        return matrizB;
    }
}