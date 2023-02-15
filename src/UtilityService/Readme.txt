Для работы службы необходима ASP.NET Core Runtime 3.1

1) Создание службы: 
    sc create "A_UtilityService" binpath="путь до UtilityService.exe"
    Н: sc create "A_UtilityService" binpath="C:\MyPetProjects\UtilityService\UtilityService.exe" 
    
    sc start A_UtilityService
    sc stop A_UtilityService
    sc stop A_UtilityService
    sc delete A_UtilityService

2) Настройка пути для развертывания сервиса указываеттся в WindowsService.json

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

3) настройка БД SQLite указывается в appsettings.json

"DBConfig": {
    "Name": "SQLite",
    "Path": "D:\\UtilityServiceDB\\BookStoreContext1.db"
  }