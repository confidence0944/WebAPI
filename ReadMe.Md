# WebApi專案介紹
Restful Api設計風格，Entity framework core存取DB，分層式架構資料存取與邏輯分層(Repository Pattern)，
Middleware做Rq與Rs Log機制，及HttpClient掛載Handler紀錄Rq與Rs，GlobleExceptionHandler機制，
設計各IOC/DI注入的服務(Register Service)，實作統一API Response格式與Formater及單元測試(MSTest)。

## 1.印出所有 API 被呼叫以及呼叫外部 API 的 request and response body log
各別是ApiLoggingMiddleware.cs及HttpClientLoggingHandler.cs

## 2.Error handling 處理 API response
GlobleExceptionHandler.cs

## 3.Swagger-ui
包含

## 4.Design pattern 實作
IOC/DI，Repository Pattern

## 5.加解密技術應用 (RSA)
EncrypTools.cs

## 6.各服務注入初始化進入的位置
Program.cs (ApiLog、Service、DBContext、HttpClient)

## 7.Request資料驗證
使用FluentValidation做為資料驗證，並且定義ValidatorFilter攔截錯誤，並轉成制定ValidatorException再往外拋
讓GlobleExceptionHandler對各別Exception做對應的處理

## 其他實作項目:
#### (一). 實作統一API回應格式與制定ReturnCode (ApiBaseController.cs、ReturnCode.cs) 
#### (二). JsonConverter Format response格式  (JsonDateTimeConverter.cs)

