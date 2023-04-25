namespace CustomerXAPI.Utilis
{
    public static class IdUtils
    {
        public static string GenerateIsraeliIDNumber()
        {
            Random rand = new Random();
            int randomNumber = rand.Next(1, 100000000); // generate a random number between 1 and 99,999,999
            string idNumber = randomNumber.ToString("D8"); // convert to string and pad with zeros to 8 digits
            int[] factors = { 1, 2, 1, 2, 1, 2, 1, 2, 1 }; // the factors used to calculate the check digit
            int sum = 0;
            for (int i = 0; i < idNumber.Length; i++)
            {
                int digit = int.Parse(idNumber[i].ToString());
                int factor = factors[i];
                int product = digit * factor;
                if (product > 9)
                {
                    product -= 9;
                }
                sum += product;
            }
            int checkDigit = (10 - (sum % 10)) % 10;
            return idNumber + checkDigit.ToString();
        }

        public static bool IsValidIsraeliID(string id)
        {
            id = id.Trim();
            if (id.Length > 9 || id.Length < 5 || !int.TryParse(id, out int result))
            {
                return false;
            }

            // Pad string with zeros up to 9 digits
            id = id.PadLeft(9, '0');

            int sum = 0;
            for (int i = 0; i < id.Length; i++)
            {
                int digit = int.Parse(id[i].ToString());
                int factor = (i % 2) + 1;
                int product = digit * factor;
                if (product > 9)
                {
                    product -= 9;
                }
                sum += product;
            }

            return (sum % 10) == 0;
        }
    }
}
