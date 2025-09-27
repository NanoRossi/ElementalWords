using ElementalWords;

Console.WriteLine("Elemental Words!");
Console.WriteLine("Enter a word:");

var input = Console.ReadLine();

var result = new ElementalWordCalculator().Calculate(input);

Console.WriteLine("Possible combinations of element symbols:");

if (result == null || result.Count == 0)
{
    Console.WriteLine([]);
}
else
{
    foreach (var combination in result)
    {
        Console.WriteLine(string.Join(", ", combination));
    }
}