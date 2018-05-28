namespace BBAuto.Logic.Services.License
{
  public interface ILicenseService
  {
    LicenseModel GetLicenseByDriverId(int driverId);
  }
}
