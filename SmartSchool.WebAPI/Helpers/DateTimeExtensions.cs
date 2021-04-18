using System;

namespace SmartSchool.WebAPI.Helpers
{
    public static class DateTimeExtensions
    {
        // Pegar Idade Atual
        // ====================================================
        public static int GetCurrentAge(this DateTime dateTime)
        {        
            var currentDate = DateTime.UtcNow;
            int age = currentDate.Year - dateTime.Year;
            // Ex.: 2020 - 1996 = Minha idade (age).

            if(currentDate < dateTime.AddYears(age))
            age--;

            return age;
        }
    }
}