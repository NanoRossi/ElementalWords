namespace ElementalWords;

/// <summary>
/// Class to calculate all possible combinations of element symbols that can form a given word.
/// </summary>
public class ElementalWordCalculator
{
    private readonly Dictionary<string, string> _elements;

    public ElementalWordCalculator()
    {
        _elements = BuildElementLookup();
    }

    /// <summary>
    /// Calculates all possible combinations of element symbols that can form the given word.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public List<List<string>> ElementalForms(string? word)
    {
        List<List<string>> forms = [];

        // Exit early if we've nothing to check
        if (string.IsNullOrWhiteSpace(word))
        {
            return forms;
        }

        GetPaths(forms, word, []);

        return forms;
    }

    /// <summary>
    /// Recurse through the string, checking for element symbol matches against the start of the string
    /// </summary>
    /// <param name="forms"></param>
    /// <param name="word"></param>
    /// <param name="currentPath"></param>
    /// <returns></returns>
    private List<List<string>> GetPaths(List<List<string>> forms, string word, List<string> currentPath)
    {
        if (string.IsNullOrEmpty(word))
        {
            // if the string is empty, we have reached the end of the string and processed every char successfully
            // so we can now add it to our forms
            if (currentPath != null && currentPath.Count > 0)
            {
                forms.Add(currentPath);
            }

            return forms; 
        }

        var strLength = word.Length;

        // We have already confirmed the string is not null or empty
        // So we can safely check the first char of it to see if its a chemical symbol
        CheckChar(forms, word, currentPath, 1);

        // We can now check for 2 and 3 char element symbols
        // using the same logic as above, split it off and recurse with the remainder
        if (strLength > 1)
        {
            CheckChar(forms, word, currentPath, 2);
        }

        if (strLength > 2)
        {
            CheckChar(forms, word, currentPath, 3);
        }

        return forms;
    }

    /// <summary>
    /// Compare the start of the string to the element dictionary
    /// If a match is found, create a new path list and recurse with the remainder of the string
    /// </summary>
    /// <param name="forms"></param>
    /// <param name="word"></param>
    /// <param name="currentPath"></param>
    /// <param name="charIndex"></param>
    private void CheckChar(List<List<string>> forms, string word, List<string> currentPath, int charIndex)
    {
        var elemString = GetElementString(word[..charIndex]);

        if (!string.IsNullOrEmpty(elemString))
        {
            var newPath = CreateNewPathList(currentPath, elemString);
            GetPaths(forms, word[charIndex..], newPath);
        }
    }

    /// <summary>
    /// Create a new list based on the current path and add the new path item to it
    /// </summary>
    /// <param name="currentPath"></param>
    /// <param name="newPathItem"></param>
    /// <returns></returns>
    private static List<string> CreateNewPathList(List<string> currentPath, string newPathItem)
    {
        return [
            .. currentPath,                         
            newPathItem
        ];
    }

    /// <summary>
    /// Retrieve an element from the dictionary and return a formatted string
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private string GetElementString(string input)
    {
        var kvpElement = _elements.GetElement(input);

        if (kvpElement.Key != null)
        {
            return $"{kvpElement.Value} ({kvpElement.Key})";
        }

        return "";
    }

