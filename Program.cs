namespace tax;

class Program
{
    static void Main(string[] args)
    {
        displayTable();

        string name = promptName();
        string tinNumber = promptTinNumber();
        double annualPay = promptAnnualPay();
        char civilStatus = promptCivilStatus();

        Console.WriteLine("\n-------------------------------------------------------------------------\n");

        calculateTax(civilStatus, annualPay);
    }

    static string promptName()
    {
        string? name;

        do
        {
            Console.Write("Enter name: ");
            name = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(name))
            {
                break;
            }

            Console.WriteLine("FAILURE: Invalid name.\n");
        } while (true);

        return name;
    }

    static string promptTinNumber()
    {
        string? tinNumber;

        do
        {
            Console.Write("Enter TIN number: ");
            tinNumber = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(tinNumber))
            {
                break;
            }

            Console.WriteLine("FAILURE: Invalid TIN number.\n");
        } while (true);

        return tinNumber;
    }

    static double promptAnnualPay()
    {
        double annualPay;

        do
        {
            Console.Write("Enter annual pay: ");

            if (double.TryParse(Console.ReadLine(), out annualPay))
            {
                break;
            }

            Console.WriteLine("FAILURE: Invalid amount.\n");
        } while (true);

        return annualPay;
    }


    static char promptCivilStatus()
    {
        char civilStatus;

        do
        {
            Console.WriteLine();
            Console.WriteLine("S - [S]ingle");
            Console.WriteLine("M - [M]arried\n");
            Console.Write("Enter civil status: ");

            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("ERROR: Invalid input.");
                continue;
            }

            input = input.Trim().ToLower();

            if (input.Length == 1 && "sm".Contains(input))
            {
                civilStatus = input[0];
                break;
            }

            Console.WriteLine("ERROR: Invalid civil status.");
        } while (true);

        return civilStatus;
    }

    static void calculateTax(char civilStatus, double annualPay)
    {
        double taxToWithold = 0.0;
        double taxExcessPercent = 0.0;
        double taxPayable = 0.0;
        double taxThreshold = 0.0;

        switch (civilStatus)
        {
            case 's':
                if (annualPay < 6300.0)
                {
                    taxExcessPercent = 0.0;
                }
                else if (annualPay < 7300.0)
                {
                    taxExcessPercent = 0.005;
                    taxThreshold = 6300;
                }
                else if (annualPay < 8800.0)
                {
                    taxToWithold = 5.0;
                    taxExcessPercent = 0.01;
                    taxThreshold = 7300.0;
                }
                else if (annualPay < 10050.0)
                {
                    taxToWithold = 20.0;
                    taxExcessPercent = 0.02;
                    taxThreshold = 8800.0;
                }
                else if (annualPay < 11200.0)
                {
                    taxToWithold = 45.0;
                    taxExcessPercent = 0.03;
                    taxThreshold = 10050.0;
                }
                else if (annualPay < 13500.0)
                {
                    taxToWithold = 79.50;
                    taxExcessPercent = 0.04;
                    taxThreshold = 11200.0;
                }
                else if (annualPay < 15000.0)
                {
                    taxToWithold = 171.50;
                    taxExcessPercent = 0.05;
                    taxThreshold = 13500.0;
                }
                else
                {
                    taxToWithold = 246.50;
                    taxExcessPercent = 0.0525;
                    taxThreshold = 15000.0;
                }
                break;
            case 'm':
                if (annualPay < 12600.0)
                {
                    taxExcessPercent = 0.0;
                }
                else if (annualPay < 14600.0)
                {
                    taxExcessPercent = 0.005;
                    taxThreshold = 12600.0;
                }
                else if (annualPay < 17600.0)
                {
                    taxToWithold = 10.0;
                    taxExcessPercent = 0.01;
                    taxThreshold = 14600.0;
                }
                else if (annualPay < 20100.0)
                {
                    taxToWithold = 40.0;
                    taxExcessPercent = 0.02;
                    taxThreshold = 17600.0;
                }
                else if (annualPay < 22400.0)
                {
                    taxToWithold = 90.0;
                    taxExcessPercent = 0.03;
                    taxThreshold = 20100.0;
                }
                else if (annualPay < 24800.0)
                {
                    taxToWithold = 159.00;
                    taxExcessPercent = 0.04;
                    taxThreshold = 22400.0;
                }
                else if (annualPay < 27600.0)
                {
                    taxToWithold = 255.00;
                    taxExcessPercent = 0.05;
                    taxThreshold = 24800.0;
                }
                else
                {
                    taxToWithold = 395.00;
                    taxExcessPercent = 0.0525;
                    taxThreshold = 27600.0;
                }
                break;
            default:
                Console.WriteLine("ERROR: Failed to calculate tax for civil status: ", civilStatus);
                return;
        }

        taxPayable = taxToWithold + (annualPay - taxThreshold) * taxExcessPercent;

        double takeHomePay = annualPay - taxPayable;

        Console.WriteLine("Annual Pay: {0:C2}", annualPay);
        Console.WriteLine("Deduction/Tax Payable: {0:C2}", taxPayable);
        Console.WriteLine("Take Home Pay: {0:C2}", takeHomePay);
        Console.WriteLine("\nComputation for tax payable:\n");

        Console.WriteLine("{0:N2} + ({1:N2} - {2:N2}) * {3:N2} = {4:N2}", taxToWithold, annualPay, taxThreshold, taxExcessPercent, taxPayable);
    }

    static void displayTable()
    {
        Console.WriteLine("A: SINGLE Person:");
        Console.WriteLine("If the amount of wages is: (after subtracting witholding allowances)\n");
        Console.WriteLine("| Over    | but less than | The amount income tax to withold is:        |");
        Console.WriteLine("| ------- | ------------- | ------------------------------------------- |");
        Console.WriteLine("| $0      | $6,300        | $0                                          |");
        Console.WriteLine("| $6,300  | $7,300        | $0 +(0.50% of the excess over $6,300)       |");
        Console.WriteLine("| $7,300  | $8,800        | $5.00 +(1.00% of the excess over $7,300)    |");
        Console.WriteLine("| $8,800  | $10,050       | $20.00 +(2.00% of the excess over $8,800)   |");
        Console.WriteLine("| $10,050 | $11,200       | $45.00 +(3.00% of the excess over $10,050)  |");
        Console.WriteLine("| $11,200 | $13,500       | $79.50 +(4.00% of the excess over $11,200)  |");
        Console.WriteLine("| $13,500 | $15,000       | $171.50 +(5.00% of the excess over $13,500) |");
        Console.WriteLine("| $15,000 | and above     | $246.50 +(5.25% of the excess over $15,000) |\n\n");

        Console.WriteLine("B: SINGLE Person:");
        Console.WriteLine("If the amount of wages is: (after subtracting witholding allowances)\n");
        Console.WriteLine("| Over    | but less than | The amount income tax to withold is:        |");
        Console.WriteLine("| ------- | ------------- | ------------------------------------------- |");
        Console.WriteLine("| $0      | $12,600       | $0                                          |");
        Console.WriteLine("| $12,600 | $14,600       | $0 +(0.50% of the excess over $12,600)      |");
        Console.WriteLine("| $14,600 | $17,600       | $10.00 +(1.00% of the excess over $14,300)  |");
        Console.WriteLine("| $17,600 | $20,100       | $40.00 +(2.00% of the excess over $17,600)  |");
        Console.WriteLine("| $20,100 | $22,400       | $90.00 +(3.00% of the excess over $20,100)  |");
        Console.WriteLine("| $22,400 | $24,800       | $159.00 +(4.00% of the excess over $22,400) |");
        Console.WriteLine("| $24,800 | $27,600       | $255.00 +(5.00% of the excess over $24,800) |");
        Console.WriteLine("| $27,600 | and above     | $395.00 +(5.25% of the excess over $27,600) |\n\n");
        Console.WriteLine("-------------------------------------------------------------------------\n\n");
    }
}
