��� ������ ������ ���������� ASP.NET Core Runtime 3.1

1) �������� ������: 
    sc create "A_UtilityService" binpath="���� �� UtilityService.exe"
    �: sc create "A_UtilityService" binpath="C:\MyPetProjects\UtilityService\UtilityService.exe" 
    
    sc start A_UtilityService
    sc stop A_UtilityService
    sc stop A_UtilityService
    sc delete A_UtilityService

2) ��������� ���� ��� ������������� ������� ������������ � WindowsService.json

{
  "Kestrel": {
    "Endpoints": {
      "Http": {
        "Url": "http://localhost:5000"
      },
      "Https": {
        "Url": "https://localhost:5001"
      }
    }
  }
}

3) ��������� �� SQLite ����������� � appsettings.json

"DBConfig": {
    "Name": "SQLite",
    "Path": "D:\\UtilityServiceDB\\BookStoreContext1.db"
  }