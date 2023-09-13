namespace trybank;
public class Trybank
{
    public bool Logged;
    public int loggedUser;

    //0 -> Número da conta
    //1 -> Agência
    //2 -> Senha
    //3 -> Saldo
    public int[,] Bank;
    public int registeredAccounts;
    private int maxAccounts = 50;
    public Trybank()
    {
        loggedUser = -99;
        registeredAccounts = 0;
        Logged = false;
        Bank = new int[maxAccounts, 4];
    }

    // 1. Construa a funcionalidade de cadastrar novas contas
    public void RegisterAccount(int number, int agency, int pass)
    {
        // Verifica se a conta já existe
        for (int i = 0; i < registeredAccounts; i++)
        {
            if (Bank[i, 0] == number && Bank[i, 1] == agency)
            {
                throw new ArgumentException("A conta já está sendo usada!");
            }
        }

        // Cadastra a nova conta
        Bank[registeredAccounts, 0] = number;
        Bank[registeredAccounts, 1] = agency;
        Bank[registeredAccounts, 2] = pass;
        Bank[registeredAccounts, 3] = 0;
        registeredAccounts++;
    }

    // 2. Construa a funcionalidade de fazer Login
    public void Login(int number, int agency, int pass)
    {
        // Verifica se já há um usuário logado
        if (Logged)
        {
            throw new AccessViolationException("Usuário já está logado!");
        }
        // Procura pela conta
        for (int i = 0; i < registeredAccounts; i++)
        {
            if (Bank[i, 0] == number && Bank[i, 1] == agency)
            {
                // Verifica a senha
                if (Bank[i, 2] == pass)
                { // Login realizado com sucesso
                    Logged = true;
                    loggedUser = i;
                    return;
                }
                else
                {
                    throw new ArgumentException("Senha incorreta!");
                }
            }
        }
        // Conta não encontrada
        throw new ArgumentException("Agência + Conta não encontrada!");
    }

    // 3. Construa a funcionalidade de fazer Logout
    public void Logout()
    {
        // Verifica se há um usuário logado
        if (!Logged)
        {
            throw new AccessViolationException("Usuário não está logado!");
        }

        // Altera o estado da variável Logged
        Logged = false;

        // Altera o índice do usuário logado
        loggedUser = -99;
    }


    // 4. Construa a funcionalidade de checar o saldo
    public int CheckBalance()
    {
        // Verifica se há um usuário logado
        if (!Logged)
        {
            throw new AccessViolationException("Usuário não está logado!");
        }

        // Retorna o saldo da conta logada
        return Bank[loggedUser, 3];
    }

    // 5. Construa a funcionalidade de depositar dinheiro
    public void Deposit(int amount)
    {
        // Verifica se há um usuário logado
        if (!Logged)
        {
            throw new AccessViolationException("Usuário não está logado!");
        }

        // Adiciona o valor ao saldo da conta
        Bank[loggedUser, 3] += amount;
    }

    // 6. Construa a funcionalidade de sacar dinheiro
    public void Withdraw(int amount)
    {
        // Verifica se há um usuário logado
        if (!Logged)
        {
            throw new AccessViolationException("Usuário não está logado!");
        }

        // Verifica se o saldo é suficiente
        if (Bank[loggedUser, 3] < amount)
        {
            throw new InvalidOperationException("Saldo insuficiente");
        }

        // Retira o valor do saldo da conta
        Bank[loggedUser, 3] -= amount;
    }

    // 7. Construa a funcionalidade de transferir dinheiro entre contas
    public void Transfer(int destinationNumber, int destinationAgency, int value)
    {
        // Verifica se há um usuário logado
        if (!Logged)
        {
            throw new AccessViolationException("Usuário não está logado!");
        }

        // Verifica se a conta de destino existe
        for (int i = 0; i < registeredAccounts; i++)
        {
            if (Bank[i, 0] == destinationNumber && Bank[i, 1] == destinationAgency)
            {
                // Verifica se o saldo é suficiente
                if (Bank[loggedUser, 3] < value)
                {
                    throw new InvalidOperationException("Saldo insuficiente");
                }

                // Transfere o valor
                Bank[loggedUser, 3] -= value;
                Bank[i, 3] += value;

                return;
            }
        }

        // Conta de destino não encontrada
        throw new ArgumentException("Conta não encontrada!");
    }




}
