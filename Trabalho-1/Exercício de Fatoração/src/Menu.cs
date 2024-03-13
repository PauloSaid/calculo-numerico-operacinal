namespace Menu;

using Matriz;

class Menu
{
    public void MostrarMenu()
    {
        Console.Clear();
        int tamanho;
        Console.WriteLine("Informe o tamanho da matriz: ");
        tamanho = int.Parse(Console.ReadLine());
        double[,] matrizA = Matriz.CriaMatriz(tamanho);
        double[] matrizB = Matriz.CriaMatrizB(tamanho);
        double[,] matrizU = Matriz.CalculaMatrizU(matrizA);

        Matriz.ImprimeMatriz(matrizU);
    }
}