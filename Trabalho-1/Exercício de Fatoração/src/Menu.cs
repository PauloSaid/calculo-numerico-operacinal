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
        Matriz.CriaMatriz(tamanho);
    }
}