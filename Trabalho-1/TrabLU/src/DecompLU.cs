using System;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Factorization;

public class DecompLU
{
    public static void DecomposicaoLU()
    {
        Console.Clear();
        /*
            Utilizando o exemplo que foi dado em sala de aula:
                3x + 2y + 4z = 1
                1x +  y + 2z = 2
                4x + 3y - 2z = 3

                espera-se que o resultado seja uma matriz X(3x1) com os resultados [-3, 5,0] */

        /* Input para pegar o tamanho da matriz */
        Console.Write("Informe o tamanho da matriz: ");
        int tamanho = int.Parse(Console.ReadLine());
            
        /* Inicializando a matriz A utilizando a biblioteca MathNet */
        var matrizA = Matrix<double>.Build.Dense(tamanho, tamanho);

        /* Utilizando 2 for loop's para preencher a matriz com os valores que o usuario digitar */
        for (int i = 0; i < tamanho; i++){
            for (int j = 0; j < tamanho; j++){
                Console.Write($"Informe o valor para a linha {i} e para a coluna {j}: ");
                matrizA[i, j] = double.Parse(Console.ReadLine());
            }
        }

        Console.WriteLine("Matriz A informada:");
        ImprimeMatriz(matrizA);

        /* Inicializando a matriz B com o tamanho que foi informado pelo usuario, ja que como se trata de um sistema linear, para cada linha,
           havera uma resposta */
        var matrizB = Vector<double>.Build.Dense(new double[tamanho]);

        /* For loop para pegar o input do usuario e formar a matriz B */
        for (int i = 0; i < tamanho; i++){
            Console.Write($"Informe o valor para a linha {i} da matriz B: ");
            matrizB[i] = double.Parse(Console.ReadLine());
        }

        Console.WriteLine("Matriz B informada:");
        ImprimeVetor(matrizB);

        /* Chamamos a bibiloteca MathNet para resolver as fatoracao LU */
        var matrizX = matrizA.Solve(matrizB);

        /* Instância de variável para salvar as matrizes L e U */
        var matrizL = matrizA.LowerTriangle();
        var matrizU = matrizA.UpperTriangle();

        Console.WriteLine("Matriz L:");
        ImprimeMatriz(matrizL);

        Console.WriteLine("Matriz U:");
        ImprimeMatriz(matrizU);
        
        Console.WriteLine("Matriz X é: ");
        ImprimeVetor(matrizX);
    }

    /* Funcao criada para imprimir a matriz, pois usando o Console.WriteLine(nomeMatriz) ficava da seguinte forma: 
        DenseMatrix 2x2-Double
        1  2
        3  4
        
        Acredito ser por causa da biblioteca, entao resolvi criar uma funcao para imprimir e ficar sem esse DenseMatrix tamanhoMatriz-TipoMatriz */
    public static void ImprimeMatriz(Matrix<double> matriz)
    {
        for(int i = 0; i < matriz.RowCount; i++)
        {
            for(int j = 0; j < matriz.ColumnCount; j++)
            {
                Console.Write(matriz[i,j] + " ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    /* Como eu usei a matriz B como um vetor, tive que criar uma outra funcao para imprimir as "matrizes" B e X */
     public static void ImprimeVetor(Vector<double> vetor)
    {
        for(int i = 0; i < vetor.Count; i++)
        {
            Console.WriteLine(vetor[i]);
        }
        Console.WriteLine();
    }
}


