using System;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ElementalWords;

/// <summary>
/// Class to calculate all possible combinations of element symbols that can form a given word.
/// </summary>
public class ElementalWordCalculator
{
    private Dictionary<string, string> _elements;

    public ElementalWordCalculator()
    {
        _elements = BuildElementLookup();
    }

    /// <summary>
    /// Calculates all possible combinations of element symbols that can form the given word.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public List<List<string>> Calculate(string? input)
    {
        List<List<string>> results = [];

        // Exit early if we've nothing to check
        if (string.IsNullOrWhiteSpace(input))
        {
            return results;
        }

        GetPaths(results, input, []);

        return results;
    }

    private List<List<string>> GetPaths(List<List<string>> results, string input, List<string> currentPath)
    {
        if (string.IsNullOrEmpty(input))
        {
            // if the string is now empty, we have reached the end of the string and processed every char successfully
            // so we can now add it to our results
            if (currentPath != null && currentPath.Count > 0)
            {
                results.Add(currentPath);
            }

            return results; 
        }

        var strLength = input.Length;

        // if string length is 1 we just need to check the char itself
        if (strLength == 1)
        {
            var elemString = GetElementString(input);

            if (!string.IsNullOrEmpty(elemString))
            {
                currentPath.Add(elemString);
                return GetPaths(results, "", currentPath);
            }
        }
        else
        {
            // if we're here, the string is longer than 1 char
            // meaning we need to recurse
            // before that we need to test against the first, the first two and the first two chars
            // As any one of them could be a chemical element symbol match
            var firstElemString = GetElementString(input[..1]);

            if (!string.IsNullOrEmpty(firstElemString))
            {
                var newPath = CreateNewPathList(currentPath, firstElemString);
                GetPaths(results, input[1..], newPath);
            }

            var secondElemString = GetElementString(input[..2]);

            if (!string.IsNullOrEmpty(secondElemString))
            {
                var newPath = CreateNewPathList(currentPath, secondElemString);
                GetPaths(results, input[2..], newPath);
            }

            if (strLength > 2)
            {
                var thirdElemString = GetElementString(input[..3]);

                if (!string.IsNullOrEmpty(thirdElemString))
                {
                    var newPath = CreateNewPathList(currentPath, thirdElemString);
                    GetPaths(results, input[2..], newPath);
                }
            }
        }

        return results;
    }

    private List<string> CreateNewPathList(List<string> currentPath, string newPathItem)
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
    private Dictionary<string, string> BuildElementLookup()
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
            { "Uus", "ununseptium" },
            { "Uuo", "Ununoctium" },
            { "Uue", "Ununennium" }
        };
    }
}