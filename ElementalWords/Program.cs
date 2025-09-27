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
    Console.WriteLine("{");

    for (int i = 0; i < result.Count; i++)
    {
        var combination = result[i];

        if (i == result.Count - 1)
        {
            Console.WriteLine(string.Format("\t{{ {0} }}", string.Join(", ", combination)));
        }
        else
        {
            Console.WriteLine("\t{{ {0} }},", string.Join(", ", combination));
        }
    }

    Console.WriteLine("}");
}