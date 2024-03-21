using System;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Factorization;
using System.Drawing;

public class ImageLU
{
    public static void ImgLU()
    {

        Bitmap originalImage = new Bitmap("./image/cachorro.bmp");

        double[,] imagem = BitmapToMatrix(originalImage);

        double[,] filtro = {
            {0, 0.1, 0},
            {0.1, -0.1, 0.1},
            {0, 0.1, 0}};

        var lu = CreateLuMatrix(filtro);
        Matrix<double> L = lu.Item1;
        Matrix<double> U = lu.Item2;

        double[,] resultadoL = Convolve(imagem, L);

        double[,] resultadoFinal = Convolve(resultadoL, U);

        Bitmap filteredImage = MatrixToBitmap(resultadoFinal);

        filteredImage.Save("./dist/filtrada.bmp");
    }

     static Tuple<Matrix<double>, Matrix<double>> CreateLuMatrix(double[,] matrix)
    {
        var m = Matrix<double>.Build.DenseOfArray(matrix);
        var lu = m.LU();
        return Tuple.Create(lu.L, lu.U);
    }

    static double[,] Convolve(double[,] image, Matrix<double> kernel)
    {
        int imageSizeX = image.GetLength(0);
        int imageSizeY = image.GetLength(1);
        int kernelSizeX = kernel.RowCount;
        int kernelSizeY = kernel.ColumnCount;

        double[,] result = new double[imageSizeX, imageSizeY];

        for (int x = 0; x < imageSizeX; x++)
        {
            for (int y = 0; y < imageSizeY; y++)
            {
                double sum = 0;
                for (int kx = 0; kx < kernelSizeX; kx++)
                {
                    for (int ky = 0; ky < kernelSizeY; ky++)
                    {
                        int imageX = x + kx - kernelSizeX / 2;
                        int imageY = y + ky - kernelSizeY / 2;
                        if (imageX >= 0 && imageX < imageSizeX && imageY >= 0 && imageY < imageSizeY)
                        {
                            sum += image[imageX, imageY] * kernel[kx, ky];
                        }
                    }
                }
                result[x, y] = sum;
            }
        }
        return result;
    }
    static double[,] BitmapToMatrix(Bitmap bitmap)
    {
        // Converter o bitmap para uma matriz de pixels
        double[,] pixels = new double[bitmap.Width, bitmap.Height];

        for (int x = 0; x < bitmap.Width; x++)
        {
            for (int y = 0; y < bitmap.Height; y++)
            {
                Color color = bitmap.GetPixel(x, y);
                // Calcular um valor de intensidade média (por exemplo, usando a média dos componentes de cor)
                double intensity = (color.R + color.G + color.B) / 3.0;
                pixels[x, y] = intensity;
            }
        }

        return pixels;
    }

    static Bitmap MatrixToBitmap(double[,] pixels)
    {
        // Converter a matriz de pixels de volta para um objeto Bitmap
        Bitmap bitmap = new Bitmap(pixels.GetLength(0), pixels.GetLength(1));

        for (int x = 0; x < pixels.GetLength(0); x++)
        {
            for (int y = 0; y < pixels.GetLength(1); y++)
            {
                int intensity = (int)pixels[x, y];
                Color color = Color.FromArgb(intensity, intensity, intensity);
                bitmap.SetPixel(x, y, color);
            }
        }

        return bitmap;
    }
    static void PrintMatrix(double[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write(matrix[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}




