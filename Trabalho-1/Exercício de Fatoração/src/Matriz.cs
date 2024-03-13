namespace Matriz;

class Matriz
{
    public static void ImprimeMatriz(double[,] matriz)
    {
        Console.Clear();
        int linha = matriz.GetLength(0);// GetLength(0) = quantidade de linhas da matriz.
        int coluna = matriz.GetLength(1); // GetLength(1) = quantidade de colunas da matriz.

        for(int i = 0; i < linha; i++)
        {
            for(int j = 0; j < coluna; j++)
            {
                Console.Write(matriz[i,j].ToString("n2") + " ");
            }
            Console.WriteLine("");
        }
    }

    public static double[,] CriaMatriz(int tamanho) // Função estática para criar matriz.
    {
        double[,] matrizA = new double[tamanho, tamanho]; // Criação de matriz regular.

        for(int linha = 0; linha < tamanho; linha++) // For para entrar em cada linha
        {
            for(int coluna = 0; coluna < tamanho; coluna++) // For para entrar em cada coluna (alternando os valores)
            {
                Console.WriteLine($"Informe o valor da matriz para a linha {linha} e coluna {coluna}");
                matrizA[linha, coluna] = double.Parse(Console.ReadLine()); // Altera o valor do elemento baseado na posição da linha e coluna atual.
            }
        }
        return matrizA; // Retorna a matriz regular.
    }

    public static double[] CriaMatrizB(int tamanho)
    {
        double[] matrizB = new double[tamanho]; // Instancia matriz B.

        for(int i = 0; i < tamanho; i++) // Atribuindo valores a cada posição Axy
        {
            Console.WriteLine($"Informe o valor resultante para a linha {i}");
            matrizB[i] = double.Parse(Console.ReadLine());
        }

        return matrizB; // retorno da nova matriz B.
    }

    public static double[,] CalculaMatrizU(double[,] matriz)
    {
        int linha = matriz.GetLength(0); // GetLenght(0) = Quantidade de linhas da matriz
        int coluna = matriz.GetLength(1); // GetLenght(1) = Quantidade de colunas da matriz
        double[,] novaMatriz = new double[linha,coluna]; // Instância da nova matriz U
        
        // Passando os valores da primeira linha para a matriz U, já que ela não sofre alteração.
        for(int j = 0; j < linha; j++)
        {
            novaMatriz[0, j] = matriz[0, j];
        }

        // Realizando os calculos com a primeira coluna da nova matriz U. -> Divide o elemento pelo pivô.
        for(int i = 1; i < linha; i++)
        {
            novaMatriz[i, 0] = matriz[i, 0] / matriz[0, 0];
        }

        // Começa a percorrer a matriz U pela segunda linha e pela segunda coluna.
        for (int i = 1; i < linha; i++)
        {
            for (int j = i; j < coluna; j++)
            {
                double soma = 0.0;
                for (int k = 0; k < i; k++) // esse loop calcula a soma dos produtos dos elmentos U e L que já foram calculados.
                {
                    soma += novaMatriz[i, k] * novaMatriz[k, j]; // Calcula a soma dos produtos
                }
                novaMatriz[i, j] = matriz[i, j] - soma; // Calcula o elemento da matrizU[i,j]
            }

            for (int j = i + 1; j < linha; j++) // Esse loop percorre as linhas abaixo da diagonal principal
            {
                double soma = 0.0;
                for (int k = 0; k < i; k++) // esse loop calcula a soma dos produtos dos elmentos U e L que já foram calculados.
                {
                    soma += novaMatriz[j, k] * novaMatriz[k, i]; // Calcula a soma dos produtos
                }
                novaMatriz[j, i] = (matriz[j, i] - soma) / novaMatriz[i, i]; // calcula o elemento da matriz u, usando a formula da decomposição LU
            }
        }
        // graças a deus, retorna a matriz U.
        return novaMatriz;
    }
}