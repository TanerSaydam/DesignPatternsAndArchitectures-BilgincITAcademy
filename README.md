# Design Patterns/Architectures Eğitim İçeriği

Design pattern, yazılımda sık karşılaşılan problemlere karşı defalarca kanıtlanmış, tekrar kullanılabilir çözüm şablonlarıdır.

Kısaca, aynı problemi her seferinde sıfırdan düşünmemek için kullanılan hazır mimari çözüm fikirleridir.

## Patterns
- **Design Principle**	“Nasıl düşünmeliyim?”
- **Design Pattern**	“Bu problemi nasıl çözerim?”
- **Architectural Pattern**	“Uygulamanın genel iskeletini ve katmanlı yapısını tanımlayan büyük ölçekli tasarım şablonudur”

## Eğitim İçeriği
- AspNetCore Framework'ünü anlayalım
- Dependency Injection
- Middleware
- Design Patterns nedir?
  - **Singleton Pattern** (1994 - Book)
  - **Factory Pattern** (1994 - Book)
  - **Abstract Factory** Pattern (1994 - Book)
  - **Builder Pattern** (1994 - Book)
  - **Prototype Pattern** (199(1994 - Book)4 - Book)
  - **Facade Pattern** (1994 - Book)
  - **Proxy Pattern** (1994 - Book)
  - **Service Pattern** (Modern)
  - **Repository Pattern** (Modern)
  - **Unit Of Work Pattern** (Modern)
  - **Command Pattern**  (1994 - Book)
  - **Mediator Pattern** (1994 - Book)
  - **CQRS Pattern** (Modern)
  - **Options Pattern** (Modern)
  - **Result Pattern** (Modern)
  - **Service Discovery Pattern** (Modern)
  - **Outbox Pattern** (Modern)
  - **Observer Pattern - Queue - Channels Library** (Modern)
  - **Rate Limiting Pattern** (Modern)
  - **Circuit Breaker Pattern / Retry Pattern** (Polly Library) (Modern)
- Architectural Patterns nedir?
  - N Tier Architecture
  - Clean Architecture
    - DDD Approach

## Framework Nedir?
Framework, uygulamanın iskeletini ve akışını belirleyen,senin yazdığın kodu kendi kuralları içinde çağıran hazır bir yapıdır.

## Library Nedir?
Library, ihtiyacın olduğunda senin çağırdığın, belirli bir işi yapan hazır kod kütüphanesidir.

- .NET bir framework / platformdur. C# ise bu platform üzerinde kullanılan programlama dilidir.
- ASP.NET Core = .NET için web uygulama framework’ü
- Console ise bir application, .NET’in sağladığı bir application modelidir

## IoC (Inversion of Control) Nedir?
Inversion of Control, programın kontrol akışının senin kodundan çıkıp bir framework / container tarafından yönetilmesi prensibidir.
Yani:
- “Ben kimi, ne zaman, nasıl çağıracağımı kontrol etmiyorum. Framework kontrol ediyor.”
- IoC bir prensiptir. ASP.NET Core bunu uygular. Program.cs ise bunun konfigürasyon yeridir.

## Design Principles, Design Patterns, Architecture Pattern

- **Design Principles**: "Nasıl düşünmeliyim?"
- **Design Pattern**: “Bu problemi nasıl çözerim?”
- **Architectural Pattern**: “Uygulamanın genel iskeletini ve katmanlı yapısını tanımlayan büyük ölçekli tasarım şablonudur”

### Design Principles
- **SOLID**
- **DRY**
- **KISS**
- **YAGNI**
- **Separation of Concerns** 
  - "Her şey kendi işini yapsın" 
  - "Modern mimari dünyası “CQRS düşünce şeklini” öneriyor"
- **High Cohesion / Low Coupling** 
  - High Cohesion = Bir modülün / class’ın tek bir amaca odaklı olması 
  - Low Coupling = Modüllerin birbirine en az bağımlı olması