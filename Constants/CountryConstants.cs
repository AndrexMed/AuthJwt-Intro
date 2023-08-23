using AuthJwt.Models;

namespace AuthJwt.Constants
{
    public class CountryConstants
    {
        public static List<CountryModel> countryModels = new()
        {
            new CountryModel() { Name = "Medellín"},
            new CountryModel() { Name = "Bogota"},
            new CountryModel() { Name = "Cali"},
        };
    }
}
