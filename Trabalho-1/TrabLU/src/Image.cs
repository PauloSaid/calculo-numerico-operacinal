using System;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Factorization;
using System.Drawing;

/*
    OBS. ->  convolução é um processo matemático que combina duas funções para produzir uma terceira.
    No contexto de processamento de imagens, a convolução é usada para aplicar um filtro ou máscara a uma imagem,
    resultando em uma nova imagem que realça ou atenua certas características da imagem original.
*/

public class ImageLU
{
    // Método principal para aplicar a decomposição LU em uma imagem
    public static void ImgLU()
    {
        // Carrega a imagem original
        Bitmap originalImage = new Bitmap("./image/cachorro.bmp");

        // Converte a imagem para uma matriz de pixels
        double[,] imagem = BitmapToMatrix(originalImage);

        // Define o filtro a ser aplicado na imagem
        double[,] filtro = {
            {0, 0.1, 0},
            {0.1, -0.1, 0.1},
            {0, 0.1, 0}};

        // Cria as matrizes L e U a partir do filtro
        var lu = CreateLuMatrix(filtro);
        Matrix<double> L = lu.Item1;
        Matrix<double> U = lu.Item2;

        // Aplica a convolução usando a matriz L
        double[,] resultadoL = Convolve(imagem, L);

        // Aplica a convolução usando a matriz U
        double[,] resultadoFinal = Convolve(resultadoL, U);

        // Converte a matriz resultante de volta para uma imagem Bitmap
        Bitmap filteredImage = MatrixToBitmap(resultadoFinal);

        // Salva a imagem filtrada
        filteredImage.Save("./dist/filtrada.bmp");
    }

     // Cria as matrizes L e U a partir de uma matriz de entrada
    static Tuple<Matrix<double>, Matrix<double>> CreateLuMatrix(double[,] matrix)
    {
        var m = Matrix<double>.Build.DenseOfArray(matrix);
        var lu = m.LU();
        return Tuple.Create(lu.L, lu.U);
    }

    // Aplica a convolução entre uma imagem e um kernel
static double[,] Convolve(double[,] image, Matrix<double> kernel)
{
    // Obtém as dimensões da imagem e do kernel
    int imageSizeX = image.GetLength(0);
    int imageSizeY = image.GetLength(1);
    int kernelSizeX = kernel.RowCount;
    int kernelSizeY = kernel.ColumnCount;

    // Cria uma matriz para armazenar o resultado da convolução
    double[,] result = new double[imageSizeX, imageSizeY];

    // Itera sobre cada pixel da imagem
    for (int x = 0; x < imageSizeX; x++)
    {
        for (int y = 0; y < imageSizeY; y++)
        {
            double sum = 0;
            // Itera sobre cada elemento do kernel
            for (int kx = 0; kx < kernelSizeX; kx++)
            {
                for (int ky = 0; ky < kernelSizeY; ky++)
                {
                    // Calcula as coordenadas na imagem considerando o deslocamento do kernel
                    int imageX = x + kx - kernelSizeX / 2;
                    int imageY = y + ky - kernelSizeY / 2;
                    // Verifica se as coordenadas estão dentro dos limites da imagem
                    if (imageX >= 0 && imageX < imageSizeX && imageY >= 0 && imageY < imageSizeY)
                    {
                        // Realiza a soma ponderada dos pixels da imagem sob o kernel
                        sum += image[imageX, imageY] * kernel[kx, ky];
                    }
                }
            }
            // Armazena o resultado da convolução para o pixel atual
            result[x, y] = sum;
        }
    }
    // Retorna a matriz resultante da convolução
    return result;
}
    // Converte uma imagem Bitmap para uma matriz de pixels
static double[,] BitmapToMatrix(Bitmap bitmap)
{
    // Cria uma matriz para armazenar os valores de intensidade dos pixels
    double[,] pixels = new double[bitmap.Width, bitmap.Height];

    // Itera sobre cada pixel da imagem
    for (int x = 0; x < bitmap.Width; x++)
    {
        for (int y = 0; y < bitmap.Height; y++)
        {
            // Obtém a cor do pixel na posição (x, y)
            Color color = bitmap.GetPixel(x, y);
            // Calcula um valor de intensidade média usando a média dos componentes de cor
            double intensity = (color.R + color.G + color.B) / 3.0;
            // Armazena o valor de intensidade na matriz de pixels
            pixels[x, y] = intensity;
        }
    }

    // Retorna a matriz de pixels resultante
    return pixels;
}

    // Converte uma matriz de pixels para uma imagem Bitmap
static Bitmap MatrixToBitmap(double[,] pixels)
{
    // Cria um objeto Bitmap com base nas dimensões da matriz de pixels
    Bitmap bitmap = new Bitmap(pixels.GetLength(0), pixels.GetLength(1));

    // Itera sobre cada posição da matriz de pixels
    for (int x = 0; x < pixels.GetLength(0); x++)
    {
        for (int y = 0; y < pixels.GetLength(1); y++)
        {
            // Obtém o valor de intensidade na posição (x, y)
            int intensity = (int)pixels[x, y];
            // Cria uma cor com base na intensidade (tons de cinza)
            Color color = Color.FromArgb(intensity, intensity, intensity);
            // Define a cor do pixel na imagem Bitmap
            bitmap.SetPixel(x, y, color);
        }
    }

    // Retorna a imagem Bitmap resultante
    return bitmap;
}
    // Imprime uma matriz na saída padrão (console)
static void PrintMatrix(double[,] matrix)
{
    // Itera sobre cada linha da matriz
    for (int i = 0; i < matrix.GetLength(0); i++)
    {
        // Itera sobre cada coluna da matriz
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            // Imprime o valor do elemento na posição (i, j) seguido de um espaço
            Console.Write(matrix[i, j] + " ");
        }
        // Ao final de cada linha, imprime uma quebra de linha
        Console.WriteLine();
    }
}
}




