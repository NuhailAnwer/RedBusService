using System.ComponentModel.DataAnnotations;

using System.Text.RegularExpressions;

namespace RedBusService.CustomAttributes
{
    public class EighteenplusAttribute : ValidationAttribute
    {
    
       
        public  override bool IsValid(object? value)
        {
            if (Convert.ToInt32(value) < 18)
            {
                return false;

                ErrorMessage = $"The Age  should be in the range of 18 to 40";

            }

            else if (Convert.ToInt32(value) > 40)
            {
                return false;

                ErrorMessage = $"The Age  should be in the range of 18 to 40";

            }
            else return true;
        
        }

    }
}