    /// <summary>
    /// Build dictionary of chemical symbol to element name lookup
    /// TODO: could move this to a file? 
    /// </summary>
    /// <returns></returns>
    private static Dictionary<string, string> BuildElementLookup()
    {
        return new Dictionary<string, string>()
        {
            { "H", "Hydrogen" },
            { "He", "Helium" },
            { "Li", "Lithium" },
            { "Be", "Beryllium" },
            { "B", "Boron" },
            { "C", "Carbon" },
            { "N", "Nitrogen" },
            { "O", "Oxygen" },
            { "F", "Fluorine" },
            { "Ne", "Neon" },
            { "Na", "Sodium" },
            { "Mg", "Magnesium" },
            { "Al", "Aluminium" },
            { "Si", "Silicon" },
            { "P", "Phosphorus" },
            { "S", "Sulfur" },
            { "Cl", "Chlorine" },
            { "Ar", "Argon" },
            { "K", "Potassium" },
            { "Ca", "Calcium" },
            { "Sc", "Scandium" },
            { "Ti", "Titanium" },
            { "V", "Vanadium" },
            { "Cr", "Chromium" },
            { "Mn", "Manganese" },
            { "Fe", "Iron" },
            { "Co", "Cobalt" },
            { "Ni", "Nickel" },
            { "Cu", "Copper" },
            { "Zn", "Zinc" },
            { "Ga", "Gallium" },
            { "Ge", "Germanium" },
            { "As", "Arsenic" },
            { "Se", "Selenium" },
            { "Br", "Bromine" },
            { "Kr", "Krypton" },
            { "Rb", "Rubidium" },
            { "Sr", "Strontium" },
            { "Y", "Yttrium" },
            { "Zr", "Zirconium" },
            { "Nb", "Niobium" },
            { "Mo", "Molybdenum" },
            { "Tc", "Technetium" },
            { "Ru", "Ruthenium" },
            { "Rh", "Rhodium" },
            { "Pd", "Palladium" },
            { "Ag", "Silver" },
            { "Cd", "Cadmium" },
            { "In", "Indium" },
            { "Sn", "Tin" },
            { "Sb", "Antimony" },
            { "Te", "Tellurium" },
            { "I", "Iodine" },
            { "Xe", "Xenon" },
            { "Cs", "Cesium" },
            { "Ba", "Barium" },
            { "La", "Lanthanum" },
            { "Ce", "Cerium" },
            { "Pr", "Praseodymium" },
            { "Nd", "Neodymium" },
            { "Pm", "Promethium" },
            { "Sm", "Samarium" },
            { "Eu", "Europium" },
            { "Gd", "Gadolinium" },
            { "Tb", "Terbium" },
            { "Dy", "Dysprosium" },
            { "Ho", "Holmium" },
            { "Er", "Erbium" },
            { "Tm", "Thulium" },
            { "Yb", "Ytterbium" },
            { "Lu", "Lutetium" },
            { "Hf", "Hafnium" },
            { "Ta", "Tantalum" },
            { "W", "Tungsten" },
            { "Re", "Rhenium" },
            { "Os", "Osmium" },
            { "Ir", "Iridium" },
            { "Pt", "Platinum" },
            { "Au", "Gold" },
            { "Hg", "Mercury" },
            { "Tl", "Thallium" },
            { "Pb", "Lead" },
            { "Bi", "Bismuth" },
            { "Po", "Polonium" },
            { "At", "Astatine" },
            { "Rn", "Radon" },
            { "Fr", "Francium" },
            { "Ra", "Radium" },
            { "Ac", "Actinium" },
            { "Th", "Thorium" },
            { "Pa", "Protactinium" },
            { "U", "Uranium" },
            { "Np", "Neptunium" },
            { "Pu", "Plutonium" },
            { "Am", "Americium" },
            { "Cm", "Curium" },
            { "Bk", "Berkelium" },
            { "Cf", "Californium" },
            { "Es", "Einsteinium" },
            { "Fm", "Fermium" },
            { "Md", "Mendelevium" },
            { "No", "Nobelium" },
            { "Lr", "Lawrencium" },
            { "Rf", "Rutherfordium" },
            { "Db", "Dubnium" },
            { "Sg", "Seaborgium" },
            { "Bh", "Bohrium" },
            { "Hs", "Hassium" },
            { "Mt", "Meitnerium" },
            { "Ds", "Darmstadtium" },
            { "Rg", "Roentgenium" },
            { "Cn", "Copernicium" },
            { "Uut", "Ununtrium" },
            { "Fl", "Flerovium" },
            { "Uup", "ununpentium" },
            { "Lv", "Livermorium" },
            { "Uus", "Ununseptium" },
            { "Uuo", "Ununoctium" },
            { "Uue", "Ununennium" }
        };
    }
}