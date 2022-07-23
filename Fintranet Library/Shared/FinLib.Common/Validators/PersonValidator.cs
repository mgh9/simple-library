using FinLib.Common.Extensions;

namespace FinLib.Common.Validators
{
    public static class PersonValidator
    {
        public static bool IsValidLastName(string lastName)
        {
            if (lastName.IsEmpty())
                return false;

            return lastName.Length > 1 && lastName.Length < 30;
        }

        public static bool IsValidMobile(string mobileNumber, bool isEmptyValid = false)
        {
            if (mobileNumber.IsEmpty())
            {
                return isEmptyValid;
            }

            return mobileNumber.Length == 11
                && NumbersValidator.IsDigitsOnly(mobileNumber)
                && mobileNumber.StartsWith("09", System.StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsValidEmployeeCode(string employeeCode, bool isEmptyValid = false)
        {
            if (employeeCode.IsEmpty())
            {
                return isEmptyValid;
            }

            return long.TryParse(employeeCode, out _);
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
