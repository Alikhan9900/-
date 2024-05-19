# ATM Project

## Description
This project simulates an ATM machine with functionalities such as deposit, withdrawal, balance check, and transfer. The ATM has different states like normal operation, out of service, and no cash state.

## Functionality
- Deposit money to an account
- Withdraw money from an account
- Check account balance
- Transfer money between accounts
- ATM state management

## How to Run
1. Clone the repository:
    ```bash
    git clone https://github.com/yourusername/ATM_Project.git
    ```
2. Navigate to the project directory:
    ```bash
    cd ATM_Project
    ```
3. Build the project:
    ```bash
    dotnet build
    ```
4. Run the project:
    ```bash
    dotnet run --project src/ATM_Project.csproj
    ```

## Programming Principles
1. **Single Responsibility Principle (SRP)**: Each class has one responsibility, e.g., `ATM` handles ATM operations, `Account` handles account operations.
2. **Open/Closed Principle (OCP)**: Classes are open for extension but closed for modification, e.g., new states can be added without modifying existing ones.
3. **Liskov Substitution Principle (LSP)**: Subtypes can be substituted for their base types without altering the correctness of the program.
4. **Interface Segregation Principle (ISP)**: Interfaces are split into smaller, more specific ones, e.g., commands implement the `ICommand` interface.
5. **Dependency Inversion Principle (DIP)**: High-level modules depend on abstractions, not concrete implementations.

## Design Patterns
1. **State Pattern**: Used to manage the different states of the ATM (NormalState, OutOfServiceState, NoCashState). [See implementation](src/ATMState.cs)
2. **Command Pattern**: Used for executing transactions like deposit, withdraw, balance check, and transfer. [See implementation](src/ICommand.cs)
3. **Memento Pattern**: Used to save and restore the state of the ATM or account. [See implementation](src/Memento.cs)

## Refactoring Techniques
1. **Extract Method**: Methods have been extracted to improve readability and maintainability.
2. **Rename Method**: Methods and variables have been renamed to be more descriptive.
3. **Encapsulate Field**: Fields have been encapsulated to protect the internal state of objects.

## File Structure
- `src/`: Contains all the source code for the project.
- `tests/`: Contains unit tests for the project.

## Contact
For any queries or issues, please contact [your-email@example.com].
