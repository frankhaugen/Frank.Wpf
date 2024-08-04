namespace Frank.Wpf.Controls.Console;

/// <summary>
/// Represents a console command that can be executed.
/// </summary>
/// <remarks>This is a simple interface that can be used to create very simple console commands.</remarks>
public interface IConsoleCommand
{
    /// <summary>
    /// Gets the name of the command, this should be unique between all commands but this is not enforced. If this is empty the command will not be listed or executed.
    /// </summary>
    /// <remarks>This is a simple string that can be used to identify the command. And is used in logging and listing commands.</remarks>
    string CommandName { get; }
    
    /// <summary>
    /// Checks if the command can be executed with the given input. This is the full parsing logic for the command.
    /// </summary>
    /// <remarks>This is a simple function that can be used to check if the command can be executed with the given input.</remarks>
    /// <param name="input">The input to check.</param>
    /// <returns>True if the command can be executed with the given input, false otherwise.</returns>
    bool CanExecute(string input);

    /// <summary>
    /// Executes the command.
    /// </summary>
    /// <param name="command">The text that was used to execute the command.</param>
    /// <remarks>This is a simple action that will be executed when the <see cref="CanExecute"/> function returns true.</remarks>
    /// <returns>The output of the command.</returns>
    string Execute(string command);
}