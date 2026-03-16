# .NET WebAPI Template

![.NET](https://img.shields.io/badge/.NET-8-blue)  ![REST API](https://img.shields.io/badge/API-RESTful-green)  ![License](https://img.shields.io/badge/license-MIT-blue)

此專案為 **ASP.NET Core WebAPI 範例專案**，採用 RESTful API 設計風格，  
整合 Repository Pattern、Middleware Logging、Global Exception Handling、FluentValidation 與單元測試。

---

# 目錄



---

# WebApi專案介紹

此專案採用 **RESTful API 設計風格**，使用 **Entity Framework Core** 存取資料庫，並支援 **SQL Server 與 SQLite 切換**。

主要設計：

- Repository Pattern 分層架構
- Middleware 記錄 API Request / Response
- HttpClient Handler 記錄外部 API 呼叫
- Global Exception Handling
- FluentValidation Request 驗證
- 統一 API Response 格式
- MSTest 單元測試


# Configuration

在 `appsettings.json` 中設定相關參數：

```json
{
  "AppConfig": {
    "IsUseSqlLite": true,
    "CoindeskApiUrl": "https://api.coindesk.com/v1/bpi/currentprice.json",
    "ConnectionStrings": {
      "DbConnection": "",
      "SqlLiteDbConnection": "Data Source=practice.db"
    }
  }
}
```

| 參數 | 說明 |
|----|----|
| IsUseSqlLite | 是否使用 SQLite |
| CoindeskApiUrl | 外部匯率 API |
| DbConnection | SQL Server 連線字串 |
| SqlLiteDbConnection | SQLite 連線字串 |

---

# Feature

## 1. API Request / Response Log

印出所有 API 被呼叫以及呼叫外部 API 的 Request / Response Body Log。

實作：

- `ApiLoggingMiddleware.cs`
- `HttpClientLoggingHandler.cs`

---

## 2. 全域 Error Handling

統一處理 API Exception。

檔案：

`GlobleExceptionHandler.cs`

```csharp
public static class GlobleExceptionHandler
{
    public static void ConfigureExceptionHandler(this WebApplication app, NLog.Logger logger)
    {
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                var exceptionHandle = context.Features.Get<IExceptionHandlerFeature>();
                var exception = exceptionHandle?.Error;

                if (exception is ValidatorException) //資料驗證錯誤Exception
                {
                    ApiResponse apiResponse = new ApiResponse(ReturnCode.ValidatorError, null);
                    await context.Response.WriteAsJsonAsync(apiResponse);
                }
                else
                {
                     //未攔截到的Exception
                    ApiResponse apiResponse = new ApiResponse(ReturnCode.InternalSystemError, null);
                    await context.Response.WriteAsJsonAsync(apiResponse);
                }
            });
        });
    }
}
```

---

## 3. 服務注入初始化位置

所有服務註冊於：

`Program.cs`

包含：

- Middleware
- Repository
- Service
- DbContext
- HttpClient

---

## 4. Request 資料驗證

使用 **FluentValidation** 進行資料驗證。

流程：

```
Request
   ↓
FluentValidation
   ↓
ValidatorFilter
   ↓
ValidatorException
   ↓
GlobleExceptionHandler
```

---

## 5. 統一 API 回應格式

檔案：

`ApiResponse.cs`

```csharp
    public class ApiResponse
    {
        public ApiResponse() : this(string.Empty, string.Empty, null)
        {
        }

        public ApiResponse(MobileBankError mberror, dynamic data)
        {
            Code = mberror.Code;
            Message = mberror.Description;
            Data = data;
            Time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
        }

        public ApiResponse(string code, string message, dynamic data)
        {
            Code = code;
            Message = message;
            Data = data;
            Time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
        }

        //回應代碼
        [JsonPropertyName("ReturnCode")]
        public string Code { get; set; }

        //回應訊息
        [JsonPropertyName("ReturnMessage")]
        public string Message { get; set; }

        //回應時間
        [JsonPropertyName("ReturnTime")]
        public string Time { get; set; }

        //回應資料
        [JsonPropertyName("Data")]
        public dynamic Data { get; set; }
    }
```

---

## 6. ReturnCode 設計

檔案：

`ReturnCode.cs`

```csharp
public static class ReturnCode
{
    public static MobileBankError Success => new() { Code = "0000", Description = "交易成功" };

    public static MobileBankError NoData => new() { Code = "201", Description = "查無資料" };

    public static MobileBankError ValidatorError => new() { Code = "402", Description = "資料驗證錯誤" };

    public static MobileBankError InternalSystemError => new() { Code = "9999", Description = "系統發生錯誤，請洽系統管理員。" };
}
```

---

# API Response Example

成功回應：

```json
{
  "ReturnCode": "0000",
  "ReturnMessage": "交易成功",
  "ReturnTime": "2025/01/01 10:00:00",
  "Data": []
}
```

錯誤回應：

```json
{
  "ReturnCode": "9999",
  "ReturnMessage": "系統發生錯誤",
  "ReturnTime": "2025/01/01 10:00:00",
  "Data": null
}
```


---

# 單元測試

測試框架：`MSTest`

資料庫模擬：`UseInMemoryDatabase`

範例：

```csharp
var options = new DbContextOptionsBuilder<PracticeContext>()
    .UseInMemoryDatabase(Guid.NewGuid().ToString())
    .Options;
```

測試範圍：
- GetAll
- Create
- Update
- Delete