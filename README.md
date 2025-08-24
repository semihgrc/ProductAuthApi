# ProductAuthApi

> **ProductAuthApi**, ürünlerin ekleme, güncelleme, silme ve listeleme işlemlerini gerçekleştiren bir **WEB API** projesidir.  
> Ayrıca **JWT tabanlı kullanıcı authentication**, **Redis cache** ve **global exception handling** özelliklerini içerir.

---

## 📁 Proje Katmanları

- **Domain**  
  Temel iş modeli ve varlıklar. Entity’ler ve domain mantığı burada bulunur.  

- **Application**  
  DTO’lar, soyut sınıflar ve interface’ler bu katmanda tanımlanır.  

- **Infrastructure**  
  Servislerin ve dış bağımlılıkların somut sınıfları burada yer alır. Örneğin ProductService’in implementasyonu.  

- **Persistence**  
  DbContext, repository’lerin somut sınıfları ve servis registration burada bulunur.  

- **Presentation / API**  
  Controller sınıfları, API endpoint’leri, Swagger konfigürasyonu ve dependency injection burada yer alır.  

---

## ✨ Özellikler

- **JWT Authentication**  
  - Kullanıcılar register ve login sonrası token alır.  
  - `[Authorize]` attribute ile endpoint’ler korunur.  

- **Redis Cache**  
  - Ürün listeleme işlemleri cache ile hızlandırılır.  
  - Ürün ekleme, güncelleme ve silme işlemlerinde cache invalidation yapılır.  

- **Global Exception Handling**  
  - Tüm API hataları tek bir middleware üzerinden yönetilir.  
  - Hatalar loglanır ve standart JSON formatında döner.  

- **Swagger API Dokümantasyonu**  
  - Token ile authorize işlemi yapılabilir.  
  - Tüm endpoint’ler detaylı açıklamaları ile listelenir.  

---

## ⚙️ Kurulum ve Çalıştırma

1. Repository’i klonlayın:  
   ```bash
   git clone https://github.com/semihgrc/ProductAuthApi.git
2. proje dizinine gidin
   ```bash
   cd ProductAuthApi
3. Bağımlılıkları yükleyin:
   ```bash
    dotnet restore
4. appsettings.json dosyasını düzenleyin:
    ```json
    "ConnectionStrings": { 
        "PostgreSqlConnection": "Server=localhost;Port=5432;Database=ProductAuthDb;Username=postgres;Password=yourpassword" 
    },
    "Jwt": {
        "Key": "SizinGizliAnahtarınız",
        "Issuer": "denemeApi",
        "Audience": "denemeApiUsers"
    },
    "Redis": {
        "Configuration": "localhost:6379",
        "InstanceName": "ProductApiCache"
    }
5. Database’i oluşturun ve migrate edin:
    ```bash
    dotnet ef database update --project ProductAuthApi.Persistance --startup-project ProductAuthApi.Api
6. Redis sunucusunun çalıştığından emin olun (127.0.0.1:6379).
7. Projeyi çalıştırın:
   ```bash
   dotnet run --project ProductAuthApi.Api
8. Swagger ile test ve dokümantasyon:
   ```bash
   https://localhost:5001/index.html

