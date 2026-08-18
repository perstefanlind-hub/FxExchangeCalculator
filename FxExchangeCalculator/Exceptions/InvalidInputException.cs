
public class InvalidInputException(string input)
    : Exception($"'{input}' is not a command. Type 'help' for instructions.")
{
}